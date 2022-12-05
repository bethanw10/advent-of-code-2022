using static Day1.RockPaperScissors;

namespace Day1
{
    public static class RockPaperScissors
    {
        public enum Move { Rock, Paper, Scissors }

        public static readonly Dictionary<char, Move> CharToMove = new()
        {
            ['A'] = Move.Rock,
            ['B'] = Move.Paper,
            ['C'] = Move.Scissors,

            ['X'] = Move.Rock,
            ['Y'] = Move.Paper,
            ['Z'] = Move.Scissors,
        };

        public static readonly Dictionary<Move, int> Scores = new()
        {
            [Move.Rock] = 1, 
            [Move.Paper] = 2, 
            [Move.Scissors] = 3,
        };

        public static readonly Dictionary<Move, Move> MoveWinsAgainst = new()
        {
            [Move.Rock] = Move.Scissors,
            [Move.Paper] = Move.Rock,
            [Move.Scissors] = Move.Paper,
        };

        public static Dictionary<Move, Move> MoveLosesAgainst = 
            MoveWinsAgainst.ToDictionary((i) => i.Value, (i) => i.Key);

        public static bool Beats(this Move move, Move opponentMove)
        {
            return MoveWinsAgainst[move] == opponentMove;
        }
    }

    internal class Program
    {
        static void Main()
        {
            var input = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            var strategy = input.Split("\r\n");

            Part1(strategy);
            Part2(strategy);
        }

        public static void Part1(string[] strategy)
        {
            var playerScore = 0;

            foreach (var move in strategy)
            {
                var opponentMove = CharToMove[move[0]];
                var playerMove = CharToMove[move[2]];

                playerScore += Scores[playerMove];

                if (playerMove.Beats(opponentMove))
                {
                    playerScore += 6;
                }
                else if (opponentMove == playerMove)
                {
                    playerScore += 3;
                }
            }

            Console.WriteLine(playerScore);
        }

        public static void Part2(string[] strategy)
        {
            var playerScore = 0;

            foreach (var move in strategy)
            {
                var opponentMove = CharToMove[move[0]];

                var playerMove = Move.Rock;

                switch (move[2])
                {
                    case 'X':
                        playerMove = MoveWinsAgainst[opponentMove];

                        break;

                    case 'Y':
                        playerMove = opponentMove;
                        playerScore += 3;

                        break;

                    case 'Z':
                        playerMove = MoveLosesAgainst[opponentMove];
                        playerScore += 6;

                        break;
                }

                playerScore += Scores[playerMove];
            }

            Console.WriteLine(playerScore);
        }
    }
}