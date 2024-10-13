using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SWP.KitStem.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.API.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentsType> ComponentsTypes { get; set; }
        public DbSet<Kit> Kits { get; set; }
        public DbSet<KitComponent> KitComponents { get; set; }
        public DbSet<KitImage> KitImages { get; set; }
        public DbSet<KitsCategory> KitsCategories { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<OrderSupport> OrderSupports { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Method> Methods { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageLab> PackageLabs { get; set; }
        public DbSet<PackageOrder> PackageOrders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<UserOrders> UserOrders { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<LabSupport> LabSupports { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Component>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Componen__3214EC072B0E2FDE");

                entity.HasOne(d => d.Type).WithMany(p => p.Components)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Component__TypeI__6754599E");
            });

            modelBuilder.Entity<ComponentsType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Componen__3214EC0732D88561");
            });

            modelBuilder.Entity<Kit>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Kit__3214EC071BF8D031");

                entity.HasOne(d => d.Category).WithMany(p => p.Kits).HasConstraintName("FK__Kit__CategoryId__6E01572D");
            });

            modelBuilder.Entity<KitComponent>(entity =>
            {
                entity.HasKey(e => new { e.KitId, e.ComponentId }).HasName("PK__KitCompo__34172BA37FE93EBD");

                entity.HasOne(d => d.Component).WithMany(p => p.KitComponents)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KitCompon__Compo__73BA3083");

                entity.HasOne(d => d.Kit).WithMany(p => p.KitComponents)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KitCompon__KitId__72C60C4A");
            });

            modelBuilder.Entity<KitImage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__KitImage__3214EC079BF747AB");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Kit).WithMany(p => p.KitImages)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KitImages__KitId__787EE5A0");
            });

            modelBuilder.Entity<KitsCategory>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__KitsCate__3214EC07C4BC9A6D");
            });

            modelBuilder.Entity<Lab>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Lab__3214EC070EF17576");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Status).HasDefaultValue(true);

                entity.HasOne(d => d.Kit).WithMany(p => p.Labs)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lab__KitId__00200768");

                entity.HasOne(d => d.Level).WithMany(p => p.Labs)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lab__LevelId__7F2BE32F");
            });

            modelBuilder.Entity<OrderSupport>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__LabSuppor__01846D66652C0B0E");

                entity.HasOne(d => d.Lab).WithMany(p => p.OrderSupports)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LabSuppor__LabId__22751F6C");

                entity.HasOne(d => d.Order).WithMany(p => p.OrderSupports)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LabSuppor__Order__236943A5");
                modelBuilder.Entity<OrderSupport>()
                    .HasIndex(l => new { l.LabId, l.PackageId, l.OrderId })
                    .IsUnique();
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Level__3214EC07C1764FCF");
            });

            modelBuilder.Entity<Method>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Method__3214EC07C4B69160");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Package__3214EC07FCAB6A0E");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Kit)
                    .WithMany(p => p.Packages)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Package__KitId__05D8E0BE");

                entity.HasMany(p => p.Carts)
                    .WithOne(c => c.Package)
                    .HasForeignKey(c => c.PackageId);
            });

            modelBuilder.Entity<PackageLab>(entity =>
            {
                entity.HasKey(e => new { e.PackageId, e.LabId }).HasName("PK__PackageL__8CFBE3412AEEA669");

                entity.HasOne(d => d.Lab)
                    .WithMany(p => p.PackageLabs)
                    .HasForeignKey(d => d.LabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PackageLa__LabId__0A9D95DB");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.PackageLabs)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PackageLa__Packa__09A971A2");
            });

            modelBuilder.Entity<PackageOrder>(entity =>
            {
                entity.HasKey(po => new { po.PackageId, po.OrderId });

                entity.HasOne(d => d.Order).WithMany().HasConstraintName("FK__PackageOr__Order__1F98B2C1");

                entity.HasOne(d => d.Package).WithMany().HasConstraintName("FK__PackageOr__Packa__1EA48E88");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC079CC5A858");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Method).WithMany(p => p.Payments).HasConstraintName("FK__Payment__MethodI__0F624AF8");

                entity.HasAlternateKey(p => p.OrderId);
            });

            modelBuilder.Entity<UserOrders>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__UserOrde__3214EC07B653418C");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.User).WithMany(p => p.UserOrders)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserOrders__UserI__151B244E");

                entity.HasMany(uo => uo.PackageOrders).WithOne(po => po.Order)
                        .HasForeignKey(po => po.OrderId);

                entity.HasOne(o => o.Payment)
                        .WithOne(p => p.UserOrders)
                        .HasForeignKey<Payment>(p => p.OrderId);

            });
            modelBuilder.Entity<LabSupport>(entity =>
            {
                // Primary key
                entity.HasKey(e => e.Id);

                // Foreign key to OrderSupport
                entity.HasOne(e => e.OrderSupport)
                    .WithMany(l => l.LabSupports) // Inverse Property
                    .HasForeignKey(e => e.OrderSupportId);

                // Foreign key to ApplicationUser (Staff)
                entity.HasOne(e => e.Staff)
                    .WithMany(u => u.LabSupports) // Inverse Property
                    .HasForeignKey(e => e.StaffId);
            });

            // Configuration for Cart
            modelBuilder.Entity<Cart>(entity =>
            {
                // Define the composite primary key (UserId, PackageId)
                entity.HasKey(c => new { c.UserId, c.PackageId });

                entity.HasOne(c => c.User)
                    .WithMany(u => u.Carts) // Assuming ApplicationUser has a collection of Carts
                    .HasForeignKey(c => c.UserId)
                    .HasConstraintName("FK__Cart__UserId__AB35320CD");

                entity.HasOne(c => c.Package)
                    .WithMany(p => p.Carts) // Assuming Package has a collection of Carts
                    .HasForeignKey(c => c.PackageId)
                    .HasConstraintName("FK__Cart__PackageId__POSI3213AAL");
            });
            modelBuilder.Entity<IdentityRole>().HasData(SeedRoles());
        }

        private ICollection<IdentityRole> SeedRoles()
        {
            return new List<IdentityRole>()
                { 
                new IdentityRole() { Name = "admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new IdentityRole() { Name = "manager", ConcurrencyStamp = "2", NormalizedName = "MANAGER" },
                new IdentityRole() { Name = "staff", ConcurrencyStamp = "3", NormalizedName = "STAFF" },
                new IdentityRole() { Name = "customer", ConcurrencyStamp = "4", NormalizedName = "CUSTOMER" }
                };
        }

    }
}
