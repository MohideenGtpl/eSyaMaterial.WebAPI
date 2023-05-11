using System;
using System.Collections.Generic;

namespace HCP.Material.DL.Entities
{
    public partial class GtImitgc
    {
        public int ItemGroupId { get; set; }
        public int ItemCategory { get; set; }
        public int ItemSubCategory { get; set; }
        public bool IsStockable { get; set; }
        public bool IsQuotationReqd { get; set; }
        public bool IsInspectionReqd { get; set; }
        public bool IsRateContract { get; set; }
        public bool? ActiveStatus { get; set; }
        public int FormId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedTerminal { get; set; }
    }
}
