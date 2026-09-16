using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Address
    {
        [BsonElement("street")]
        public string? Street { get; set; } = default!;
        [BsonElement("city")]
        public string? City { get; set; } = default!;
        [BsonElement("country")]
        public string? Country { get; set; } = default!;        
    }
}
