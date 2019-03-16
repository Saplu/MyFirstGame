using System;
using NUnit.Framework;
using CombatLogicClassLibrary;
using Moq;
using Utils;

namespace Tests
{
    public class AttackLogicUnitTests
    {
        private Mock<IRandomContext> _randomMock;

        [SetUp]
        public void Init()
        {
            _randomMock = new Mock<IRandomContext>();
            _randomMock.Setup(x => x.GetRandom(80, 120)).Returns(100);
            _randomMock.Setup(x => x.GetRandom(1, 100)).Returns(1);
            RandomProvider.Context = _randomMock.Object;
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual(200,AttackLogic.CalculateAttackDamage(100, 20, 1, 0));
        }
    }
}