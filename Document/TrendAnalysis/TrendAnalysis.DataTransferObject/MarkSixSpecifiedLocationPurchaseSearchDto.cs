using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendAnalysis.DataTransferObject
{
    public class MarkSixSpecifiedLocationPurchaseSearchDto : BaseSearchDto
    {
        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        /// <summary>
        /// 期次
        /// </summary>
        public string Times { get; set; }

        /// <summary>
        /// 第几个号码
        /// </summary>
        public int Location { get; set; }

    }
}
