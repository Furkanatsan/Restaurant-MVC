using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _220318_OS_RestaurantMVC.Models
{
    public class Kategori
    {
        [Key]
        [Required]
        public int KategoriId { get; set; }
        [Required]
        public string KategoriAdi { get; set; }
        public List<Urun> Urunler { get; set; }
    }

}
