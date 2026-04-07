using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace EntityConfiguration.Mongo;

public sealed class ValueObjectBsonSerializer<TValueObject, TPrimitive> : SerializerBase<TValueObject>
{
    public override TValueObject Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var primitive = BsonSerializer.Deserialize<TPrimitive>(context.Reader);
        return (TValueObject)Activator.CreateInstance(typeof(TValueObject), primitive)!;
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TValueObject value)
    {
        var primitive = (TPrimitive)value?.GetType().GetProperty("Value")!.GetValue(value)!;
        BsonSerializer.Serialize(context.Writer, primitive);
    }
}
