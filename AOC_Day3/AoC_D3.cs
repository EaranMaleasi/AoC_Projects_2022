namespace AOC_Day3
{
	public static class AoC_D3
	{
		public static int PriorityScorePt1 { get; set; } = 0;
		public static int PriorityScorePt2 { get; set; } = 0;

		public static void ReadInputAndCalculateContents()
		{
			using StreamReader streamReader = new StreamReader("AoC_3_input.txt");
			Backpack[] packs = new Backpack[3];
			int i = 0;
			do
			{
				string contents = streamReader.ReadLine();
				if (string.IsNullOrWhiteSpace(contents))
				{
					continue;
				}
				Backpack current = Backpack.CreateBackpack(contents);
				PriorityScorePt1 += (int)current.SharedContent;
				packs[i] = current;

				if (i == 2)
				{
					char sharedContent = packs[0].AllContents.Intersect(packs[1].AllContents).Intersect(packs[2].AllContents).ToList()[0];
					Enum.TryParse(sharedContent.ToString(), out LetterValues SharedValue);
					PriorityScorePt2 += (int)SharedValue;
					i = 0;
					continue;
				}
				i++;
			} while (!streamReader.EndOfStream);
		}
	}

	class Backpack
	{
		public string AllContents { get; set; }
		public string LeftCompartment { get; set; }
		public string RightCompartment { get; set; }

		public LetterValues SharedContent { get; set; }

		public static Backpack CreateBackpack(string contents)
		{
			Backpack backpack = new() { AllContents = contents };
			int halfContents = contents.Length / 2;

			backpack.LeftCompartment = contents.Substring(0, halfContents);
			backpack.RightCompartment = contents.Substring(halfContents, halfContents);

			char both = backpack.LeftCompartment.Intersect(backpack.RightCompartment).ToList()[0];
			Enum.TryParse(both.ToString(), out LetterValues SharedValue);
			backpack.SharedContent = SharedValue;
			return backpack;
		}
	}

	enum LetterValues
	{
		a = 1, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z,
		A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
	}
}