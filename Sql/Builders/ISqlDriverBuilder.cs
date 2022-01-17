using Core.Builders;
using Core.Entities;

namespace Sql.Builders {
    public interface ISqlDriverBuilder : IDriverBuilder {
        ISqlDriverBuilder WithSet<T>() where T : Entity;
    }
}