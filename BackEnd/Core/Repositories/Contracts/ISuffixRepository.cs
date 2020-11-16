using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.Contracts
{
    public interface ISuffixRepository
    {
        Task<Suffix> GetSuffixByValue(string value);
        void AddSuffix(Suffix suffix);
    }
}
