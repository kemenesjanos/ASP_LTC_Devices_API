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
                    UseSqlServer(@"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\DeviceDB.mdf;integrated security=True;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
