using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_Day1
{
	public static class AoC_D1
	{
		public static List<Elf> Elves { get; set; } = new List<Elf>();

		public static Elf MostCalories { get; private set; }
		public static int Top3Calories { get; private set; }

		public static void ReadListAndCalculate()
		{
			using StreamReader streamReader = new StreamReader("AoC_1_input.txt");

			Elf current = new Elf();
			Elves.Add(current);

			do
			{
				string line = streamReader.ReadLine();
				if (int.TryParse(line, out int caloriesPack))
					current.AddPAckage(caloriesPack);
				else
				{
					current = new Elf();
					Elves.Add(current);
				}
			} while (!streamReader.EndOfStream);

			MostCalories = Elves.MaxBy(x => x.TotalCalories);
			List<Elf> temp = Elves.OrderByDescending(x => x.TotalCalories).ToList();
			Top3Calories = temp[0].TotalCalories + temp[1].TotalCalories + temp[2].TotalCalories;
		}
	}

	public class Elf
	{
		public List<int> CaloriesPackages { get; set; } = new List<int>();
		public int TotalCalories { get; private set; }


		public void AddPAckage(int packacke)
		{
			TotalCalories += packacke;
			CaloriesPackages.Add(packacke);
		}
	}
}
