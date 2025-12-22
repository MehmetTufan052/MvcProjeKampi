using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAdminService
    {
        Admin GetByUserName(string username);
        bool Login(string username, string password);
        bool IsAdmin(string username);
        bool HasRole(string username, string role);
    }
}
