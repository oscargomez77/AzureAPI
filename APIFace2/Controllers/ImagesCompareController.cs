using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using APIFace2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIFace2.Controllers
{
    public class ImagesCompareController : Controller
    {
        //private readonly MvcWebAppDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        private readonly IHostingEnvironment _env;

        public ImagesCompareController(IHostingEnvironment env)
        {
            _env = env;
        }

        public IActionResult UploadImage()
        {
            //if(MyViewData != null)
            //{
            //    return View(MyViewData);
            //}else
            UploadImagesView nuevo = new UploadImagesView();
          

            return View(nuevo);
        }

        [HttpPost]
        public async Task<IActionResult>  UploadImage(UploadImagesView DataView)
        {
            UploadImagesView SelectedImages = new UploadImagesView();
            string sFileNameOnly1 = "";
            string sFileNameOnly2 = "";

            SelectedImages.ImageFile1 = DataView.ImageFile1;
            SelectedImages.ImageFile2 = DataView.ImageFile2;

            if(DataView.ImageFile1 != null && DataView.ImageFile2 != null)
            {
                SelectedImages.ImagePath = DataView.ImageFile1.FileName;
                SelectedImages.ImagePath2 = DataView.ImageFile2.FileName;
            }
            else
            {              
                return View(SelectedImages);
                // mensaje error
            }
          
                       
            string sFilename = Path.GetFileNameWithoutExtension(DataView.ImageFile1.FileName);
            string extension = Path.GetExtension(DataView.ImageFile1.FileName);
            sFilename = sFilename + DateTime.Now.ToString("yymmssfff") + extension;
            sFileNameOnly1 = sFilename;
            SelectedImages.ImagePath = "~/images/" + sFilename;
            sFilename = Path.Combine(_env.WebRootPath, "images", sFilename);

            string sFilename2 = Path.GetFileNameWithoutExtension(DataView.ImageFile2.FileName);
            string extension2 = Path.GetExtension(DataView.ImageFile2.FileName);
            sFilename2 = sFilename2 + DateTime.Now.ToString("yymmssfff") + extension2;
            sFileNameOnly2 = sFilename2;
            SelectedImages.ImagePath2 = "~/images/" + sFilename2;
            sFilename2 = Path.Combine(_env.WebRootPath, "images", sFilename2);

            DBContextImages AccesoDB = new DBContextImages();
            ImageCompare ObjetoInsert = new ImageCompare();
            ObjetoInsert.FirstImageName = DataView.ImageFile1.FileName.ToString();
            ObjetoInsert.ImagePath = sFilename;
            ObjetoInsert.SecondImageName = DataView.ImageFile2.FileName.ToString();
            ObjetoInsert.ImagePath2 = sFilename2;
            ObjetoInsert.DateRecord = DateTime.Now;
            AccesoDB.Add(ObjetoInsert);
            AccesoDB.SaveChanges();
                    
            using (var stream = new FileStream(sFilename, FileMode.Create))
            {
                await DataView.ImageFile1.CopyToAsync(stream);
            }

            using (var stream = new FileStream(sFilename2, FileMode.Create))
            {
                await DataView.ImageFile2.CopyToAsync(stream);
            }

            string sRootPath = Path.Combine(_env.WebRootPath, "images");
            var images = Directory.EnumerateFiles(sRootPath)
                             .Select(fn => "Path" + Path.GetFileName(fn));

            DirectoryInfo DirInfo = new DirectoryInfo(sRootPath);

            var files1 = from f in DirInfo.EnumerateFiles()
                            where f.Name == sFileNameOnly1
                            select f;

            var files2 = from f in DirInfo.EnumerateFiles()
                            where f.Name == sFileNameOnly2
                            select f;

            ViewBag.Image1 = DataView.ImageFile1;

            return View(SelectedImages);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduct,ProductCode,ProductType,Description,UnitPrice,QtyInStock")] Product product, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(product);
               // await _context.SaveChangesAsync();
                // Code to upload image
                if (file != null || file.Length != 0)
                {
                    FileInfo fi = new FileInfo(file.FileName);
                    var newFilename = product.IdProduct + "_" + String.Format("{0:d}", (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                    var webPath = hostingEnvironment.WebRootPath;
                    var path = Path.Combine("", webPath + @"\ImageFiles\" + newFilename);
                    var pathToSave = @"/ImageFiles/" + newFilename;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    product.ImagePath = pathToSave;
                    //_context.Update(product);
                    //await _context.SaveChangesAsync();
                }
                //return RedirectToAction(nameof(Index));
                return View();
            }
            return View(product);
        }

    }
}