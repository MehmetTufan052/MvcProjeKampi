using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using PagedList;
using PagedList.Mvc;
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
        WriterManager wm = new WriterManager(new EfWriterDal());
        Context c = new Context();
        

        // GET: WriterPanel
        [HttpGet]
        public ActionResult WriterProfile(int id = 0)
        {
            string p = User.Identity.Name;
            id = c.Writers
                            .Where(x => x.WriterMail == p)
                            .Select(y => y.WriterID)
                            .FirstOrDefault();
            var writerValue = wm.GetByID(id);
            return View(writerValue);
        }

        [HttpPost]
        public ActionResult WriterProfile(Writer p)
        {
            WriterValidator writerValidator = new WriterValidator();
            ValidationResult results = writerValidator.Validate(p);
            if (results.IsValid)
            {
                wm.WriterUpdate(p);
                return RedirectToAction("AllHeading","WriterPanel");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
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
        public ActionResult AllHeading(int p = 1)
        {
            var headings = hm.GetList().ToPagedList(p, 4);
            return View(headings);
        }
    }
}