using Newtonsoft.Json;

namespace PonyMaze.Services.Models
{
    public class MazeResponseModel
    {
        [JsonProperty(PropertyName = "maze_id")]
        public string MazeId { get; set; }
    }
}
