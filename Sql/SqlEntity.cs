using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PluggablePersistenceLayer.Core;

namespace Sql {
    public abstract class SqlEntity : Entity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid Id { get; set; }
    }
}