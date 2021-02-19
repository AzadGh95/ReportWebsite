using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportWebsite.Models
{
    public class PasswordModel
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string RepeatPassword { get; set; }
    }
}