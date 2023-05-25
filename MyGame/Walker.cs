using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MyGame
{
	public class Walker
	{
		private static readonly Dictionary<Direction, Size> directionToOffset = new Dictionary<Direction, Size>
		{
			{Direction.Up, new Size(0, -1)},
			{Direction.Down, new Size(0, 1)},
			{Direction.Left, new Size(-1, 0)},
			{Direction.Right, new Size(1, 0)}
		};

		private static readonly Dictionary<Size, Direction> offsetToDirection = new Dictionary<Size, Direction>
		{
			{new Size(0, -1), Direction.Up},
			{new Size(0, 1), Direction.Down},
			{new Size(-1, 0), Direction.Left},
			{new Size(1, 0), Direction.Right}
		};

		public static readonly IReadOnlyList<Size> PossibleDirections = offsetToDirection.Keys.ToList();


		public Point Position { get; }
		public Point? PointOfCollision { get; }

		public Walker(Point position)
		{
			Position = position;
			PointOfCollision = null;
		}

		private Walker(Point position, Point pointOfCollision)
		{
			Position = position;
			PointOfCollision = pointOfCollision;
		}

		public static Direction ConvertOffsetToDirection(Size offset)
		{
			return offsetToDirection[offset];
		}
		
		public static int GetDistanceBetweenPoints(Point first, Point second)
		{
			return (int) Math.Round(Math.Sqrt((first.X - second.X) * (first.X - second.X) +
			                                  (first.Y - second.Y) * (first.Y - second.Y)));
		}
	}
}