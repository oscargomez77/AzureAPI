using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIFace2.Models
{
    public class UploadImagesView
    {
        [Display(Name = "Select the Image")]
        public string ImagePath { get; set; }


        [Display(Name = "Select the Image")]
        public string ImagePath2 { get; set; }


        public IFormFile ImageFile1 { get; set; }

        public IFormFile ImageFile2 { get; set; }


    }
}
