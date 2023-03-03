using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAppClient.Models
{
    public partial class Test_InvoiceContext : DbContext
    {
        public Test_InvoiceContext()
        {
        }

        public Test_InvoiceContext(DbContextOptions<Test_InvoiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerTypes> CustomerTypes { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerTypes>(entity =>
            {
                entity.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(70);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                entity.Property(e => e.Adress)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.Property(e => e.CustName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.CustomerTypeId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CustomerType)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CustomerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customers_CustomerTypes");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {

                entity.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                entity.Property(e => e.SubTotal).HasColumnType("money");

                entity.Property(e => e.Total).HasColumnType("money");

                entity.Property(e => e.TotalItbis).HasColumnType("money");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Invoice_Customers");
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.SubTotal).HasColumnType("money");

                entity.Property(e => e.Total).HasColumnType("money");

                entity.Property(e => e.TotalItbis).HasColumnType("money");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.InvoiceDetail)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_InvoiceDetail_Invoice");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
