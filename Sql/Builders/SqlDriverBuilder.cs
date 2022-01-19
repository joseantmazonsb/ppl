using PluggablePersistenceLayer.Core.Builders;
using PluggablePersistenceLayer.Core.Drivers;

namespace Sql.Builders {
    public abstract class SqlDriverBuilder : DriverBuilder {

        protected SqlDriverBuilder(string connectionString) : base(connectionString) { }

        protected override IDriver DoBuild() {
            var context = CreateContext();
            context.ConnectionString = ConnectionString;
            return new SqlDriver(context, Datasets);
        }

        protected abstract SqlContext CreateContext();
    }
}