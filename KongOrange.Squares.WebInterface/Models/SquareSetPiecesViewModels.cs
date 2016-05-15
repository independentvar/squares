using System.ComponentModel.DataAnnotations;
using System.Web;

namespace KongOrange.Squares.WebInterface.Models
{
    public class CreateSquareSetPieceViewModel
    {
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Name is either too short or too long")]
        public string Name { get; set; }

        [Required]
        public HttpPostedFileBase Image { get; set; }

        [Required]
        public int SquareSetId { get; set; }
    }
}