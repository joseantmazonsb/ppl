using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PluggablePersistenceLayer.Core;
using PluggablePersistenceLayer.Sql;

namespace PluggablePersistenceLayer.MySql; 

public class MySqlDriver : SqlDriver {
    public MySqlDriver(string connectionString, IEnumerable<Dataset> datasets, Action<IRelationalDbContextOptionsBuilderInfrastructure> options) : base(connectionString, datasets, options) {
    }

    protected override SqlContext CreateContext() {
        return Utils.CreateDbContext<MySqlContext>(Datasets);
    }
}