using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DemExBumagaProgramm.Data
{
    public partial class BumagaContext : DbContext
    {
        public BumagaContext()
            : base("name=BumagaContext")
        {
        }

        public virtual DbSet<Agent> Agent { get; set; }
        public virtual DbSet<AgentType> AgentType { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductSale> ProductSale { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.Addres)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.Director)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.INN)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.KPP)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .HasMany(e => e.ProductSale)
                .WithRequired(e => e.Agent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AgentType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AgentType>()
                .HasMany(e => e.Agent)
                .WithRequired(e => e.AgentType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Articul)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.MinPriceForAgent)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductSale)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ProductType>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.ProductType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);
        }
    }
}
