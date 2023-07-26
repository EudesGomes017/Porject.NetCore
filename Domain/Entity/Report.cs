using Hvex.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hvex.Domain.Entity {
    public class Report : EntityBase {
        public bool Status { get; set; }
            //RelationShips EFCORE
        public int TestId { get; set; }
        [JsonIgnore]
        public virtual Test Test { get; set; }
        public int TransformerId { get; set; }
        [JsonIgnore]
        public virtual Transformer Transformer { get; set; }

    }
}
