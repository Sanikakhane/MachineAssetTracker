using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MachineAssetTracker.Models
{
    public class Asset
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

        [JsonRequired]
        [Required(ErrorMessage ="The AssetName feild id required")]
        public string AssetName { get; set; } = string.Empty;

        [JsonRequired]
        [Required(ErrorMessage ="Add at least one series")]
        [MinLength(1, ErrorMessage = "At least one series is required.")]
        public List<string> Series { get; set; } = new List<string>(); 
    }
}
