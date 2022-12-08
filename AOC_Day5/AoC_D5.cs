namespace AOC_Day5
{
	public static class AoC_D5
	{
		public static string CargoStackMessagePt1 { get; private set; } = string.Empty;
		public static string CargoStackMessagePt2 { get; private set; } = string.Empty;

		private static Stack<string>[] CargoStacksPt1 = new Stack<string>[9];
		private static Stack<string>[] CargoStacksPt2 = new Stack<string>[9];

		static AoC_D5()
		{
			for (int i = 0; i < CargoStacksPt1.Length; i++)
			{
				CargoStacksPt1[i] = new Stack<string>();
				CargoStacksPt2[i] = new Stack<string>();
			}
		}

		public static void ReadInputAndCalculate()
		{
			using StreamReader streamReader = new StreamReader("AoC_5_input.txt");

			Stack<string> initStack = new Stack<string>();

			for (int row = 0; row < 8; row++)
			{
				string line = streamReader.ReadLine();
				initStack.Push(line);
			}

			//Setup Stack 1 and 2 identically.
			while (initStack.TryPop(out string initLine))
			{
				for (int i = 0; i < 9; i++)
				{
					CargoStacksPt1[i].Push(initLine[4 * i + 1].ToString());
					CargoStacksPt2[i].Push(initLine[4 * i + 1].ToString());
				}
			}

			//No need to check both cargostacks, they're identical anyway.
			for (int stack = 0; stack < CargoStacksPt1.Length; stack++)
			{
				while (string.IsNullOrWhiteSpace(CargoStacksPt1[stack].Peek()))
				{
					CargoStacksPt1[stack].Pop();
					CargoStacksPt2[stack].Pop();
				}
			}

			//Read over the next 2 lines that are irrelevant/empty.
			_ = streamReader.ReadLine();
			_ = streamReader.ReadLine();

			do
			{
				string line = streamReader.ReadLine();
				string[] lineParts = line.Split(' ');

				int amount = int.Parse(lineParts[1]);
				int from = int.Parse(lineParts[3]) - 1;
				int to = int.Parse(lineParts[5]) - 1;

				CalculateMovementsForPt1(amount, from, to);
				CalculateMovementsForPt2(amount, from, to);

			} while (!streamReader.EndOfStream);

			for (int stack = 0; stack < CargoStacksPt1.Length; stack++)
			{
				CargoStackMessagePt1 += CargoStacksPt1[stack].Peek();
				CargoStackMessagePt2 += CargoStacksPt2[stack].Peek();
			}
		}

		public static void CalculateMovementsForPt1(int amount, int from, int to)
		{
			//CrateMover 9000 moves one crate at a time.
			for (int toDo = 0; toDo < amount; toDo++)
			{
				string item = CargoStacksPt1[from].Pop();
				CargoStacksPt1[to].Push(item);
			}
		}
		public static void CalculateMovementsForPt2(int amount, int from, int to)
		{
			//Since CrateMover 9001 can lift multiple crates at once, the counterstack represents the grabbing device of the crane.
			Stack<string> counterStack = new Stack<string>();
			for (int toDo = 0; toDo < amount; toDo++)
			{
				string crate = CargoStacksPt2[from].Pop();
				counterStack.Push(crate);
			}
			while (counterStack.TryPop(out string crate))
			{
				CargoStacksPt2[to].Push(crate);
			}
		}
	}
}