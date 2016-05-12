using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KongOrange.Squares.DomainClasses;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KongOrange.Squares.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<SquareSet> SquareSets { get; set; }
        public DbSet<SquareSetPiece> SquareSetPieces { get; set; }
    }
}
