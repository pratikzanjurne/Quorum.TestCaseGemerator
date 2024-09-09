using System;
using System.Collections.Generic;

namespace Quorum.TestCaseGenerator.Models.Models
{
    public partial class ControlMaster
    {
        public int Id { get; set; }
        public string CtrlNm { get; set; } = null!;
        public string LogoPath { get; set; } = null!;
    }
}
