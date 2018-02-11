using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendAnalysis.Models;

namespace TrendAnalysis.DataTransferObject
{
    /// <summary>
    /// 分析记录的数据传输对象
    /// </summary>
    public class MarkSixAnalyseSpecifiedLocationDto
    {
        /// <summary>
        /// 第几位数字
        /// </summary>
        public int Location { get; set; }


        /// <summary>
        /// 期次
        /// </summary>
        public string Times { get; set; }


        /// <summary>
        /// 十位数，允许最大的间隔数（最大连续期数-指定期次此因子连续次数）
        /// </summary>
        public int TensAllowMaxInterval { get; set; } = int.MaxValue;


        /// <summary>
        /// 个位数，允许最大的间隔数（最大连续期数-指定期次此因子连续次数）
        /// </summary>
        public int OnesAllowMaxInterval { get; set; } = int.MaxValue;


        /// <summary>
        /// 十位数，分析集合时，允许的最小连续数，大于等于此数才记录连续次数
        /// </summary>
        public int TensAllowMinTimes { get; set; } = 1;

        /// <summary>
        /// 个位数，分析集合时，允许的最小连续数，大于等于此数才记录连续次数
        /// </summary>
        public int OnesAllowMinTimes { get; set; } = 1;


        /// <summary>
        /// 十位数，几个期次的记录一起分析，此参数为最大连续期次数
        /// </summary>
        public int TensAroundCount { get; set; } = 18;


        /// <summary>
        /// 个位数，几个期次的记录一起分析，此参数为最大连续期次数
        /// </summary>
        public int OnesAroundCount { get; set; } = 10;

        /// <summary>
        /// 十位数，允许的最小指定期次此因子连续次数
        /// </summary>
        public int TensAllowMinFactorCurrentConsecutiveTimes { get; set; } = 1;


        /// <summary>
        /// 个位数，允许的最小指定期次此因子连续次数
        /// </summary>
        public int OnesAllowMinFactorCurrentConsecutiveTimes { get; set; } = 1;


        /// <summary>
        /// 十位数，记录尾部切去数量，比如原长度100，切去10，最终保留90
        /// </summary>
        public int TensNumbersTailCutCount { get; set; }


        /// <summary>
        /// 个位数，记录尾部切去数量，比如原长度100，切去10，最终保留90
        /// </summary>
        public int OnesNumbersTailCutCount { get; set; }
    }
}
