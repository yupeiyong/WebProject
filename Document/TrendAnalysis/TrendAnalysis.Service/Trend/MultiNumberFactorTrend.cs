using System;
using System.Collections.Generic;
using System.Linq;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.DataTransferObject.Trend;
using TrendAnalysis.Models.Trend;


namespace TrendAnalysis.Service.Trend
{

    /// <summary>
    ///     多号码因子的趋势分析
    /// </summary>
    public class MultiNumberFactorTrend
    {

        /// <summary>
        ///     分析
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<FactorTrendAnalyseResult<T>> Analyse<T>(MultiNumberFactorTrendAnalyseDto<T> dto)
        {
            /*
            位置索引：	0	1	2	3	4	5	6	7	8	9	10	11	12	13
                号码：	2	1	1	6	6	4	5	4	6	0	1	4	8	2
	  分析的开始位置：  如:6,那么可以和0-5这5个位置的数字任意组合，加或减（暂不考虑乘除），比如：(0)2 + (6)5=7%10


             */
            if (dto.AnalyseConsecutiveCompareFunc == null)
                throw new Exception("错误，统计连续次数的比较委托方法不能为空！");

            if (dto.PredictiveConsecutivesCompareFunc == null)
                throw new Exception("错误，分析可能连续次数的委托方法不能为空！");

            if (dto.PredictiveFactorAction == null)
                throw new Exception("错误，分析可能因子的委托方法不能为空！");

            var factorResults = AnalyseConsecutives(dto);
            factorResults = factorResults.Where(t => t.HistoricalConsecutiveTimes.Count > 0).ToList();
            foreach (var item in factorResults)
            {
                var times = 0;

                //记录集合倒序检查，因子是否包含当前号码
                for (var i = dto.Numbers.Count - 1; i >= dto.MultiNumberMaxCount; i--)
                {
                    //if (!item.Factor.Contains(dto.Numbers[i]))
                    if (!dto.PredictiveConsecutivesCompareFunc(dto.Numbers, item.Factor, i))
                        break;
                    times++;
                }

                /* 暂时不重围可能的因子
                //重置可能的因子
                dto.PredictiveFactorAction(dto.Numbers, item.PredictiveFactor);
                */
                //记录因子当前连续次数
                item.FactorCurrentConsecutiveTimes = times;
            }

            //1、按允许的最小因子当前连续次数和允许的最大间隔次数筛选
            //2、先按最大连续次数然后按最小间隔次数排序
            factorResults = factorResults
                .Where(m => m.FactorCurrentConsecutiveTimes >= dto.AllowMinFactorCurrentConsecutiveTimes && m.Interval <= dto.AllowMaxInterval)
                .OrderByDescending(t => t.FactorCurrentConsecutiveTimes)
                .ThenBy(t => t.Interval).ToList();

            return factorResults;

        }


        /// <summary>
        ///     解析因子在记录中的连续次数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<FactorTrendAnalyseResult<T>> AnalyseConsecutives<T>(MultiNumberFactorTrendAnalyseDto<T> dto)
        {
            var resultList = new List<FactorTrendAnalyseResult<T>>();
            foreach (var factor in dto.Factors)
            {
                var curDto = new MultiNumberAnalyseConsecutiveDto<T>
                {
                    Numbers = dto.Numbers,
                    MultiNumberMaxCount = dto.MultiNumberMaxCount,
                    AllowMaxInterval = dto.AllowMaxInterval,
                    AllowMinTimes = dto.AllowMinTimes,
                    AllowMinFactorCurrentConsecutiveTimes = dto.AllowMinFactorCurrentConsecutiveTimes,
                    NumbersTailCutCount = dto.NumbersTailCutCount,
                    AnalyseConsecutiveCompareFunc = dto.AnalyseConsecutiveCompareFunc
                };

                if (factor.Left != null && factor.Left.Count > 0)
                {
                    curDto.Factor = factor.Left;
                    curDto.PredictiveFactor = factor.Right;
                    resultList.Add(AnalyseConsecutive(curDto));
                }
                if (factor.Right != null && factor.Right.Count > 0)
                {
                    curDto.Factor = factor.Right;
                    curDto.PredictiveFactor = factor.Left;
                    resultList.Add(AnalyseConsecutive(curDto));
                }

            }
            return resultList;
        }


