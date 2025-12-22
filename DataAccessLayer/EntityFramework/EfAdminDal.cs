using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfAdminDal: GenericRepository<Admin>,IAdminDal
    {      
    public Admin GetByUserName(string username)
        {
            return Get(x => x.AdminUserName == username);

        }
    }

}

