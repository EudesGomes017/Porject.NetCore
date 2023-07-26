using Hvex.Domain.Interface.Repository;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hvex.Domain.Entity {
    public class Test : EntityBase {
        public int DurationInSeconds { get; set; }
        public bool Status { get; set; }
        //RelationShips EFCORE
        public int TransformerId { get; set; }
        [JsonIgnore]
        public virtual Transformer Transformer { get; set; }
        [JsonIgnore]
        public virtual ICollection<Report> Reports { get; set; }

    }


  


    
}
