using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace SudokuSolver
{
    public class Possibilities
    {
        public ICollection<CellPossibles> Values { get; init; }
    }

    public static class PossibilitiesExtensions
    {
        public static IDictionary<int, int> RowCounts(this Possibilities p)
        {
            var d = new Dictionary<int, int>();

            return p.Values.SelectMany(n => n.Values).GroupBy(n => n).ToDictionary(n => n.Key, n => n.Count());
        }

        public static void Negate(this Possibilities p)
        {
            for (int i = 1; i < 10; ++i)
            {
                SectionConstraint sectionConstraint = null;
                foreach (var possibility in p.Values)
                {
                    if (!possibility.Values.Contains(i)) continue;
                    
                    if (sectionConstraint == null)
                    {
                        sectionConstraint = new SectionConstraint {Value = i, Box = possibility.Cell.Box};
                    }
                    else if (sectionConstraint.Box != possibility.Cell.Box)
                    {
                        sectionConstraint = null;
                        break;
                    }
                    
                    sectionConstraint.Cells.Add(possibility.Cell);
                }

                if (sectionConstraint != null)
                {
                    foreach (var cell in sectionConstraint.Box.Cells)
                    {
                        if (!sectionConstraint.Cells.Contains(cell))
                        {
                            cell.Not.Add(sectionConstraint.Value);
                        }
                    }
                }
            }
        }
    }
}