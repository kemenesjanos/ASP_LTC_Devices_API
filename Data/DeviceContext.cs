using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace Data
{
    public class DeviceContext : DbContext
    {
        public DeviceContext(DbContextOptions<DeviceContext> opt) : base(opt)
        {

        }

        public DeviceContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.
                    UseLazyLoadingProxies().
                    UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=DeviceDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
                new { Id = "341743f0-asd2–42de-afbf-59kmkkmk72cf6", Name = "Admin", NormalizedName = "ADMIN" },
                new { Id = "341743f0-dee2–42de-bbbb-59kmkkmk72cf6", Name = "Customer", NormalizedName = "CUSTOMER" }
            );

            var appUser = new IdentityUser
            {
                Id = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                Email = "kemenes.janos@stud.uni-obuda.hu",
                NormalizedEmail = "kemenes.janos@stud.uni-obuda.hu",
                EmailConfirmed = true,
                UserName = "kemenes.janos@stud.uni-obuda.hu",
                NormalizedUserName = "kemenes.janos@stud.uni-obuda.hu",
                SecurityStamp = string.Empty
            };

            var appUser2 = new IdentityUser
            {
                Id = "e2174cf0–9412–4cfe-afbf-59f706d72cf6",
                Email = "kovacs.andras@nik.uni-obuda.hu",
                NormalizedEmail = "kovacs.andras@nik.uni-obuda.hu",
                EmailConfirmed = true,
                UserName = "kovacs.andras@nik.uni-obuda.hu",
                NormalizedUserName = "kovacs.andras@nik.uni-obuda.hu",
                SecurityStamp = string.Empty
            };

            appUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "titok");
            appUser2.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "jelszo");


            modelBuilder.Entity<IdentityUser>().HasData(appUser);
            modelBuilder.Entity<IdentityUser>().HasData(appUser2);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "341743f0-asd2–42de-afbf-59kmkkmk72cf6",
                UserId = "02174cf0–9412–4cfe-afbf-59f706d72cf6"
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "341743f0-dee2–42de-bbbb-59kmkkmk72cf6",
                UserId = "e2174cf0–9412–4cfe-afbf-59f706d72cf6"
            });


            modelBuilder.Entity<Method>(entity =>
            {
                entity
                .HasOne(method => method.Device)
                .WithMany(device => device.Methods)
                .HasForeignKey(method => method.DeviceUid);
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity
                .HasOne(property => property.Device)
                .WithMany(device => device.Properties)
                .HasForeignKey(property => property.DeviceUid);
            });
        }

        public DbSet<Method> Methods { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Device> Devices { get; set; }
    }
}
