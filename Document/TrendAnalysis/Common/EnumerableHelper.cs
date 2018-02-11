using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;


namespace Common
{

    public static class EnumerableHelper
    {
        /// <summary>
        /// 将集合转换为数据表
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static DataTable ConvertDataTable<T>(this IEnumerable<T> source) where T : class
        {
            return ConvertDataTable(source,null);
        }


        /// <summary>
        /// 将集合转换为数据表
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="propertiesAction">处理属性，比如重新排序或重命名等</param>
        /// <returns></returns>
        public static DataTable ConvertDataTable<T>(this IEnumerable<T> source, Action<List<PropertyInfo>> propertiesAction ) where T : class
        {
            return ConvertDataTable(source, null,propertiesAction);
        }
        /// <summary>
        /// 将集合转换为数据表
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="ignoreColumns">忽略的列名</param>
        /// <param name="propertiesAction">处理属性，比如重新排序或重命名等</param>
        /// <returns></returns>
        public static DataTable ConvertDataTable<T>(this IEnumerable<T> source,List<string>ignoreColumns,Action<List<PropertyInfo>>propertiesAction=null  ) where T : class
        {
            var type = typeof(T);
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            var properties =ignoreColumns!=null? type.GetProperties(flags).Where(p=>ignoreColumns.Any(c=>c!=p.Name)).ToList(): type.GetProperties(flags).ToList();
            //处理属性列表
            propertiesAction?.Invoke(properties);

            var table = new DataTable();
            var columnNameDict = new Dictionary<string, string>();

            var columns = new List<DataColumn>();
            foreach (var property in properties)
            {
                var description = property.GetCustomAttribute<DescriptionAttribute>();
                columns.Add(new DataColumn { ColumnName = property.Name, DataType = property.PropertyType });
                //保存说明性列名
                var columnName = description == null ? property.Name : description.Description;
                columnNameDict.Add(property.Name, columnName);
            }

            table.Columns.AddRange(columns.ToArray());
            foreach (var item in source)
            {
                var row = table.NewRow();
                foreach (var property in properties)
                {
                    row[property.Name] = property.GetValue(item);
                }
                table.Rows.Add(row);
            }
            foreach (var col in table.Columns.Cast<DataColumn>().Where(col => columnNameDict.ContainsKey(col.ColumnName)))
            {
                col.ColumnName = columnNameDict[col.ColumnName];
            }
            return table;
        }

    }

}