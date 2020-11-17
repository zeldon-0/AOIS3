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
    public class ClassificationController : ControllerBase
    {
        private IClassificationService _classificationService;
        private IDrugRegistryService _drugRegistryService;
        public ClassificationController(IClassificationService classificationService,
            IDrugRegistryService drugRegistryService)
        {
            _classificationService = classificationService;
            _drugRegistryService = drugRegistryService;
        }
        [HttpPost]
        public async Task<IActionResult> ClassifyImage()
        {
            var files = Request.Form.Files;
            string output = await _classificationService
                .ClassifyImageCharacters(files[0]);
            bool isDrug = await
                _drugRegistryService
                .IsDrug(output);
            ClassificationResult result =
                new ClassificationResult(output, isDrug);
            return Ok(result);

        }
    }
}
