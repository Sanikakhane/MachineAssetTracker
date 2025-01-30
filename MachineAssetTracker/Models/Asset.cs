using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MachineAssetTracker.Models
{
    public class Asset
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        public string AssetName { get; set; } = string.Empty;
        public List<string> Series { get; set; } = default; 
    }
}
