using System;
using System.Collections.Generic;

namespace HCP.Material.DL.Entities
{
    public partial class GtEiitgr
    {
        public GtEiitgr()
        {
            GtEiitgc = new HashSet<GtEiitgc>();
        }

        public int ItemGroup { get; set; }
        public string ItemGroupDesc { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedTerminal { get; set; }

        public virtual ICollection<GtEiitgc> GtEiitgc { get; set; }
    }
}
