using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KongOrange.Squares.DomainClasses
{
    public class SquareSet
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Name is either too short or too long")]
        public string Name { get; set; }
        public virtual ICollection<SquareSetPiece> Pieces { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
