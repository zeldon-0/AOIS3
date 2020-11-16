using Core.Domain.Models;
using Core.Repositories.Contracts;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Repositories
{
    public class SuffixRepository : ISuffixRepository
    {
        private DrugContext _context;
        public SuffixRepository(DrugContext context)
        {
            _context = context;
        }
        public void AddSuffix(Suffix suffix)
        {
            _context.Suffixes.Add(suffix);
        }

        public async Task<Suffix> GetSuffixByValue(string value)
        {
            Suffix suffix = await _context
                .Suffixes
                .AsQueryable()
                .Where(s => s.Value == value)
                .FirstOrDefaultAsync();
            return suffix;
        }
    }
}
