using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendAnalysis.DataTransferObject
{
    public class MarkSixRecordSearchDto:BaseSearchDto
    {
        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public string Times { get; set; }
        
    }
}
