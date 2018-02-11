using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendAnalysis.Models;
using TrendAnalysis.Models.MarkSix;


namespace TrendAnalysis.Data.Mapping
{
   public  class MarkSixSpecifiedLocationPurchaseMap:EntityTypeConfiguration<MarkSixSpecifiedLocationPurchase>
    {
        public MarkSixSpecifiedLocationPurchaseMap()
        {
            Property(p => p.Times).HasMaxLength(7).IsRequired();
        }
    }
}
