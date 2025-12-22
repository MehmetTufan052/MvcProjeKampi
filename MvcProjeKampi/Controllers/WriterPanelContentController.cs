using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Concrete; // Add this using directive for Context

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelContentController : Controller
    {
        // GET: WriterPanelContent
        ContentManager cm = new ContentManager(new EfContentDal());
        Context c = new Context();
        public ActionResult MyContent()
        {
           

            string writerMail = User.Identity.Name;

            var writerId = c.Writers
                .Where(x => x.WriterMail == writerMail)
                .Select(x => x.WriterID)
                .FirstOrDefault();

            var contentValues = cm.GetListByWriter(writerId);

            return View(contentValues);
        }
        [HttpGet]
        public ActionResult AddContent(int id)
        {
            ViewBag.d = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddContent(Content p)
        {
            string writerMail = User.Identity.Name;

            var writerId = c.Writers
                .Where(x => x.WriterMail == writerMail)
                .Select(x => x.WriterID)
                .FirstOrDefault();
            p.ContentDate=DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterID = writerId;
            p.ContentStatus = true;
            cm.ContentAdd(p);
            return RedirectToAction("MyContent");
        }
        public ActionResult ToDoList()
        {
            return View();
        }
    }
}