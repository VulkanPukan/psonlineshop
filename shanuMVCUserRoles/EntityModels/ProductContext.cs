using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using shanuMVCUserRoles.Models;



namespace shanuMVCUserRoles.EntityModels
{
    public class ProductContext : DbContext
    {

        public ProductContext()
            : base("ProductConnection")
        { }

        public DbSet<ProductModel> Products { get; set; }
    }
}