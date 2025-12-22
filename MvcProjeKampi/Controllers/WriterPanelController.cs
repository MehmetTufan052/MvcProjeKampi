using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{

    public class WriterPanelController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        Context c = new Context();
       
        // GET: WriterPanel
        public ActionResult WriterProfile()
        {
            return View();
        }
        public ActionResult MyHeading(string p)
        {
            

          
            string writerMail = User.Identity.Name;

           
            var writerIdinfo = c.Writers
                            .Where(x => x.WriterMail == writerMail)
                            .Select(y => y.WriterID)
                            .FirstOrDefault();

            var values = hm.GetListByWriter(writerIdinfo);
            return View(values);
        }
        [HttpGet]
        public ActionResult NewHeading()
        {
            
            
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()

                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            return View();
        }
        [HttpPost]
        public ActionResult NewHeading(Heading p)
        {
            Context c = new Context();

            // Giriş yapan yazarın maili
            string writerMail = User.Identity.Name;

            // Writer ID
            var writerId = c.Writers
                            .Where(x => x.WriterMail == writerMail)
                            .Select(y => y.WriterID)
                            .FirstOrDefault();

            // Güvenlik kontrolü (önemli)
            if (writerId == 0)
            {
                return RedirectToAction("Login", "WriterLogin");
            }

            p.HeadingDate = DateTime.Now;
            p.WriterID = writerId;
            p.HeadingStatus = true;

            hm.HeadingAdd(p);

            return RedirectToAction("MyHeading");
        }

        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()

                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            var HeadingValue = hm.GetByID(id);
            return View(HeadingValue);

        }
        [HttpPost]
        public ActionResult EditHeading(Heading p)
        {
            hm.HeadingUpdate(p);
            return RedirectToAction("MyHeading");
        }
        public ActionResult DeleteHeading(int id)
        {
            var heading = hm.GetByID(id);
            heading.HeadingStatus = !heading.HeadingStatus;
            hm.HeadingUpdate(heading);
            return RedirectToAction("MyHeading");
        }
        public ActionResult AllHeading()
        {
            var headings = hm.GetList();
            return View(headings);
        }
    }
}