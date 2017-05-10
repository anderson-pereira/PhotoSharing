using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoSharingTests.Doubles;
using PhotoSharing.Controllers;
using System.Web.Mvc;
using PhotoSharing.Models;
using System.Linq;
using System.Collections.Generic;

namespace PhotoSharingTests
{
    [TestClass]
    public class PhotoControllerTests
    {
        //Nome da classe igual a funcionalidade que será testada.
        [TestMethod]
        public void TestIndexReturnView()
        {
            var context = new FakePhotoSharingContext();
            var controller = new PhotosController(context);
            //Adicionado a referência para o System.Web.Mvc do próprio projeto para o projeto de testes, assim não há erro,
            //na chamada do controller e nem no uso do ViewResult.
            var result = controller.Index() as ViewResult;

            //Testa se o nome da view é realmente Index.
            Assert.AreEqual("Index", result.ViewName);
        }

        //Verifica se a partialView retorna uma lista de fotos.
        [TestMethod]
        public void TestPhotoGalleryModelType()
        {
            var context = new FakePhotoSharingContext();

            context.Photos = new[]
            {
                new Photo(),
                new Photo(),
                new Photo(),
                new Photo(),
            }.AsQueryable();

            var controller = new PhotosController(context);

            var result = controller._PhotoGallery() as PartialViewResult;

            Assert.AreEqual(typeof(List<Photo>), result.Model.GetType());
        }

        //Verifica o tipo de retorno do getImage.
        [TestMethod]
        public void TestGetImageReturnType()
        {
            var context = new FakePhotoSharingContext();

            context.Photos = new[]
            {
                new Photo() { PhotoID = 1, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo() { PhotoID = 2, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo() { PhotoID = 3, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo() { PhotoID = 4, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
            }.AsQueryable();

            var controller = new PhotosController(context);

            var result = controller.GetImage(1) as ActionResult;

            Assert.AreEqual(typeof(FileContentResult), result.GetType());
        }

        //Verifica se ao chamar o PhotoGallery sem paramêtros ele retorna todas as fotos.
        [TestMethod]
        public void TestPhotoGalleryNoParameter()
        {
            var context = new FakePhotoSharingContext();

            context.Photos = new[]
            {
                new Photo(),
                new Photo(),
                new Photo(),
                new Photo(),
            }.AsQueryable();

            var controller = new PhotosController(context);

            var result = controller._PhotoGallery() as PartialViewResult;

            var modelPhotos = (IEnumerable<Photo>)result.Model;

            Assert.AreEqual(4, modelPhotos.Count());
        }

        //Verifica se ao chamar o PhotoGallery com paramêtros ele retorna todas as fotos.
        [TestMethod]
        public void TestPhotoGalleryInParameter()
        {
            var context = new FakePhotoSharingContext();

            context.Photos = new[]
            {
                new Photo(),
                new Photo(),
                new Photo(),
                new Photo(),
            }.AsQueryable();

            var controller = new PhotosController(context);

            var result = controller._PhotoGallery(3) as PartialViewResult;

            var modelPhotos = (IEnumerable<Photo>)result.Model;

            Assert.AreEqual(3, modelPhotos.Count());
        }
    }
}
