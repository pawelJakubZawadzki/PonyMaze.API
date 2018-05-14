using System;
using System.Collections.Generic;
using System.Linq;
using PonyMaze.Services.Enums;

namespace PonyMaze.Services
{
    public class PonyService: IPonyService
    {
        public List<string> GetPonies()
        {
            var poniesKeys = Enum.GetNames(typeof(PonyEnum)).Cast<string>();

            return poniesKeys.Select(key => Resources.PonyService.ResourceManager.GetString(key)).ToList();
        }
    }
}
