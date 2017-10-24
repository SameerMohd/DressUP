using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp;
using WebApp.Controllers;
using BusinessObjects;

namespace WebApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DressUp()
        {
            HomeController controller = new HomeController();
            DressUpModel Mdl = new DressUpModel();
            Mdl.Commands = "8, 6, 2, 4";
            Mdl.Temp = "HOT";
            // Act
            ViewResult result = controller.DressUp(Mdl) as ViewResult;
            
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void DressUp_TakeOffPajamasFirst()
        {
            HomeController controller = new HomeController();
            DressUpModel Mdl = new DressUpModel();
            Mdl.Commands = "7,1";
            Mdl.Temp = "HOT";
            // Act
            ViewResult result = controller.DressUp(Mdl) as ViewResult;
            DressUpModel viewResult = (DressUpModel)result.Model;
            
            Assert.AreEqual("fail", viewResult.Result);
        }

        [TestMethod]
        public void DressUp_OnePieceEach()
        {
            HomeController controller = new HomeController();
            DressUpModel Mdl = new DressUpModel();
            Mdl.Commands = "8, 6, 1, 1";
            Mdl.Temp = "HOT";
            // Act
            ViewResult result = controller.DressUp(Mdl) as ViewResult;
              DressUpModel viewResult = (DressUpModel)result.Model;
            Assert.AreEqual("Removing PJs, shorts, sandals, fail", viewResult.Result);
        }
        [TestMethod]
        public void DressUp_SocksWhenHot()
        {
            HomeController controller = new HomeController();
            DressUpModel Mdl = new DressUpModel();
            Mdl.Commands = "8,3";
            Mdl.Temp = "HOT";
            // Act
            ViewResult result = controller.DressUp(Mdl) as ViewResult;
              DressUpModel viewResult = (DressUpModel)result.Model;
            Assert.AreEqual("Removing PJs, fail", viewResult.Result);
        }
        [TestMethod]
        public void DressUp_JacketWhenHot()
        {
            HomeController controller = new HomeController();
            DressUpModel Mdl = new DressUpModel();
            Mdl.Commands = "7,1";
            Mdl.Temp = "HOT";
            // Act
            ViewResult result = controller.DressUp(Mdl) as ViewResult;
              DressUpModel viewResult = (DressUpModel)result.Model;
            Assert.AreEqual("fail", viewResult.Result);
        }
        [TestMethod]
        public void DressUp_SocksAfterShoes()
        {
            HomeController controller = new HomeController();
            DressUpModel Mdl = new DressUpModel();
            Mdl.Commands = "8, 6, 1, 2, 3";
            Mdl.Temp = "COLD";
            // Act
            ViewResult result = controller.DressUp(Mdl) as ViewResult;
              DressUpModel viewResult = (DressUpModel)result.Model;
            Assert.AreEqual("Removing PJs, pants, fail", viewResult.Result);
        }

        [TestMethod]
        public void DressUp_PantsAfterShoes()
        {
            HomeController controller = new HomeController();
            DressUpModel Mdl = new DressUpModel();
            Mdl.Commands = "8, 1, 6, 2, 3";
            Mdl.Temp = "COLD";
            // Act
            ViewResult result = controller.DressUp(Mdl) as ViewResult;
              DressUpModel viewResult = (DressUpModel)result.Model;
            Assert.AreEqual("Removing PJs, fail", viewResult.Result);
        }
        [TestMethod]
        public void DressUp_ShirtAfterJacket()
        {
            HomeController controller = new HomeController();
            DressUpModel Mdl = new DressUpModel();
            Mdl.Commands = "8, 3, 5, 4";
            Mdl.Temp = "COLD";
            // Act
            ViewResult result = controller.DressUp(Mdl) as ViewResult;
              DressUpModel viewResult = (DressUpModel)result.Model;
            Assert.AreEqual("Removing PJs, socks, fail", viewResult.Result);
        }
        [TestMethod]
        public void DressUp_ShirtAfterHeadwear()
        {
            HomeController controller = new HomeController();
            DressUpModel Mdl = new DressUpModel();
            Mdl.Commands = "8, 6, 2, 4";
            Mdl.Temp = "HOT";
            // Act
            ViewResult result = controller.DressUp(Mdl) as ViewResult;
              DressUpModel viewResult = (DressUpModel)result.Model;
            Assert.AreEqual("Removing PJs, shorts, fail", viewResult.Result);
        }
        [TestMethod]
        public void DressUp_NotEnoughClothes()
        {
            HomeController controller = new HomeController();
            DressUpModel Mdl = new DressUpModel();
            Mdl.Commands = "8, 6, 3, 4, 2, 5, 7";
            Mdl.Temp = "COLD";
            // Act
            ViewResult result = controller.DressUp(Mdl) as ViewResult;
              DressUpModel viewResult = (DressUpModel)result.Model;
            Assert.AreEqual("Removing PJs, pants, socks, shirt, hat, jacket, fail", viewResult.Result);
        }
        [TestMethod]
        public void DressUp_NotValidCommand1()
        {
            HomeController controller = new HomeController();
            DressUpModel Mdl = new DressUpModel();
            Mdl.Commands = "8, 16, 3, 4, 2, 5, 7";
            Mdl.Temp = "COLD";
            // Act
            ViewResult result = controller.DressUp(Mdl) as ViewResult;
              DressUpModel viewResult = (DressUpModel)result.Model;
            Assert.AreEqual("Removing PJs, fail", viewResult.Result);
        }
        [TestMethod]
        public void DressUp_NotValidCommand2()
        {
            HomeController controller = new HomeController();
            DressUpModel Mdl = new DressUpModel();
            Mdl.Commands = "8, -1, 3, 4, 2, 5, 7";
            Mdl.Temp = "COLD";
            // Act
            ViewResult result = controller.DressUp(Mdl) as ViewResult;
              DressUpModel viewResult = (DressUpModel)result.Model;
            Assert.AreEqual("Removing PJs, fail", viewResult.Result);
        }
    }
}
