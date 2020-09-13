using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceInvaders;

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


    }
}
