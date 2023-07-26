using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hvex.Domain.Dto
{
    public class ReportDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        //RelationShips EFCORE
        public int TestId { get; set; }
        public int TransformerId { get; set; }
       
    }
}
