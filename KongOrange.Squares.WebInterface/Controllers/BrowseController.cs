using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using KongOrange.Squares.DataAccess;

namespace KongOrange.Squares.WebInterface.Controllers
{
    public class BrowseController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Browse
        public async Task<ActionResult> Index()
        {
            var squareSets = _db.SquareSets.Include(o => o.Pieces);
            return View(await squareSets.ToListAsync());
        }
    }
}