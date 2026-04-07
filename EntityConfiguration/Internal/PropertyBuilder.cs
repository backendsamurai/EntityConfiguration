using EntityConfiguration.Common;

namespace EntityConfiguration.Internal;

public sealed class PropertyBuilder<T, TProp> where T : class, new()
{
    private readonly EntityConfigurationBuilder<T> _rootBuilder;
    private readonly string _propertyName;

    public PropertyBuilder(EntityConfigurationBuilder<T> rootBuilder, string propertyName)
    {
        _rootBuilder = rootBuilder;
        _propertyName = propertyName;
    }

    public PropertyBuilder<T, TProp> Sortable(Action<SortablePropertyBuilder> action)
    {
        var builder = new SortablePropertyBuilder(_propertyName);

        action(builder);

        var sortableProperty = builder.Build();

        _rootBuilder.AddSortableProperty(sortableProperty);

        return this;
    }

    public PropertyBuilder<T, TProp> Sortable()
    {
        var builder = new SortablePropertyBuilder(_propertyName);

        var field = builder.Build();

        _rootBuilder.AddSortableProperty(field);

        return this;
    }
}