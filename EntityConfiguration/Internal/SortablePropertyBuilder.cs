using EntityConfiguration.Extensions;
using EntityConfiguration.Models;

namespace EntityConfiguration.Internal;

public sealed class SortablePropertyBuilder
{
    private SortableProperty _sortableProperty;

    public SortablePropertyBuilder(string propertyName)
    {
        _sortableProperty = new SortableProperty(propertyName, propertyName.ToCamelCase());
    }

    public SortablePropertyBuilder FieldName(string fieldName)
    {
        _sortableProperty = _sortableProperty with { FieldName = fieldName };

        return this;
    }

    public void Alias(string alias)
    {
        _sortableProperty = _sortableProperty with { Alias = alias };
    }

    public SortableProperty Build() => _sortableProperty;
}
