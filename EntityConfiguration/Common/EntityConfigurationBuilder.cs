using EntityConfiguration.Internal;
using EntityConfiguration.Models;
using System.Linq.Expressions;

namespace EntityConfiguration.Common;

public sealed class EntityConfigurationBuilder<T> : IEntityConfigurationBuilder where T : class, new()
{
    private readonly List<SortableProperty> _sortableProperties = [];

    public IReadOnlyList<SortableProperty> SortableFields => _sortableProperties;

    internal void AddSortableProperty(SortableProperty field)
    {
        _sortableProperties.Add(field);
    }

    public PropertyBuilder<T, TProp> Property<TProp>(Expression<Func<T, TProp>> selector) {
        var propertyName = GetPropertyName(selector);

        return new PropertyBuilder<T, TProp>(this, propertyName);
    }

    private static string GetPropertyName<TProp>(Expression<Func<T, TProp>> selector)
    {
        return selector.Body switch
        {
            MemberExpression member => member.Member.Name,
            UnaryExpression unary when unary.Operand is MemberExpression unaryMember => unaryMember.Member.Name,
            _ => throw new InvalidOperationException("Selector must be a property expression")
        };
    }

    public EntityConfigurationMetadata Build()
    {
        return new EntityConfigurationMetadata
        {
            SortableProperties = _sortableProperties
        };
    }
}
