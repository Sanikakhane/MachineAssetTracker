using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MachineAssetTracker.Models
{
    public class Machine
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        public string MachineType { get; set; } = string.Empty;
        public List<Asset> Assets { get; set; } = new List<Asset>();
    }
}
