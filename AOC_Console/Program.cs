using System.Diagnostics;

using AOC_Day1;

using AOC_Day11;

using AOC_Day2;

using AOC_Day3;

using AoC_Day4;

using AOC_Day5;

using AOC_Day6;

using AOC_Day7;

using AoC_Day8;

namespace AoC_Projects
{
	internal class Program
	{
		static void Main(string[] args)
		{
			AoC_D1.ReadListAndCalculate();
			Console.WriteLine("Solution Day_1_P1:" + AoC_D1.MostCalories.TotalCalories);
			Console.WriteLine("Solution Day_1_P2:" + AoC_D1.Top3Calories);
			Console.WriteLine();


			AoC_D2.ReadDataAndCalculateOutcome();
			Console.WriteLine("Solution Day_2_P1:" + AoC_D2.StrategyGuideScorePt1);
			Console.WriteLine("Solution Day_2_P2:" + AoC_D2.StrategyGuideScorePt2);
			Console.WriteLine();

			AoC_D3.ReadInputAndCalculateContents();
			Console.WriteLine("Solution Day_3_P1:" + AoC_D3.PriorityScorePt1);
			Console.WriteLine("Solution Day_3_P2:" + AoC_D3.PriorityScorePt2);
			Console.WriteLine();

			AoC_D4.ReadInputAndCalculate();
			Console.WriteLine("Solution Day_4_P1:" + AoC_D4.FullyContainedAssignments);
			Console.WriteLine("Solution Day_4_P2:" + AoC_D4.OverlappingAssignments);
			Console.WriteLine();

			AoC_D5.ReadInputAndCalculate();
			Console.WriteLine("Solution Day_5_P1:" + AoC_D5.CargoStackMessagePt1);
			Console.WriteLine("Solution Day_5_P2:" + AoC_D5.CargoStackMessagePt2);
			Console.WriteLine();

			AoC_D6.ReadInputAndCalculate();
			Console.WriteLine("Solution Day_6_P1:" + AoC_D6.MarkerPt1);
			Console.WriteLine("Solution Day_6_P2:" + AoC_D6.MarkerPt2);
			Console.WriteLine();

			AoC_D7.ReadInputAndCalculate();
			Console.WriteLine("Solution Day_7_P1:" + AoC_D7.TotalFolderSizePt1);
			Console.WriteLine("Solution Day_7_P2:" + AoC_D7.TotalFolderSizePt2);
			Console.WriteLine();

			AoC_D8.ReadInputAndCalculate();
			Console.WriteLine("Solution Day_8_P1:" + AoC_D8.VisibleTrees);
			Console.WriteLine("Solution Day_8_P2:" + AoC_D8.HighestScenicScore);
			Console.WriteLine();

			AoC_D11.ReadInputAndCalculate();
			Console.WriteLine("Solution Day_11_P1:" + AoC_D11.MonkeyBusinessPt1);
			Console.WriteLine("Solution Day_11_P2:" + AoC_D11.MonkeyBusinessPt2);
			Console.ReadKey();
		}
	}
}