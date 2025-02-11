using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace MachineAssetTracker.Models
{
    public class MachineAsset
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

        [JsonRequired]
        public string MachineType { get; set; }= string.Empty;
        public string Asset { get; set; } = string.Empty;
        public string Series { get; set; } = string.Empty;

    }
}
