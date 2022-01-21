using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PluggablePersistenceLayer.Core;
using PluggablePersistenceLayer.Sql;

namespace PluggablePersistenceLayer.Postgres; 

public class PostgresDriver : SqlDriver {
    public PostgresDriver(string connectionString, IEnumerable<Dataset> datasets, Action<IRelationalDbContextOptionsBuilderInfrastructure> options) : base(connectionString, datasets, options) {
    }

    protected override SqlContext CreateContext() {
        return Utils.CreateDbContext<PostgresContext>(Datasets);
    }
}