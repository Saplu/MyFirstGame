using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void enemyHealTest()
        {
            var percents = new double[4] { -.5, 0, .3, .3 };
            var asd = new CharacterClassLibrary.NPCClasses.Goblin(1, 1);

            var actual = asd.ChooseAlly(percents);
            var expected = 2;

            Assert.AreEqual(expected, actual);
        }
    }
}
