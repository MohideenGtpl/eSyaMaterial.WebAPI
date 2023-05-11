using System;
using System.Collections.Generic;

namespace HCP.Material.DL.Entities
{
    public partial class GtImitsp
    {
        public int ItemSptype { get; set; }
        public string ItemSptypeDesc { get; set; }
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
