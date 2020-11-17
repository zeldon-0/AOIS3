using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Contracts
{
    public interface IDrugRegistryService
    {
        Task<bool> IsDrug(string input);
        Task AddDrugToRegistry(Drug drug);
    }
}
