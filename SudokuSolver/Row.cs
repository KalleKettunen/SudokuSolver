using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    public class Row
    {
        public HashSet<Cell> Cells;
        public HashSet<int> Values => Cells.Where(c => c?.Value != 0).Select(c => c.Value).ToHashSet();
    }

    public static class RowExtensions
    {
        public static Possibilities ToPossibilities(this Row row)
        {
            return new Possibilities{Values = row.Cells.Select(v => new CellPossibles{Cell = v, Values = v.PossibleValues()}).ToList()};
        }
    }
}