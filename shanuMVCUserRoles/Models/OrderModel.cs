using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using shanuMVCUserRoles.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace shanuMVCUserRoles.Models
{
    public class OrderModel
    {
        public int id { get; set; }
        public virtual ICollection<ProductModel> products { get; set; }
        public string customerName { get; set; }
        [Phone]
        public string customerNumber { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string customerMail { get; set; }
        public double total { get; set; }
    }
    public class OrderContext : DbContext
    {

        public OrderContext()
            : base("OrderConnection")
        { }

        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<ProductModel> Products { get; set; }
    }
}