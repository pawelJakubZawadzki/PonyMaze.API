using Newtonsoft.Json;
using System.Collections.Generic;

namespace PonyMaze.Services.Models
{
    public class MazeDataResponseModel
    {
        [JsonProperty(PropertyName = "pony")]
        public List<int> PonyLocation { get; set; }

        [JsonProperty(PropertyName = "domokun")]
        public List<int> DomokunLocation { get; set; }

        [JsonProperty(PropertyName = "end-point")]
        public List<int> EndPointLocation { get; set; }

        [JsonProperty(PropertyName = "size")]
        public List<int> Size { get; set; }

        [JsonProperty(PropertyName = "data")]
        public List<List<string>> Data { get; set; }

        [JsonProperty(PropertyName = "mazeId")]
        public string MazeId { get; set; }
    }
}
