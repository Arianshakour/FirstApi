using FirstApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Context
{
    public class CityContext : DbContext
    {
        public CityContext(DbContextOptions<CityContext> options) : base(options)
        {

        }
        public DbSet<City> City { get; set; }
        //chon dar entity ham constructor gozashtam momken bood
        //warning bede ke inja nadadesh age midad injoori benevis public DbSet<City> City { get; set; } = null!;
        public DbSet<PointOfInterest> PointOfInterest { get; set; }
        public DbSet<CityUser> CityUser { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(new City("Tehran")
            {
                Id= 1,
                Des = "des tehran"
            }, new City("Shiraz")
            {
                Id = 2,
                Des = "des Shiraz"
            }, new City("Esfehan")
            {
                Id = 3,
                Des = "des Esfehan"
            });
            modelBuilder.Entity<PointOfInterest>().HasData(new PointOfInterest("noghte 1")
            {
                Id = 1,
                CityId = 1,
                Des = "des noqte 1"
            }, new PointOfInterest("noghte 2")
            {
                Id = 2,
                CityId = 1,
                Des = "des noqte 2"
            },new PointOfInterest("noghte 3")
            {
                Id = 3,
                CityId = 1,
                Des = "des noqte 3"
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
