using System;
using System.Collections.Generic;

namespace Quorum.TestCaseGenerator.Models.Models
{
    public partial class TestCase
    {
        public int Id { get; set; }
        public string Step { get; set; } = null!;
        public string ExpectedResult { get; set; } = null!;
    }
}
