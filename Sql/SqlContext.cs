using Microsoft.EntityFrameworkCore;

namespace Sql {
    /// <summary>
    /// Base context used to interact with the inner data layer.
    /// </summary>
    public abstract class SqlContext : DbContext {
        public string ConnectionString { get; set; } = string.Empty;
    }
}