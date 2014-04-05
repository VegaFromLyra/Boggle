using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Given a n x n board of characters, 
// find all possible valid words that can be generated 
// by moving left, right, top or down.
// The same letter cannot be used twice in one go
// Note: This is similar to Wordament
namespace Boggle
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] board = {
                              {'w', 'a', 's'},
                              {'d', 'r', 'o'},
                              {'e', 'e', 'n'}
                            };

            Dictionary.Add("war");
            Dictionary.Add("ward");
            Dictionary.Add("draw");
            Dictionary.Add("raw");
            Dictionary.Add("two");
            Dictionary.Add("one");
            Dictionary.Add("are");
            Dictionary.Add("son");
            Dictionary.Add("sore");
            Dictionary.Add("reed");
            Dictionary.Add("saw");
            Dictionary.Add("deer");
            Dictionary.Add("need");
            Dictionary.Add("drone");

            List<string> words = BoggleSolver(board);

            Console.WriteLine("Valid words are:");

            foreach(string str in words)
            {
                Console.WriteLine(str);
            }
        }

        static List<string> BoggleSolver(char[,] board)
        {
            List<string> result = new List<string>();

            bool[,] visited = new bool[board.GetLength(0), board.GetLength(1)];

            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    visited[row, column] = true;

                    List<string> subset = GetValidWords(board,
                                                  new Point(row, column),
                                                  visited,
                                                  board[row, column].ToString());

                    visited[row, column] = false;

                    if (subset.Count > 0)
                    {
                        result.AddRange(subset);
                    }
                }
            }

            return result;
        }

        static List<string> GetValidWords(char[,] board, Point currentPoint, bool[,] visited, string word)
        {
            List<string> result = new List<string>();

            List<Point> points = GetUnVisitedNeighbors(board, currentPoint, visited);

            foreach (Point pt in points)
            {
                visited[pt.Row, pt.Column] = true;
                word += board[pt.Row, pt.Column];

                if (IsValidWord(word))
                {
                    result.Add(word);
                }

                List<string> otherWords = GetValidWords(board, pt, visited, word);

                if (otherWords.Count > 0)
                {
                    result.AddRange(otherWords);
                }

                visited[pt.Row, pt.Column] = false;
                word = word.Remove(word.Length - 1);
            }

            return result;
        }

        static HashSet<string> Dictionary = new HashSet<string>();

        static bool IsValidWord(string input)
        {
            return Dictionary.Contains(input);
        }

        static List<Point> GetUnVisitedNeighbors(char[,] board, Point currentPoint, bool[,] visited)
        {
            List<Point> result = new List<Point>();

            // Get top neighbor
            if ((currentPoint.Row - 1 >= 0) && !visited[currentPoint.Row - 1, currentPoint.Column])
            {
                Point topPoint = new Point(currentPoint.Row - 1, currentPoint.Column);
                result.Add(topPoint);
            }

            // Get bottom neighbor
            if ((currentPoint.Row + 1 < board.GetLength(0)) && !visited[currentPoint.Row + 1, currentPoint.Column])
            {
                Point bottomPoint = new Point(currentPoint.Row + 1, currentPoint.Column);
                result.Add(bottomPoint);
            }

            // get left neighbor
            if ((currentPoint.Column - 1 >= 0) && !visited[currentPoint.Row, currentPoint.Column -1])
            {
                Point leftPoint = new Point(currentPoint.Row, currentPoint.Column - 1);
                result.Add(leftPoint);
            }

            // get right neighbor
            if ((currentPoint.Column + 1 < board.GetLength(1)) && !visited[currentPoint.Row, currentPoint.Column + 1])
            {
                Point rightPoint = new Point(currentPoint.Row, currentPoint.Column + 1);
                result.Add(rightPoint);
            }

            return result;
        }
    }

    class Point
    {
        public Point(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}
