using _220318_OS_RestaurantMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _220318_OS_RestaurantMVC.Controllers
{
    public class MalzemeController : Controller
    {
        private readonly RestaurantContext _db;
        public MalzemeController(ILogger<HomeController> logger, RestaurantContext db)
        {
            this._db = db;
        }

        public IActionResult Index()
        {
            var malzemeler = _db.Malzemeler.ToList();
            return View(malzemeler);
        }

        public IActionResult Yeni()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Yeni(string MalzemeAdi)
        {
            Malzeme yeniMalzeme = new Malzeme();
            yeniMalzeme.MalzemeAdi = MalzemeAdi;
            _db.Malzemeler.Add(yeniMalzeme);
            _db.SaveChanges();
            return RedirectToAction("Index", "Malzeme");
        }

        public IActionResult Duzenle(int MalzemeId)
        {
            var malzeme = _db.Malzemeler.FirstOrDefault(x => x.MalzemeId == MalzemeId);

            if (malzeme == null)
            {
                return NotFound();
            }
            return View(malzeme);
        }

        [HttpPost]
        public IActionResult Duzenle(Malzeme malzeme)
        {
            if (ModelState.IsValid)
            {
                _db.Malzemeler.Update(malzeme);
                _db.SaveChanges();
                return RedirectToAction("Index", "Malzeme");
            }
            return View();
        }

        public IActionResult Sil(int? MalzemeId)
        {
            if (MalzemeId == null)
            {
                return RedirectToAction("Index", "Malzeme");
            }
            Malzeme kategori = _db.Malzemeler.Find(MalzemeId);
            if (kategori == null)
            {
                return NotFound();
            }
            _db.Malzemeler.Remove(_db.Malzemeler.FirstOrDefault(x => x.MalzemeId == MalzemeId));
            _db.SaveChanges();
            return RedirectToAction("Index", "Malzeme");
        }
    }
}
