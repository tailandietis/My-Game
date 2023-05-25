using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Dungeon;

namespace MyGame
{
    public static class WayFinder
    {
        public static Direction FindDirection(Map gameMap, Point monsterLocation, Point playerLocation)
        {
            var path = FindPaths(gameMap, monsterLocation, playerLocation).FirstOrDefault()?.ToArray();
            if (path == null || path.Length < 2)
                return Direction.None;
            var newPoint = path[^2];
            var offset = new Size(newPoint.X - monsterLocation.X,
                newPoint.Y - monsterLocation.Y);
            return offset.IsEmpty ? Direction.None : Walker.ConvertOffsetToDirection(offset);
        }

        private static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point player)
        {
            var visitedPoints = new HashSet<Point> {start};
            var paths = new Queue<SinglyLinkedList<Point>>();
            paths.Enqueue(new SinglyLinkedList<Point>(start));
            while (paths.Count != 0)
            {
                var path = paths.Dequeue();
                var currentPosition = path.Value;
                if (currentPosition == player)
                    yield return path;
                var unvisitedNeighbors = GetUnvisitedNeighbors(map, currentPosition, visitedPoints);
                foreach (var point in unvisitedNeighbors)
                {
                    visitedPoints.Add(point);
                    paths.Enqueue(new SinglyLinkedList<Point>(point, path));
                }
            }
        }

        private static Point[] GetUnvisitedNeighbors(Map map, Point currentPosition, HashSet<Point> visitedPoints)
        {
            return Walker
                .PossibleDirections
                .Select(offset
                    => currentPosition + offset)
                .Where(coordinates => IsRightNeighbor(map, visitedPoints, coordinates))
                .ToArray();
        }

        private static bool IsRightNeighbor(Map map, HashSet<Point> visited, Point currentPosition)
        {
            return !visited.Contains(currentPosition) && map.InBounds(currentPosition)
                                                      && (map.Cells[currentPosition.X, currentPosition.Y] ==
                                                      Cell.Empty);
        }
        
    }
}