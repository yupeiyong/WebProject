using System.ComponentModel;


namespace TrendAnalysis.Models.Trend
{

    /// <summary>
    ///     历史趋势类型
    /// </summary>
    public enum HistoricalTrendTypeEnum
    {
        /// <summary>
        /// MarkSix个位普通分析
        /// </summary>
        [Description("MarkSix个位普通分析")]
        MarkSixOnesNormal = 1,
        /// <summary>
        /// MarkSix十位普通分析
        /// </summary>
        [Description("MarkSix十位普通分析")]
        MarkSixTensNormal = 2,
        /// <summary>
        /// MarkSix个位多号码结合分析
        /// </summary>
        [Description("MarkSix个位多号码结合分析")]
        MarkSixOnesMultiNumber = 3,
        /// <summary>
        /// MarkSix个位多号码结合分析
        /// </summary>
        [Description("MarkSix十位多号码结合分析")]
        MarkSixTensMultiNumber = 4,


        /// <summary>
        /// MarkSix个位排列因子分析
        /// </summary>
        [Description("MarkSix个位排列因子分析")]
        MarkSixOnesPermutationFactor = 5,
        /// <summary>
        /// MarkSix十位排列因子分析
        /// </summary>
        [Description("MarkSix十位排列因子分析")]
        MarkSixTensPermutationFactor = 6

    }

}