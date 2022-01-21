using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PluggablePersistenceLayer.Core;
using PluggablePersistenceLayer.Sql;

namespace PluggablePersistenceLayer.Postgres; 

public class PostgresDriver : SqlDriver {
    public PostgresDriver(string connectionString, IEnumerable<Dataset> datasets, 
        Action<IRelationalDbContextOptionsBuilderInfrastructure> options, Action<ModelBuilder> onModelCreating) : 
        base(connectionString, datasets, options, onModelCreating) {
    }

    protected override SqlContext CreateContext() {
        return Utils.CreateDbContext<PostgresContext>(Datasets);
    }
}