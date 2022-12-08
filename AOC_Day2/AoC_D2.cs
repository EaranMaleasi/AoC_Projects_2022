using System.Net.Http.Headers;

namespace AOC_Day2
{
	public static class AoC_D2
	{
		public static int StrategyGuideScorePt1 { get; private set; }
		public static int StrategyGuideScorePt2 { get; private set; }

		public static void ReadDataAndCalculateOutcome()
		{
			using StreamReader sr = new("AoC_2_input.txt");
			do
			{
				string line = sr.ReadLine();
				string[] strats = line.Split(' ');

				Game current = CalculateGameFromLinePt1(strats);
				StrategyGuideScorePt1 += (int)current.Me + (int)current.Outcome;

				current = CalculateGameFromLinePt2(strats);
				StrategyGuideScorePt2 += (int)current.Me + (int)current.Outcome;

			} while (!sr.EndOfStream);
		}


		private static Game CalculateGameFromLinePt1(string[] line)
		{
			Game current = new()
			{
				Oppopnent = line[0] switch
				{
					"A" => Handsign.Rock,
					"B" => Handsign.Paper,
					"C" => Handsign.Scissors,
				},
				Me = line[1] switch
				{
					"X" => Handsign.Rock,
					"Y" => Handsign.Paper,
					"Z" => Handsign.Scissors,
				}
			};

			current.Outcome = current.Oppopnent switch
			{
				Handsign.Rock => current.Me switch
				{
					Handsign.Rock => Result.Draw,
					Handsign.Paper => Result.Win,
					Handsign.Scissors => Result.Loss
				},
				Handsign.Paper => current.Me switch
				{
					Handsign.Rock => Result.Loss,
					Handsign.Paper => Result.Draw,
					Handsign.Scissors => Result.Win
				},
				Handsign.Scissors => current.Me switch
				{
					Handsign.Rock => Result.Win,
					Handsign.Paper => Result.Loss,
					Handsign.Scissors => Result.Draw
				}
			};
			return current;
		}

		private static Game CalculateGameFromLinePt2(string[] line)
		{

			Game current = new()
			{
				Oppopnent = line[0] switch
				{
					"A" => Handsign.Rock,
					"B" => Handsign.Paper,
					"C" => Handsign.Scissors,
				},
				Outcome = line[1] switch
				{
					"X" => Result.Loss,
					"Y" => Result.Draw,
					"Z" => Result.Win,
				}
			};

			current.Me = current.Oppopnent switch
			{
				Handsign.Rock => current.Outcome switch
				{
					Result.Draw => Handsign.Rock,
					Result.Win => Handsign.Paper,
					Result.Loss => Handsign.Scissors
				},
				Handsign.Paper => current.Outcome switch
				{
					Result.Draw => Handsign.Paper,
					Result.Win => Handsign.Scissors,
					Result.Loss => Handsign.Rock
				},
				Handsign.Scissors => current.Outcome switch
				{
					Result.Draw => Handsign.Scissors,
					Result.Win => Handsign.Rock,
					Result.Loss => Handsign.Paper
				}
			};
			return current;
		}
	}

	public class Game
	{
		public Handsign Oppopnent { get; set; }
		public Handsign Me { get; set; }
		public Result Outcome { get; set; }
	}

	public enum Handsign
	{
		Rock = 1,
		Paper,
		Scissors
	}

	public enum Result
	{
		Loss = 0,
		Draw = 3,
		Win = 6
	}
}