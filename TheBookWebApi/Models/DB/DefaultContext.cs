using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TheBookWebApi.Models.DB
{
    public partial class DefaultContext : DbContext
    {
        public DefaultContext()
        {
        }

        public DefaultContext(DbContextOptions<DefaultContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Expense> Expense { get; set; }
        public virtual DbSet<Income> Income { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<Tax> Tax { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-I0QHN4U\\SQLEXPRESS;Database=SalesManagement;Persist Security Info=True;User ID=sa; Password=sa;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(10);

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Bill_Supplier");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustId);

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(10);
            });

            modelBuilder.Entity<Income>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(10);
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.HasKey(e => e.InvoId);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CustId)
                    .HasConstraintName("FK_Invoices_Customer");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(10);

                entity.HasOne(d => d.Tax)
                    .WithMany(p => p.Purchase)
                    .HasForeignKey(d => d.TaxId)
                    .HasConstraintName("FK_Purchase_Tax");
            });

            modelBuilder.Entity<Sales>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(10);

                entity.HasOne(d => d.Tax)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.TaxId)
                    .HasConstraintName("FK_Sales_Sales");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(20);

                entity.Property(e => e.FirstName).HasMaxLength(10);

                entity.Property(e => e.LastName).HasMaxLength(10);
            });

            modelBuilder.Entity<Tax>(entity =>
            {
                entity.Property(e => e.Abbrevation).HasMaxLength(10);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(10);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.BillId)
                    .HasConstraintName("FK_Transaction_Bill");

                entity.HasOne(d => d.Invo)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.InvoId)
                    .HasConstraintName("FK_Transaction_Invoices");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(40);

                entity.Property(e => e.Email).HasMaxLength(20);

                entity.Property(e => e.FirstName).HasMaxLength(10);

                entity.Property(e => e.LastName).HasMaxLength(10);

                entity.Property(e => e.Password).HasMaxLength(10);
            });
        }
    }
}
