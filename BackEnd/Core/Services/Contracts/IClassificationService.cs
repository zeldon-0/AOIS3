using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Contracts
{
    public interface IClassificationService
    {
        Task<string> ClassifyImageCharacters(IFormFile file);
    }
}
