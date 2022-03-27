using _220318_OS_RestaurantMVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _220318_OS_RestaurantMVC.Controllers
{
    public class UrunlerController : Controller
    {
        private readonly RestaurantContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UrunlerController(RestaurantContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View(_db.Urunler
                .Include(x => x.Kategori)
                .Include(x => x.UrunlerMalzemeler)
                .ThenInclude(x => x.Malzeme)
                .OrderByDescending(x => x.UrunId)
                .ToList());
        }

        public IActionResult Ekle()
        {
            ViewBag.Kategoriler = new SelectList(_db.Kategoriler.ToList(), "KategoriId", "KategoriAdi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Ekle(Urun urun, IFormFile resim)
        {
            ResimHataKontrolleri(resim);
            if (ModelState.IsValid)
            {
                urun.UrunResimURL = ResimYukle(resim);
                _db.Urunler.Add(urun);
                _db.SaveChanges();
                return RedirectToAction("Index", "Urunler");
            }
            ViewBag.Kategoriler = new SelectList(_db.Kategoriler.ToList(), "KategoriId", "KategoriAdi");
            return View();
        }
        public IActionResult Sil(int id)
        {
            Urun silinecekUrun = _db.Urunler.FirstOrDefault(x => x.UrunId == id);
            if (silinecekUrun == null)
            {
                return NotFound();
            }
            return View(silinecekUrun);
        }
        [HttpPost]
        [ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public IActionResult SilOnay(int id)
        {
            Urun silinecekUrun = _db.Urunler.Find(id);
            _db.Urunler.Remove(silinecekUrun);
            _db.SaveChanges();
            ResimSil(silinecekUrun.UrunResimURL);
            TempData["mesaj"] = "Ürün başarıyla silindi.";
            return RedirectToAction("Index", "Urunler");
        }
        public IActionResult Duzenle(int id)
        {
            Urun duzenlenecekUrun = _db.Urunler.FirstOrDefault(x => x.UrunId == id);
            if (duzenlenecekUrun == null)
            {
                return NotFound();
            }
            ViewBag.Kategoriler = new SelectList(_db.Kategoriler.ToList(), "KategoriId", "KategoriAdi");
            return View(duzenlenecekUrun);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Duzenle(Urun urun, IFormFile resim)
        {
            if (resim != null)
            {
                ResimHataKontrolleri(resim);
            }
            if (ModelState.IsValid)
            {
                if (resim != null)
                {
                    ResimSil(urun.UrunResimURL);
                    urun.UrunResimURL = ResimYukle(resim);
                }
                _db.Update(urun);
                _db.SaveChanges();
                return RedirectToAction("Index", "Urunler");
            }
            ViewBag.Kategoriler = new SelectList(_db.Kategoriler.ToList(), "KategoriId", "KategoriAdi");
            return View(urun);
        }

        public IActionResult MalzemeDuzenle(int urunId)
        {
            Urun urun = _db.Urunler
                .Include(x => x.UrunlerMalzemeler)
                .ThenInclude(x => x.Malzeme)
                .FirstOrDefault(x => x.UrunId == urunId);
            if (urun == null)
            {
                return NotFound();
            }
            SelectList selectLists = new SelectList(_db.Malzemeler.ToList(), "MalzemeId", "MalzemeAdi");
            foreach (var item in selectLists)
            {
                if (urun.UrunlerMalzemeler
                    .Select(x => x.MalzemeId)
                    .ToList()
                    .Contains(Convert.ToInt32(item.Value)))
                {
                    item.Selected = true;
                }
            }
            ViewBag.Malzemeler = selectLists;
            return View(urunId);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MalzemeDuzenle(int urunId, List<int> ids)
        {
            Urun urun = _db.Urunler
                .Include(x => x.UrunlerMalzemeler)
                .FirstOrDefault(x => x.UrunId == urunId);
            if (urun == null)
            {
                return NotFound();
            }
            foreach (var item in urun.UrunlerMalzemeler)
            {
                _db.UrunlerMalzemeler.Remove(item);
            }
            if (ids.Count != 0)
            {
                List<UrunMalzeme> urunMalzemeler = new List<UrunMalzeme>();
                foreach (var item in ids)
                {
                    urunMalzemeler.Add(new UrunMalzeme()
                    {
                        MalzemeId = item,
                        UrunId = urunId
                    });
                }
                urun.UrunlerMalzemeler = urunMalzemeler;
            }
            _db.SaveChanges();
            return RedirectToAction("Index", "Urunler");
        }
        private void ResimSil(string urunResimURL)
        {
            if (!string.IsNullOrEmpty(urunResimURL))
            {
                var silYol = Path.Combine(_webHostEnvironment.WebRootPath, "images", "uploads", urunResimURL);
                if (System.IO.File.Exists(silYol))
                {
                    System.IO.File.Delete(silYol);
                }
            }
        }
        private string ResimYukle(IFormFile resim)
        {
            string ext = Path.GetExtension(resim.FileName);
            string resimAd = Guid.NewGuid() + ext;
            string dosyaYolu = Path.Combine(_webHostEnvironment.WebRootPath, "images", "uploads", resimAd);
            using (var stream = new FileStream(dosyaYolu, FileMode.Create))
            {
                resim.CopyTo(stream);
            }
            return resimAd;
        }
        private void ResimHataKontrolleri(IFormFile resim)
        {
            string[] izinVerilenler = { ".jpg", ".png", ".jpeg" };
            if (resim != null)
            {
                string ext = Path.GetExtension(resim.FileName);
                if (!izinVerilenler.Contains(ext))
                {
                    ModelState.AddModelError("resim", "İzin verilen dosya uzantıları jpeg, jpg, png");
                }
                else if (resim.Length > 1000 * 1000 * 1)//1MB
                {
                    ModelState.AddModelError("resim", "Maximum Dosya Boyutu 1MB.");
                }
            }
            else
            {
                ModelState.AddModelError("resim", "Resim Zorunludur.");
            }
        }
    }
}
