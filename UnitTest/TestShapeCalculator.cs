using System;
using System.Linq;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShapeCalculator;
namespace UnitTest
{
    [TestClass]
    public class TestShapeCalculator
    {
        [TestMethod]
        public void TestSquareCalculator()
        {
            Shape mySquare = new Square(2);
            Assert.AreEqual(4, mySquare.Area);
        }
        [TestMethod]
        public void TestCircleCalculator()
        {
            Shape myCircle = new Circle(2);
            Assert.AreNotEqual(4, myCircle.Area);
        }
        [TestMethod]
        public void TestRectangleCalculator()
        {
            Shape myRectangle = new Rectangle(2,4);
            Assert.AreEqual(8, myRectangle.Area);
        }

        //[TestMethod]
        //public void TestWeatherAnalysis()
        //{
        //    Weather2016  w2016 = new Weather2016();
        //    DataTable tbl2016 = w2016.Weather2016Data();
        //    var hotestDay = tbl2016.Rows.Cast<DataRow>().OrderByDescending(x => x["MaxTemp"]).Take(1).ToList();
        //    Assert.AreEqual("8/12/2016 12:00:00 AM",hotestDay[0]["Date"].ToString());
        //}
    }
}
