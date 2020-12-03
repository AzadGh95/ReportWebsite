﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportWebsite.Entities
{
    public class EN_ViewEditModel
    {
        public int ItemId { get; set; }
        public int ElementId { get; set; }

        public string Value { get; set; }
        public string ItemText { get; set; }

        public bool Status { get; set; }
    }
}