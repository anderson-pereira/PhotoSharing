﻿using System;
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
    [HandleError(View = "Error")]
    [ValueReporter]
    public class PhotosController : Controller
    {
        private IPhotoSharingContext context;

        public PhotosController()
        {
            context = new PhotoSharingContext();
        }

        public PhotosController(IPhotoSharingContext context)
        {
            this.context = context;
        }
        
        // GET: Photos
        public ActionResult Index()
        {
            //Não é mais necessário o parâmetro pois agora é chamado diretamente o PhotoGallery.
            //return View(context.Photos.ToList());
            //Nomear a view para ser recupearo no teste.
            return View("Index");
        }

        //Não pode ser chamada diretamente.
        [ChildActionOnly]
        public ActionResult _PhotoGallery(int number = 0)
        {
            List<Photo> photos;

            if (number == 0)
            {
                photos = context.Photos.ToList();
            }
            else
            {
                photos = (from p in context.Photos
                          orderby p.CreatedDate descending
                          select p).Take(number).ToList();
            }

            return PartialView(photos);
        }

        // GET: Photos/Details/5
        public ActionResult Display(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            Photo photo = context.FindPhotoById(id.Value); //Id nullable

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

                context.Add<Photo>(photo); //Método Genérico
                context.SaveChanges();

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
            Photo photo = context.FindPhotoById(id.Value);
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
            Photo photo = context.FindPhotoById(id);
            context.Delete(photo); //Método Genérico 2 forma de ser chamado.
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileContentResult GetImage(int id)
        {
            Photo photo = context.FindPhotoById(id);

            if(photo != null)
            {
                return File(photo.PhotoFile, photo.ImageMimeType);
            }else
            {
                return null;
            }
        }

        public ActionResult SlideShow()
        {
            throw new NotImplementedException("The SlidShow action is not yet ready.");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
