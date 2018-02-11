using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TrendAnalysis.Models.DataBase;


namespace TrendAnalysis.Models.MarkSix
{

    /// <summary>
    ///     MarkSix指定位置购买记录
    /// </summary>
    public class MarkSixSpecifiedLocationPurchase : BaseEntity
    {

        [Description("期次")]
        public string Times { get; set; }

        /// <summary>
        ///     赔率
        /// </summary>
        [Description("赔率")]
        public int Odds { get; set; }
        /// <summary>
        ///     指定的位置
        /// </summary>
        [Description("指定的位置")]
        public byte Location { get; set; }

        /// <summary>
        ///     购买清单
        /// </summary>
        [Description("购买清单")]
        public string PurchaseList { get; set; }


        /// <summary>
        ///     购买清单，键为号码，值为购买金额
        /// </summary>
        public Dictionary<byte, string> Purchases
        {
            get
            {
                if (string.IsNullOrWhiteSpace(PurchaseList))
                    return new Dictionary<byte, string>();

                var arr = PurchaseList.Split(';').ToList();
                arr.RemoveAll(s => string.IsNullOrWhiteSpace(s));
                return arr.Select(a => a.Split(':')).ToDictionary(k => byte.Parse(k[0]), v => v.Length == 2 ? v[1] : "");
            }
        }


        /// <summary>
        ///     购买金额
        /// </summary>
        [Description("购买金额")]
        public int PurchaseAmount { get; set; }


        /// <summary>
        ///     开奖号码
        /// </summary>
        [Description("开奖号码")]
        public byte AwardingNum { get; set; }

        /// <summary>
        ///     当次收益
        /// </summary>
        [Description("当次收益")]
        public int Profit { get; set; }

        /// <summary>
        ///     余额
        /// </summary>
        [Description("余额")]
        public int Balance { get; set; }

        /// <summary>
        ///     录入时间
        /// </summary>
        [Description("录入时间")]
        public DateTime OnCreated { get; set; }

        /// <summary>
        ///     修改时间
        /// </summary>
        [Description("修改时间")]
        public DateTime OnModified { get; set; }

    }

}