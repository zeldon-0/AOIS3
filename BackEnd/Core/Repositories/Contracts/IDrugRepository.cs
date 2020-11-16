using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.Contracts
{
    public interface IDrugRepository
    {
        Task<Drug> GetDrugByNameAsync(string name);
        void AddDrug(Drug drug);
    }
}
