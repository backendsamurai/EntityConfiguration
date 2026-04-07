namespace EntityConfiguration.Internal;

public interface IEntityConfiguration
{
    Type EntityType { get; }

    void Configure(IEntityConfigurationBuilder builder);
}
