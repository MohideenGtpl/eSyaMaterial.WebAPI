using System;
using System.Collections.Generic;

namespace HCP.Material.DL.Entities
{
    public partial class GtEiitwu
    {
        public int BusinessKey { get; set; }
        public int ItemCode { get; set; }
        public int StoreCode { get; set; }
        public decimal MaximumStockLevel { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal SafetyStockLevel { get; set; }
        public decimal MinimumStockLevel { get; set; }
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
