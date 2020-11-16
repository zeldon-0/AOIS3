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
    public class DrugRepository : IDrugRepository
    {
        private DrugContext _context;
        public DrugRepository(DrugContext context)
        {
            _context = context;
        }
        public void AddDrug(Drug drug)
        {
            _context.Drugs.Add(drug);
        }

        public async Task<Drug> GetDrugByNameAsync(string name)
        {
            Drug drug = await _context
                .Drugs
                .AsQueryable()
                .Where(d => d.Name == name)
                .FirstOrDefaultAsync();
            return drug;
        }
    }
}
