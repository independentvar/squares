using System.Data.Entity;
using System.Dynamic;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using KongOrange.Squares.Business;
using KongOrange.Squares.DataAccess;
using KongOrange.Squares.DomainClasses;
using KongOrange.Squares.WebInterface.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KongOrange.Squares.WebInterface.Controllers
{
    public class SquareSetPiecesController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: SquareSetPieces/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SquareSetPiece squareSetPiece = await _db.SquareSetPieces.FindAsync(id);
            if (squareSetPiece == null)
            {
                return HttpNotFound();
            }

            return View(squareSetPiece);
        }

        // GET: SquareSetPieces/Create
        public ActionResult Create(int squareSetId)
        {
            ViewBag.SquareSetId = squareSetId;
            return View();
        }

        // POST: SquareSetPieces/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateSquareSetPieceViewModel squareSetPieceViewModel)
        {
            if (ModelState.IsValid)
            {
                var storage = new StorageFacade();
                foreach (var image in squareSetPieceViewModel.Images)
                {
                    var url = storage.Store(image.FileName, image.InputStream, User.Identity.GetUserId());
                    var squareSetPiece = new SquareSetPiece
                    {
                        SquareSetId = squareSetPieceViewModel.SquareSetId,
                        ImageUrl = url
                    };

                    _db.SquareSetPieces.Add(squareSetPiece);
                }

                await _db.SaveChangesAsync();
                return RedirectToAction("Edit", "SquareSets", new { id = squareSetPieceViewModel.SquareSetId });
            }

            return View(squareSetPieceViewModel);
        }

        // GET: SquareSetPieces/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SquareSetPiece squareSetPiece = await _db.SquareSetPieces.FindAsync(id);
            if (squareSetPiece == null)
            {
                return HttpNotFound();
            }

            var squareSetPieceViewModel = new EditSquareSetPieceViewModel
            {
                Id = squareSetPiece.Id,
                CurrentImageUrl = squareSetPiece.ImageUrl,
                SquareSetId = squareSetPiece.SquareSetId
            };

            return View(squareSetPieceViewModel);
        }

        // POST: SquareSetPieces/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditSquareSetPieceViewModel squareSetPieceViewModel)
        {
            if (ModelState.IsValid)
            {
                // upload the new image
                var storage = new StorageFacade();
                var image = squareSetPieceViewModel.UpdatedImage;
                var url = storage.Store(image.FileName, image.InputStream, User.Identity.GetUserId());

                // update the db
                var setPiece = await _db.SquareSetPieces.FirstAsync(o => o.Id == squareSetPieceViewModel.Id);
                setPiece.ImageUrl = url;

                await _db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = squareSetPieceViewModel.Id });
            }

            return View(squareSetPieceViewModel);
        }

        // GET: SquareSetPieces/Delete/5
        public async Task<ActionResult> Delete(
            int? id,
            int squareSetId,
            int backToRouteId,
            string backToAction = "Details", 
            string backToController = "SquareSetPieces"
            )
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SquareSetPiece squareSetPiece = await _db.SquareSetPieces.FindAsync(id);
            if (squareSetPiece == null)
            {
                return HttpNotFound();
            }

            ViewBag.Action = backToAction;
            ViewBag.Controller = backToController;
            ViewBag.RouteId = backToRouteId;
            ViewBag.SquareSetId = squareSetId;

            return View(squareSetPiece);
        }

        // POST: SquareSetPieces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, int squareSetId)
        {
            SquareSetPiece squareSetPiece = await _db.SquareSetPieces.FindAsync(id);
            _db.SquareSetPieces.Remove(squareSetPiece);
            await _db.SaveChangesAsync();
            return RedirectToAction("Details", "SquareSets", new { Id = squareSetId });
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
