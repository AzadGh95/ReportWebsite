using Dualp.Common.Types;
using ReportWebsite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportWebsite.IRepositories
{
    public interface IRoleRepositpry:IDisposable
    {
        ResultActivity Insert(EN_Role en_Role);
        EN_Role GetRole(int id);
        List<EN_Role> GetRoles();
    }
}
