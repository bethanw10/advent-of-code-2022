namespace Day1
{
    internal class Program
    {
        static void Main()
        {
            var input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            var rucksacks = input.Split("\r\n");

            Part1(rucksacks);
            Part2(rucksacks);
        }

        public static void Part1(string[] rucksacks)
        {
            var sum = 0;
            foreach (var rucksack in rucksacks)
            {
                var compartment1 = rucksack.Take(rucksack.Length / 2).ToHashSet();
                var compartment2 = rucksack.Skip(rucksack.Length / 2).ToHashSet();

                var shared = compartment1.Intersect(compartment2).ToList().Single();

                var priority = GetPriority(shared);

                sum += priority;
            }

            Console.WriteLine(sum);
        }

        public static void Part2(string[] rucksacks)
        {
            var sum = 0;

            for (int i = 0; i <= rucksacks.Length - 3; i += 3)
            {
                var group = rucksacks.Skip(i).Take(3);

                var shared = group.First().ToHashSet();

                foreach (var rucksack in group)
                {
                    shared.IntersectWith(rucksack.ToHashSet());
                }

                sum += GetPriority(shared.ToList().Single());
            }

            Console.WriteLine(sum);
        }

        public static int GetPriority(char c)
        {
            var priority = c % 32;

            if (char.IsUpper(c))
            {
                return priority + 26;
            }

            return priority;
        }
    }
}