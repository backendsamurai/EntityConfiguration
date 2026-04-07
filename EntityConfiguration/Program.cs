using EntityConfiguration.Common;
using EntityConfiguration.Mongo;
using EntityConfiguration.Sample;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

var configurationRegistry = new ConfigurationRegistry();

configurationRegistry.AddFromAssembly(typeof(Program).Assembly);
configurationRegistry.BuildRegistry();

var conventionPack = new ConventionPack
{
    new CamelCaseElementNameConvention(),
};

ConventionRegistry.Register("Default", conventionPack, (t) => true);
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var types = configurationRegistry.GetEliegibleValueObjectTypesForSerialization();

BsonSerializer.RegisterSerializationProvider(new ValueObjectSerializerProvider(types));

var projectId = new ProjectId(Guid.NewGuid());

var project = new Project
{
    Id = projectId,
    Title = "Test Project",
    Description = "This is a test project.",
    Author = new Author("John Doe"),
};

var bsonDocument = new BsonDocument();
using var writer = new BsonDocumentWriter(bsonDocument);

BsonSerializer.Serialize(writer, project);

Console.ReadLine();