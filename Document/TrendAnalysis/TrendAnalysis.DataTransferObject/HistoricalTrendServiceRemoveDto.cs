using TrendAnalysis.Models.Trend;


namespace TrendAnalysis.DataTransferObject
{

    public class HistoricalTrendServiceRemoveDto
    {

        /// <summary>
        ///     第几位数
        /// </summary>
        public int Location { get; set; }


        /// <summary>
        ///     开始期次
        /// </summary>
        public int StartTimesValue { get; set; }


        /// <summary>
        ///     结束期次
        /// </summary>
        public int EndTimesValue { get; set; }


        /// <summary>
        ///     开始的允许最大的间隔数（最大连续期数-指定期次此因子连续次数），由小到大
        /// </summary>
        public int StartAllowMaxInterval { get; set; }


        /// <summary>
        ///     结束的允许最大的间隔数（最大连续期数-指定期次此因子连续次数），由小到大
        /// </summary>
        public int EndAllowMaxInterval { get; set; }


        /// <summary>
        ///     开始的允许的最小指定期次此因子连续次数，由大到小
        /// </summary>
        public int StartAllowConsecutiveTimes { get; set; }


        /// <summary>
        ///     结束的允许的最小指定期次此因子连续次数，由大到小
        /// </summary>
        public int EndAllowConsecutiveTimes { get; set; }


        /// <summary>
        ///     历史趋势类型
        /// </summary>
        public HistoricalTrendTypeEnum HistoricalTrendType { get; set; }


        /// <summary>
        ///     明细项分类
        /// </summary>
        public int HistoricalTrendItemType { get; set; }

    }

}