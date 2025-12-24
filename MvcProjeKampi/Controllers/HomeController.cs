using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class HomeController : Controller
    {
        SkillManager sm = new SkillManager(new EfSkillDal());

        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult SweetAlert()
        {
            return View();
        }
    }
}