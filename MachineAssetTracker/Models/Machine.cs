using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MachineAssetTracker.Models
{
    public class Machine
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        [JsonRequired]
        [Required(ErrorMessage = "Machine Type is required.")]
        public string MachineType { get; set; } = string.Empty;
        public List<Asset> Assets { get; set; } = new List<Asset>();
    }
}
