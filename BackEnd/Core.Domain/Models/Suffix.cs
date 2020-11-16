using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Models
{
    public class Suffix
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public IEnumerable<Drug> Drugs { get; set; }
    }
}
