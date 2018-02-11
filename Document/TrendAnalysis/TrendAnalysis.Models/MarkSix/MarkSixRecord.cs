using System;
using System.ComponentModel;
using TrendAnalysis.Models.DataBase;


namespace TrendAnalysis.Models.MarkSix
{

    /// <summary>
    ///     MarkSix历史记录
    /// </summary>
    public class MarkSixRecord : BaseEntity
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

        [Description("1")]
        public byte FirstNum { get; set; }

        [Description("2")]
        public byte SecondNum { get; set; }

        [Description("3")]
        public byte ThirdNum { get; set; }

        [Description("4")]
        public byte FourthNum { get; set; }

        [Description("5")]
        public byte FifthNum { get; set; }

        [Description("6")]
        public byte SixthNum { get; set; }

        [Description("7")]
        public byte SeventhNum { get; set; }


        /// <summary>
        ///     开奖日期
        /// </summary>
        [Description("开奖日期")]
        public DateTime AwardingDate { get; set; }

    }

}