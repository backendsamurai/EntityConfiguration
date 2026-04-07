using MongoDB.Bson.Serialization;

namespace EntityConfiguration.Mongo;

public class ValueObjectSerializerProvider : IBsonSerializationProvider
{
    private readonly HashSet<Type> _supportedTypes;

    public ValueObjectSerializerProvider(IEnumerable<Type> valueObjectTypes)
    {
        _supportedTypes = [.. valueObjectTypes];
    }

    public IBsonSerializer? GetSerializer(Type type)
    {
        if (!_supportedTypes.Contains(type))
            return null;

        var primitive = type.GetProperty("Value")!.PropertyType;

        var serializerType = typeof(ValueObjectBsonSerializer<,>)
            .MakeGenericType(type, primitive);

        return (IBsonSerializer)Activator.CreateInstance(serializerType)!;
    }
}
