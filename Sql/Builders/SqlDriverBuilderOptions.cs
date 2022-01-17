using Core.Builders;

namespace Sql.Builders {
    public class SqlDriverBuilderOptions : IDriverBuilderOptions {
        public SqlProvider Provider { get; }

        public SqlDriverBuilderOptions(SqlProvider provider) {
            Provider = provider;
        }
    }
}