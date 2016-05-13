using System.ComponentModel.DataAnnotations;

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
}