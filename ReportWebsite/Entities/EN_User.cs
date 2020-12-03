using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportWebsite.Entities
{
    public class EN_User
    {
        public EN_User()
        {

        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsLock { get; set; }
        public string Email { get; set; }
    }
}