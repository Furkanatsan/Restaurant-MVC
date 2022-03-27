using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _220318_OS_RestaurantMVC.Models
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {

        }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Malzeme> Malzemeler { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<UrunMalzeme> UrunlerMalzemeler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UrunMalzeme>().HasKey(x => new { x.UrunId, x.MalzemeId });
        }

    }

  
}
