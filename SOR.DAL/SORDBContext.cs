using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SOR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOR.DAL
{
    public class SORDBContext : IdentityDbContext<SORUser>
    {
        public SORDBContext(DbContextOptions<SORDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Reservation>().HasKey(res => res.ReservationId);
            builder.Entity<MenuFood>().HasKey(res => res.MenuFoodId);
            builder.Entity<Table>().Property(x => x.TableId).HasDefaultValueSql("NEWID()");
            builder.Entity<Menu>().Property(x => x.MenuId).HasDefaultValueSql("NEWID()");
            builder.Entity<Food>().Property(x => x.FoodId).HasDefaultValueSql("NEWID()");
            builder.Entity<IdentityRole>().HasData
                (
                    new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Administrator",
                        NormalizedName = "ADMINISTRATOR",
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    },
                    new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "User",
                        NormalizedName = "USER",
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    }
                    );
            builder.Entity<Menu>().HasData(
                new Menu
                {
                    MenuId = Guid.NewGuid(),
                    Title = "Karta restauracji"
                }
                );
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuFood> MenuFoods { get; set; }
    }
}
