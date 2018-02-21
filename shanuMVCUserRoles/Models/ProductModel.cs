using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Security.Claims;

namespace shanuMVCUserRoles.Models
{
    public class ProductModel
    {
        public int id { get; set; }
        public string title { get; set; }
        [DataType(DataType.MultilineText)]
        public string description { get; set; }
        public double price { get; set; }
        
        public string imagePath { get; set; }
    }

  
}