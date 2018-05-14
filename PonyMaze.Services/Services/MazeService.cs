using Newtonsoft.Json;
using PonyMaze.Services.Helpers;
using PonyMaze.Services.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PonyMaze.Services
{
    public class MazeService: IMazeService
    {
        private static Dictionary<string, MazeSolverHelper> mazes;
        private readonly HttpClient httpClient;
        private const string ponyApiUri = "https://ponychallenge.trustpilot.com/pony-challenge/maze";

        public MazeService()
        {
            mazes = new Dictionary<string, MazeSolverHelper>();
            httpClient = new HttpClient();
        }

        public async Task<MazeDataResponseModel> GetMazeData(MazeRequestModel mazeRequestModel)
        {
            var response = await httpClient.PostAsync(ponyApiUri, new StringContent(
                JsonConvert.SerializeObject(mazeRequestModel).ToString(),
                Encoding.UTF8,
                "application/json"
            ));

            var responseContent = await response.Content.ReadAsStringAsync();
            var mazeId = JsonConvert.DeserializeObject<MazeResponseModel>(responseContent).MazeId;

            if (!mazes.ContainsKey(mazeId))
            {
                var mazeDataResponse = await httpClient.GetAsync($"{ponyApiUri}/{mazeId}");
                var mazeDataResponseContent = await mazeDataResponse.Content.ReadAsStringAsync();
                var mazeDataResponseModel = JsonConvert.DeserializeObject<MazeDataResponseModel>(mazeDataResponseContent);

                mazeDataResponseModel.MazeId = mazeId;
                mazes.Add(mazeId, new MazeSolverHelper(mazeDataResponseModel));

                return mazeDataResponseModel;
            }

            return mazes[mazeId].MazeData;
        }

        public async Task<PonyMoveResponseModel> MovePony(string mazeId, PonyMoveRequestModel ponyMoveRequestModel)
        {
            if (!mazes.ContainsKey(mazeId))
            {
                return new PonyMoveResponseModel
                {
                    IsSuccessful = false,
                    MoveResult = Resources.MazeService.WrongMazeMessage
                };
            }

            if (ponyMoveRequestModel.Direction.Equals(Resources.MazeService.Automatically))
            {
                ponyMoveRequestModel.Direction = mazes[mazeId].GetMoveDirection();
            }
            else
            {
                mazes[mazeId].ShouldSolveAgain = true;
            }

            var moveResponse = await httpClient.PostAsync($"{ponyApiUri}/{mazeId}", new StringContent(
                JsonConvert.SerializeObject(ponyMoveRequestModel).ToString(),
                Encoding.UTF8,
                "application/json"
            ));

            var moveResponseContent = await moveResponse.Content.ReadAsStringAsync();
            var ponyMoveResponseModel = JsonConvert.DeserializeObject<PonyMoveResponseModel>(moveResponseContent);
            var isMoveSuccessful = ponyMoveResponseModel.MoveResult.Equals(Resources.MazeService.MoveAccepted);

            if (!isMoveSuccessful)
            {
                ponyMoveResponseModel.IsSuccessful = false;

                return ponyMoveResponseModel;
            }

            var mazeDataResponse = await httpClient.GetAsync($"{ponyApiUri}/{mazeId}");
            var mazeDataResponseContent = await mazeDataResponse.Content.ReadAsStringAsync();
            var mazeDataResponseModel = JsonConvert.DeserializeObject<MazeDataResponseModel>(mazeDataResponseContent);

            ponyMoveResponseModel.IsSuccessful = true;
            ponyMoveResponseModel.MazeData = mazeDataResponseModel;
            mazes[mazeId].UpdatePonyLocation(ponyMoveRequestModel.Direction);

            return ponyMoveResponseModel;
        }
    }
}
