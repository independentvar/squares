using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using KongOrange.Squares.DomainClasses;

namespace KongOrange.Squares.WebInterface.Models
{
    public class CreateSquareSetViewModel
    {
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Name is either too short or too long")]
        public string Name { get; set; }
    }

    public interface ISquareSetViewModel
    {
        int Id { get; set; }
        string Name { get; set; }
        IEnumerable<HttpPostedFileBase> Images { get; set; }
    }

    public class EditSquareSetViewModel : ISquareSetViewModel
    {
        [Required]
        public int Id { get; set; }

        [StringLength(255, MinimumLength = 3, ErrorMessage = "Name is either too short or too long")]
        public string Name { get; set; }

        public IEnumerable<SquareSetPiece> CurrentPieces { get; set; }

        public IEnumerable<HttpPostedFileBase> Images { get; set; }
    }

    public class SquareSetViewModel : ISquareSetViewModel
    {
        [Required]
        public int Id { get; set; }

        [StringLength(255, MinimumLength = 3, ErrorMessage = "Name is either too short or too long")]
        public string Name { get; set; }

        public IEnumerable<HttpPostedFileBase> Images { get; set; }
    }
}