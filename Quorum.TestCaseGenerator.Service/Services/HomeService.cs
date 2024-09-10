using Microsoft.Extensions.Configuration;
using Quorum.TestCaseGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quorum.TestCaseGenerator.Service.Services
{
    public class HomeService
    {
        private ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public HomeService(IConfiguration configurations, ApplicationDbContext context)
        {
            _context = context;
            _configuration = configurations;
        }

        public async Task ExportExcel()
        {
            ExcelExport_Service service = new ExcelExport_Service(_configuration, _context);
            await service.ExportToExcelAsync("TestCases");
        }
    }
}
