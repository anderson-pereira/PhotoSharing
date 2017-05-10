using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoSharingTests.Doubles;
using PhotoSharing.Controllers;
using System.Web.Mvc;

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
    }
}
