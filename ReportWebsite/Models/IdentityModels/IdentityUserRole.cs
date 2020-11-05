using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dualp.Customs.Data.IdentityEntities
{
    public class IdentityUserRole
    {
        /// <summary>
        ///     UserId for the user that is in the role
        /// </summary>
        public virtual Guid UserId { get; set; }

        /// <summary>
        ///     RoleId for the role
        /// </summary>
        public virtual Guid RoleId { get; set; }
    }
}
