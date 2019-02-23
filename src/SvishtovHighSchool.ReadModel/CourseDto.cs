using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SvishtovHighSchool.ReadModel
{
    public class CourseDto : IReadEntity
    {
        // TODO should be immutable
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("AggregateId")]
        public string AggregateId { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
