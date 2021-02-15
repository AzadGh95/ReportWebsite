using Dualp.Common.Types;
using ReportWebsite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportWebsite.IRepositories
{
    public interface IUserRepository:IDisposable
    {
        ResultActivity  InsertUser(EN_User user);
        ResultActivity  DeleteUser(int id);
        ResultActivity  LockUser(int id, bool doLock);
        ResultActivity  EditUser(EN_User user, int id);
        EN_User GetUser(int id);
        EN_User GetUser(string user);
        List<EN_User> GetUsers();

        bool Login(string user , string pass);
        bool CheckUsername(string username);

        bool ChangePassword(int userId,string newPass);

    }
}
