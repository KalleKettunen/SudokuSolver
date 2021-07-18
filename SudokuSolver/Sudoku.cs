using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SudokuSolver
{
    public class Sudoku
    {
        public IReadOnlyCollection<Cell> Cells { get; init; }
        public IReadOnlyCollection<Row> Rows { get; }
        public IReadOnlyCollection<Row> Columns { get; }
        public IReadOnlyCollection<Row> Boxes { get; }
        public int Width { get; init; }
        public int Height { get; init; }

        public IReadOnlyCollection<Row> Areas => Rows.Concat(Columns).Concat(Boxes).ToImmutableList();
        public Sudoku(IReadOnlyCollection<Cell> cells, int width, int height)
        {
            Cells = cells;
            Width = width;
            Height = height;

            Rows = cells
                .Select((cell, index) => (cell, index))
                .GroupBy(i => i.index / Width)
                .Select(g => new Row {Cells = new HashSet<Cell>(g.Select(i => i.cell), new CellComparer())})
                .ToList();

            foreach (var row in Rows)
            {
                foreach (var cell in row.Cells)
                {
                    cell.Row = row;
                }
            }
            Columns = cells
                .Select((cell, index) => (cell, index))
                .GroupBy(i => i.index % Width)
                .Select(g => new Row {Cells = new HashSet<Cell>(g.Select(i => i.cell), new CellComparer())})
                .ToList();
            
            foreach (var column in Columns)
            {
                foreach (var cell in column.Cells)
                {
                    cell.Column = column;
                }
            }
            
            Boxes = cells
                .Select((cell, index) => (cell, index))
                .GroupBy(i => $"{(i.index)%9/3}{i.index/9/3}")
                .Select(g => new Row {Cells = new HashSet<Cell>(g.Select(i => i.cell), new CellComparer())})
                .ToList();
            
            foreach (var box in Boxes)
            {
                foreach (var cell in box.Cells)
                {
                    cell.Box = box;
                }
            }
        }

        public void Print()
        {
            foreach (var row in Rows)
            {
                foreach (var cell in row.Cells)
                {
                    Console.Write(cell);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public bool Ready()
        {
            return Cells.All(c => c.Value != 0);
        }
    }
}