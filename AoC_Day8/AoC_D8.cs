namespace AoC_Day8
{
	public static class AoC_D8
	{
		public static TreeGrid MainGrid { get; } = new TreeGrid();
		public static int VisibleTrees { get; private set; }
		public static int HighestScenicScore { get; private set; }

		public static void ReadInputAndCalculate()
		{
			using StreamReader streamReader = new StreamReader("AoC_8_input.txt");
			do
			{
				string line = streamReader.ReadLine();
				if (string.IsNullOrWhiteSpace(line))
					continue;
				MainGrid.AddRowFromLine(line);
			} while (!streamReader.EndOfStream);
			CalculateVisibleTrees();
			CalculateScenicScores();
			HighestScenicScore = MainGrid.AllTrees.Max(x => x.ScenicScore);
		}

		private static void CalculateVisibleTrees()
		{
			foreach (var rows in MainGrid.Rows)
				foreach (var tree in rows.Trees)
					if (tree.CalculateTreeVisibility())
						VisibleTrees++;
		}

		private static void CalculateScenicScores()
		{
			foreach (var rows in MainGrid.Rows)
				foreach (var tree in rows.Trees)
					tree.CalculateScore();
		}
	}

	public class Tree
	{
		public int Size { get; set; }
		public int Row { get; set; }
		public int Column { get; set; }
		public TreeGrid Grid { get; set; }

		public int ScenicScore { get; set; }

		public void CalculateScore()
		{
			if (Row == 0 || Row == 98 || Column == 0 || Column == 98)
				ScenicScore = 0;

			ScenicScore = CalculateScoreAbove() *
						  CalculateScoreBelow() *
						  CalculateScoreLeft() *
						  CalculateScoreRight();
		}

		public bool CalculateTreeVisibility()
		{
			if (Row == 0 || Row == 98 || Column == 0 || Column == 98)
				return true;

			return IsTreeVisibleFromAbove() ||
				   IsTreeVisibleFromBelow() ||
				   IsTreeVisibleFromLeft() ||
				   IsTreeVisibleFromRight();
		}

		private int CalculateScoreLeft()
		{
			for (int searchColumn = Column - 1; searchColumn >= 0; searchColumn--)
			{
				int searchSize = Grid[Row][searchColumn].Size;
				if (searchSize >= Size)
					return Column - searchColumn;
			}
			return Column;
		}

		private int CalculateScoreRight()
		{
			for (int searchColumn = Column + 1; searchColumn < 99; searchColumn++)
			{
				int searchSize = Grid[Row][searchColumn].Size;
				if (searchSize >= Size)
					return searchColumn - Column;
			}
			return 98 - Column;
		}

		private int CalculateScoreAbove()
		{
			for (int searchRow = Row - 1; searchRow >= 0; searchRow--)
			{
				int searchSize = Grid[searchRow][Column].Size;
				if (searchSize >= Size)
					return Row - searchRow;
			}
			return Row;
		}

		private int CalculateScoreBelow()
		{
			for (int searchRow = Row + 1; searchRow < 99; searchRow++)
			{
				int searchSize = Grid[searchRow][Column].Size;
				if (searchSize >= Size)
					return searchRow - Row;
			}
			return 98 - Row;
		}

		private bool IsTreeVisibleFromLeft()
		{
			for (int searchColumn = Column - 1; searchColumn >= 0; searchColumn--)
			{
				int searchSize = Grid[Row][searchColumn].Size;
				if (searchSize >= Size)
					return false;
			}
			return true;
		}
		private bool IsTreeVisibleFromRight()
		{
			for (int searchColumn = Column + 1; searchColumn < 99; searchColumn++)
			{
				int searchSize = Grid[Row][searchColumn].Size;
				if (searchSize >= Size)
					return false;
			}
			return true;
		}

		private bool IsTreeVisibleFromAbove()
		{
			for (int searchRow = Row - 1; searchRow >= 0; searchRow--)
			{
				int searchSize = Grid[searchRow][Column].Size;
				if (searchSize >= Size)
					return false;
			}
			return true;
		}

		private bool IsTreeVisibleFromBelow()
		{
			for (int searchRow = Row + 1; searchRow < 99; searchRow++)
			{
				int searchSize = Grid[searchRow][Column].Size;
				if (searchSize >= Size)
					return false;
			}
			return true;
		}
	}

	public class TreeGrid
	{
		private int rowNo = 0;
		public List<TreeRow> Rows { get; } = new List<TreeRow>();
		public List<Tree> AllTrees { get; } = new List<Tree>();
		public TreeRow this[int i] => Rows[i];

		public void AddRowFromLine(string line)
		{
			TreeRow current = new TreeRow(rowNo);
			for (int column = 0; column < line.Length; column++)
			{
				current.Trees.Add(new Tree
				{
					Column = column,
					Row = rowNo,
					Grid = this,
					Size = int.Parse(line[column].ToString())
				});
				AllTrees.Add(current.Trees.Last());
			}
			Rows.Add(current);
			rowNo++;
		}
	}

	public class TreeRow
	{
		public int RowNo { get; }
		public List<Tree> Trees { get; } = new List<Tree>();
		public Tree this[int i] => Trees[i];

		public TreeRow(int rowNo)
		{ RowNo = rowNo; }
	}
}