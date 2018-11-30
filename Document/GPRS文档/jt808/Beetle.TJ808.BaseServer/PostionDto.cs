namespace Beetle.TJ808.BaseServer
{
    public class PostionDto
    {
        /// <summary>
        /// 设备号
        /// </summary>
        public string Sim { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 方向
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// 速度
        /// </summary>
        public string Speed { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 启动状态
        /// </summary>
        public string Status { get; set; }
    }
}