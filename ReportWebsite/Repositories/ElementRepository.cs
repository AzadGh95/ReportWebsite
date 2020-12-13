using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dualp.Common.Logger;
using Dualp.Common.Types;
using EntityFramework.Extensions;
using ReportWebsite.Entities;

namespace ReportWebsite.Repositories
{
    public class ElementRepository : IElementRepository
    {
        private readonly DataBaseContext.DataBaseContext _context;
        public ElementRepository()
        {
            this._context = new DataBaseContext.DataBaseContext();
        }
        public ElementRepository(DataBaseContext.DataBaseContext mainContext)
        {
            this._context = mainContext;
        }

        public int CountElement()
        {
            try
            {
                return _context.Elements.Count();
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }

        public ResultActivity Delete(int Id)
        {
            _context.Elements.Where(x => x.ElementId == Id).Delete();
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context?. Dispose();
        }

        public ResultActivity Edit(En_Element Element, int Id)
        {
            try
            {
                _context.Elements.Where(x => x.ItemId == Id).Update(x => new En_Element
                {
                 ItemId = Element.ItemId,
                 ItemText = Element.ItemText,
                 Status = Element.Status,
                 Value = Element.Value,
                });
                return new ResultActivity(true);
            }
            catch (Exception ex)
            {
                this.Log().Fatal(ex.Message);
                throw;
            }

            
        }

        public En_Element GetElement(int Id)
        {
            throw new NotImplementedException();
        }

        public List<En_Element> GetEnElements()
        {
            throw new NotImplementedException();
        }

        public ResultActivity Insert(En_Element Element)
        {
            throw new NotImplementedException();
        }
    }
}