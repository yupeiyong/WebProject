using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendAnalysis.Models.Trend;
using TrendAnalysis.Service.Trend;


namespace TrendAnalysis.Service.Test.Trend
{
    [TestClass]
    public class UnitTestFactorTrend
    {
        [TestMethod]
        public void TestMethod1()
        {
            var ls = new List<Factor<string>>() { new Factor<string> { Left = new List<string> { "1", "2", "3" }, Right = new List<string> { } } };
            var numbers = new List<string> { "3", "2", "1", "2", "0", "0", "1", "2", "3", "3", "4", "4", "2", "3", "3","0", "2", "3", "3" };
            var rows= FactorsCompareTrend.AnalyseConsecutives(numbers, ls);
            Assert.IsTrue(rows.Count > 0);
            Assert.IsTrue(rows.Count == 1);
            Assert.IsTrue(rows[0].HistoricalConsecutiveTimes.Count == 2);

            var keys = rows[0].HistoricalConsecutiveTimes.Keys.ToList();
            keys.Sort();
            var dict=rows[0].HistoricalConsecutiveTimes;
            Assert.IsTrue(keys.Count == 2);
            Assert.IsTrue(keys[0] == 3 && dict[keys[0]] == 2);
            Assert.IsTrue(keys[1] == 4 && dict[keys[0]] == 2);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var ls = new List<Factor<int>>() { new Factor<int> { Left = new List<int> { 1,2,3 }, Right = new List<int> { } } };
            var numbers = new List<int> { 3, 2, 1, 2, 0, 0, 1, 2, 3, 3, 4, 4, 2, 3, 3, 0, 2, 3, 3 };
            var rows = FactorsCompareTrend.AnalyseConsecutives(numbers, ls);

            Assert.IsTrue(rows.Count > 0);
            Assert.IsTrue(rows.Count == 1);
            Assert.IsTrue(rows[0].HistoricalConsecutiveTimes.Count == 2);

            var keys = rows[0].HistoricalConsecutiveTimes.Keys.ToList();
            keys.Sort();
            var dict = rows[0].HistoricalConsecutiveTimes;
            Assert.IsTrue(keys.Count == 2);
            Assert.IsTrue(keys[0] == 3 && dict[keys[0]] == 2);
            Assert.IsTrue(keys[1] == 4 && dict[keys[0]] == 2);

        }

    }
}
