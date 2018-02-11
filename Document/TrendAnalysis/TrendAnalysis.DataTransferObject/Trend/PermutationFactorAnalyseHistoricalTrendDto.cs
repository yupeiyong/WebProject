using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendAnalysis.Models.Trend;

namespace TrendAnalysis.DataTransferObject.Trend
{
    /// <summary>
    /// 排列因子分析一段时期的历史趋势
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PermutationFactorAnalyseHistoricalTrendDto<T> : BaseAnalyseHistoricalTrendDto<T>
    {

        /// <summary>
        ///     排列因子
        /// </summary>
        public List<List<Factor<T>>> PermutationFactors { get; set; }
    }
}
