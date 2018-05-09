using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PonyMaze.Services;

namespace PonyMaze.Api.Controllers
{
    [Route("api/[controller]")]
    public class PonyController : Controller
    {
        private IPonyService ponyService;

        public PonyController(IPonyService ponyService)
        {
            this.ponyService = ponyService;
        }

        [HttpGet]
        public List<string> Get()
        {
            return ponyService.GetPonies();
        }
    }
}