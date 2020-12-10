using Dualp.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportWebsite.Repositories
{
    public interface IElementRepository : IDisposable
    {
        ResultActivity Delete(int Id);
        ResultActivity Insert(Entities.En_Element Element);
        ResultActivity Edit(Entities.En_Element Element, int Id);
        Entities.En_Element GetElement(int Id);
        List<Entities.En_Element> GetEnElements();
    }
}
