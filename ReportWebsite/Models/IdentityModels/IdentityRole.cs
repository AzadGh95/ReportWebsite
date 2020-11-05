using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace Dualp.Customs.Data.IdentityEntities
{
    public class IdentityRole:IRole<Guid>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public IdentityRole()
        {
            Users = new List<IdentityUserRole>();
        }

        /// <summary>
        ///     Navigation property for users in the role
        /// </summary>
        public virtual ICollection<IdentityUserRole> Users { get; }

        /// <summary>
        ///     Role id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Role name
        /// </summary>
        public string Name { get; set; }
    }
}
