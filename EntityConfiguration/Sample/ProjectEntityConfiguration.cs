using EntityConfiguration.Common;

namespace EntityConfiguration.Sample;

public class ProjectEntityConfiguration : EntityConfiguration<Project>
{
    public override void Configure(EntityConfigurationBuilder<Project> builder)
    {
        builder
            .Property(p => p.CreatedAt)
            .Sortable(s => s.FieldName("created_at").Alias("createdAtUtc"));

        builder
            .Property(p => p.UpdatedAt)
            .Sortable(s => s.FieldName("updated_at").Alias("updatedAtUtc"));
    }
}
