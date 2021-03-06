using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PluggablePersistenceLayer.Core;
using PluggablePersistenceLayer.Sql;

namespace PluggablePersistenceLayer.Sqlite; 

public class SqliteDriver : SqlDriver {
    public SqliteDriver(string connectionString, IEnumerable<Dataset> datasets,
        Action<IRelationalDbContextOptionsBuilderInfrastructure> options,  Action<ModelBuilder> onModelCreating) 
        : base(connectionString, datasets, options, onModelCreating) {
    }

    protected override SqlContext CreateContext() {
        return Utils.CreateDbContext<SqliteContext>(Datasets);
    }
}