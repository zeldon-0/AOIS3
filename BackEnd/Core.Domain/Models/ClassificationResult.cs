using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Models
{
    public class ClassificationResult
    {
        public string Output { get; set; }
        public bool IsDrug { get; set; }
        public ClassificationResult(string output,
            bool isDrug)
        {
            Output = output;
            IsDrug = isDrug;
        }
    }
}
