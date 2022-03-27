using _220318_OS_RestaurantMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _220318_OS_RestaurantMVC.Controllers
{
    public class KategoriController : Controller
    {
        private readonly RestaurantContext _db;

        public KategoriController(ILogger<HomeController> logger, RestaurantContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            var kategoriler = _db.Kategoriler.ToList();
            return View(kategoriler);
        }

        public IActionResult Yeni()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Yeni(string KategoriAdi)
        {
            Kategori yeniKategori = new Kategori();
            yeniKategori.KategoriAdi = KategoriAdi;
            _db.Kategoriler.Add(yeniKategori);
            _db.SaveChanges();
            return RedirectToAction("Index", "Kategori");
        }

        public IActionResult Duzenle(int KategoriId)
        {
            var kategori = _db.Kategoriler.FirstOrDefault(x => x.KategoriId == KategoriId);

            if (kategori == null)
            {
                return NotFound();
            }
            return View(kategori);
        }

        [HttpPost]
        public IActionResult Duzenle(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                _db.Kategoriler.Update(kategori);
                _db.SaveChanges();
                return RedirectToAction("Index", "Kategori");
            }
            return View();
        }

        public IActionResult Sil(int? KategoriId)
        {
            if(KategoriId == null)
            {
                return RedirectToAction("Index", "Kategori");
            }
            Kategori kategori = _db.Kategoriler.Find(KategoriId);
            if (kategori == null)
            {
                return NotFound();
            }
            _db.Kategoriler.Remove(_db.Kategoriler.FirstOrDefault(x => x.KategoriId == KategoriId));
            _db.SaveChanges();
            return RedirectToAction("Index", "Kategori");
        }


    }
}
