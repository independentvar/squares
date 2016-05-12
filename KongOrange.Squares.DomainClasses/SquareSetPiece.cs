namespace KongOrange.Squares.DomainClasses
{
    public class SquareSetPiece
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public virtual SquareSet SquareSet { get; set; }
    }
}