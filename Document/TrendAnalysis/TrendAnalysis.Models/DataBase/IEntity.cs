namespace TrendAnalysis.Models.DataBase
{
    public interface IEntity<T>where T:struct
    {
        /// <summary>
        /// 主键
        /// </summary>
        T Id { get; set; }

        byte[] RowVersion { get; set; }
    }
}
