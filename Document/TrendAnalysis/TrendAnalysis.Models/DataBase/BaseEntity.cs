using System;


namespace TrendAnalysis.Models.DataBase
{

    public class BaseEntity : IEntity<long>
    {

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime? CreatorTime { get; set; }


        /// <summary>
        ///     修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        public long Id { get; set; }

        public byte[] RowVersion { get; set; }

    }

}