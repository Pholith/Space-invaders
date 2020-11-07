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
            Vecteur2D v1 = new Vecteur2D(3, 4);
            Vecteur2D v2 = new Vecteur2D(2, -1);
            Assert.AreEqual(v1 + v2, new Vecteur2D(5, 3));
        }

        [TestMethod]
        public void TestOperatorSub()
        {
            Vecteur2D v1 = new Vecteur2D(3, 4);
            Vecteur2D v2 = new Vecteur2D(1, -1);
            Assert.AreEqual(v1 - v2, new Vecteur2D(2, 5));
        }

        [TestMethod]
        public void TestOperatorMult()
        {
            Vecteur2D v1 = new Vecteur2D(3, 4);
            Assert.AreEqual(v1 * 2, new Vecteur2D(6, 8));
            Assert.AreEqual(-2 * v1, new Vecteur2D(-6, -8));
        }
        [TestMethod]
        public void TestOperatorEquals()
        {
            Vecteur2D v1 = new Vecteur2D(3, 4);
            Vecteur2D v2 = new Vecteur2D(3, 4);
            Assert.IsTrue(v1 == v2);
            Assert.IsFalse(v1 != v2);
            Assert.IsFalse(v1 == v2 * 2);
        }

        [TestMethod]
        public void TestRotation()
        {
            Vecteur2D v1 = new Vecteur2D(1, 0);
            Assert.AreEqual(v1.Rotate(0), v1);
            Assert.AreEqual(v1.Rotate(Math.PI).Round(), new Vecteur2D(-1, 0));
            Assert.AreEqual(v1.Rotate(Math.PI/2).Round(), new Vecteur2D(0, 1));

            Vecteur2D v2 = new Vecteur2D(5, 2);
            Assert.AreEqual(v2.Rotate(Math.PI).Round(), new Vecteur2D(-5, -2));
            Assert.AreEqual(v2.Rotate(90).Round(), new Vecteur2D(-2, 5));
        }

    }
}
