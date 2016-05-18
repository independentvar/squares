using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace KongOrange.Squares.WebInterface.Models
{
    public class CreateSquareSetPieceViewModel
    {
        [Required]
        public int SquareSetId { get; set; }

        [Required]
        public IEnumerable<HttpPostedFileBase> Images { get; set; }
    }

    public class EditSquareSetPieceViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int SquareSetId { get; set; }

        [Required]
        public HttpPostedFileBase UpdatedImage { get; set; }

        public string CurrentImageUrl { get; set; }
    }
}