namespace EntityConfiguration.Models;

public sealed record EntityConfigurationMetadata
{
    public List<SortableProperty> SortableProperties { get; set; } = [];
}
