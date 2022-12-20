using System.Numerics;

namespace AOC_Day11
{
	public static class AoC_D11
	{
		public static decimal MonkeyBusinessPt1 { get; set; }
		public static decimal MonkeyBusinessPt2 { get; set; }

		public static void ReadInputAndCalculate()
		{
			List<Monkey> monkeysPT1 = ReadInput();
			List<Monkey> monkeysPT2 = ReadInput();
			for (int i = 0; i < 20; i++)
			{
				foreach (var monkey in monkeysPT1)
				{
					List<ThrowResult> results = monkey.InspectAllItemsInQueueueueueueueueueue(true);
					foreach (var result in results)
					{
						monkeysPT1[result.DestinationMonkey].Items.Enqueue(result.Item);
					}
				}
			}
			List<Monkey> businessMonkeys20 = monkeysPT1.OrderByDescending(x => x.InspectionCounter).ToList();
			MonkeyBusinessPt1 = businessMonkeys20[0].InspectionCounter * businessMonkeys20[1].InspectionCounter;


			for (int i = 0; i < 10000; i++)
			{
				foreach (var monkey in monkeysPT2)
				{
					List<ThrowResult> results = monkey.InspectAllItemsInQueueueueueueueueueue(false);
					foreach (var result in results)
					{
						monkeysPT2[result.DestinationMonkey].Items.Enqueue(result.Item);
					}
				}
			}
			List<Monkey> businessMonkeys10K = monkeysPT2.OrderByDescending(x => x.InspectionCounter).ToList();
			MonkeyBusinessPt2 = (businessMonkeys10K[0].InspectionCounter) * businessMonkeys10K[1].InspectionCounter;
		}

		private static List<Monkey> ReadInput()
		{
			List<Monkey> monkeys = new List<Monkey>();

			Monkey current = null;
			using StreamReader streamReader = new StreamReader("AoC_11_input.txt");
			do
			{
				string line = streamReader.ReadLine().Trim();
				string[] parts = line.Split(' ');
				if (string.IsNullOrWhiteSpace(line))
					continue;
				else if (parts[0] == "Monkey")
					monkeys.Add(current = new Monkey());
				else if (parts[0] == "Starting")
					for (int i = 2; i < parts.Length; i++)
						current.Items.Enqueue(new Throwable(int.Parse(parts[i].TrimEnd(','))));
				else if (parts[0] == "Operation:")
				{
					current.IncreaseOperation = parts[4] switch
					{
						"*" => WorryOperation.Multiply,
						"+" => WorryOperation.Add
					};
					if (parts[5] == "old")
					{
						current.WorryIncrease = -1;
						current.IncreaseOperation = WorryOperation.Old;
					}
					else
						current.WorryIncrease = int.Parse(parts[5]);
				}
				else if (parts[0] == "Test:")
					current.TestCondition = int.Parse(parts[3]);
				else if (parts[0] == "If")
				{
					bool throwTarget = bool.Parse(parts[1].TrimEnd(':'));
					if (throwTarget)
						current.ThrowLocationTrue = int.Parse(parts[5]);
					else
						current.ThrowLocationFalse = int.Parse(parts[5]);
				}
			} while (!streamReader.EndOfStream);
			return monkeys;
		}
	}

	class Monkey
	{
		public Queue<Throwable> Items { get; } = new Queue<Throwable>();

		public int WorryIncrease { get; set; }
		public WorryOperation IncreaseOperation { get; set; }
		public int TestCondition { get; set; }
		public int ThrowLocationTrue { get; set; }
		public int ThrowLocationFalse { get; set; }

		public decimal InspectionCounter { get; set; }

		private ThrowResult InspectAndThrowItemPt1(bool decreaseWorry)
		{
			if (Items.Count == 0)
				return null;

			InspectionCounter++;

			Throwable current = Items.Dequeue();
			current.IncreaseWorryPt1(IncreaseOperation, WorryIncrease);
			if (decreaseWorry)
				current.DecreaseWorryPt1();


			if (current.WorryLevel % TestCondition == 0)
				return new ThrowResult { DestinationMonkey = ThrowLocationTrue, Item = current };
			else
				return new ThrowResult { DestinationMonkey = ThrowLocationFalse, Item = current };
		}

		public List<ThrowResult> InspectAllItemsInQueueueueueueueueueue(bool decreaseWorry)
		{
			List<ThrowResult> result = new List<ThrowResult>();
			while (Items.Count > 0)
				result.Add(InspectAndThrowItemPt1(decreaseWorry));
			return result;
		}
	}

	class Throwable
	{
		public BigInteger WorryLevel { get; private set; }

		public Throwable(int initialLevel)
			=> WorryLevel = initialLevel;

		public void DecreaseWorryPt1()
			=> WorryLevel /= 3;

		public void IncreaseWorryPt1(WorryOperation operation, int level)
		{
			WorryLevel = operation switch
			{
				WorryOperation.Add => WorryLevel + level,
				WorryOperation.Multiply => WorryLevel * level,
				WorryOperation.Old => WorryLevel * WorryLevel
			};
			WorryLevel %= 9699690; //lowest common multiple for test dividents from input.
		}
	}

	class ThrowResult
	{
		public int DestinationMonkey { get; set; }
		public Throwable Item { get; set; }
	}

	enum WorryOperation
	{
		None,
		Add,
		Multiply,
		Old
	}
}