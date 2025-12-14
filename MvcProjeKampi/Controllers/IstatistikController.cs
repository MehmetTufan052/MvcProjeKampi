using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class IstatistikController : Controller
    {
        IstatistikManager im = new IstatistikManager();

        public ActionResult Index()
        {
            ViewBag.ToplamKategori = im.ToplamKategoriSayisi();
            ViewBag.YazilimBaslik = im.YazilimBaslikSayisi();
            ViewBag.AHarfiYazar = im.AHarfiYazarSayisi();
            ViewBag.EnFazlaBaslikKategori = im.EnFazlaBaslikKategori();
            ViewBag.KategoriDurumFarki = im.KategoriDurumFarki();

            return View();
        }
    }

}