using Dualp.Common.Logger;
using Dualp.Common.Types;
using ReportWebsite.Entities;
using ReportWebsite.IRepositories;
using ReportWebsite.Models;
using ReportWebsite.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace ReportWebsite.Plugins
{
    public class UserDataProvider
    {
        private readonly IUserRepository _userRepository;
        public UserDataProvider()
        {
            _userRepository = new UserRepository();

        }
        public ResultActivity Insert(User user)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required,
                TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    Entities.EN_User en_user = user.ToUser();

                    var result = _userRepository.InsertUser(en_user);
                    ts.Complete();
                    return result;
                }
                catch (Exception e)
                {
                    this.Log().Fatal(e.Message);
                    return new ResultActivity(false, e.Message);
                }
            }
        }
        public bool Lock(int id)
        {
            try
            {
               
                    var user = GetUser(id);
                    return _userRepository.LockUser(id, !(user.IsLock));

            }

            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }
        public ResultActivity Delete(int id)
        {
            return _userRepository.DeleteUser(id);
        }


        public ResultActivity Update(User user, int id)
        {
            try
            {
                using (var ts = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    // var id = user.Id;
                    var oldUser = _userRepository.GetUser(id);
                    Entities.EN_User eN_User = new Entities.EN_User()
                    {
                        Id = id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        IsLock = user.IsLock,
                        LastName = user.LastName,
                        Password = user.Password,
                        Phone = user.Phone,
                        UserName = user.UserName,
                    };
                    var result = _userRepository.EditUser(eN_User, id);
                    ts.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }
        public User GetUser(int id)
        {
            return _userRepository.GetUser(id);
        }
        public User GetUser(string username)
        {
            return _userRepository.GetUser(username);
        }
        public List<EN_User> GetUsers()
        {
            return _userRepository.GetUsers();
        }
        public bool Login(string user , string pass)
        {
            return _userRepository.Login(user, pass);
        }
        public bool ExitUser(string username)
        {
            return _userRepository.CheckUsername(username);
        }

        public bool ChangePassword(int userId , string newPass)
        {
            return _userRepository.ChangePassword(userId, newPass);
        }
    }
}