using PonyMaze.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace PonyMaze.Services.Helpers
{
    public class MazeSolverHelper
    {
        public bool ShouldSolveAgain { get; set; }
        public MazeDataResponseModel MazeData { get; set; }
        private Queue<string> RightMovements { get; set; }
        private List<MazePath> MazePaths { get; set; }

        public MazeSolverHelper(MazeDataResponseModel mazeData)
        {
            RightMovements = new Queue<string>();
            MazePaths = new List<MazePath>();
            MazeData = mazeData;
            ShouldSolveAgain = true;
        }

        public string GetMoveDirection()
        {
            if (ShouldSolveAgain)
            {
                SolveMaze();

                ShouldSolveAgain = false;
            }

            return RightMovements.Count > 1
                ? RightMovements.Dequeue()
                : RightMovements.First();
        }

        public void UpdatePonyLocation(string direction)
        {
            MazeData.PonyLocation[0] = GetLocationByDirection(MazeData.PonyLocation.First(), direction);
        }

        private void SolveMaze()
        {
            var possibleMovements = GetPossibleMovements(MazeData.PonyLocation[0]);

            possibleMovements.ForEach(movement =>
            {
                var movements = new Queue<string>();
                movements.Enqueue(movement);
                MazePaths.Add(new MazePath(GetLocationByDirection(MazeData.PonyLocation.First(), movement), movements));
            });

            var isMazeSolved = false;

            while (!isMazeSolved)
            {
                var newPaths = new List<MazePath>();

                MazePaths.ForEach(path =>
                {
                    possibleMovements = GetPossibleMovements(path.CurrentLocation)
                        .Where(movement => !movement.Equals(GetOppositeDirection(path.Movements.Last())))
                        .ToList();

                    if (possibleMovements.Any())
                    {
                        var remainingMovements = possibleMovements.Where(movement => !movement.Equals(possibleMovements.First()));

                        if (remainingMovements.Any())
                        {
                            newPaths.AddRange(remainingMovements.Select(movement => new MazePath(
                                GetLocationByDirection(path.CurrentLocation, movement),
                                new Queue<string>(path.Movements.Append(movement))
                            )));
                        }

                        path.Movements.Enqueue(possibleMovements.First());
                        path.CurrentLocation = GetLocationByDirection(path.CurrentLocation, possibleMovements.First());
                    }
                });

                MazePaths.AddRange(newPaths);

                isMazeSolved = MazePaths.Any(path => path.CurrentLocation.Equals(MazeData.EndPointLocation.First()));
            }

            RightMovements = MazePaths.First(path => path.CurrentLocation.Equals(MazeData.EndPointLocation.First())).Movements;
        }

        private List<string> GetPossibleMovements(int location)
        {
            var possibleMovements = MazeData.Data[location].Contains(Resources.MazeService.North)
                ? MazeData.Data[location].Contains(Resources.MazeService.West)
                    ? new List<string>()
                    : new List<string> { Resources.MazeService.West }
                : MazeData.Data[location].Contains(Resources.MazeService.West)
                    ? new List<string> { Resources.MazeService.North }
                    : new List<string> { Resources.MazeService.North, Resources.MazeService.West };

            if (location + 1 < MazeData.Size[0] * MazeData.Size[1] && !MazeData.Data[location + 1].Contains(Resources.MazeService.West))
            {
                possibleMovements.Add(Resources.MazeService.East);
            }

            if (location + MazeData.Size[0] < MazeData.Size[0] * MazeData.Size[1] && !MazeData.Data[location + MazeData.Size[0]].Contains(Resources.MazeService.North))
            {
                possibleMovements.Add(Resources.MazeService.South);
            }

            return possibleMovements;
        }

        private int GetLocationByDirection(int currentLocation, string direction)
        {
            var newLocation = currentLocation + MazeData.Size[0];

            if (direction.Equals(Resources.MazeService.North))
            {
                newLocation = currentLocation - MazeData.Size[0];
            }
            else if (direction.Equals(Resources.MazeService.West))
            {
                newLocation = currentLocation - 1;
            }
            else if (direction.Equals(Resources.MazeService.East))
            {
                newLocation = currentLocation + 1;
            }

            return newLocation;
        }

        private string GetOppositeDirection(string direction)
        {
            var oppositeDirection = Resources.MazeService.North;

            if (direction.Equals(Resources.MazeService.West))
            {
                oppositeDirection = Resources.MazeService.East;
            }
            else if (direction.Equals(Resources.MazeService.East))
            {
                oppositeDirection = Resources.MazeService.West;
            }
            else if (direction.Equals(Resources.MazeService.North))
            {
                oppositeDirection = Resources.MazeService.South;
            }

            return oppositeDirection;
        }
    }
}
