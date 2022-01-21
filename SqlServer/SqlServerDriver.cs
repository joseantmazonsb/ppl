using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PluggablePersistenceLayer.Core;
using PluggablePersistenceLayer.Sql;

namespace PluggablePersistenceLayer.SqlServer; 

public class SqlServerDriver : SqlDriver {
    public SqlServerDriver(string connectionString, IEnumerable<Dataset> datasets,
        Action<IRelationalDbContextOptionsBuilderInfrastructure> options) : base(connectionString, datasets, options) {
    }

    protected override SqlContext CreateContext() {
        return Utils.CreateDbContext<SqlServerContext>(Datasets);
    }
}