using System.ComponentModel;


namespace TrendAnalysis.Models.Trend
{

    /// <summary>
    ///     用于分析历史趋势的临时记录
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TemporaryRecord<T>
    {

        /// <summary>
        ///     期次
        /// </summary>
        [Description("期次")]
        public string Times { get; set; }

        /// <summary>
        ///     代表期次的整型值
        /// </summary>
        [Description("代表期次的整型值")]
        public int TimesValue { get; set; }

        /// <summary>
        ///     号码
        /// </summary>
        public T Number { get; set; }

    }

}