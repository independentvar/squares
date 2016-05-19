using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using KongOrange.Squares.Business;
using KongOrange.Squares.DataAccess;
using KongOrange.Squares.DomainClasses;
using KongOrange.Squares.WebInterface.Areas.Admin.Models;
using Microsoft.AspNet.Identity;

namespace KongOrange.Squares.WebInterface.Areas.Admin.Controllers
{
    [Authorize]
    public class SquareSetsController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: SquareSets/userId
        public async Task<ActionResult> Index(string userId = null)
        {
            IQueryable<SquareSet> squareSets;
            if (userId != null)
            {
                squareSets = _db.SquareSets.Where(o => o.UserId == userId).Include(o => o.Pieces);
            }
            else
            {
                squareSets = _db.SquareSets.Include(o => o.Pieces);
            }
            
            return View(await squareSets.ToListAsync());
        }

        // GET: SquareSets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SquareSet squareSet = await _db.SquareSets.FindAsync(id);
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
                _db.SquareSets.Add(squareSet);
                UploadImages(squareSetViewModel);

                await _db.SaveChangesAsync();
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

                        _db.SquareSetPieces.Add(squareSetPiece);
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

            SquareSet squareSet = await _db.SquareSets.FindAsync(id);
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

            SquareSet squareSet = _db.SquareSets.First(o => o.Id == squareSetViewModel.Id);
            squareSetViewModel.CurrentPieces = squareSet.Pieces;

            if (ModelState.IsValid)
            {
                squareSet.Name = squareSetViewModel.Name;
                UploadImages(squareSetViewModel);

                await _db.SaveChangesAsync();
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
            SquareSet squareSet = await _db.SquareSets.FindAsync(id);
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
            SquareSet squareSet = await _db.SquareSets.FindAsync(id);
            _db.SquareSets.Remove(squareSet);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
