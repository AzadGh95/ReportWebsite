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
    public class IdentityUserManager : UserManager<IdentityUser, Guid>
    {
        private SqlConnection _SqlConnection;
        private bool _isopen;
        public IdentityUserManager(IUserStore<IdentityUser, Guid> store) : base(store)
        {
        }
        public static IdentityUserManager Create(IdentityFactoryOptions<IdentityUserManager>
            options, IOwinContext context)
        {
            var db = context.Get<SqlConnection>();
            var manager =
                new IdentityUserManager(
                    new UserStore(db));

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 4
            };

            return manager;
        }

        public static IdentityUserManager Create(SqlConnection context)
        {
            var db = context;
            var manager =
                new IdentityUserManager(
                    new UserStore(db));

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 4
            };

            return manager;
        }

    }
}
