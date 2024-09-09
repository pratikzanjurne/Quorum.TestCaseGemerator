using System;
using System.Collections.Generic;

namespace Quorum.TestCaseGenerator.Models.Models
{
    public partial class ControlMasterIp
    {
        public int Id { get; set; }
        public int CrlId { get; set; }
        public int TypeId { get; set; }
        public string CtrlIpName { get; set; } = null!;
        public bool? IsRequired { get; set; }
    }
}
