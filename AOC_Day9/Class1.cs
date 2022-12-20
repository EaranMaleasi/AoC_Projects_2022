using System.Drawing;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace AOC_Day9
{
	public class AoC_D9
	{
		public int TailDistinctPositions { get; private set; }

		public static void ReadInputAndCalculate()
		{
			RopePosition currentHeadPosition = new() { Location = (0, 0), VisitCount = 0 };
			RopePosition oldHeadPosition;

			RopePosition currentTailPosition = new() { Location = (0, 0), VisitCount = 0 };
			RopePosition oldTailPosition;

			List<RopePosition> tailPositions = new();

			using StreamReader streamReader = new StreamReader("AoC_9_input.txt");
			do
			{
				string line = streamReader.ReadLine();
				string[] directions = line.Split(' ');



			} while (!streamReader.EndOfStream);
		}

		RopePosition CalculateNewTailPosition(RopePosition lastTail, RopePosition newHead, RopePosition oldHead)
		{
			if (lastTail.LocationEquals(newHead))
				return null;

			IntVector distance = lastTail.CalculateDistanceTo(newHead);
			if (Math.Abs(distance.X) < 2 && Math.Abs(distance.Y) < 2)
				return null;

			return oldHead;
		}
	}

	enum RopeDirections
	{
		Up,
		Down,
		Left,
		Right
	}

	class RopePosition
	{
		public IntVector Location { get; set; }

		public int VisitCount { get; set; }

		public bool LocationEquals(RopePosition other)
			=> other != null &&
			   Location.X == other.Location.X &&
			   Location.Y == other.Location.Y;

		public IntVector CalculateDistanceTo(RopePosition other)
			=> (Location.X - other.Location.X, Location.Y - other.Location.Y);
	}

	internal record struct RopeMovement(int Moves, RopeDirections Direction)
	{
		public static implicit operator (int moves, RopeDirections direction)(RopeMovement value)
		{
			return (value.Moves, value.Direction);
		}

		public static implicit operator RopeMovement((int Moves, RopeDirections Direction) value)
		{
			return new RopeMovement(value.Moves, value.Direction);
		}
	}
	internal record struct IntVector(int X, int Y)
	{
		public static implicit operator (int X, int Y)(IntVector value)
		{
			return (value.X, value.Y);
		}

		public static implicit operator IntVector((int X, int Y) value)
		{
			return new IntVector(value.X, value.Y);
		}
	}
}
