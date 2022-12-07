using static System.IO.File;

namespace Day7
{
    public abstract class Item
    {
        public Folder? Parent { get; set; }

        public string Name { get; set; }

        public abstract int Size { get; }

        protected Item(string name) => Name = name;
    }

    public class Folder : Item
    {
        public List<Item> Contents { get; set; } = new();

        public override int Size => TotalSize();

        public Folder(string name) : base(name) { }

        public int TotalSize()
        {
            return Contents.Sum(item => item.Size);
        }

        public List<Folder> GetAllFolders()
        {
            var folders = new List<Folder>();

            foreach (var content in Contents.OfType<Folder>())
            {
                folders.AddRange(content.GetAllFolders());
            }

            folders.AddRange(Contents.OfType<Folder>());

            return folders;
        }
    }

    public class File : Item
    {
        public override int Size { get; }

        public File(string name, int size) : base(name) => Size = size;
    }

    internal class Program
    {
        private static void Main()
        {
            var input = ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            var commands = input.Split("\r\n");

            Part1(commands);
            Part2(commands);
        }

        private static void Part1(IEnumerable<string> terminalOutput)
        {
            var root = BuildFileSystem(terminalOutput);

            var allFolders = new List<Folder> { root };
            allFolders.AddRange(root.GetAllFolders());

            var total = allFolders.Where(folder => folder.Size <= 100000).Sum(folder => folder.Size);

            Console.WriteLine(total);
        }

        private static void Part2(IEnumerable<string> terminalOutput)
        {
            var root = BuildFileSystem(terminalOutput);

            var allFolders = new List<Folder> { root };
            allFolders.AddRange(root.GetAllFolders());

            const int fileSystemSpace = 70000000;
            const int updateSpace = 30000000;

            var spaceNeeded = updateSpace - (fileSystemSpace - root.Size);

            var total = allFolders.Where(folder => folder.Size >= spaceNeeded).Min(folder => folder.Size);

            Console.WriteLine(total);
        }

        private static Folder BuildFileSystem(IEnumerable<string> terminalOutput)
        {
            var currentFolder = new Folder("/");
            var root = currentFolder;

            foreach (var line in terminalOutput.Skip(1))
            {
                var words = line.Split(" ");

                if (line.StartsWith('$'))
                {
                    var command = words[1];

                    if (command == "cd")
                    {
                        var value = words[2];

                        if (value == "..")
                        {
                            currentFolder = currentFolder?.Parent;
                        }
                        else
                        {
                            currentFolder = currentFolder?.Contents
                                .OfType<Folder>()
                                .Single(c => c.Name == value);
                        }
                    }
                }
                else
                {
                    if (words[0] == "dir")
                    {
                        var name = words[1];
                        var folder = new Folder(name)
                        {
                            Parent = currentFolder
                        };

                        currentFolder?.Contents.Add(folder);
                    }
                    else
                    {
                        var size = words[0];
                        var name = words[1];

                        var file = new File(name, int.Parse(size));

                        currentFolder?.Contents.Add(file);
                    }
                }
            }

            return root;
        }
    }
}