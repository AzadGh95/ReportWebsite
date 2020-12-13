using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dualp.Common.Logger;
using Dualp.Common.Types;
using EntityFramework.Extensions;
using ReportWebsite.Entities;
using ReportWebsite.Enums;
using ReportWebsite.Models;

namespace ReportWebsite.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataBaseContext.DataBaseContext _context;


        public ItemRepository()
        {
            this._context = new DataBaseContext.DataBaseContext();
        }

        public ItemRepository(DataBaseContext.DataBaseContext mainContext)
        {
            this._context = mainContext;
        }

        public List<EN_Item> GetItems(ReportWebSiteType.WebSiteType type)
        {
            try
            {
                var result = _context.Items.Where(v => v.Type == type).ToList();
                return result;
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message
                    );
                throw;
            }
        }

        ResultActivity IItemRepository.Delete(int id)
        {
            try
            {
                _context.Items.Where(x => x.ItemId == id).Delete();
                return new ResultActivity(true);
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;

            }

        }

        void IDisposable.Dispose()
        {
            _context?.Dispose();
        }

        ResultActivity IItemRepository.Edit(EN_Item item, int id)
        {
            try
            {
                _context.Items.Where(x => x.ItemId == id).Update(x => new EN_Item
                {
                    Text = item.Text,
                    Type = item.Type,

                });

                return new ResultActivity(true);

            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }

        EN_Item IItemRepository.GetItem(int id)
        {
            try
            {
                return _context.Items.AsNoTracking().FirstOrDefault(x => x.ItemId == id);
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }
        }

        List<EN_Item> IItemRepository.GetItems()
        {
            try
            {
                return _context.Items.AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }


        }

        ResultActivity IItemRepository.Insert(EN_Item item)
        {
            try
            {
                _context.Items.Add(item);
                _context.SaveChanges();
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