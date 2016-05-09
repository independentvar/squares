using System.Collections.Generic;

namespace KongOrange.Squares.DomainClasses
{
    public class SquareSet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SquareSetPiece> Pieces { get; set; }
    }
}
