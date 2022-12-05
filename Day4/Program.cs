namespace Day4
{
    internal class Program
    {
        private static void Main()
        {
            var input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            var assignments = input.Split("\r\n");

            Part1(assignments);
            Part2(assignments);
        }

        public static void Part1(string[] assignments)
        {
            var total = 0;
            foreach(var assignment in assignments)
            {
                var elfPair = assignment.Split(",");

                var elf1 = elfPair.First().Split("-").Select(int.Parse).ToArray();
                var elf2 = elfPair.Last().Split("-").Select(int.Parse).ToArray();

                if (elf1.First() <= elf2.First() && elf1.Last() >= elf2.Last() || 
                    elf2.First() <= elf1.First() && elf2.Last() >= elf1.Last())
                {
                    total++;
                }
            }

            Console.WriteLine(total);
        }

        public static void Part2(string[] assignments)
        {
            var total = 0;
            foreach (var assignment in assignments)
            {
                var elfPair = assignment.Split(",");

                var elf1 = elfPair.First().Split("-").Select(int.Parse).ToArray();
                var elf2 = elfPair.Last().Split("-").Select(int.Parse).ToArray();

                var elf1Set = Enumerable.Range(
                    elf1.First(), 
                    elf1.Last() - elf1.First() + 1).ToHashSet();

                var elf2Set = Enumerable.Range(
                    elf2.First(), 
                    elf2.Last() - elf2.First() + 1).ToHashSet();

                var overlap = elf1Set.Intersect(elf2Set);

                if (overlap.Any())
                {
                    total += 1;
                }
            }

            Console.WriteLine(total);
        }
    }
}