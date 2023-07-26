using Hvex.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hvex.Domain.Entity {
    public class User : EntityBase {
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public virtual ICollection<Transformer> Transformers { get; set; }

    }
}
