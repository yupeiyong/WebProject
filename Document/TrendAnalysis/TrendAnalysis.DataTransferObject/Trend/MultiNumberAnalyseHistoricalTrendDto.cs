using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendAnalysis.Models.Trend;

namespace TrendAnalysis.DataTransferObject.Trend
{
    public class MultiNumberAnalyseHistoricalTrendDto<T> : BaseAnalyseHistoricalTrendDto<T>
    {

        /// <summary>
        ///     分析因子
        /// </summary>
        public List<Factor<T>> Factors { get; set; }


        public int MultiNumberMaxCount { get; set; }


        /// <summary>
        /// 分析连续次数时的比较器
        /// </summary>
        public Func<IReadOnlyList<T>, List<T>, int, bool> AnalyseConsecutiveCompareFunc { get; set; }

        /// <summary>
        /// 分析可能连续次数的比较器
        /// </summary>
        public Func<IReadOnlyList<T>, List<T>, int, bool> PredictiveConsecutivesCompareFunc { get; set; }


        /// <summary>
        /// 可能因子的分析器
        /// </summary>
        public Action<IReadOnlyList<T>, List<T>> PredictiveFactorAction { get; set; }

    }

}