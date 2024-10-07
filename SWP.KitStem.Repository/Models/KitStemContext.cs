using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SWP.KitStem.Repository.Models;

public partial class KitStemContext : DbContext
{
    public KitStemContext()
    {
    }

    public KitStemContext(DbContextOptions<KitStemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Kit> Kits { get; set; }

    public virtual DbSet<KitComponent> KitComponents { get; set; }

    public virtual DbSet<Lab> Labs { get; set; }

    public virtual DbSet<LabSupport> LabSupports { get; set; }

    public virtual DbSet<Level> Levels { get; set; }

    public virtual DbSet<Method> Methods { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderSupport> OrderSupports { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<PackageOrder> PackageOrders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Type> Types { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(45);
            entity.Property(e => e.LastName).HasMaxLength(45);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.PackageId });

            entity.ToTable("Cart");

            entity.HasIndex(e => e.PackageId, "IX_Cart_PackageId");

            entity.HasOne(d => d.Package).WithMany(p => p.Carts)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK__Cart__PackageId__POSI3213AAL");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Cart__UserId__AB35320CD");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KitsCate__3214EC07C4BC9A6D");

            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Componen__3214EC072B0E2FDE");

            entity.ToTable("Component");

            entity.HasIndex(e => e.TypeId, "IX_Component_TypeId");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Type).WithMany(p => p.Components)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Component__TypeI__6754599E");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KitImage__3214EC079BF747AB");

            entity.ToTable("Image");

            entity.HasIndex(e => e.KitId, "IX_Image_KitId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Url).IsUnicode(false);

            entity.HasOne(d => d.Kit).WithMany(p => p.Images)
                .HasForeignKey(d => d.KitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KitImages__KitId__787EE5A0");
        });

        modelBuilder.Entity<Kit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Kit__3214EC071BF8D031");

            entity.ToTable("Kit");

            entity.HasIndex(e => e.CategoryId, "IX_Kit_CategoryId");

            entity.Property(e => e.Brief).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.Kits)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Kit__CategoryId__6E01572D");
        });

        modelBuilder.Entity<KitComponent>(entity =>
        {
            entity.HasKey(e => new { e.KitId, e.ComponentId }).HasName("PK__KitCompo__34172BA37FE93EBD");

            entity.ToTable("KitComponent");

            entity.HasIndex(e => e.ComponentId, "IX_KitComponent_ComponentId");

            entity.HasOne(d => d.Component).WithMany(p => p.KitComponents)
                .HasForeignKey(d => d.ComponentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KitCompon__Compo__73BA3083");

            entity.HasOne(d => d.Kit).WithMany(p => p.KitComponents)
                .HasForeignKey(d => d.KitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KitCompon__KitId__72C60C4A");
        });

        modelBuilder.Entity<Lab>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Lab__3214EC070EF17576");

            entity.ToTable("Lab");

            entity.HasIndex(e => e.KitId, "IX_Lab_KitId");

            entity.HasIndex(e => e.LevelId, "IX_Lab_LevelId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.Url).IsUnicode(false);

            entity.HasOne(d => d.Kit).WithMany(p => p.Labs)
                .HasForeignKey(d => d.KitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Lab__KitId__00200768");

            entity.HasOne(d => d.Level).WithMany(p => p.Labs)
                .HasForeignKey(d => d.LevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Lab__LevelId__7F2BE32F");
        });

        modelBuilder.Entity<LabSupport>(entity =>
        {
            entity.ToTable("LabSupport");

            entity.HasIndex(e => e.OrderSupportId, "IX_LabSupport_OrderSupportId");

            entity.HasIndex(e => e.StaffId, "IX_LabSupport_StaffId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.OrderSupport).WithMany(p => p.LabSupports).HasForeignKey(d => d.OrderSupportId);

            entity.HasOne(d => d.Staff).WithMany(p => p.LabSupports).HasForeignKey(d => d.StaffId);
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Level__3214EC07C1764FCF");

            entity.ToTable("Level");

            entity.HasIndex(e => e.Name, "UQ__Level__737584F6684B4625").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Method>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Method__3214EC07C4B69160");

            entity.ToTable("Method");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.NormalizedName).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserOrde__3214EC07B653418C");

            entity.ToTable("Order");

            entity.HasIndex(e => e.PaymentId, "IX_Order_PaymentId").IsUnique();

            entity.HasIndex(e => e.UserId, "IX_Order_UserId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.Payment).WithOne(p => p.Order)
                .HasForeignKey<Order>(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserOrders__Payme__160F4887");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserOrders__UserI__151B244E");
        });

        modelBuilder.Entity<OrderSupport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LabSuppor__01846D66652C0B0E");

            entity.ToTable("OrderSupport");

            entity.HasIndex(e => new { e.LabId, e.PackageId, e.OrderId }, "IX_OrderSupport_LabId_PackageId_OrderId").IsUnique();

            entity.HasIndex(e => e.OrderId, "IX_OrderSupport_OrderId");

            entity.HasIndex(e => e.PackageId, "IX_OrderSupport_PackageId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Lab).WithMany(p => p.OrderSupports)
                .HasForeignKey(d => d.LabId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LabSuppor__LabId__22751F6C");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderSupports)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LabSuppor__Order__236943A5");

            entity.HasOne(d => d.Package).WithMany(p => p.OrderSupports).HasForeignKey(d => d.PackageId);
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Package__3214EC07FCAB6A0E");

            entity.ToTable("Package");

            entity.HasIndex(e => e.KitId, "IX_Package_KitId");

            entity.HasIndex(e => e.LevelId, "IX_Package_LevelId");

            entity.HasOne(d => d.Kit).WithMany(p => p.Packages)
                .HasForeignKey(d => d.KitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Package__KitId__05D8E0BE");

            entity.HasOne(d => d.Level).WithMany(p => p.Packages).HasForeignKey(d => d.LevelId);

            entity.HasMany(d => d.Labs).WithMany(p => p.Packages)
                .UsingEntity<Dictionary<string, object>>(
                    "PackageLab",
                    r => r.HasOne<Lab>().WithMany()
                        .HasForeignKey("LabId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PackageLa__LabId__0A9D95DB"),
                    l => l.HasOne<Package>().WithMany()
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PackageLa__Packa__09A971A2"),
                    j =>
                    {
                        j.HasKey("PackageId", "LabId").HasName("PK__PackageL__8CFBE3412AEEA669");
                        j.ToTable("PackageLab");
                        j.HasIndex(new[] { "LabId" }, "IX_PackageLab_LabId");
                    });
        });

        modelBuilder.Entity<PackageOrder>(entity =>
        {
            entity.HasKey(e => new { e.PackageId, e.OrderId });

            entity.ToTable("PackageOrder");

            entity.HasIndex(e => e.OrderId, "IX_PackageOrder_OrderId");

            entity.HasOne(d => d.Order).WithMany(p => p.PackageOrders)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__PackageOr__Order__1F98B2C1");

            entity.HasOne(d => d.Package).WithMany(p => p.PackageOrders)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK__PackageOr__Packa__1EA48E88");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC079CC5A858");

            entity.ToTable("Payment");

            entity.HasIndex(e => e.MethodId, "IX_Payment_MethodId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Method).WithMany(p => p.Payments)
                .HasForeignKey(d => d.MethodId)
                .HasConstraintName("FK__Payment__MethodI__0F624AF8");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("RefreshToken");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Componen__3214EC0732D88561");

            entity.ToTable("Type");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
