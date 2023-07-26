using System;

namespace Hvex.Domain.Entity {
    public class EntityBase {
        public int Id { get;  set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string Name { get; set; }

    }
}
