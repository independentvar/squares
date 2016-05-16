using System.ComponentModel.DataAnnotations;

namespace KongOrange.Squares.DomainClasses
{
    public class SquareSetPiece
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int SquareSetId { get; set; }
        public virtual SquareSet SquareSet { get; set; }
    }
}