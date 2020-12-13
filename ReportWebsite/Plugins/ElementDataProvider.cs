using Dualp.Common.Logger;
using Dualp.Common.Types;
using ReportWebsite.Models;
using ReportWebsite.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace ReportWebsite.Plugins
{
    public class ElementDataProvider
    {
        private readonly IElementRepository _elementRepository;
        public ElementDataProvider()
        {
            _elementRepository = new ElementRepository();
        }

        public ElementDataProvider(IElementRepository elementRepository)
        {
            _elementRepository = elementRepository;
        }
        public ResultActivity InsertElement(Element element)
        {
            try
            {
                Entities.En_Element eN_Element = element.ToElement();
                using (var ts = new TransactionScope(TransactionScopeOption.Required,
                 TransactionScopeAsyncFlowOption.Enabled))
                {
                    var result = _elementRepository.Insert(eN_Element);
                    ts.Complete();
                    return result;
                }
            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                return new ResultActivity(false, e.Message);
            }

        }
        public List<Element> GetElements()
        {
            return _elementRepository.GetEnElements()?.Select(i => (Element)i).ToList();
        }
        public Element GetElement(int id)
        {
            try
            {
                return _elementRepository.GetElement(id);
            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw;
            }
        }
        public ResultActivity DeleteElement(int id)
        {
            try
            {
                return _elementRepository.Delete(id);
            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw;
            }
        }
        public ResultActivity UpdateElement(Element element)
        {
            try
            {
                using (var ts = new TransactionScope(TransactionScopeOption.Required,
                 TransactionScopeAsyncFlowOption.Enabled))
                {
                    var id = element.ElementId;
                    var oldElement = _elementRepository.GetElement(id);
                    Entities.En_Element en_Element = new Entities.En_Element()
                    {
                        ElementId = id,
                        ItemId = element.ItemId,
                        ItemText = element.ItemText,
                        SiteId = element.SiteId,
                        Status = element.Status,
                        Value = element.Value,

                    };
                    var result = _elementRepository.Edit(en_Element, id);
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
    }
}