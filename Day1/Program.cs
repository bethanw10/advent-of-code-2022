namespace Day1
{

    internal class Program
    {
        static void Main()
        {
            var input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            var inventories = input.Split("\r\n\r\n");

            Part1(inventories);
            Part2(inventories);
        }

        public static void Part1(string[] inventories)
        {
            var highest = 0;

            foreach (var inventory in inventories)
            {
                var items = inventory.Split("\r\n").Select(int.Parse);

                var total = items.Sum();

                if (highest < total)
                {
                    highest = total;
                }
            }

            Console.WriteLine(highest);
        }

        public static void Part2(string[] inventories)
        {
            var totals = new List<int>();

            foreach (var inventory in inventories)
            {
                var items = inventory.Split("\r\n").Select(int.Parse);

                var total = items.Sum();

                totals.Add(total);
            }

            totals.Sort();

            var sumTopThree = totals.TakeLast(3).Sum();

            Console.WriteLine(sumTopThree);
        }
    }
}