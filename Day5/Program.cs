using System.Text.RegularExpressions;

namespace Day5
{
 /*
    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2
*/
 internal class Program
    {
        private struct Instruction
        {
            public int MoveNum;
            public int From;
            public int To;
        }

        private static void Main()
        {
            var input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            var procedure = input.Split("\r\n\r\n");

            var stacks = ParseBoxes(procedure);
            var instructions = ParseInstructions(procedure);

            Part1(stacks, instructions);

            stacks = ParseBoxes(procedure);
            instructions = ParseInstructions(procedure);

            Part2(stacks, instructions);
        }

        private static void Part1(IReadOnlyList<Stack<char>> stacks, IEnumerable<Instruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                for (var i = 0; i < instruction.MoveNum; i++)
                {
                    var boxToMove = stacks[instruction.From - 1].Pop();

                    stacks[instruction.To - 1].Push(boxToMove);
                }
            }

            foreach (var stack in stacks)
            {
                Console.Write(stack.Peek());
            }

            Console.WriteLine();
        }

        private static void Part2(IReadOnlyList<Stack<char>> stacks, IEnumerable<Instruction> instructions)
        {
            foreach (var instruction in instructions)
            { 
                var moveStack = new Stack<char>();

                for (var i = 0; i < instruction.MoveNum; i++)
                {
                    var boxToMove = stacks[instruction.From - 1].Pop();

                    moveStack.Push(boxToMove);   
                }

                while (moveStack.TryPop(out var boxToMove))
                {
                    stacks[instruction.To - 1].Push(boxToMove);
                }
            }

            foreach (var stack in stacks)
            {
                Console.Write(stack.Peek());
            }
        }

        private static List<Instruction> ParseInstructions(IEnumerable<string> procedure)
        {
            var writtenInstructions = procedure.Last().Split("\r\n");

            const string pattern = @"move (?<move>\d+) from (?<from>\d+) to (?<to>\d+)";

            return writtenInstructions
                .Select(instruction =>  Regex.Matches(instruction, pattern).Single())
                .Select(matches => new Instruction()
                {
                    MoveNum = int.Parse(matches.Groups["move"].Value), 
                    From = int.Parse(matches.Groups["from"].Value), 
                    To = int.Parse(matches.Groups["to"].Value),
                }).ToList();
        }

        private static List<Stack<char>> ParseBoxes(IEnumerable<string> procedure)
        {
            var boxRows = procedure.First().Split("\r\n").SkipLast(1).ToArray();
            var numStacks = boxRows.First().Length / 4 + 1;

            var stacks = new List<Stack<char>>();

            for (var i = 0; i < numStacks; i++)
            {
                var stack = new Stack<char>();

                foreach (var boxRow in boxRows.Reverse())
                {
                    var box = boxRow[i * 4 + 1];

                    if (char.IsLetter(box))
                    {
                        stack.Push(box);
                    }
                }

                stacks.Add(stack);
            }

            return stacks;
        }
        
    }
}