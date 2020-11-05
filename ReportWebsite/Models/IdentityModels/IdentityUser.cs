using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Dualp.Customs.Data.IdentityEntities
{
    public class IdentityUser : IUser<Guid>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public IdentityUser()
        {
            Claims = new List<IdentityUserClaim>();
            Roles = new List<IdentityUserRole>();
            Logins = new List<IdentityUserLogin>();
        }

        /// <summary>
        ///     Email
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        ///     True if the email is confirmed, default is false
        /// </summary>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        ///     The salted/hashed form of the user password
        /// </summary>
        public virtual string PasswordHash { get; set; }

        /// <summary>
        ///     A random value that should change whenever a users credentials have changed (password changed, login removed)
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        ///     PhoneNumber for the user
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        ///     True if the phone number is confirmed, default is false
        /// </summary>
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        ///     Is two factor enabled for the user
        /// </summary>
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        ///     DateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        ///     Is lockout enabled for this user
        /// </summary>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        ///     Used to record failures for the purposes of lockout
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        ///     Navigation property for user roles
        /// </summary>
        public virtual ICollection<IdentityUserRole> Roles { get; }

        /// <summary>
        ///     Navigation property for user claims
        /// </summary>
        public virtual ICollection<IdentityUserClaim> Claims { get; }

        /// <summary>
        ///     Navigation property for user logins
        /// </summary>
        public virtual ICollection<IdentityUserLogin> Logins { get; }

        /// <summary>
        ///     User ID (Primary Key)
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        ///     User name
        /// </summary>
        public virtual string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BithDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }

        public string PostalAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string StateOrProvince { get; set; }
        public string PostalCode { get; set; }
        public string ProfileImage { get; set; }
        public string Language { get; set; }
        public string TimeZone { get; set; }

    }
}
