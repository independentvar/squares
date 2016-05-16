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
    public class SquareSetPiecesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SquareSetPieces
        public async Task<ActionResult> Index(int squareSetId)
        {
            ViewBag.SquareSetId = squareSetId;
            return View(await db.SquareSetPieces.Where(s => s.SquareSetId == squareSetId).ToListAsync());
        }

        // GET: SquareSetPieces/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SquareSetPiece squareSetPiece = await db.SquareSetPieces.FindAsync(id);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateSquareSetPieceViewModel squareSetPieceViewModel)
        {
            if (ModelState.IsValid)
            {
                var file = squareSetPieceViewModel.Image;
                var storage = new StorageFacade();
                var url = storage.Store(file.FileName, file.InputStream, User.Identity.GetUserId());

                var squareSetPiece = new SquareSetPiece
                {
                    SquareSetId = squareSetPieceViewModel.SquareSetId,
                    ImageUrl = url
                };

                db.SquareSetPieces.Add(squareSetPiece);
                await db.SaveChangesAsync();
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
            SquareSetPiece squareSetPiece = await db.SquareSetPieces.FindAsync(id);
            if (squareSetPiece == null)
            {
                return HttpNotFound();
            }
            return View(squareSetPiece);
        }

        // POST: SquareSetPieces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,ImageUrl")] SquareSetPiece squareSetPiece)
        {
            if (ModelState.IsValid)
            {
                db.Entry(squareSetPiece).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(squareSetPiece);
        }

        // GET: SquareSetPieces/Delete/5
        public async Task<ActionResult> Delete(int? id, int squareSetId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SquareSetPiece squareSetPiece = await db.SquareSetPieces.FindAsync(id);
            if (squareSetPiece == null)
            {
                return HttpNotFound();
            }

            ViewBag.SquareSetId = squareSetId;
            return View(squareSetPiece);
        }

        // POST: SquareSetPieces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, int squareSetId)
        {
            SquareSetPiece squareSetPiece = await db.SquareSetPieces.FindAsync(id);
            db.SquareSetPieces.Remove(squareSetPiece);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { squareSetId = squareSetId });
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
