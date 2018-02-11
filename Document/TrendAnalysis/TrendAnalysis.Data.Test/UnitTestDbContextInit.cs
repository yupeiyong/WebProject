using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendAnalysis.Models;
using TrendAnalysis.Models.MarkSix;


namespace TrendAnalysis.Data.Test
{
    [TestClass]
    public class UnitTestDbContextInit
    {
        [TestMethod]
        public void TestMethod1()
        {
            using(var dao=new TrendDbContext())
            {
                dao.Set<MarkSixRecord>().Add(new MarkSixRecord {Times="2017004",FirstNum=12,AwardingDate=DateTime.Now });
                dao.SaveChanges();
            }
        }
    }
}
