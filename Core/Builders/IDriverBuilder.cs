using Core.Drivers;

namespace Core.Builders {
    public interface IDriverBuilder {
        IDriver Build();
        IDriverBuilder WithOptions(IDriverBuilderOptions options);
    }
}