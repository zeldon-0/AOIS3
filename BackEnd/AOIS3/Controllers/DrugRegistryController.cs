using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Models;
using Core.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class DrugRegistryController : ControllerBase
    {
        private IDrugRegistryService _drugRegistryService;
        public DrugRegistryController(IDrugRegistryService drugRegistryService)
        {
            _drugRegistryService = drugRegistryService;
        }
        [HttpPost]
        public async Task<IActionResult> AddDrug(Drug drug)
        {
            await _drugRegistryService
                .AddDrugToRegistry(drug);
            return Ok();

        }
    }
}
