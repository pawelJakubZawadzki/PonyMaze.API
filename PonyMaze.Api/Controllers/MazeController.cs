using Microsoft.AspNetCore.Mvc;
using PonyMaze.Services;
using PonyMaze.Services.Models;
using System.Threading.Tasks;

namespace PonyMaze.Api.Controllers
{
    [Route("api/[controller]")]
    public class MazeController : Controller
    {
        private IMazeService mazeService;

        public MazeController(IMazeService mazeService)
        {
            this.mazeService = mazeService;
        }

        [HttpPost]
        public async Task<MazeDataResponseModel> Post([FromBody] MazeRequestModel mazeRequestModel)
        {
            return await mazeService.GetMazeData(mazeRequestModel);
        }

        [HttpPost]
        [Route("{mazeId}")]
        public async Task<PonyMoveResponseModel> Post(string mazeId, [FromBody] PonyMoveRequestModel ponyMoveRequestModel)
        {
            return await mazeService.MovePony(mazeId, ponyMoveRequestModel);
        }
    }
}