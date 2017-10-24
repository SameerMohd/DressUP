using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;

namespace BusinessLogicTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TakeOffPajamasFirst()
        {
            var obj = new DressUPLogic();
            String response = obj.getResponses("HOT", "7,1");
            Assert.AreEqual("fail", response);
        }

        [TestMethod]
        public void OnePieceEach()
        {
            var obj = new DressUPLogic();
            String response = obj.getResponses("HOT", "8, 6, 1, 1");
            Assert.AreEqual("Removing PJs, shorts, sandals, fail", response);
        }

        [TestMethod]
        public void SocksWhenHot()
        {
            var obj = new DressUPLogic();
            String response = obj.getResponses("HOT", "8,3");
            Assert.AreEqual("Removing PJs, fail", response);
        }

        [TestMethod]
        public void JacketWhenHot()
        {
            var obj = new DressUPLogic();
            String response = obj.getResponses("HOT", "7,1");
            Assert.AreEqual("fail", response);
        }

        [TestMethod]
        public void SocksAfterShoes()
        {
            var obj = new DressUPLogic();
            String response = obj.getResponses("COLD", "8, 6, 1, 2, 3");
            Assert.AreEqual("Removing PJs, pants, fail", response);
        }

        [TestMethod]
        public void PantsAfterShoes()
        {
            var obj = new DressUPLogic();
            String response = obj.getResponses("COLD", "8, 1, 6, 2, 3");
            Assert.AreEqual("Removing PJs, fail", response);
        }
        [TestMethod]
        public void ShirtAfterJacket()
        {
            var obj = new DressUPLogic();
            String response = obj.getResponses("COLD", "8, 3, 5, 4");
            Assert.AreEqual("Removing PJs, socks, fail", response);
        }

        [TestMethod]
        public void ShirtAfterHeadwear()
        {
            var obj = new DressUPLogic();
            String response = obj.getResponses("HOT", "8, 6, 2, 4");
            Assert.AreEqual("Removing PJs, shorts, fail", response);
        }
        [TestMethod]
        public void NotEnoughClothes()
        {
            var obj = new DressUPLogic();
            String response = obj.getResponses("COLD", "8, 6, 3, 4, 2, 5, 7");
            Assert.AreEqual("Removing PJs, pants, socks, shirt, hat, jacket, fail", response);
        }
        [TestMethod]
        public void NotValidCommand1()
        {
            var obj = new DressUPLogic();
            String response = obj.getResponses("COLD", "8, 16, 3, 4, 2, 5, 7");
            Assert.AreEqual("Removing PJs, fail", response);
        }

        [TestMethod]
        public void NotValidCommand2()
        {
            var obj = new DressUPLogic();
            String response = obj.getResponses("COLD", "8, -1, 3, 4, 2, 5, 7");
            Assert.AreEqual("Removing PJs, fail", response);
        }
    }
}
