using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendAnalysis.DataTransferObject
{
    public class MarkSixSpecifiedLocationPurchaseSaveDto
    {
        public long Id { get; set; }

        public string Times { get; set; }

        /// <summary>
        /// 赔率
        /// </summary>
        public int Odds { get; set; }
        /// <summary>
        /// 指定的位置
        /// </summary>
        public byte Location { get; set; }

        /// <summary>
        /// 购买清单
        /// </summary>
        public string PurchaseList { get; set; }


        /// <summary>
        /// 购买金额
        /// </summary>
        public int PurchaseAmount
        {
            get
            {
                var amount = 0;
                if(!string.IsNullOrWhiteSpace(PurchaseList))
                {
                    var strArray = PurchaseList.Split(';');
                    foreach(var str in strArray)
                    {
                        var numArray = str.Split(';');
                        if (numArray.Length == 2)
                        {
                            var currentAmount = 0;
                            if(int.TryParse(numArray[1],out currentAmount))
                            {
                                amount += currentAmount;
                            }
                        }
                    }
                }
                return amount;
            }
        }

        /// <summary>
        /// 开奖号码
        /// </summary>
        public byte AwardingNum { get; set; }

        /// <summary>
        /// 当次收益
        /// </summary>
        public int Profit { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public int Balance { get; set; }
    }
}
