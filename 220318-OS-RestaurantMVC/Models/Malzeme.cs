using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _220318_OS_RestaurantMVC.Models
{
    public class Malzeme
    {
        [Key]
        [Required]
        public int MalzemeId { get; set; }
        [Required]
        public string MalzemeAdi { get; set; }
        public List<UrunMalzeme> UrunlerMalzemeler { get; set; }
    }
}
