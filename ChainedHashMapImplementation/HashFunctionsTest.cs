using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ChainedHashMapImplementation
{
    [TestFixture]
    class HashFunctionsTest
    {
        [Test]
        public void LastTwoDigitsTest()
        {
            Assert.AreEqual(31, HashFunctions.LastTwoDigit(4531));
        }
        [Test]
        public void LastThreeDigitsTest()
        {
            Assert.AreEqual(243, HashFunctions.LastThreeDigit(1243));
        }
        [Test]
        public void SumOfThree()
        {
            Assert.AreEqual(10, HashFunctions.SumDigits(1234));
        }
        [Test]
        public void HashModuloTest()
        {
            var moduloConstant = 12;
            Assert.AreEqual(5, HashFunctions.HashModuloApply(5));
        }
    }
}
