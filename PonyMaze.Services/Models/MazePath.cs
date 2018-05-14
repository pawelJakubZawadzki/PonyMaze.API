using System.Collections.Generic;

namespace PonyMaze.Services.Models
{
    public class MazePath
    {
        public Queue<string> Movements { get; set; }
        public int CurrentLocation { get; set; }

        public MazePath(int currentLocation)
        {
            CurrentLocation = currentLocation;
            Movements = new Queue<string>();
        }

        public MazePath(int currentLocation, Queue<string> movements)
        {
            CurrentLocation = currentLocation;
            Movements = movements;
        }
    }
}
