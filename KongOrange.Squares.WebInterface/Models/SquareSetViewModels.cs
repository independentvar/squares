using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KongOrange.Squares.DomainClasses;

namespace KongOrange.Squares.WebInterface.Models
{
    public class CreateSquareSetViewModel
    {
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Name is either too short or too long")]
        public string Name { get; set; }
    }

    public class EditSquareSetViewModel
    {
        [Required]
        public int Id { get; set; }

        [StringLength(255, MinimumLength = 3, ErrorMessage = "Name is either too short or too long")]
        public string Name { get; set; }
    }

    public class SquareSetViewModel
    {
        [Required]
        public int Id { get; set; }

        [StringLength(255, MinimumLength = 3, ErrorMessage = "Name is either too short or too long")]
        public string Name { get; set; }

        public ICollection<SquareSetPiece> SquareSetPieces { get; set; }
    }
}