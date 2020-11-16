using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Models
{
    public class Drug
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Suffix Suffix { get; set; }
        public int SuffixId { get; set; }
    }
}
