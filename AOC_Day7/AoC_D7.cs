namespace AOC_Day7
{
	public static class AoC_D7
	{
		public static int TotalFolderSizePt1 { get; private set; }
		public static int TotalFolderSizePt2 { get; private set; }


		public static Folder Root { get; private set; } = new Folder("/", null);

		public static void ReadInputAndCalculate()
		{
			using StreamReader streamReader = new StreamReader("AoC_7_input.txt");
			Folder current = null;
			do
			{
				string line = streamReader.ReadLine();
				string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

				if (parts[0] == "$")
					if (parts[1] == "cd")
						current = parts[2] switch
						{
							"/" => Root,
							".." => current.Parent,
							_ => current = current.Folders[parts[2]],
						};
					else
						continue;
				else if (parts[0] == "dir")
					current.AddFolder(new Folder(parts[1], current));
				else
					current.AddFile(new File(parts[1], int.Parse(parts[0]), current));

			} while (!streamReader.EndOfStream);

			TotalFolderSizePt1 = Root.GetFoldersBelowSize(100000).Sum(x => x.TotalSize);

			CalculdatePt2();
		}

		private static void CalculdatePt2()
		{
			int requiredSpace = 30000000;
			int totalSpace = 70000000;
			int usedSpace = Root.TotalSize;
			int freeSpace = totalSpace - usedSpace;
			int neededSpace = requiredSpace - freeSpace;

			List<Folder> allFolders = Root.GetFoldersBelowSize(int.MaxValue).ToList();
			Folder deleteFolder = allFolders.OrderBy(x => x.TotalSize).Where(x => x.TotalSize >= neededSpace).First();
			TotalFolderSizePt2 = deleteFolder.TotalSize;
		}
	}

	public class Folder
	{
		public string Name { get; set; }
		public int TotalSize { get; private set; }
		public List<File> Files { get; } = new List<File>();
		public Dictionary<string, Folder> Folders { get; } = new Dictionary<string, Folder>();
		public Folder Parent { get; }

		private List<Action> SizeChangedSubscribers { get; } = new List<Action>();

		public Folder(string name, Folder parent)
		{
			Name = name;
			Parent = parent;
		}

		public void AddFile(File newFile)
		{
			Files.Add(newFile);
			TotalSize += newFile.Size;
			SizeChanged();
		}

		public void AddFolder(Folder newFolder)
		{
			Folders[newFolder.Name] = newFolder;
			TotalSize += newFolder.TotalSize;
			newFolder.SubscribeSizeChanged(RecalculateTotalSize);
			SizeChanged();
		}

		public void SubscribeSizeChanged(Action callback)
			=> SizeChangedSubscribers.Add(callback);

		protected void SizeChanged()
		{
			foreach (var item in SizeChangedSubscribers)
				item();
		}

		private void RecalculateTotalSize()
		{
			TotalSize = 0;
			foreach (var item in Files)
				TotalSize += item.Size;

			foreach (var item in Folders)
				TotalSize += item.Value.TotalSize;

			SizeChanged();
		}

		public List<Folder> GetFoldersBelowSize(int size)
		{
			List<Folder> result = new List<Folder>();
			foreach (var item in Folders)
			{
				if (item.Value.TotalSize <= size)
					result.Add(item.Value);

				result.AddRange(item.Value.GetFoldersBelowSize(size));
			}
			return result;
			//This was the code that made me go validate the result by hand going through the input,
			//printing out my file/folder tree into the console, checking all the folders and their sizes
			//with notepad and calculator open, until I realized that the objective was to get the
			//size of ALL folders, not just the topmost ones even if its just stacked empty folders
			//with only the bottom most one having a file in it.
			//
			//LESSON: Reading is important!
			//
			/*foreach (var item in Folders)
			*{
			*	if (item.Value.TotalSize <= size)
			*		result.Add(item.Value);
			*	else
			*		result.AddRange(item.Value.GetFoldersBelowSize(size));
			*}
			*return result;
			*/
		}

		/// <summary>
		/// Prints a nice file/folder structure into the console/Debug/whatever. <br />
		/// Why? because I was grasping at straws for why my code returned the wrong total size.<br />
		/// See comments in <see cref="GetFoldersBelowSize"/> for more info.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string result = string.Empty;
			if (Parent == null)
			{
				result += $"- {Name} (dir, TotalSize={TotalSize})\r\n";
			}
			else
			{
				Folder current = Parent;
				int offset = 0;
				do
				{
					offset++;
					current = current.Parent;
				} while (current != null);

				result += $"{"".PadLeft(offset * 2)}- {Name} (dir, TotalSize={TotalSize})\r\n";
			}

			foreach (var item in Files)
			{
				result += item.ToString();
			}
			foreach (var item in Folders)
			{
				result += item.Value.ToString();
			}

			return result;
		}
	}

	public class File
	{
		public string Name { get; set; }
		public int Size { get; set; }
		public Folder Parent { get; set; }

		public File(string name, int size, Folder parent)
		{
			Name = name;
			Size = size;
			Parent = parent;
		}

		public override string ToString()
		{
			Folder current = Parent;
			int offset = 0;
			do
			{
				offset++;
				current = current.Parent;
			} while (current != null);
			return $"{"".PadLeft(offset * 2)}- {Name} (file, size={Size})\r\n";
		}
	}
}