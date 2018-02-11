using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendAnalysis.Service.Trend;


namespace TrendAnalysis.Service.Test.Trend
{
    [TestClass]
    public class UnitTestCompositeNumber
    {
        [TestMethod]
        public void TestCreate()
        {
            var compositeNumber = new CompositeNumber(1, 49);
            var Composites = compositeNumber.CompositeNumbers;
            var numbers = compositeNumber.GetNumbers(new List<uint>() {1,2 });
        }
    }
}
