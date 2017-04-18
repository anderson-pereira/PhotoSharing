using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhotoSharing.Models;

namespace PhotoSharing.Controllers
{
    [ValueReporter]
    public class PhotosController : Controller
    {
        private PhotoSharingContext db = new PhotoSharingContext();

        // GET: Photos
        public ActionResult Index()
        {
            return View(db.Photos.ToList());
        }

        // GET: Photos/Details/5
        public ActionResult Display(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: Photos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Photos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Photo photo, HttpPostedFileBase image)
        {
            //Upload da imagem
            photo.CreatedDate = DateTime.Today;
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    photo.ImageMimeType = image.ContentType;

                    photo.PhotoFile = new byte[image.ContentLength];

                    //Ler do 0 até o final da imagem.
                    image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                }

                db.Photos.Add(photo);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(photo);
        }

        // GET: Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileContentResult GetImage(int id)
        {
            Photo photo = db.Photos.Find(id);

            if(photo != null)
            {
                return File(photo.PhotoFile, photo.ImageMimeType);
            }else
            {
                return null;
            }
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
