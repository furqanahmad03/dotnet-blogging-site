using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using X.Models;

namespace X.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Contact> Contacts { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseMySql("server=localhost;port=3306;database=BlogSite;user=root;password=root;",
                new MySqlServerVersion(new Version(8, 0, 31)));

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.UseCollation("utf8mb4_general_ci");
        builder.HasCharSet("utf8mb4");

        builder.Entity<User>(entity =>
        {
            entity.Property(u => u.Id).HasMaxLength(191);
            entity.Property(u => u.UserName).HasMaxLength(191);
            entity.Property(u => u.NormalizedUserName).HasMaxLength(191);
            entity.Property(u => u.Email).HasMaxLength(191);
            entity.Property(u => u.NormalizedEmail).HasMaxLength(191);
            entity.Property(u => u.PhoneNumber).HasMaxLength(15);
            entity.Property(u => u.Status).HasConversion<string>();
        });

        builder.Entity<IdentityRole>(entity =>
        {
            entity.Property(r => r.Id).HasMaxLength(191);
            entity.Property(r => r.Name).HasMaxLength(191);
            entity.Property(r => r.NormalizedName).HasMaxLength(191);
        });

        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.Property(l => l.LoginProvider).HasMaxLength(128);
            entity.Property(l => l.ProviderKey).HasMaxLength(128);
        });

        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.Property(t => t.LoginProvider).HasMaxLength(128);
            entity.Property(t => t.Name).HasMaxLength(128);
        });
    }
}








// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore;
// using X.Models;

// namespace X.Data;

// public class ApplicationDbContext : IdentityDbContext<User>
// {
//     public DbSet<Blog> Blogs { get; set; }
//     public DbSet<Post> Posts { get; set; }
//     public DbSet<Comment> Comments { get; set; }

//     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//         : base(options)
//     {
//     }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         if (!optionsBuilder.IsConfigured)
//             optionsBuilder.UseSqlServer("Server=localhost,1433;Database=BlogSite;User Id=SA;Password=Root.03@!;TrustServerCertificate=True;");
//     }

//     protected override void OnModelCreating(ModelBuilder builder)
//     {
//         base.OnModelCreating(builder);

//         builder.Entity<User>(entity =>
//         {
//             entity.Property(u => u.Id).HasMaxLength(450);
//             entity.Property(u => u.UserName).HasMaxLength(256);
//             entity.Property(u => u.NormalizedUserName).HasMaxLength(256);
//             entity.Property(u => u.Email).HasMaxLength(256);
//             entity.Property(u => u.NormalizedEmail).HasMaxLength(256);
//             entity.Property(u => u.PhoneNumber).HasMaxLength(15);
//             entity.Property(u => u.Status).HasConversion<string>();
//         });

//         builder.Entity<IdentityRole>(entity =>
//         {
//             entity.Property(r => r.Id).HasMaxLength(450);
//             entity.Property(r => r.Name).HasMaxLength(256);
//             entity.Property(r => r.NormalizedName).HasMaxLength(256);
//         });

//         builder.Entity<IdentityUserLogin<string>>(entity =>
//         {
//             entity.Property(l => l.LoginProvider).HasMaxLength(450);
//             entity.Property(l => l.ProviderKey).HasMaxLength(450);
//         });

//         builder.Entity<IdentityUserToken<string>>(entity =>
//         {
//             entity.Property(t => t.LoginProvider).HasMaxLength(450);
//             entity.Property(t => t.Name).HasMaxLength(450);
//         });
//     }
// }

