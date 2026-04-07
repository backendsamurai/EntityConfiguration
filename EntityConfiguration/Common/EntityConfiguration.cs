using EntityConfiguration.Internal;

namespace EntityConfiguration.Common;

public abstract class EntityConfiguration<T> : IEntityConfiguration where T : class, new()
{
    public Type EntityType => typeof(T);

    public void Configure(IEntityConfigurationBuilder builder) {
        Configure((EntityConfigurationBuilder<T>)builder);
    }

    public virtual void Configure(EntityConfigurationBuilder<T> builder)
    {
    }
}
