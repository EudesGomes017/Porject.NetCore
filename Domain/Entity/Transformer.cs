using Hvex.Domain.Interface.Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hvex.Domain.Entity {
    public class Transformer : EntityBase {
        public int InternalNumber { get; set; }
        public string TensionClass { get; set; }
        public int Potency { get; set; }
        public string Current { get; set; }
        //relationShips EFCORE
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual ICollection<Report> Reports { get; set; }
        [JsonIgnore]
        public virtual ICollection<Test> Tests { get; set; }

    }
}
