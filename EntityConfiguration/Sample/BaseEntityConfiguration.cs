using EntityConfiguration.Common;

namespace EntityConfiguration.Sample;

public abstract class BaseEntityConfiguration<T, TId> : EntityConfiguration<T> where T : Entity<TId>, new() where TId : Id
{
    public override void Configure(EntityConfigurationBuilder<T> builder)
    {
        builder.Property(e => e.CreatedAt).Sortable();
        builder.Property(e => e.UpdatedAt).Sortable();
    }
}
