using Dualp.Common.Types;
using ReportWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportWebsite.Repositories
{
    public interface IItemRepository : IDisposable
    {

        ResultActivity Delete(int id);
        ResultActivity Insert(Entities.EN_Item item);
        ResultActivity Edit(Entities.EN_Item item, int id);
        Entities.EN_Item GetItem(int id);
        List<Entities.EN_Item> GetItems();
    }
}

