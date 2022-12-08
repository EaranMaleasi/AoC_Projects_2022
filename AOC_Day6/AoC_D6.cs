namespace AOC_Day6
{
	public static class AoC_D6
	{
		public static int MarkerPt1 { get; set; }
		public static int MarkerPt2 { get; set; }

		public static void ReadInputAndCalculate()
		{
			using StreamReader streamReader = new StreamReader("AoC_6_input.txt");
			string input = streamReader.ReadToEnd();
			MarkerPt1 = CalculateMarker(input, 4);
			MarkerPt2 = CalculateMarker(input, 14);
		}

		private static int CalculateMarker(string input, int markerLength)
		{
			bool markerFound = false;

			for (int inputPosition = 0; inputPosition < input.Length - markerLength; inputPosition++)
			{
				char[] buffer = input.Substring(inputPosition, markerLength).ToCharArray();
				HashSet<char> found = new HashSet<char>();
				for (int bufferPosition = 0; bufferPosition < buffer.Length; bufferPosition++)
				{
					markerFound = found.Add(buffer[bufferPosition]);
					if (!markerFound)
					{
						break;
					}
				}
				if (markerFound)
				{
					return inputPosition + markerLength;
				}
			}
			return 0;
		}
	}
}