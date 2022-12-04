using CareviewTest.Models;
using System.Data.Entity;

namespace CareviewTest.Data
{
    public class CareviewDbContext : DbContext
    {
        public CareviewDbContext() : base("careViewEntities")
        {
            Database.SetInitializer(new DatabaseInitializer());
            Configuration.LazyLoadingEnabled = false;
        }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }
        public virtual DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().Property(c => c.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Client>().Property(c => c.DateOfBirth).IsRequired().HasColumnType("datetime2");
            modelBuilder.Entity<Client>().Property(c => c.NDISNumber).HasMaxLength(20);
            modelBuilder.Entity<Invoice>().Property(c => c.InvoiceNumber).HasMaxLength(50);


        }
    }
}
