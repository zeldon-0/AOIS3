using Core.Repositories.Contracts;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DrugContext _context;
        public UnitOfWork(DrugContext context, 
            IDrugRepository drugRepository,
            ISuffixRepository suffixRepository)
        {
            _context = context;
            DrugRepository = drugRepository;
            SuffixRepository = suffixRepository;
        }
        public IDrugRepository DrugRepository { get; }
        public ISuffixRepository SuffixRepository { get; }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
