using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Capstone.Entities
{
    public partial class final_capstoneContext : DbContext
    {
        public final_capstoneContext()
        {
        }

        public final_capstoneContext(DbContextOptions<final_capstoneContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddressTable> AddressTable { get; set; }
        public virtual DbSet<Lease> Lease { get; set; }
        public virtual DbSet<MaintenanceRequest> MaintenanceRequest { get; set; }
        public virtual DbSet<Properties> Properties { get; set; }
        public virtual DbSet<RenterInformation> RenterInformation { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=final_capstone;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AddressTable>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("PK_address_id");

                entity.ToTable("address_table");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PropertyType)
                    .IsRequired()
                    .HasColumnName("property_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Region)
                    .IsRequired()
                    .HasColumnName("region")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnName("street")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Street2)
                    .IsRequired()
                    .HasColumnName("street2")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Zip).HasColumnName("zip");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AddressTable)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_userId_users");
            });

            modelBuilder.Entity<Lease>(entity =>
            {
                entity.ToTable("lease");

                entity.Property(e => e.LeaseId).HasColumnName("lease_id");

                entity.Property(e => e.CurrentStatus)
                    .IsRequired()
                    .HasColumnName("current_status")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.PropertyId).HasColumnName("property_id");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Lease)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_propertyId_prop");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Lease)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_userId_user");
            });

            modelBuilder.Entity<MaintenanceRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK_request_id");

                entity.ToTable("maintenance_request");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.IsAssigned).HasColumnName("is_assigned");

                entity.Property(e => e.IsFixed).HasColumnName("is_fixed");

                entity.Property(e => e.PostFixReport)
                    .HasColumnName("post_fix_report")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PropertyId).HasColumnName("property_id");

                entity.Property(e => e.RenterId).HasColumnName("renter_id");

                entity.Property(e => e.RequestInfo)
                    .IsRequired()
                    .HasColumnName("request_info")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.WorkerId).HasColumnName("worker_id");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.MaintenanceRequest)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_propertyId");

                entity.HasOne(d => d.Renter)
                    .WithMany(p => p.MaintenanceRequestRenter)
                    .HasForeignKey(d => d.RenterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_renter");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.MaintenanceRequestWorker)
                    .HasForeignKey(d => d.WorkerId)
                    .HasConstraintName("FK_worker");
            });

            modelBuilder.Entity<Properties>(entity =>
            {
                entity.HasKey(e => e.PropertyId)
                    .HasName("PK_propertyId");

                entity.ToTable("properties");

                entity.Property(e => e.PropertyId).HasColumnName("property_id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.Bathrooms).HasColumnName("bathrooms");

                entity.Property(e => e.Bedrooms).HasColumnName("bedrooms");

                entity.Property(e => e.Photo)
                    .HasColumnName("photo")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PropDesc)
                    .IsRequired()
                    .HasColumnName("prop_desc")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_addr_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_prop_user");
            });

            modelBuilder.Entity<RenterInformation>(entity =>
            {
                entity.HasKey(e => e.RenterId)
                    .HasName("PK_renter_id");

                entity.ToTable("renter_information");

                entity.Property(e => e.RenterId).HasColumnName("renter_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.LeaseType)
                    .IsRequired()
                    .HasColumnName("lease_type")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("phone_number")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Salary)
                    .HasColumnName("salary")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RenterInformation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKuser_id");
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK_transaction_id");

                entity.ToTable("transactions");

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.Property(e => e.AmountPaid)
                    .HasColumnName("amount_paid")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.LateFees)
                    .HasColumnName("late_fees")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.LeaseId).HasColumnName("lease_id");

                entity.Property(e => e.Paid).HasColumnName("paid");

                entity.Property(e => e.PaymentDueDate)
                    .HasColumnName("payment_due_date")
                    .HasColumnType("date");

                entity.Property(e => e.PropertyId).HasColumnName("property_id");

                entity.HasOne(d => d.Lease)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.LeaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lease_id_");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_property_id_trans");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_user");

                entity.ToTable("users");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("password_hash")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("salt")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserRole)
                    .IsRequired()
                    .HasColumnName("user_role")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
