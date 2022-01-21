using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PluggablePersistenceLayer.Core;
using PluggablePersistenceLayer.Sql;

namespace PluggablePersistenceLayer.MySql; 

public class MySqlDriver : SqlDriver {
    public MySqlDriver(string connectionString, IEnumerable<Dataset> datasets, 
        Action<IRelationalDbContextOptionsBuilderInfrastructure> options, Action<ModelBuilder> onModelCreating) 
        : base(connectionString, datasets, options, onModelCreating) {
    }

    protected override SqlContext CreateContext() {
        return Utils.CreateDbContext<MySqlContext>(Datasets);
    }
}