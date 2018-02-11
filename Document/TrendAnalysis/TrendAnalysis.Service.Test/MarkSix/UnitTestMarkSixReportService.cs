using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.Service.MarkSix;


namespace TrendAnalysis.Service.Test.MarkSix
{
    [TestClass]
    public class UnitTestMarkSixReportService
    {
        [TestMethod]
        public void TestSearch()
        {
            using(new TransactionScope())
            {
                var service = new MarkSixRecordService();
                var rows=service.Search(new MarkSixRecordSearchDto { StartIndex=0,PageSize=20});
                Assert.IsTrue(rows.Count > 0);
            }
        }
    }
}
