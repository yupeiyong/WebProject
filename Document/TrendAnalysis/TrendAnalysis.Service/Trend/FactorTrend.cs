using System;
using System.Collections.Generic;
using System.Linq;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.Models.Trend;


namespace TrendAnalysis.Service.Trend
{

    /// <summary>
    ///     因子的历史趋势
    /// </summary>
    public class FactorTrend
    {

        /// <summary>
        ///     分析
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<FactorTrendAnalyseResult<T>> Analyse<T>(FactorTrendAnalyseDto<T> dto)
        {
            var factorResults = AnalyseConsecutives(dto.Numbers, dto.Factors, dto.NumbersTailCutCount, dto.AllowMinTimes);
            factorResults = factorResults.Where(t => t.HistoricalConsecutiveTimes.Count > 0).ToList();
            foreach (var item in factorResults)
            {
                var times = 0;

                //记录集合倒序检查，因子是否包含当前号码
                for (var i = dto.Numbers.Count - 1; i >= 0; i--)
                {
                    if (!item.Factor.Contains(dto.Numbers[i]))
                        break;
                    times++;
                }

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
        /// <param name="numbers">记录</param>
        /// <param name="factors"></param>
        /// <param name="numbersTailCutCount"></param>
        /// <param name="allowMinTimes">允许的最小连续数，大于等于此数才记录</param>
        /// <returns></returns>
        public static List<FactorTrendAnalyseResult<T>> AnalyseConsecutives<T>(List<T> numbers, List<Factor<T>> factors, int numbersTailCutCount = 1, int allowMinTimes = 1)
        {
            var resultList = new List<FactorTrendAnalyseResult<T>>();
            foreach (var factor in factors)
            {
                if (factor.Left != null && factor.Left.Count > 0)
                {
                    resultList.Add(AnalyseConsecutive(numbers, factor.Left, factor.Right, numbersTailCutCount, allowMinTimes));
                }
                if (factor.Right != null && factor.Right.Count > 0)
                {
                    resultList.Add(AnalyseConsecutive(numbers, factor.Right, factor.Left, numbersTailCutCount, allowMinTimes));
                }
            }
            return resultList;
        }


        /// <summary>
        ///     解析连续在因子中的记录数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numbers">记录</param>
        /// <param name="factor">判断因子</param>
        /// <param name="predictiveFactor">反因子</param>
        /// <param name="numbersTailCutCount"></param>
        /// <param name="allowMinTimes">允许的最小连续数，大于等于此数才记录</param>
        /// <returns></returns>
        private static FactorTrendAnalyseResult<T> AnalyseConsecutive<T>(IReadOnlyList<T> numbers, List<T> factor, List<T> predictiveFactor, int numbersTailCutCount = 1, int allowMinTimes = 1)
        {
            var curResult = new FactorTrendAnalyseResult<T> {Factor = factor, PredictiveFactor = predictiveFactor, HistoricalConsecutiveTimes = new SortedDictionary<int, int>()};
            var i = 0;

            //连续次数
            var times = 0;
            var length = numbers.Count - numbersTailCutCount;
            while (i < length)
            {
                if (factor.Contains(numbers[i]))
                {
                    times++;
                }
                else
                {
                    if (curResult.HistoricalConsecutiveTimes.ContainsKey(times))
                    {
                        curResult.HistoricalConsecutiveTimes[times]++;
                    }
                    else if (times >= allowMinTimes)
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
            else if (times >= allowMinTimes)
            {
                curResult.HistoricalConsecutiveTimes.Add(times, 1);
            }
            return curResult;
        }


        /// <summary>
        ///     [准备废弃的代码] 分析一段日期的历史趋势，（通过号码集合分析历史趋势）
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Obsolete]
        public List<HistoricalTrend> AnalyseHistoricalTrend_Old(AnalyseHistoricalTrendDto<byte> dto)
        {
            var trends = new List<HistoricalTrend>();

            if (dto.Numbers.Count < dto.AnalyseNumberCount)
                throw new Exception("分析历史趋势时，分析记录数量不能大于记录数量！");

            var analyseNumbers = dto.Numbers.OrderByDescending(n => n.TimesValue).Skip(0).Take(dto.AnalyseNumberCount).ToList();

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
                        HistoricalTrendType = dto.HistoricalTrendType,
                        StartTimes = analyseNumbers[0].TimesValue,
                        Items = new List<HistoricalTrendItem>(),
                        Location = dto.Location,
                        AllowConsecutiveTimes = consecutiveTimes,
                        AllowInterval = interval,
                        AnalyseNumberCount = dto.AnalyseNumberCount,
                        TypeDescription = dto.TypeDescription
                    };
                    trends.Add(trend);
                    for (int i = 0, maxCount = analyseNumbers.Count; i < maxCount; i++)
                    {
                        var number = analyseNumbers[i].Number;
                        var times = analyseNumbers[i].Times;
                        var timesValue = analyseNumbers[i].TimesValue;
                        var numbers = dto.Numbers.Where(n => n.TimesValue < timesValue).Select(n => n.Number).ToList();

                        var factorResults = Analyse(new FactorTrendAnalyseDto<byte>
                        {
                            Numbers = numbers,
                            Factors = dto.Factors,
                            AllowMinTimes = dto.AllowMinTimes,
                            NumbersTailCutCount = dto.NumbersTailCutCount,
                            AllowMinFactorCurrentConsecutiveTimes = consecutiveTimes,
                            AllowMaxInterval = interval
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

                        var trendItem = new HistoricalTrendItem {Times = times, Number = number, Success = success, ResultConsecutiveTimes = resultConsecutiveTimes, ResultInterval = resultInterval, PredictiveFactor = factors};

                        trend.AnalyticalCount = resultCount;
                        trend.CorrectCount = successCount;
                        trend.CorrectRate = trend.AnalyticalCount == 0 ? 0 : (double) trend.CorrectCount/trend.AnalyticalCount;
                        trend.Items.Add(trendItem);
                    }
                }
            }
            return trends;
        }


        /// <summary>
        ///     分析一段日期的历史趋势，（通过号码集合分析历史趋势）
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<HistoricalTrend> AnalyseHistoricalTrend(AnalyseHistoricalTrendDto<byte> dto)
        {
            var trends = new List<HistoricalTrend>();

            if (dto.Numbers.Count < dto.AnalyseNumberCount)
                throw new Exception("分析历史趋势时，分析记录数量不能大于记录数量！");

            var analyseNumbers = dto.Numbers.OrderByDescending(n => n.TimesValue).Skip(0).Take(dto.AnalyseNumberCount).ToList();

            var factorResultDict = new Dictionary<int, List<FactorTrendAnalyseResult<byte>>>();

            //先记录分析结果
            for (int i = 0, maxCount = analyseNumbers.Count; i < maxCount; i++)
            {
                var timesValue = analyseNumbers[i].TimesValue;
                var numbers = dto.Numbers.Where(n => n.TimesValue < timesValue).Select(n => n.Number).ToList();

                var factorResults = Analyse(new FactorTrendAnalyseDto<byte>
                {
                    Numbers = numbers,
                    Factors = dto.Factors,
                    AllowMinTimes = dto.AllowMinTimes,
                    NumbersTailCutCount = dto.NumbersTailCutCount,
                    AllowMinFactorCurrentConsecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes,
                    AllowMaxInterval = dto.StartAllowMaxInterval
                });
                factorResultDict.Add(i, factorResults);
            }


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
                        HistoricalTrendType = dto.HistoricalTrendType,
                        StartTimes = analyseNumbers[0].TimesValue,
                        Items = new List<HistoricalTrendItem>(),
                        Location = dto.Location,
                        AllowConsecutiveTimes = consecutiveTimes,
                        AllowInterval = interval,
                        AnalyseNumberCount = dto.AnalyseNumberCount,
                        TypeDescription = dto.TypeDescription
                    };
                    trends.Add(trend);
                    for (int i = 0, maxCount = analyseNumbers.Count; i < maxCount; i++)
                    {
                        var number = analyseNumbers[i].Number;
                        var times = analyseNumbers[i].Times;

                        var factorResults = factorResultDict[i];

                        //结果是否正确
                        var success = false;

                        //对结果再分析
                        //1、按允许的最小因子当前连续次数和允许的最大间隔次数筛选
                        //2、先按最大连续次数然后按最小间隔次数排序
                        factorResults = factorResults
                            .Where(m => m.FactorCurrentConsecutiveTimes >= consecutiveTimes && m.Interval <= interval)
                            .OrderByDescending(t => t.FactorCurrentConsecutiveTimes)
                            .ThenBy(t => t.Interval).ToList();

                        var factorResult = factorResults.OrderByDescending(t => t.FactorCurrentConsecutiveTimes).FirstOrDefault();

                        //有符合条件的因子分析结果，才记录，减少无用数据以提高程序性能
                        if (factorResult == null) continue;
                        var resultConsecutiveTimes = factorResult.FactorCurrentConsecutiveTimes;
                        var resultInterval = factorResult.Interval;
                        if (factorResult.PredictiveFactor != null && factorResult.PredictiveFactor.Count > 0)
                        {
                            resultCount++;

                            if (factorResult.PredictiveFactor.Contains(number))
                            {
                                successCount++;
                                success = true;
                            }
                        }

                        var trendItem = new HistoricalTrendItem
                        {
                            Times = times,
                            Number = number,
                            Success = success,
                            ResultConsecutiveTimes = resultConsecutiveTimes,
                            ResultInterval = resultInterval,
                            PredictiveFactor = factorResult.PredictiveFactor
                        };
                        trend.Items.Add(trendItem);

                        /* 记录所有分析结果，包括条件为0的 [2018-1-18]
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


                        var trendItem = new HistoricalTrendItem { Times = times, Number = number, Success = success, ResultConsecutiveTimes = resultConsecutiveTimes, ResultInterval = resultInterval, PredictiveFactor = factors };
                        trend.Items.Add(trendItem);    
                     */
                    }
                    trend.AnalyticalCount = resultCount;
                    trend.CorrectCount = successCount;
                    trend.CorrectRate = trend.AnalyticalCount == 0 ? 0 : (double) trend.CorrectCount/trend.AnalyticalCount;
                }
            }
            return trends;
        }

    }

}