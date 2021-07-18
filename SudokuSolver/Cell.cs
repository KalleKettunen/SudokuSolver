using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SudokuSolver
{
    [DebuggerDisplay("{ToString()}")]
    public class Cell : IEquatable<Cell>
    {
        public int Value { get; set; }
        public Row Row{ get; set; }
        public Row Column { get; set; }
        public Row Box { get; set; }

        public HashSet<int> Not { get; } = new HashSet<int>();

        public static implicit operator Cell(int value)
        {
            return new Cell {Value = value};
        }

        public bool Equals(Cell other)
        {
            if (Value == 0 || other.Value == 0) return false;
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (Value == 0) return false;
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Cell) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value != 0 ? Value.ToString() : " ";
        }
    }

    public static class CellExtensions
    {
        public static HashSet<int> PossibleValues(this Cell cell)
        {
            if (cell.Value != 0)
                return new HashSet<int> {};
            
            var area = cell.Box.Values.Union(cell.Column.Values).Union(cell.Row.Values);

            return new HashSet<int> {1, 2, 3, 4, 5, 6, 7, 8, 9}.Except(area).Except(cell.Not).ToHashSet();
        }
    }
}