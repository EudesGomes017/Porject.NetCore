using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hvex.Domain.Dto
{
    public class TransformerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InternalNumber { get; set; }
        public string TensionClass { get; set; }
        public int Potency { get; set; }
        public string Current { get; set; }
        public int UserId { get; set; }
    }
}
