using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LinXi_Model
{
    public partial class LinXi_CloudStorageContext : DbContext
    {
        public LinXi_CloudStorageContext()
        {
        }

        public LinXi_CloudStorageContext(DbContextOptions<LinXi_CloudStorageContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LxResource> LxResource { get; set; }
        public virtual DbSet<LxResourceAccount> LxResourceAccount { get; set; }
        public virtual DbSet<LxShare> LxShare { get; set; }
        public virtual DbSet<LxUsers> LxUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Integrated Security=True; DataBase=LinXi_CloudStorage; server=.;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LxResource>(entity =>
            {
                entity.ToTable("Lx_Resource");

                entity.Property(e => e.LxResourceId).HasColumnName("Lx_ResourceID");

                entity.Property(e => e.LxResourceCategoryId).HasColumnName("Lx_ResourceCategoryID");

                entity.Property(e => e.LxResourceMdfive)
                    .HasColumnName("Lx_ResourceMDFive")
                    .HasMaxLength(50);

                entity.Property(e => e.LxResourceName)
                    .IsRequired()
                    .HasColumnName("Lx_ResourceName");

                entity.Property(e => e.LxResourcePath)
                    .HasColumnName("Lx_ResourcePath")
                    .HasMaxLength(50);

                entity.Property(e => e.LxResourceSize)
                    .IsRequired()
                    .HasColumnName("Lx_ResourceSize")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LxResourceAccount>(entity =>
            {
                entity.ToTable("Lx_ResourceAccount");

                entity.Property(e => e.LxResourceAccountId).HasColumnName("Lx_ResourceAccountID");

                entity.Property(e => e.LxGuid)
                    .IsRequired()
                    .HasColumnName("Lx_Guid");

                entity.Property(e => e.LxPid).HasColumnName("Lx_Pid");

                entity.Property(e => e.LxRecycle).HasColumnName("Lx_Recycle");

                entity.Property(e => e.LxResourceAccountName)
                    .IsRequired()
                    .HasColumnName("Lx_ResourceAccountName");

                entity.Property(e => e.LxResourceAccountTime)
                    .HasColumnName("Lx_ResourceAccountTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LxResourceId).HasColumnName("Lx_ResourceID");

                entity.Property(e => e.LxUsersId).HasColumnName("Lx_UsersID");

                entity.HasOne(d => d.LxResource)
                    .WithMany(p => p.LxResourceAccount)
                    .HasForeignKey(d => d.LxResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lx_ResourceAccount_Lx_Resource");

                entity.HasOne(d => d.LxUsers)
                    .WithMany(p => p.LxResourceAccount)
                    .HasForeignKey(d => d.LxUsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lx_ResourceAccount_Lx_Users");
            });

            modelBuilder.Entity<LxShare>(entity =>
            {
                entity.ToTable("Lx_Share");

                entity.Property(e => e.LxShareId).HasColumnName("Lx_ShareId");

                entity.Property(e => e.LxCodeKey)
                    .IsRequired()
                    .HasColumnName("Lx_CodeKey");

                entity.Property(e => e.LxCreateTime)
                    .HasColumnName("Lx_CreateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LxResourceAccountId).HasColumnName("Lx_ResourceAccountID");

                entity.Property(e => e.LxShareLink)
                    .IsRequired()
                    .HasColumnName("Lx_ShareLink")
                    .HasMaxLength(50);

                entity.Property(e => e.LxShareTime)
                    .IsRequired()
                    .HasColumnName("Lx_ShareTime")
                    .HasMaxLength(50);

                entity.Property(e => e.LxUserId).HasColumnName("Lx_UserID");

                entity.Property(e => e.LxValidCode)
                    .IsRequired()
                    .HasColumnName("Lx_ValidCode")
                    .HasMaxLength(50);

                entity.HasOne(d => d.LxResourceAccount)
                    .WithMany(p => p.LxShare)
                    .HasForeignKey(d => d.LxResourceAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lx_Share_Lx_ResourceAccount");

                entity.HasOne(d => d.LxUser)
                    .WithMany(p => p.LxShare)
                    .HasForeignKey(d => d.LxUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lx_Share_Lx_Users");
            });

            modelBuilder.Entity<LxUsers>(entity =>
            {
                entity.ToTable("Lx_Users");

                entity.Property(e => e.LxUsersId).HasColumnName("Lx_UsersID");

                entity.Property(e => e.LxUsersLoginEmail)
                    .IsRequired()
                    .HasColumnName("Lx_UsersLoginEmail")
                    .HasMaxLength(40);

                entity.Property(e => e.LxUsersLoginName)
                    .IsRequired()
                    .HasColumnName("Lx_UsersLoginName")
                    .HasMaxLength(50);

                entity.Property(e => e.LxUsersLoginPwd)
                    .IsRequired()
                    .HasColumnName("Lx_UsersLoginPWD")
                    .HasMaxLength(40);

                entity.Property(e => e.LxUsersName)
                    .HasColumnName("Lx_UsersName")
                    .HasDefaultValueSql("('DefaultUser')");

                entity.Property(e => e.LxUsersPhone)
                    .HasColumnName("Lx_UsersPhone")
                    .HasMaxLength(11);

                entity.Property(e => e.LxUsersPic)
                    .HasColumnName("Lx_UsersPic")
                    .HasMaxLength(50);

                entity.Property(e => e.LxUsersRemark)
                    .HasColumnName("Lx_UsersRemark")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
