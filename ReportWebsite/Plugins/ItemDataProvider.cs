using Dualp.Common.Types;
using ReportWebsite.Models;
using ReportWebsite.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;
using Dualp.Common.Logger;
using static ReportWebsite.Enums.ReportWebSiteType;

namespace ReportWebsite.Plugins
{
    public class ItemDataProvider
    {
        private readonly IItemRepository _itemRepository;
        
        public ItemDataProvider()
        {
            _itemRepository = new ItemRepository();
        }

        public ItemDataProvider(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        ResultActivity InsertItem(Item item)
        {
            try
            {
                Entities.EN_Item eN_Item = item.ToItem();
                using (var ts = new TransactionScope(TransactionScopeOption.Required,
                 TransactionScopeAsyncFlowOption.Enabled))
                {
                    var result = _itemRepository.Insert(eN_Item);
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
        public List<Item> GetItems()
        {
            return _itemRepository.GetItems()?.Select(i => (Item)i).ToList();
        }
        public Item GetItem(int id)
        {
            try
            {
                return _itemRepository.GetItem(id);
            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw;
            }
        }
        ResultActivity DeleteItem(int id)
        {
            try
            {
                return _itemRepository.Delete(id);
            }
            catch (Exception e)
            {
                this.Log().Fatal(e.Message);
                throw;
            }
        }

        public List<Item> GetItems(WebSiteType type) {
            return _itemRepository.GetItems(type)?.Select(i => (Item)i).ToList();

        }

    }
}