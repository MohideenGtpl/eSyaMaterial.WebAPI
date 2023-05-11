﻿using System;
using System.Collections.Generic;

namespace HCP.Material.DL.Entities
{
    public partial class GtEskucd
    {
        public int Skuid { get; set; }
        public string Skutype { get; set; }
        public int Skucode { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedTerminal { get; set; }
    }
}