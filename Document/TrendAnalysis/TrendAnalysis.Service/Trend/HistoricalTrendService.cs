using System;
using System.Collections.Generic;
using System.Linq;
using TrendAnalysis.Data;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.Models.Trend;
using Z.EntityFramework.Plus;


namespace TrendAnalysis.Service.Trend
{

    public class HistoricalTrendService
    {

        public static void AddRange(List<HistoricalTrend> historicalTrends)
        {
            using (var dao = new TrendDbContext())
            {
                historicalTrends.ForEach(trend =>
                {
                    trend.CreatorTime = DateTime.Now;
                    trend.LastModifyTime = DateTime.Now;
                });
                dao.BulkInsert(historicalTrends, options => options.IncludeGraph = true);
                dao.BulkSaveChanges();
            }
        }
        public static void AddRangeAsync(List<HistoricalTrend> historicalTrends)
        {
            using (var dao = new TrendDbContext())
            {
                historicalTrends.ForEach(trend =>
                {
                    trend.CreatorTime = DateTime.Now;
                    trend.LastModifyTime = DateTime.Now;
                });

                dao.Set<HistoricalTrend>().AddRange(historicalTrends);
                dao.SaveChangesAsync();
            }
        }

        public static void Remove(HistoricalTrendServiceRemoveDto dto)
        {
            using (var dao = new TrendDbContext())
            {
                ////删除相同的记录
                //var trends = dao.Set<HistoricalTrend>().Where(ht => ht.Location == dto.Location
                //    && ht.StartTimes >= dto.StartTimesValue && ht.StartTimes <= dto.EndTimesValue
                //    && ht.AllowConsecutiveTimes >= dto.StartAllowConsecutiveTimes && ht.AllowConsecutiveTimes <= dto.EndAllowConsecutiveTimes
                //    && ht.AllowInterval >= dto.StartAllowMaxInterval && ht.AllowInterval <= dto.EndAllowMaxInterval
                //    && ht.HistoricalTrendType == dto.HistoricalTrendType
                //    && ht.HistoricalTrendItemType == dto.HistoricalTrendItemType)
                //    .ToList();

                //foreach (var item in trends)
                //{
                //    //删除子项
                //    dao.Set<HistoricalTrendItem>().RemoveRange(item.Items);
                //}
                //dao.Set<HistoricalTrend>().RemoveRange(trends);
                //dao.SaveChanges();

                //删除相同的记录
                var trendsSource = dao.Set<HistoricalTrend>().Where(ht => ht.Location == dto.Location
                    && ht.StartTimes >= dto.StartTimesValue && ht.StartTimes <= dto.EndTimesValue
                    && ht.AllowConsecutiveTimes >= dto.StartAllowConsecutiveTimes && ht.AllowConsecutiveTimes <= dto.EndAllowConsecutiveTimes
                    && ht.AllowInterval >= dto.EndAllowMaxInterval  && ht.AllowInterval <= dto.StartAllowMaxInterval
                    && ht.HistoricalTrendType == dto.HistoricalTrendType
                    && ht.HistoricalTrendItemType == dto.HistoricalTrendItemType);

                var trendItemIds = trendsSource.SelectMany(t => t.Items).Select(hti => hti.Id);
                //删除子项
                dao.Set<HistoricalTrendItem>().Where(hti => trendItemIds.Any(id => id == hti.Id)).Delete();
                
                trendsSource.Delete();
            }
        }

    }

}