        /// <summary>
        ///     解析连续在因子中的记录数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        private static FactorTrendAnalyseResult<T> AnalyseConsecutive<T>(MultiNumberAnalyseConsecutiveDto<T> dto)
        {
            var curResult = new FactorTrendAnalyseResult<T>
            {
                Factor = dto.Factor,
                PredictiveFactor = dto.PredictiveFactor,
                HistoricalConsecutiveTimes = new SortedDictionary<int, int>()
            };
            var i = dto.MultiNumberMaxCount;

            //连续次数
            var times = 0;
            var length = dto.Numbers.Count - dto.NumbersTailCutCount;
            while (i < length)
            {
                if (dto.AnalyseConsecutiveCompareFunc(dto.Numbers, dto.Factor, i))
                {
                    times++;
                }
                else
                {
                    if (curResult.HistoricalConsecutiveTimes.ContainsKey(times))
                    {
                        curResult.HistoricalConsecutiveTimes[times]++;
                    }
                    else if (times >= dto.AllowMinTimes)
                    {
                        curResult.HistoricalConsecutiveTimes.Add(times, 1);
                    }
                    times = 0;
                }
                i++;
            }
            if (curResult.HistoricalConsecutiveTimes.ContainsKey(times))
            {
                curResult.HistoricalConsecutiveTimes[times]++;
            }
            else if (times >= dto.AllowMinTimes)
            {
                curResult.HistoricalConsecutiveTimes.Add(times, 1);
            }
            return curResult;
        }


        /// <summary>
        ///     分析一段日期的历史趋势，（通过号码集合分析历史趋势）
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<HistoricalTrend> AnalyseHistoricalTrend(MultiNumberAnalyseHistoricalTrendDto<byte> dto)
        {
            var trends = new List<HistoricalTrend>();

            if (dto.Numbers.Count < dto.AnalyseNumberCount)
                throw new Exception("分析历史趋势时，分析记录数量不能大于记录数量！");
            
            var analyseNumbers = dto.Numbers.OrderByDescending(n => n.TimesValue).Skip(0).Take(dto.AnalyseNumberCount).ToList();
            //var str = string.Join(",", dto.Numbers.Select(n=>n.Number));
            //允许的连续次数，由小到大
            for (var consecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes; consecutiveTimes <= dto.EndAllowMinFactorCurrentConsecutiveTimes; consecutiveTimes++)
            {
                //允许的间隔数，由大到小
                for (var interval = dto.StartAllowMaxInterval; interval >= dto.EndAllowMaxInterval; interval--)
                {
                    var resultCount = 0;
                    var successCount = 0;

                    var trend = new HistoricalTrend
                    {
                        HistoricalTrendType =dto.HistoricalTrendType,
                        StartTimes = analyseNumbers[0].TimesValue,
                        Items = new List<HistoricalTrendItem>(),
                        Location = dto.Location,
                        AllowConsecutiveTimes = consecutiveTimes,
                        AllowInterval = interval,
                        AnalyseNumberCount=dto.AnalyseNumberCount,
                        TypeDescription = dto.TypeDescription
                    };
                    trends.Add(trend);
                    for (int i = 0, maxCount = analyseNumbers.Count; i < maxCount; i++)
                    {
                        var number = analyseNumbers[i].Number;
                        var times = analyseNumbers[i].Times;
                        var timesValue = analyseNumbers[i].TimesValue;
                        var numbers = dto.Numbers.Where(n => n.TimesValue < timesValue).Select(n => n.Number).ToList();

                        var factorResults = Analyse(new MultiNumberFactorTrendAnalyseDto<byte>
                        {
                            Numbers = numbers,
                            Factors = dto.Factors,
                            AllowMinTimes = dto.AllowMinTimes,
                            NumbersTailCutCount = dto.NumbersTailCutCount,
                            AllowMinFactorCurrentConsecutiveTimes = consecutiveTimes,
                            AllowMaxInterval = interval,
                            AnalyseConsecutiveCompareFunc=dto.AnalyseConsecutiveCompareFunc,
                            PredictiveConsecutivesCompareFunc=dto.PredictiveConsecutivesCompareFunc,
                            PredictiveFactorAction=dto.PredictiveFactorAction,
                            MultiNumberMaxCount=dto.MultiNumberMaxCount
                        });

                        //结果是否正确
                        var success = false;

                        //对结果再分析
                        var factorResult = factorResults.OrderByDescending(t => t.FactorCurrentConsecutiveTimes).FirstOrDefault();
                        var factors = new List<byte>();
                        var resultConsecutiveTimes = 0;
                        var resultInterval = 0;
                        if (factorResult != null)
                        {
                            factors = factorResult.PredictiveFactor;
                            resultConsecutiveTimes = factorResult.FactorCurrentConsecutiveTimes;
                            resultInterval = factorResult.Interval;
                            if (factorResult.PredictiveFactor != null && factorResult.PredictiveFactor.Count > 0)
                            {
                                resultCount++;

                                if (factors.Contains(number))
                                {
                                    successCount++;
                                    success = true;
                                }
                            }
                        }

                        var trendItem = new HistoricalTrendItem
                        {
                            Times = times,
                            Number = number,
                            Success = success,
                            ResultConsecutiveTimes = resultConsecutiveTimes,
                            ResultInterval = resultInterval,
                            PredictiveFactor = factors
                        };

                        trend.AnalyticalCount = resultCount;
                        trend.CorrectCount = successCount;
                        trend.CorrectRate = trend.AnalyticalCount == 0 ? 0 : (double)trend.CorrectCount / trend.AnalyticalCount;
                        trend.Items.Add(trendItem);
                    }
                }
            }
            return trends;
        }

    }

}