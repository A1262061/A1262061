﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShoppingCartMVC.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ShoppingCartEntities : DbContext
    {
        public ShoppingCartEntities()
            : base("name=ShoppingCartEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdminAccount> AdminAccount { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Orders_Items> Orders_Items { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductActivity> ProductActivity { get; set; }
        public virtual DbSet<ProductClassify> ProductClassify { get; set; }
        public virtual DbSet<ProductStyle> ProductStyle { get; set; }
        public virtual DbSet<Score> Score { get; set; }
        public virtual DbSet<SecondHandProduct> SecondHandProduct { get; set; }
        public virtual DbSet<SecondHandProductRequire> SecondHandProductRequire { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<AboutUs> AboutUs { get; set; }
        public virtual DbSet<News> News { get; set; }
    }
}
