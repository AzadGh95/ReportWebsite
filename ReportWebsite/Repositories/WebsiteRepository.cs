using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dualp.Common.Logger;
using Dualp.Common.Types;
using EntityFramework.Extensions;
using ReportWebsite.Entities;
using ReportWebsite.Enums;

namespace ReportWebsite.Repositories
{
    public class WebsiteRepository : IWebsiteRepository
    {
        private readonly DataBaseContext.DataBaseContext _context;
        public WebsiteRepository()
        {
            this._context = new DataBaseContext.DataBaseContext();
        }
        public WebsiteRepository(DataBaseContext.DataBaseContext mainContext)
        {
            this._context = mainContext;
        }
        public int CountWeb(ReportWebSiteType.WebSiteType webSiteType)
        {
            try
            {
                return _context.WebSites.Count();
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }

        public ResultActivity Delete(int Id)
        {
            try
            {
                _context.WebSites.Where(x => x.SiteId == Id).Delete();

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

        public ResultActivity Edit(EN_WebSite WebSite)
        {
            try
            {
                _context.WebSites.Where(x => x.SiteId == WebSite.SiteId).Update(x => new EN_WebSite {
                    PasswordSuper = WebSite.PasswordSuper,      
                    Admin = WebSite.Admin,
                    PasswordSite = WebSite.PasswordSite,
                    UserSite = WebSite.UserSite,
                    Name = WebSite.Name,
                    Description = WebSite.Description,
                    UserSuper = WebSite.UserSuper
                });
                return new ResultActivity(true);
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }

        public EN_WebSite GetWebSite(int id)
        {
            try
            {
                return _context.WebSites.First(x => x.SiteId == id);
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }

        public List<EN_WebSite> GetWebSites(ReportWebSiteType.WebSiteType type)
        {
            try
            {
                return  _context.WebSites.Where(i=>i.Type==type).ToList();

            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }

        public ResultActivity Insert(EN_WebSite WebSite)
        {
            try
            {
                _context.WebSites.Add(WebSite);
                _context.SaveChanges();
                return new ResultActivity();
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }
    }
}