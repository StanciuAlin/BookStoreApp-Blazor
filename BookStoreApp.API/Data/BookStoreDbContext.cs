using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Data;

public partial class BookStoreDbContext : IdentityDbContext<UserApi> // DbContext
{
    public BookStoreDbContext()
    {
    }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3214EC07AC406AF2");

            entity.Property(e => e.Bio).HasMaxLength(250);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC07C81DD9FE");

            entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EACE030A08").IsUnique();

            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Summary).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Books_ToAuthors");
        });

        //var timeStampUserRole = Guid.NewGuid().ToString("D");
        //var timeStampAdminRole = Guid.NewGuid().ToString("D");
        //var securityStampUserRole = Guid.NewGuid().ToString("D");
        //var securityStampAdminRole = Guid.NewGuid().ToString("D");
        
        var timeStampUserRole = "fa63cb7f-efff-49e6-b864-99c58aceffef";
        var timeStampAdminRole = "fb63cb7f-efff-49e6-b864-99c58aceffef";
        var securityStampUserRole = "02c9ca8a-7cf5-46a2-91a4-a6400269c8e4";
        var securityStampAdminRole = "03c9ca8a-7cf5-46a2-91a4-a6400269c8e4";
        modelBuilder.Entity<IdentityRole>().HasData(

            new IdentityRole 
            { 
                Id = "0cc95a86-62b1-4d07-95c6-c8bf203cd7d4", 
                Name = "User", 
                NormalizedName = "USER"
            },
            new IdentityRole 
            { 
                Id = "6743a03e-9508-436e-b18d-75543029feb8", 
                Name = "Administrator", 
                NormalizedName = "ADMINISTRATOR"
            }
        );

        var hasher = new PasswordHasher<UserApi>();
        var password = hasher.HashPassword(null, "P@ssword1");
        var passwordString = "AQAAAAIAAYagAAAAEOlHyIGgX5P3C/bEO5PBDy/8XnoPYcwDOJSSqf3iojzbNxfNNSEd+ItVvK8Qs+dcng==";
        modelBuilder.Entity<UserApi>().HasData(
            new UserApi
            {
                Id = "9e14a82c-0f1e-47a1-9166-564c6677657a",
                UserName = "admin@bookstore.com",
                NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                Email = "admin@bookstore.com",
                NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                FirstName = "System",
                LastName = "Admin",
                PasswordHash = passwordString,
                ConcurrencyStamp = timeStampAdminRole,
                SecurityStamp = securityStampAdminRole
            },
            new UserApi
            {
                Id = "af684b54-9a92-4982-8d2c-3d1e9075fdf4",
                UserName = "user@bookstore.com",
                NormalizedUserName = "USER@BOOKSTORE.COM",
                Email = "user@bookstore.com",
                NormalizedEmail = "USER@BOOKSTORE.COM",
                FirstName = "System",
                LastName = "User",
                PasswordHash = passwordString,
                ConcurrencyStamp = timeStampUserRole,
                SecurityStamp = securityStampUserRole
            }
            );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "0cc95a86-62b1-4d07-95c6-c8bf203cd7d4",
                UserId = "af684b54-9a92-4982-8d2c-3d1e9075fdf4",
            },
            new IdentityUserRole<string>
            {
                RoleId = "6743a03e-9508-436e-b18d-75543029feb8",
                UserId = "9e14a82c-0f1e-47a1-9166-564c6677657a"
            }
            );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
