using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CustomerContractData.Models
{
    public partial class OrangeCustomerSubscriptionContext : DbContext
    {
        public OrangeCustomerSubscriptionContext()
        {
        }

        public OrangeCustomerSubscriptionContext(DbContextOptions<OrangeCustomerSubscriptionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContractDate> ContractDates { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Service> Services { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=OrangeCustomerSubscription;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");

            modelBuilder.Entity<ContractDate>(entity =>
            {
                entity.HasKey(e => new { e.CstId, e.ServiceId, e.ContractId });

                entity.ToTable("ContractDate");

                entity.Property(e => e.CstId).HasColumnName("cstId");

                entity.Property(e => e.ServiceId).HasColumnName("serviceId");

                entity.Property(e => e.ContractId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("contractId");

                entity.Property(e => e.ContractDate1)
                    .HasColumnType("date")
                    .HasColumnName("contractDate");

                entity.Property(e => e.ContractExpiryDate)
                    .HasColumnType("date")
                    .HasColumnName("contractExpiryDate");

                entity.HasOne(d => d.Cst)
                    .WithMany(p => p.ContractDates)
                    .HasForeignKey(d => d.CstId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractDate_Customer");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ContractDates)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractDate_Service");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CstId);

                entity.ToTable("Customer");

                entity.Property(e => e.CstId).HasColumnName("cstId");

                entity.Property(e => e.CstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cstName");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.ServiceId)
                    .ValueGeneratedNever()
                    .HasColumnName("serviceId");

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("serviceName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
