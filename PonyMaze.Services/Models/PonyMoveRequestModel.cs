using Newtonsoft.Json;

namespace PonyMaze.Services.Models
{
    public class PonyMoveRequestModel
    {
        [JsonProperty(PropertyName = "direction")]
        public string Direction { get; set; }
    }
}
