namespace AoC_Day4
{
	public static class AoC_D4
	{
		public static int FullyContainedAssignments { get; set; } = 0;
		public static int OverlappingAssignments { get; set; } = 0;

		public static void ReadInputAndCalculate()
		{
			using StreamReader streamReader = new StreamReader("AoC_4_input.txt");
			do
			{
				string line = streamReader.ReadLine();
				if (string.IsNullOrWhiteSpace(line))
					continue;

				CleaningSections sections = new CleaningSections(line);
				int[] remaindersFirst = sections.FirstSections.Except(sections.SecondSections).ToArray();
				int[] remainderssecond = sections.SecondSections.Except(sections.FirstSections).ToArray();

				if (remaindersFirst.Length == 0 || remainderssecond.Length == 0)
					FullyContainedAssignments++;

				bool overlap = sections.FirstSections.Intersect(sections.SecondSections).Any();
				if (overlap)
					OverlappingAssignments++;

			} while (!streamReader.EndOfStream);
		}
	}

	public class CleaningSections
	{
		public int[] FirstSections { get; set; }
		public int[] SecondSections { get; set; }

		public CleaningSections(string line)
		{
			string[] ranges = line.Split(',');

			CalculateRange(ranges[0], out int start, out int length);
			FirstSections = Enumerable.Range(start, length).ToArray();

			CalculateRange(ranges[1], out start, out length);
			SecondSections = Enumerable.Range(start, length).ToArray();
		}

		public void CalculateRange(string range, out int start, out int length)
		{
			string[] sections = range.Split('-');
			start = int.Parse(sections[0]);
			int end = int.Parse(sections[1]);
			length = (end - start) + 1;
		}
	}
}
