using BusinessLayer.Abstract;
using BusinessLayer.Utilities;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AdminManager : IAdminService
    {
        IAdminDal _adminDal;

        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public void AdminAdd(Admin admin)
        {
            _adminDal.Insert(admin);
        }

        public void AdminDelete(Admin admin)
        {
            throw new NotImplementedException();
        }

        public void AdminUpdate(Admin admin)
        {
            throw new NotImplementedException();
        }

        public Admin GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Admin GetByUserName(string username)
        {
            return _adminDal.GetByUserName(username);
        }

        public List<Admin> GetList()
        {
            return _adminDal.List();
        }

        public bool HasRole(string username, string role)
        {
            var admin = _adminDal.GetByUserName(username);
            return admin != null && admin.AdminRole == role;
        }

        public bool IsAdmin(string username)
        {
            return _adminDal.GetByUserName(username) != null;
        }

        public bool Login(string username, string password)
        {
            var admin = _adminDal.GetByUserName(username);
            if (admin == null) return false;

            string hash =
                PasswordHasher.HashPassword(password, admin.Salt);

            return hash == admin.PasswordHash;
        }
    }
}
