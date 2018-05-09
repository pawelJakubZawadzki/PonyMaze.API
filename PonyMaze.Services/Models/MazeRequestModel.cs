using Newtonsoft.Json;

namespace PonyMaze.Services.Models
{
    public class MazeRequestModel
    {
        [JsonProperty(PropertyName = "maze-width")]
        public int MazeWidth { get; set; }

        [JsonProperty(PropertyName = "maze-height")]
        public int MazeHeight { get; set; }

        [JsonProperty(PropertyName = "maze-player-name")]
        public string PlayerName { get; set; }

        [JsonProperty(PropertyName = "difficulty")]
        public int Difficulty { get; set; }
    }
}
