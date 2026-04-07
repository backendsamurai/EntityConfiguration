using EntityConfiguration.Models;

namespace EntityConfiguration.Internal;

public interface IEntityConfigurationBuilder
{
    EntityConfigurationMetadata Build();
}
