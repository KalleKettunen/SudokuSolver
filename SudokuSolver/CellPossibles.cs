using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    public class CellPossibles
    {
        public Cell Cell { get; init; }
        public HashSet<int> Values { get; init; }

        public bool Any() => Values.Any();

    }
}