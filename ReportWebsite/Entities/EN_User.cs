using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReportWebsite.Entities
{
    public class EN_User
    {
        public EN_User()
        {

        }
        [Key]
        public int Id { get; set; }

        [Column(TypeName="Nvarchar")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(100)]
        public string UserName { get; set; }
        [Column(TypeName = "Nvarchar")]
        [StringLength(100)]
        public string Password { get; set; }
        [Column(TypeName = "Nvarchar")]
        [StringLength(100)]
        public string Phone { get; set; }
        [Column(TypeName = "Nvarchar")]
        [StringLength(100)]
        public string Role { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }
        public bool IsLock { get; set; }
        [Column(TypeName = "Nvarchar")]
        [StringLength(100)]
        public string Email { get; set; }
    }
}