using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendAnalysis.Models.Trend
{
    /// <summary>
    /// 因子
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Factor<T>
    {
        /// <summary>
        /// 左因子
        /// </summary>
        public List<T> Left { get; set; }

        /// <summary>
        /// 右因子
        /// </summary>
        public List<T> Right { get; set; }
    }
}
