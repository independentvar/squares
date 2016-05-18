using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace KongOrange.Squares.WebInterface.Models
{
    public class CreateSquareSetPieceViewModel
    {
        [Required]
        public IEnumerable<HttpPostedFileBase> Images { get; set; }

        [Required]
        public int SquareSetId { get; set; }
    }
}