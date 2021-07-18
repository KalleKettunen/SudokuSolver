using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            // var sudoko = new Sudoku(new Cell[]
            // {
            //     5,3, 0, 0, 7, 0, 0, 0, 0,
            //     6, 0, 0, 1,9,5, 0, 0, 0,
            //     0, 9, 8, 0, 0, 0, 0,6, 0,
            //     8, 0, 0, 0,6, 0, 0, 0, 3,
            //     4, 0, 0, 8, 0, 3, 0, 0, 1,
            //     7, 0, 0, 0, 2, 0, 0, 0, 6,
            //     0, 6, 0, 0, 0, 0, 2, 8, 0,
            //     0, 0, 0, 4, 1, 9, 0, 0, 5,
            //     0, 0, 0, 0,8, 0, 0, 7, 9
            // }, 9, 9);
            
            var sudoku = new Sudoku(new Cell[]
            {
                0, 0, 7, 8, 0, 0, 0, 4, 0,
                2, 0, 0, 0, 0, 7, 0, 5, 0,
                0, 8, 1, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0,
                7, 0, 0, 0, 9, 0, 0, 0, 4,
                3, 0, 0, 0, 0, 2, 0, 6, 0,
                0, 0, 0, 0, 0, 8, 0, 0, 1,
                4, 0, 0, 0, 0, 6, 0, 0, 3,
                1, 3, 0, 2, 0, 0, 0, 0, 9
            }, 9, 9);
            sudoku.Print();
            while (!sudoku.Ready())
            {
                var p = sudoku.Cells.Select(c => new CellPossibles {Cell = c, Values = c.PossibleValues()})
                    .Where(p => p.Any()).ToList();
                
                Console.WriteLine();
                var p1 = p.Where(c => c.Values.Count == 1).ToList();
                if (p1.Any())
                    foreach (var possibles in p.Where(c => c.Values.Count == 1))
                    {
                        possibles.Cell.Value = possibles.Values.First();
                        sudoku.Print();
                    }
                else
                {
                    foreach (var sudokuRow in sudoku.Areas)
                    {
                        sudokuRow.ToPossibilities().Negate();
                        var possibilities = sudokuRow.ToPossibilities();
                        var counts = possibilities.RowCounts();
                        
                        foreach (var value in counts.Keys.Where(k => counts[k] == 1))
                        {
                            var cell = sudokuRow.ToPossibilities().Values.First(c => c.Values.Contains(value));
                            cell.Cell.Value = value;
                            sudoku.Print();
                        }
                        
                    }
                }
                //Console.ReadKey();
            }
        }
    }
}