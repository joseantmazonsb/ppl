using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PluggablePersistenceLayer.Core;
using PluggablePersistenceLayer.Sql;

namespace PluggablePersistenceLayer.SqlServer; 

public class SqlServerDriver : SqlDriver {
    public SqlServerDriver(string connectionString, IEnumerable<Dataset> datasets,
        Action<IRelationalDbContextOptionsBuilderInfrastructure> options,  Action<ModelBuilder> onModelCreating) 
        : base(connectionString, datasets, options, onModelCreating) {
    }

    protected override SqlContext CreateContext() {
        return Utils.CreateDbContext<SqlServerContext>(Datasets);
    }
}