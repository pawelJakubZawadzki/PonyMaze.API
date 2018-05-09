using Newtonsoft.Json;

namespace PonyMaze.Services.Models
{
    public class PonyMoveResponseModel
    {
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "state-result")]
        public string MoveResult { get; set; }

        [JsonProperty(PropertyName = "mazeData")]
        public MazeDataResponseModel MazeData { get; set; }

        [JsonProperty(PropertyName = "isSuccessful")]
        public bool IsSuccessful { get; set; }
    }
}
