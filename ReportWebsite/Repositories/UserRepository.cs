using Dualp.Common.Logger;
using Dualp.Common.Types;
using EntityFramework.Extensions;
using ReportWebsite.Entities;
using ReportWebsite.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ReportWebsite.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DataBaseContext.DataBaseContext _context;
        public UserRepository()
        {
            _context = new DataBaseContext.DataBaseContext();
        }


        public ResultActivity DeleteUser(int id)
        {
            try
            {
                _context.Users.Where(x => x.Id == id).Delete();
                return new ResultActivity(true);
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public ResultActivity EditUser(EN_User user, int id)
        {
            _context.Users.Where(x => x.Id == id).Update(x => new EN_User
            {
                Email = user.Email,
                LastName = user.LastName,
                IsLock = user.IsLock,
                Password = user.Password,
                Phone = user.Phone,
                UserName = user.UserName,
                FirstName = user.FirstName,

            });
            return new ResultActivity(true);
        }

        public EN_User GetUser(int id)
        {
            try
            {
                var u = _context.Users.AsNoTracking().FirstOrDefault(i => i.Id == id);
                return u;
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }

        public List<EN_User> GetUsers()
        {
            try
            {
                var us = _context.Users.AsNoTracking().ToList();
                return us;
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }

        public ResultActivity InsertUser(EN_User user)
        {
            try
            {
                _context.Users.Add(user);
                return new ResultActivity(true);
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }

        public ResultActivity LockUser(int id, bool doLock)
        {
            try
            {
                _context.Users.Where(x => x.Id == id)
                    .Update(x => new EN_User
                    {
                        IsLock = doLock
                    });
                return new ResultActivity(true);
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }


        public bool Login(string user, string pass)
        {
            try
            {
                string Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "MD5");

                if (!_context.Users.Any(u => u.UserName == user && u.Password == Password))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }
        public bool CheckUsername(string username)
        {
            try
            {
                if (!_context.Users.Any(u => u.UserName == username))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }

    }
}