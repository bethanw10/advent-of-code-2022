namespace Day6
{
    internal class Program
    {
        private static void Main()
        {
            var input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));
            
            Part1(input);
            Part2(input);
        }

        private static void Part1(string input)
        {
            IndexOfNDistinctCharacters(input, 4);
        }

        private static void Part2(string input)
        {
            IndexOfNDistinctCharacters(input, 14);
        }

        private static void IndexOfNDistinctCharacters(string input, int numDistinct)
        {
            var packetBuffer = new Queue<char>();
            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];
                packetBuffer.Enqueue(c);

                if (packetBuffer.Count > numDistinct)
                {
                    packetBuffer.Dequeue();
                }

                if (packetBuffer.Distinct().Count() == numDistinct)
                {
                    Console.WriteLine(i + 1);
                    break;
                }
            }

            Console.WriteLine();
        }
    }
}