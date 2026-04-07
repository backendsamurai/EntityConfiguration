using System.Diagnostics.CodeAnalysis;

namespace EntityConfiguration.Models;

public sealed record SortableProperty
{
    public required string PropertyName { get; init; }

    public required string FieldName { get; init; }

    public string? Alias { get; init; }

    [SetsRequiredMembers]
    public SortableProperty(string propertyName, string fieldName, string? alias = null)
    {
        PropertyName = propertyName;
        FieldName = fieldName;
        Alias = alias;
    }
}