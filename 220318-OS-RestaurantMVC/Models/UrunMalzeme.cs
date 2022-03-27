using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _220318_OS_RestaurantMVC.Models
{
    public class UrunMalzeme
    {
        [ForeignKey("Malzeme")]
        public int MalzemeId { get; set; }
        public Malzeme Malzeme { get; set; }

        [ForeignKey("Urun")]
        public int UrunId { get; set; }
        public Urun Urun { get; set; }

    }
}
