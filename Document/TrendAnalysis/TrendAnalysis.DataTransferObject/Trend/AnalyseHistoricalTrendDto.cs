using System.Collections.Generic;
using TrendAnalysis.DataTransferObject.Trend;
using TrendAnalysis.Models.Trend;


namespace TrendAnalysis.DataTransferObject
{

    /// <summary>
    ///     分析一段时期历史趋势的传输对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AnalyseHistoricalTrendDto<T> : BaseAnalyseHistoricalTrendDto<T>
    {

        /// <summary>
        ///     分析因子
        /// </summary>
        public List<Factor<T>> Factors { get; set; }
    }

}