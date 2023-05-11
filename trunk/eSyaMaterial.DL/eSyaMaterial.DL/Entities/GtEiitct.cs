﻿using System;
using System.Collections.Generic;

namespace HCP.Material.DL.Entities
{
    public partial class GtEiitct
    {
        public GtEiitct()
        {
            GtEiitgc = new HashSet<GtEiitgc>();
            GtEiitsc = new HashSet<GtEiitsc>();
        }

        public int ItemCategory { get; set; }
        public string ItemCategoryDesc { get; set; }
        public decimal OriginalBudgetAmount { get; set; }
        public decimal RevisedBudgetAmount { get; set; }
        public decimal ComittmentAmount { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedTerminal { get; set; }

        public virtual ICollection<GtEiitgc> GtEiitgc { get; set; }
        public virtual ICollection<GtEiitsc> GtEiitsc { get; set; }
    }
}
