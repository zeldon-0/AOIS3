using Core.Domain.Models;
using Core.Repositories.Contracts;
using Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class DrugRegistryService : IDrugRegistryService
    {
        private IUnitOfWork _unitOfWork;
        public DrugRegistryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddDrugToRegistry(Drug input)
        {
            Suffix suffix = new Suffix
            {
                Value = new string(
                    input
                    .Name
                    .Skip(input.Name.Length - 3)
                    .ToArray()
                )
            };

            input.Suffix = suffix;

            _unitOfWork.DrugRepository
                .AddDrug(input);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> IsDrug(string input)
        {
            Drug drug = await
                _unitOfWork
                .DrugRepository
                .GetDrugByNameAsync(input);
            string inputSuffix = new string(
                    input
                    .Skip(input.Length - 3)
                    .ToArray()
                );
            Suffix suffix = await
                _unitOfWork
                .SuffixRepository
                .GetSuffixByValueAsync(inputSuffix);

            if (drug == null && suffix == null)
            {
                return false;
            }
            return true;
        }
    }
}
