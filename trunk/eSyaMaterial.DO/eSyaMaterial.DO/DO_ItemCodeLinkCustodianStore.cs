using System;
using System.Collections.Generic;
using System.Text;

namespace HCP.Material.DO
{
   public class DO_ItemCodeLinkCustodianStore
    {
        public int BusinessKey { get; set; }
        public int ItemCode { get; set; }
        public int AccountingStore { get; set; }
        public int CustodianStore { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
    }
}
