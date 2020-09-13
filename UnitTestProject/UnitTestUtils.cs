using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SpaceInvaders.Utils;
using System.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestUtils
    {

        [TestMethod]
        public void TestOperatorAdd()
        {
            List<int> l = new List<int>()
            {
                5, 3, 4
            };
            Assert.AreEqual("[5, 3, 4]", l.ToReadableList());

            Assert.AreEqual("[]", new List<int>());
        }
    }
}
