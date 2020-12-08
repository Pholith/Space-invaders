using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceInvaders;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestVecteur2D
    {
        [TestMethod]
        public void TestOperatorAdd()
        {
            Vector2 v1 = new Vector2(3, 4);
            Vector2 v2 = new Vector2(2, -1);
            Assert.AreEqual(v1 + v2, new Vector2(5, 3));
        }

        [TestMethod]
        public void TestOperatorSub()
        {
            Vector2 v1 = new Vector2(3, 4);
            Vector2 v2 = new Vector2(1, -1);
            Assert.AreEqual(v1 - v2, new Vector2(2, 5));
        }

        [TestMethod]
        public void TestOperatorMult()
        {
            Vector2 v1 = new Vector2(3, 4);
            Assert.AreEqual(v1 * 2, new Vector2(6, 8));
            Assert.AreEqual(-2 * v1, new Vector2(-6, -8));
        }
        [TestMethod]
        public void TestOperatorEquals()
        {
            Vector2 v1 = new Vector2(3, 4);
            Vector2 v2 = new Vector2(3, 4);
            Assert.IsTrue(v1 == v2);
            Assert.IsFalse(v1 != v2);
            Assert.IsFalse(v1 == v2 * 2);
        }

        [TestMethod]
        public void TestRotation()
        {
            Vector2 v1 = new Vector2(1, 0);
            Assert.AreEqual(v1.Rotate(0), v1);
            Assert.AreEqual(v1.Rotate(Math.PI).Round(), new Vector2(-1, 0));
            Assert.AreEqual(v1.Rotate(Math.PI/2).Round(), new Vector2(0, 1));

            Vector2 v2 = new Vector2(5, 2);
            Assert.AreEqual(v2.Rotate(Math.PI).Round(), new Vector2(-5, -2));
            Assert.AreEqual(v2.Rotate(90).Round(), new Vector2(-2, 5));
        }

    }
}
