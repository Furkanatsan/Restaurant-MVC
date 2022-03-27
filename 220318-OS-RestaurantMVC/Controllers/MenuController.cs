using _220318_OS_RestaurantMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _220318_OS_RestaurantMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly RestaurantContext _db;

        public MenuController(RestaurantContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Kategoriler
                .Include(x=>x.Urunler)
                .ThenInclude(x=>x.UrunlerMalzemeler)
                .ThenInclude(x=>x.Malzeme)
                .ToList());
        }
    }
}
