using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dualp.Customs.Data.IdentityEntities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Dualp.Customs.Data.ADO.Identities
{
    public class IdentityRoleManager : RoleManager<IdentityRole, Guid>
    {
        private SqlConnection _sqlConnection;
        private bool _isopen;
        public IdentityRoleManager(IRoleStore<IdentityRole, Guid> store) : base(store)
        {
        }
        public static IdentityRoleManager Create(IdentityFactoryOptions<IdentityRoleManager>
            options, IOwinContext context)
        {
            var db = context.Get<SqlConnection>();
            var manager = new IdentityRoleManager(new RoleStore(db));
            return manager;
        }

        public static IdentityRoleManager Create(SqlConnection context)
        {
            var db = context;
            var manager = new IdentityRoleManager(new RoleStore(db));
            return manager;
        }


    }
}
