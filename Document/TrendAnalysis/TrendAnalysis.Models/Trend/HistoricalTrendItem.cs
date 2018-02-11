using System.Collections.Generic;
using System.Linq;
using TrendAnalysis.Models.DataBase;


namespace TrendAnalysis.Models.Trend
{

    /// <summary>
    ///     历史趋势明细项
    /// </summary>
    public class HistoricalTrendItem : BaseEntity
    {

        /// <summary>
        ///     号码
        /// </summary>
        public byte Number;

        /// <summary>
        ///     期次
        /// </summary>
        public string Times { get; set; }

        /// <summary>
        ///     连续次数
        /// </summary>
        public int ResultConsecutiveTimes { set; get; }


        /// <summary>
        ///     间隔数
        /// </summary>
        public int ResultInterval { get; set; }


        /// <summary>
        ///     是否正确
        /// </summary>
        public bool Success { get; set; }


        /// <summary>
        ///     可能的因子
        /// </summary>
        public List<byte> PredictiveFactor {
            get
            {
                return string.IsNullOrWhiteSpace(PredictiveFactorString) 
                    ? new List<byte>() 
                    : PredictiveFactorString.Split(',').ToList().Select(byte.Parse).ToList();
            }
            set
            {
                if (value != null)
                {
                    PredictiveFactorString = string.Join(",", value);
                }
            } }

        public string PredictiveFactorString { get; set; }
    }

}