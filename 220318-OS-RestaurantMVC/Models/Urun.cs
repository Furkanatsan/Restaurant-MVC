using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _220318_OS_RestaurantMVC.Models
{
    public class Urun 
    {
        [Key]
        [Required]
        public int UrunId { get; set; }
        [Required]
        public string UrunAdi { get; set; }
        [Required]
        public string UrunTanimi { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public decimal UrunFiyat { get; set; }
        public string UrunResimURL { get; set; }
        [Required]
        [ForeignKey("Kategori")]
        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }
        public List<UrunMalzeme> UrunlerMalzemeler { get; set; }

    }
}
