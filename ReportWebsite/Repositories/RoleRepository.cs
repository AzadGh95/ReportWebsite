using Dualp.Common.Logger;
using Dualp.Common.Types;
using ReportWebsite.Entities;
using ReportWebsite.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportWebsite.Repositories
{
    public class RoleRepository: IRoleRepositpry
    {
        private readonly DataBaseContext.DataBaseContext _context;

        public RoleRepository()
        {
            this._context = new DataBaseContext.DataBaseContext();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public EN_Role GetRole(int id)
        {
            try
            {
                var role = _context.Roles.AsNoTracking().FirstOrDefault(i => i.RoleId == id);
                return role;
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }

        public List<EN_Role> GetRoles()
        {
            try
            {
                var users = _context.Roles.AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
            throw new NotImplementedException();
        }

        public ResultActivity Insert(EN_Role en_Role)
        {
            try
            {
                _context.Roles.Add(en_Role);
                return new ResultActivity(true);
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }
    }
}