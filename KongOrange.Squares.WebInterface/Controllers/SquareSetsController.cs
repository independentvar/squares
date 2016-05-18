using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using KongOrange.Squares.Business;
using KongOrange.Squares.DataAccess;
using KongOrange.Squares.DomainClasses;
using KongOrange.Squares.WebInterface.Models;
using Microsoft.AspNet.Identity;

namespace KongOrange.Squares.WebInterface.Controllers
{
    [Authorize]
    public class SquareSetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SquareSets
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var squareSets = db.SquareSets.Where(o => o.UserId == userId).Include(o => o.Pieces);
            return View(await squareSets.ToListAsync());
        }

        // GET: SquareSets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SquareSet squareSet = await db.SquareSets.FindAsync(id);
            if (squareSet == null)
            {
                return HttpNotFound();
            }
            return View(squareSet);
        }

        // GET: SquareSets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SquareSets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SquareSetViewModel squareSetViewModel)
        {
            var squareSet = new SquareSet
            {
                Name = squareSetViewModel.Name,
                UserId = User.Identity.GetUserId()
            };

            if (ModelState.IsValid)
            {
                db.SquareSets.Add(squareSet);
                UploadImages(squareSetViewModel);

                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = squareSet.Id });
            }

            return View(squareSet);
        }

        private void UploadImages(ISquareSetViewModel squareSetViewModel)
        {
            if (squareSetViewModel.Images != null && squareSetViewModel.Images.Any())
            {
                var storage = new StorageFacade();
                foreach (var image in squareSetViewModel.Images)
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        var url = storage.Store(image.FileName, image.InputStream, User.Identity.GetUserId());
                        var squareSetPiece = new SquareSetPiece
                        {
                            SquareSetId = squareSetViewModel.Id,
                            ImageUrl = url
                        };

                        db.SquareSetPieces.Add(squareSetPiece);
                    }
                }
            }
        }

        // GET: SquareSets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SquareSet squareSet = await db.SquareSets.FindAsync(id);
            if (squareSet == null)
            {
                return HttpNotFound();
            }

            var squareSetViewModel = new EditSquareSetViewModel
            {
                Id = squareSet.Id,
                Name = squareSet.Name,
                CurrentPieces = squareSet.Pieces
            };

            return View(squareSetViewModel);
        }

        // POST: SquareSets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Exclude = "CurrentPieces")]EditSquareSetViewModel squareSetViewModel)
        {
            if (squareSetViewModel.Id == 0)
            {
                return View(squareSetViewModel);
            }

            SquareSet squareSet = db.SquareSets.First(o => o.Id == squareSetViewModel.Id);
            squareSetViewModel.CurrentPieces = squareSet.Pieces;

            if (ModelState.IsValid)
            {
                squareSet.Name = squareSetViewModel.Name;
                UploadImages(squareSetViewModel);

                await db.SaveChangesAsync();
                return RedirectToAction("Details", new {id = squareSet.Id});
            }

            return View(squareSetViewModel);
        }

        // GET: SquareSets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SquareSet squareSet = await db.SquareSets.FindAsync(id);
            if (squareSet == null)
            {
                return HttpNotFound();
            }
            return View(squareSet);
        }

        // POST: SquareSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SquareSet squareSet = await db.SquareSets.FindAsync(id);
            db.SquareSets.Remove(squareSet);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
