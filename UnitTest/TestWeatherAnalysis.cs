using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherAnalysis;
using System.Data;
using System.Collections.Generic;
using System.Linq;
namespace UnitTest
{
    [TestClass]
    public class TestWeatherAnalysis
    {
        [TestMethod]
        public void TestWeatherCalculator()
        {
            Weather2016 w2016 = new Weather2016();
            List<DayInfo> tbl2016 = w2016.Weather2016Data("../../weather.csv");
            var hotestDay = tbl2016.OrderByDescending(x => x.MaxTemp).Take(1).ToList();
            Assert.AreEqual("8/12/2016", hotestDay[0].theDay.ToString().Substring(0, 9));
        }
    }
}
