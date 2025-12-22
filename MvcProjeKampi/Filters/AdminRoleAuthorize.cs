using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Filters
{
    public class AdminRoleAuthorize : AuthorizeAttribute
    {
        public string Role { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var username = httpContext.User.Identity.Name;

            if (string.IsNullOrEmpty(username))
                return false;

            AdminManager am = new AdminManager(new EfAdminDal());
            return am.HasRole(username, Role);
        }
    }
}