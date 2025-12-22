using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        AdminManager _adminManager = new AdminManager(new EfAdminDal());
        WriterManager _writerService = new WriterManager(new EfWriterDal());


        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(Admin p)
        {
            bool loginResult = _adminManager.Login(
                p.AdminUserName, p.AdminPassword);

            if (loginResult)
            {
                FormsAuthentication.SetAuthCookie(
                    p.AdminUserName, false);

                return RedirectToAction(
                    "Index", "AdminCategory");
            }

            ViewBag.Error = "Kullanıcı adı veya şifre hatalı";
            return View();
        }
        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterLogin(Writer p)
        {
            bool result = _writerService.WriterLogin(
                p.WriterMail, p.WriterPassword);

            if (result)
            {
                FormsAuthentication.SetAuthCookie(
                    p.WriterMail, false);

                return RedirectToAction(
                    "MyContent", "WriterPanelContent");
            }

            ViewBag.Error = "Kullanıcı adı veya şifre hatalı";
            return View();
        }
    
    

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Headings","Default");
        }
    }
}
