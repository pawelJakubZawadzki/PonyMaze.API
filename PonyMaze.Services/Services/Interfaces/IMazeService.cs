using PonyMaze.Services.Models;
using System.Threading.Tasks;

namespace PonyMaze.Services
{
    public interface IMazeService
    {
        Task<MazeDataResponseModel> GetMazeData(MazeRequestModel mazeRequestModel);
        Task<PonyMoveResponseModel> MovePony(string mazeId, PonyMoveRequestModel ponyMoveRequestModel);
    }
}
