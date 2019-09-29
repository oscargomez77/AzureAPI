using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFace2.Models
{
    public class DBContextImages:DbContext
    {
        string sConnectionString = "Data Source =LAPTOPOSCAR ; Initial Catalog = ImagesDB; User= sa; Password= Seguridad19";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(sConnectionString, options =>{

            });
        }

        public DbSet<ImageCompare> ImageCompare { get; set; }
    }
}
