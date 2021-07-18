using System.Collections.Generic;

namespace SudokuSolver
{
    public class SectionConstraint
    {
        public ICollection<Cell> Cells { get; } = new HashSet<Cell>(new ReferenceComparer<Cell>());
        public int Value { get; init; }
        public Row Box { get; init; }
    }
}