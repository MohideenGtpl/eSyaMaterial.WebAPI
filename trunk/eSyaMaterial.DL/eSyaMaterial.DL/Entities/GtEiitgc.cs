﻿using System;
using System.Collections.Generic;

namespace HCP.Material.DL.Entities
{
    public partial class GtEiitgc
    {
        public int ItemGroup { get; set; }
        public int ItemCategory { get; set; }
        public int ItemSubCategory { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedTerminal { get; set; }

        public virtual GtEiitct ItemCategoryNavigation { get; set; }
        public virtual GtEiitgr ItemGroupNavigation { get; set; }
    }
}
