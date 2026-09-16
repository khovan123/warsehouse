using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Warehouse
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = default!;
        [BsonElement("code")]
        public string Code { get; set; } = default!;
        [BsonElement("name")]
        public string Name { get; set; } = default!;
        [BsonElement("organization")]
        public string Organization { get; set; } = default!;
        [BsonElement("active")]
        public bool IsActive { get; set; } = default!;
    }
}
