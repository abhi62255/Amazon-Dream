using Amazon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Amazon
{
    public class AKARTDBContext : DbContext 
    {
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Seller> Seller { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductDescrption> ProductDescrption { get; set; }
        public virtual DbSet<ProductPicture> ProductPicture { get; set; }
        public virtual DbSet<SellerRequest> SellerRequest { get; set; }
        public virtual DbSet<ProductRequest> ProductRequest { get; set; }
        public virtual DbSet<Trend> Trend { get; set; }
        public virtual DbSet<TrendRequest> TrendRequest { get; set; }
        public virtual DbSet<Kart> Kart { get; set; }
        public virtual DbSet<OrderPlaced> OrderPlaced { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }











        [NotMapped]
        public System.Data.Entity.DbSet<Amazon.Models.SellerAddress> SellerAddresses { get; set; }

        [NotMapped]
        public System.Data.Entity.DbSet<Amazon.Models.ProductAndDescription> ProductAndDescription { get; set; }

        [NotMapped]
        public System.Data.Entity.DbSet<Amazon.Models.ProductPictureDescrption> ProductPictureDescrptions { get; set; }
    }
}