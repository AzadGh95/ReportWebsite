using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReportWebsite.Entities
{
    public class EN_Role
    {
        //[Key]
        //[Required]
        //[MaxLength(1)]
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleInSystem { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<EN_User> Users { get; set; }

    }
}