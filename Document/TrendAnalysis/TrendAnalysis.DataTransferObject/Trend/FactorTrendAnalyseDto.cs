using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendAnalysis.DataTransferObject.Trend;
using TrendAnalysis.Models;
using TrendAnalysis.Models.Trend;

namespace TrendAnalysis.DataTransferObject
{
    /// <summary>
    /// 分析数字记录的传输对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FactorTrendAnalyseDto<T>:BaseTrendAnalyseDto<T>
    {

        public List<Factor<T>> Factors { get; set; }

    }
}
