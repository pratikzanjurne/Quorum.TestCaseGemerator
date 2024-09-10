using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using Quorum.TestCaseGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Quorum.TestCaseGenerator.Service.Services
{
    public class ExcelExport_Service
    {
        private readonly IConfiguration _configuration;

        private readonly ApplicationDbContext _context;
        public ExcelExport_Service(IConfiguration configurations , ApplicationDbContext context)
        {
            _context = context;
            _configuration = configurations;
        }

        public async Task ExportToExcelAsync(string tableName)
        {
            // Use reflection to get the DbSet from the DbContext
            try
            {
                var dbSet = _context.GetType().GetProperty(tableName , BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?.GetValue(_context) as IQueryable<object>;

                if (dbSet == null)
                {
                    throw new ArgumentException($"Table {tableName} does not exist.");
                }

                var data = await dbSet.ToListAsync();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add(tableName);

                    // Add headers
                    var properties = data.FirstOrDefault()?.GetType().GetProperties();
                    if (properties != null)
                    {
                        for (int i = 0; i < properties.Length; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = properties[i].Name;
                        }

                        // Add data
                        for (int row = 0; row < data.Count; row++)
                        {
                            for (int col = 0; col < properties.Length; col++)
                            {
                                worksheet.Cells[row + 2, col + 1].Value = properties[col].GetValue(data[row]);
                            }
                        }
                    }

                    var filePath = _configuration["ExcelExportPath"] + tableName + $"_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                    // Save the file
                    var fileInfo = new FileInfo(filePath);
                    package.SaveAs(fileInfo);
                    return;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }
    }
}
