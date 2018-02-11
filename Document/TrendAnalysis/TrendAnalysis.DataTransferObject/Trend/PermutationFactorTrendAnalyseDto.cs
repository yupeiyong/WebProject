using System.Collections.Generic;
using TrendAnalysis.DataTransferObject.Trend;
using TrendAnalysis.Models.Trend;


namespace TrendAnalysis.DataTransferObject
{

    public class PermutationFactorTrendAnalyseDto<T> : BaseTrendAnalyseDto<T>
    {


        /// <summary>
        ///     排列因子
        /// </summary>
        public List<List<Factor<T>>> PermutationFactors { get; set; }


    }

}