using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIFace2.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProduct { get; set; }

        [Display(Name = "Enter Product Code")]
        [StringLength(20)]
        [Required(ErrorMessage = "{0} Is required.")]
        public string ProductCode { get; set; }

        [Display(Name = "Enter Product Type")]
        [StringLength(50)]
        [Required(ErrorMessage = "{0} Is required.")]
        public string ProductType { get; set; }

        [Display(Name = "Enter Product Description")]
        [StringLength(150)]
        public string Description { get; set; }

        [Display(Name = "Unit Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "{0} is required.")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Quantity in Stock")]
        [Required(ErrorMessage = "{0} Is required.")]
        public int QtyInStock { get; set; }

        [Display(Name = "Select the Image")]
        public string ImagePath { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
