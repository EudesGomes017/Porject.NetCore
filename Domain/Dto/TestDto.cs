using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hvex.Domain.Dto
{
    public class TestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DurationInSeconds { get; set; }
        public bool Status { get; set; }
        //RelationShips EFCORE
        public int TransformerId { get; set; }
    }
}
