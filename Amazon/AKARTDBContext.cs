﻿using Amazon.Models;
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


        [NotMapped]
        public System.Data.Entity.DbSet<Amazon.Models.SellerAddress> SellerAddresses { get; set; }
    }
}