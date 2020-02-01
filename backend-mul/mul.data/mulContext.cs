using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace mul.data
{
    public partial class mulContext : DbContext
    {
        public mulContext()
        {
        }

        public mulContext(DbContextOptions<mulContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=DESKTOP-HK4B96F\\MSSQLSERVER01;Database=mul;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.ToTable("accounts");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountName)
                    .HasColumnName("account_name")
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .IsUnicode(false);

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_accounts_users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("text");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_users_accounts");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
