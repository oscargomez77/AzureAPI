using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFace2.Models
{
    public class ImageCompare
    {
        public int Id { get; set; }
        public string  FirstImageName { get; set; }
        public string ImagePath { get; set; }
        public string SecondImageName { get; set; }
        public string ImagePath2 { get; set; }
        public DateTime? DateRecord { get; set; }
    }
}
