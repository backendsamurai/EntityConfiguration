using EntityConfiguration.Internal;
using EntityConfiguration.Models;
using System.Reflection;

namespace EntityConfiguration.Common;

public sealed class ConfigurationRegistry
{
    private readonly List<IEntityConfiguration> _entityConfigurations = [];
    private readonly Dictionary<Type, EntityConfigurationMetadata> _registry = [];

    public IReadOnlyDictionary<Type, EntityConfigurationMetadata> Registry => _registry;

    public ConfigurationRegistry() { }

    public void AddConfiguration<T>(EntityConfiguration<T> configuration) where T : class, new()
    {
        _entityConfigurations.Add(configuration);
    }

    public void AddFromAssembly(Assembly assembly)
    {
        var configurations = assembly.GetTypes()
            .Where(t => typeof(IEntityConfiguration).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract && !t.ContainsGenericParameters)
            .Select(Activator.CreateInstance)
            .Cast<IEntityConfiguration>();

        _entityConfigurations.AddRange(configurations);
    }

    public void BuildRegistry()
    {
        foreach (var entityConfiguration in _entityConfigurations)
        {
            var config = (IEntityConfiguration)Activator.CreateInstance(entityConfiguration.GetType())!;

            var builderType = typeof(EntityConfigurationBuilder<>)
                .MakeGenericType(config.EntityType);

            var builder = (IEntityConfigurationBuilder)Activator.CreateInstance(builderType)!;

            config.Configure(builder);

            var data = builder.Build();
            _registry.Add(config.EntityType, data);
        }
    }

    public List<Type> GetEliegibleValueObjectTypesForSerialization()
    {
        List<Type> valueObjectTypes = [];

        foreach (var entity in _registry.Keys)
        {
            var types = entity.GetProperties()
                .Where(p => IsASinglePropertyValueObject(p.PropertyType))
                .Select(p => p.PropertyType)
                .ToList();

            valueObjectTypes.AddRange(types);
        }

        return [.. valueObjectTypes.DistinctBy(t => t.FullName)];
    }

    private static bool IsASinglePropertyValueObject(Type type)
    {
        if (!type.IsClass && !type.IsValueType)
            return false;

        if (type.IsAbstract)
            return false;

        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        if (props.Length != 1)
            return false;

        var valueProp = props[0];

        if (valueProp.Name != "Value")
            return false;

        var ctor = type.GetConstructor([valueProp.PropertyType]);

        if (ctor == null)
            return false;

        return true;
    }
}
