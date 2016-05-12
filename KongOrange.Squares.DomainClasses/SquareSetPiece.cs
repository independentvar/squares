using System.ComponentModel.DataAnnotations;

namespace KongOrange.Squares.DomainClasses
{
    public class SquareSetPiece
    {
        public int Id { get; set; }

        [StringLength(255, MinimumLength = 3, ErrorMessage = "Name is either too short or too long")]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int SquareSetId { get; set; }
        public virtual SquareSet SquareSet { get; set; }
    }
}