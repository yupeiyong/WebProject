using System;


namespace TrendAnalysis.DataTransferObject
{

    public class BaseSearchDto
    {

        /// <summary>
        ///     开始位置
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        ///     每一页数量
        /// </summary>
        public int PageSize { get; set; }


        /// <summary>
        ///     总条数
        /// </summary>
        public int TotalCount { get; set; }


        /// <summary>
        ///     是否获取总条数
        /// </summary>
        public bool IsGetTotalCount { get; set; } = true;


        /// <summary>
        ///     总页数
        /// </summary>
        public int PageCount => PageSize <= 0 ? 0 : (int) Math.Ceiling((double) TotalCount/PageSize);

    }

}