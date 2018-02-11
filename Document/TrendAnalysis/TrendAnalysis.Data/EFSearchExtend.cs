using System;
using System.Linq;
using System.Linq.Expressions;


namespace TrendAnalysis.Data
{

    public static class EfSearchExtend
    {

        /// <summary>
        ///     生成日期查询的数据源
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="fieldName">日期字段名称</param>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> WhereDateTime<TEntity>(this IQueryable<TEntity> source, string fieldName, DateTime? startDateTime, DateTime? endDateTime) where TEntity : class
        {
            var type = typeof (TEntity);
            var property = type.GetProperty(fieldName);
            if (property == null)
                throw new Exception($"错误，类型：{type.Name}不存在{fieldName}属性！");

            if (startDateTime == null && endDateTime == null) return source;
            if (startDateTime == null)
                startDateTime = DateTime.MinValue;

            if (endDateTime == null)
                endDateTime = DateTime.MaxValue.AddDays(-1);

            if (endDateTime.Value < startDateTime.Value)
            {
                var tmp = endDateTime.Value;
                endDateTime = startDateTime;
                startDateTime = tmp;
            }
            var start = startDateTime.Value.Date;
            var end = endDateTime.Value.Date.AddDays(1);

            var parameterExpr = Expression.Parameter(type, "m");
            var propertyAccessExpr = Expression.MakeMemberAccess(parameterExpr, property);

            //是否为可空类型
            if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof (Nullable<>))
            {
                var notNullExpr = Expression.NotEqual(propertyAccessExpr, Expression.Constant(null)); //m.OnLastUpdated != null

                var greaterThanOrEqualExpr = Expression.GreaterThanOrEqual(Expression.Property(propertyAccessExpr, "Value"), Expression.Constant(start)); //m.OnLastUpdated.Value >=start
                var lessThanExpr = Expression.LessThan(Expression.Property(propertyAccessExpr, "Value"), Expression.Constant(end)); //m.OnLastUpdated.Value <end
                var whereExpr = Expression.AndAlso(Expression.AndAlso(notNullExpr, greaterThanOrEqualExpr), lessThanExpr); //m.OnLastUpdated != null && m.OnLastUpdated.Value >= start && m.OnLastUpdated.Value < end
                var lambdaExpr = Expression.Lambda(whereExpr, parameterExpr);
                var resultExpression = Expression.Call(typeof (Queryable), "Where", new[] {type}, source.Expression, Expression.Quote(lambdaExpr));
                return source.Provider.CreateQuery<TEntity>(resultExpression);
            }
            else
            {
                var greaterThanOrEqualExpr = Expression.GreaterThanOrEqual(propertyAccessExpr, Expression.Constant(start)); //m.OnLastUpdated >=start
                var lessThanExpr = Expression.LessThan(propertyAccessExpr, Expression.Constant(end)); //m.OnLastUpdated <end
                var whereExpr = Expression.AndAlso(greaterThanOrEqualExpr, lessThanExpr); //m.OnLastUpdated >= start && m.OnLastUpdated < end
                var lambdaExpr = Expression.Lambda(whereExpr, parameterExpr);
                var resultExpression = Expression.Call(typeof (Queryable), "Where", new[] {type}, source.Expression, Expression.Quote(lambdaExpr));
                return source.Provider.CreateQuery<TEntity>(resultExpression);
            }
        }

    }

}