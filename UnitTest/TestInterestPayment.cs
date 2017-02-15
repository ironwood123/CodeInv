using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterestPayment;
namespace UnitTest
{
    [TestClass]
    public class TestInterestPayment
    {

        [TestMethod]
        public void TestBalanceCalculator()
        {
            BalanceCalculator bc = new BalanceCalculator();

            Assert.AreEqual(105, bc.EndBalance(100, 0.05m));
        }
    }
}
