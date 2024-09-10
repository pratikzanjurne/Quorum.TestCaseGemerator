using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Quorum.TestCaseGenerator.Models;
using Quorum.TestCaseGenerator.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quorum.TestCaseGenerator.Service.Services
{
    public class PersonService
    {

        private ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public PersonService(IConfiguration configurations ,ApplicationDbContext context)
        {
            _context = context;
            _configuration = configurations;
        }

        public IEnumerable<Person> GetAllBooks()
        {
            return _context.Persons.ToList();
        }

        public async Task<Person> GetPersonById(int id)
        {
            return await _context.Persons.FindAsync(id);
        }

        public async Task<Person> Details(int id)
        {
            return await _context.Persons.FirstOrDefaultAsync(m => m.Personid == id);
        }

        public async Task Create(Person person)
        {
            _context.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Person person)
        {
            _context.Update(person);
            await _context.SaveChangesAsync();
        }

        public bool IsPersonExist(int id)
        {
            return (_context.Persons?.Any(p => p.Personid == id)).GetValueOrDefault();
        }

        public async Task Remove(Person person)
        {
            ExcelExport_Service exportService = new ExcelExport_Service(_configuration,_context);
            await exportService.ExportToExcelAsync("TestCases");
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }
    }
}
