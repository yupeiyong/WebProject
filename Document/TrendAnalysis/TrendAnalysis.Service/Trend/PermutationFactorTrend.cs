using System;
using System.Collections.Generic;
using System.Linq;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.DataTransferObject.Trend;
using TrendAnalysis.Models.Trend;


namespace TrendAnalysis.Service.Trend
{

    /// <summary>
    ///     排列因子的历史趋势
    /// </summary>
    public class PermutationFactorTrend
    {

        /// <summary>
        ///     分析
        /// </summary>
        /// <param name="dto">记录集合、比较因子、允许的最小连续次数，大于等于此数才记录......</param>
        /// <returns></returns>
        public List<PermutationFactorTrendAnalyseResult<T>> Analyse<T>(PermutationFactorTrendAnalyseDto<T> dto)
        {
            //统计连续次数
            var factorResults = CountConsecutives(dto.Numbers, dto.PermutationFactors, dto.NumbersTailCutCount, dto.AllowMinTimes);
            factorResults = factorResults.Where(t => t.HistoricalConsecutiveTimes.Count > 0).ToList();
            foreach (var item in factorResults)
            {
                var times = 0;

                //因子列表数量
                var factorCount = item.Factors.Count;

                /*
                 根据连续次数来分析可能的趋势：
                 
                 第一种情况：
                 如：[{left:1,2,right:3,4},{left:5,6,right:7,8}]为因子列表,排列因子列表：[{1,2},{5,6}]取第一个排列因子的反因子为{3,4}
                 倒序的历史记录列表：6,2,5,2,6,1,5,7,8,5,6...
                 分析结果：
                 |6,2,|  |5,2,|  |6,1,| 5,7,8,5,6...
                 连续次数3次
                 可能的趋势是{3,4}

                 第二种情况：（当前方法采用的）
                 如：[{left:1,2,right:3,4},{left:5,6,right:7,8}]为因子列表,排列因子列表：[{1,2},{5,6}]取最后一个排列因子的反因子为{7,8}
                 倒序的历史记录列表：1,6,2,5,2,6,1,5,7,8,5,6...
                 分析结果：
                 1,| |6,2,|  |5,2,|  |6,1,| 5,7,8,5,6...
                 连续次数3次
                 可能的趋势是{7,8}

                 如果排列是多个，将不止有两种情况，结果应该=排列数
                 */

                //号码索引位置
                var i = dto.Numbers.Count - 1;

                //从因子列表中的倒数第二个开始遍历
                var m = factorCount - 2;
                for (; m >= 0 && i - m >= 0; m--, i--)
                {
                    if (!item.Factors[m].Contains(dto.Numbers[i]))
                        break;
                }

                //所有因子都包含，则次数递增
                if (m < 0)
                {
                    times++;
                }
                else
                {
                    continue;
                }

                //记录集合倒序检查，因子是否包含当前号码
                for (; i >= 0; i -= factorCount)
                {
                    var n = item.Factors.Count - 1;

                    //倒序遍历因子列表
                    //j是控制获取号码索引位置的指针
                    for (var j = 0; n >= 0 && i - j >= 0; n--, j++)
                    {
                        if (!item.Factors[n].Contains(dto.Numbers[i - j]))
                            break;
                    }

                    //所有因子都包含，则次数递增
                    if (n < 0)
                    {
                        times++;
                    }
                    else
                    {
                        break;
                    }
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
        ///     分析，分析所取的反因子，是排列因子列表的首个因子取反
        /// </summary>
        /// <param name="dto">记录集合、比较因子、允许的最小连续次数，大于等于此数才记录......</param>
        /// <returns></returns>
        public List<PermutationFactorTrendAnalyseResult<T>> AnalysepredictiveFactorAtFirst<T>(PermutationFactorTrendAnalyseDto<T> dto)
        {
            //统计连续次数
            var factorResults = CountConsecutives(dto.Numbers, dto.PermutationFactors, dto.NumbersTailCutCount, dto.AllowMinTimes);
            factorResults = factorResults.Where(t => t.HistoricalConsecutiveTimes.Count > 0).ToList();
            foreach (var item in factorResults)
            {
                var times = 0;

                //因子列表数量
                var factorCount = item.Factors.Count;

                /*
                 根据连续次数来分析可能的趋势：
                 
                 第一种情况：（当前方法采用的）
                 如：[{left:1,2,right:3,4},{left:5,6,right:7,8}]为因子列表,排列因子列表：[{1,2},{5,6}]取第一个排列因子的反因子为{3,4}
                 倒序的历史记录列表：6,2,5,2,6,1,5,7,8,5,6...
                 分析结果：
                 |6,2,|  |5,2,|  |6,1,| 5,7,8,5,6...
                 连续次数3次
                 可能的趋势是{3,4}

                 第二种情况：
                 如：[{left:1,2,right:3,4},{left:5,6,right:7,8}]为因子列表,排列因子列表：[{1,2},{5,6}]取最后一个排列因子的反因子为{7,8}
                 倒序的历史记录列表：1,6,2,5,2,6,1,5,7,8,5,6...
                 分析结果：
                 1,| |6,2,|  |5,2,|  |6,1,| 5,7,8,5,6...
                 连续次数3次
                 可能的趋势是{7,8}

                 如果排列是多个，将不止有两种情况，结果应该=排列数
                 */

                //记录集合倒序检查，因子是否包含当前号码
                for (var i = dto.Numbers.Count - 1; i >= 0; i -= factorCount)
                {
                    var n = item.Factors.Count - 1;

                    //倒序遍历因子列表
                    //j是控制获取号码索引位置的指针
                    for (var j = 0; n >= 0 && i - j >= 0; n--, j++)
                    {
                        if (!item.Factors[n].Contains(dto.Numbers[i - j]))
                            break;
                    }

                    //所有因子都包含，则次数递增
                    if (n < 0)
                    {
                        times++;
                    }
                    else
                    {
                        break;
                    }
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
        /// <param name="permutationFactors">排列因子</param>
        /// <param name="numbersTailCutCount">切去尾部数量，尾部不计入统计数量</param>
        /// <param name="allowMinTimes">允许的最小连续数，大于等于此数才记录</param>
        /// <returns></returns>
        public static List<PermutationFactorTrendAnalyseResult<T>> CountConsecutives<T>(List<T> numbers, List<List<Factor<T>>> permutationFactors, int numbersTailCutCount = 1, int allowMinTimes = 1)
        {
            #region 因子排列

            /*
             因子
             12 34
             13 24
             14 23

            排列因子（两层排列，2*6=12种组合）：
            1、12
               12
            2、12
               34
            3、12
               13
            4、12
               24
            5、12
               14
            6、12
               23
            7、34
               12
            8、34
               34
            9、34
               13
            10、34
                24
            11、34
                14
            12、34
                23
             */

            /*
             如果只有两层
             for(var i=0;i<arr[0].Count;i++)
             {
                 for(var j=0;j<arr[1].Count;j++)
                 {
                     1、var factors=new List<<List<T>>(){arr[0][i].Left,arr[1][j].Left}; 反因子：arr[1][j].Right
                     2、var factors=new List<<List<T>>(){arr[0][i].Left,arr[1][j].Right};反因子：arr[1][j].Left
                     3、var factors=new List<<List<T>>(){arr[0][i].Right,arr[1][j].Left}; 反因子：arr[1][j].Right
                     4、var factors=new List<<List<T>>(){arr[0][i].Right,arr[1][j].Right};反因子：arr[1][j].Left
                 }

            }
             
             */

            #endregion


            var factors = TraversePermutationFactor(permutationFactors);
            factors = factors.Distinct().ToList();
            var resultList = new List<PermutationFactorTrendAnalyseResult<T>>();
            foreach (var factor in factors)
            {
                if (factor != null && factor.Count > 0)
                {
                    //取保存在最后位置的反因子
                    var predictiveFactor = factor[factor.Count - 1];

                    //删除保存在最后位置的反因子
                    var curFactor = factor;
                    curFactor.RemoveAt(factor.Count - 1);
                    resultList.Add(CountConsecutive(numbers, curFactor, predictiveFactor, numbersTailCutCount, allowMinTimes));
                }
            }


            return resultList;
        }


        /// <summary>
        ///     解析因子在记录中的连续次数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numbers">记录</param>
        /// <param name="factors"></param>
        /// <param name="predictiveFactor">反因子</param>
        /// <param name="numbersTailCutCount"></param>
        /// <param name="allowMinTimes">允许的最小连续数，大于等于此数才记录</param>
        /// <returns></returns>
        public static PermutationFactorTrendAnalyseResult<T> CountConsecutive<T>(IReadOnlyList<T> numbers, List<List<T>> factors, List<T> predictiveFactor, int numbersTailCutCount = 1, int allowMinTimes = 1)
        {
            var curResult = new PermutationFactorTrendAnalyseResult<T>
            {
                Factors = factors,
                PredictiveFactor = predictiveFactor,
                HistoricalConsecutiveTimes = new SortedDictionary<int, int>()
            };
            var i = 0;

            //连续次数
            var times = 0;

            //统计记录的长度
            var length = numbers.Count - numbersTailCutCount;

            //遍历所有记录
            while (i < length)
            {
                /*
                排列因子：{1,2}{3,4}
                号码索引位置：1 2 3 4 5 6 7 8 9 10 11 12 13 
                号码：        6 1 3 5 2 3 2 4 1 0  0  1  1
                              0 1 2 0 1 2 1 2 1 0  0  1  1
                （1命中第一个因子，2命中第二个因子，只有同时命中1、2才能算一次，并且索引位置跳到下一位置，比如索引位置2，3，同时命中1，2，连续次数递增1）
                 
                 */
                var n = 0;
                for (; n < factors.Count && n + i < length; n++)
                {
                    if (!factors[n].Exists(m => m.Equals(numbers[n + i])))
                    {
                        break;
                    }
                }

                //排列因子全部相等
                if (n >= factors.Count)
                {
                    //连续次数递增
                    times++;

                    //索引位置调整，指向下一索引位置的前一条记录
                    i = i + factors.Count - 1;
                }
                else
                {
                    //是否有相同的连续次数，有则递增，否则新增一条连续次数记录
                    if (curResult.HistoricalConsecutiveTimes.ContainsKey(times))
                    {
                        curResult.HistoricalConsecutiveTimes[times]++;
                    }
                    else if (times >= allowMinTimes)
                    {
                        curResult.HistoricalConsecutiveTimes.Add(times, 1);
                    }

                    //连续次数清零，下一次重新统计
                    times = 0;
                }
                i++;
            }

            //是否有相同的连续次数，有则递增，否则新增一条连续次数记录
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


        #region  遍历因子

        /// <summary>
        ///     遍历排列因子，组装分析趋势的因子时，记录反因子按首个因子取反
        ///     比如：
        ///     { 1, 2 }, { 3, 4 },
        ///     排列结果：
        ///     1,3
        ///     1,4
        ///     2,3
        ///     2,4
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="permutationFactors">要遍历的排列因子，二维列表</param>
        /// <returns>遍历结果，总条数是每一行数据条数相乘的结果</returns>
        public static List<List<List<T>>> TraversePermutationFactorpredictiveFactorAtFirst<T>(List<List<Factor<T>>> permutationFactors)
        {
            var length = permutationFactors.Count;
            var result = new List<List<List<T>>>();

            //列表数组,最后一个元素为反因子
            var factors = new List<T>[length + 1];

            //每一因子索引位置数组，记录了相当每一行因子的位置
            var indexArray = new int[length];

            //记录每一因子遍历数量，记录了相当每一行遍历过的因子数量，因为每个因子有左右列表，所以每一行遍历数为每 一行元素数量*2
            var countArray = new int[length];
            var i = 0;
            while (i < length)
            {
                if (i < length - 1)
                {
                    var curLength = permutationFactors[i].Count;
                    if (indexArray[i] < curLength)
                    {
                        //取2的模如果=0，表示遍历到当前元素
                        if (countArray[i] % 2 == 0)
                        {
                            factors[i] = permutationFactors[i][indexArray[i]].Left;
                            if (i == 0)
                            {
                                //记录反因子到最后位置
                                factors[length] = permutationFactors[i][indexArray[i]].Right;
                            }
                        }
                        else
                        {
                            factors[i] = permutationFactors[i][indexArray[i]].Right;
                            if (i == 0)
                            {
                                //记录反因子到最后位置
                                factors[length] = permutationFactors[i][indexArray[i]].Left;
                            }

                            //可以遍历下一个元素
                            indexArray[i]++;
                        }
                        countArray[i]++;
                    }
                    else
                    {
                        if (i == 0) break;
                        indexArray[i] = 0;
                        i--;
                        continue;
                    }
                }
                else
                {
                    for (var j = 0; j < permutationFactors[i].Count; j++)
                    {
                        factors[i] = permutationFactors[i][j].Left;

                        result.Add(factors.ToList());

                        factors[i] = permutationFactors[i][j].Right;

                        result.Add(factors.ToList());
                    }
                    i--;
                    continue;
                }
                i++;
            }
            return result;
        }


        /// <summary>
        ///     遍历排列因子，组装分析趋势的因子时，记录反因子按最后因子取反
        ///     比如：
        ///     { 1, 2 }, { 3, 4 },
        ///     排列结果：
        ///     1,3
        ///     1,4
        ///     2,3
        ///     2,4
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="permutationFactors">要遍历的排列因子，二维列表</param>
        /// <returns>遍历结果，总条数是每一行数据条数相乘的结果</returns>
        public static List<List<List<T>>> TraversePermutationFactor<T>(List<List<Factor<T>>> permutationFactors)
        {
            var length = permutationFactors.Count;
            var result = new List<List<List<T>>>();

            //列表数组,最后一个元素为反因子
            var factors = new List<T>[length + 1];

            //每一因子索引位置数组，记录了相当每一行因子的位置
            var indexArray = new int[length];

            //记录每一因子遍历数量，记录了相当每一行遍历过的因子数量，因为每个因子有左右列表，所以每一行遍历数为每 一行元素数量*2
            var countArray = new int[length];
            var i = 0;
            while (i < length)
            {
                if (i < length - 1)
                {
                    var curLength = permutationFactors[i].Count;
                    if (indexArray[i] < curLength)
                    {
                        //取2的模如果=0，表示遍历到当前元素
                        if (countArray[i] % 2 == 0)
                        {
                            factors[i] = permutationFactors[i][indexArray[i]].Left;
                        }
                        else
                        {
                            factors[i] = permutationFactors[i][indexArray[i]].Right;

                            //可以遍历下一个元素
                            indexArray[i]++;
                        }
                        countArray[i]++;
                    }
                    else
                    {
                        if (i == 0) break;
                        indexArray[i] = 0;
                        i--;
                        continue;
                    }
                }
                else
                {
                    for (var j = 0; j < permutationFactors[i].Count; j++)
                    {
                        factors[i] = permutationFactors[i][j].Left;

                        //记录反因子
                        factors[i + 1] = permutationFactors[i][j].Right;
                        result.Add(factors.ToList());

                        factors[i] = permutationFactors[i][j].Right;

                        //记录反因子
                        factors[i + 1] = permutationFactors[i][j].Left;
                        result.Add(factors.ToList());
                    }
                    i--;
                    continue;
                }
                i++;
            }
            return result;
        }

        #endregion


        /// <summary>
        ///     分析一段日期的历史趋势，（通过号码集合分析历史趋势）
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Obsolete]
        public List<HistoricalTrend> AnalyseHistoricalTrend_Old(PermutationFactorAnalyseHistoricalTrendDto<byte> dto)
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

                        var factorResults = Analyse(new PermutationFactorTrendAnalyseDto<byte>
                        {
                            Numbers = numbers,
                            AllowMinTimes = dto.AllowMinTimes,
                            NumbersTailCutCount = dto.NumbersTailCutCount,
                            AllowMinFactorCurrentConsecutiveTimes = consecutiveTimes,
                            AllowMaxInterval = interval,
                            PermutationFactors = dto.PermutationFactors
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


        /// <summary>
        ///     分析一段日期的历史趋势，（通过号码集合分析历史趋势）
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<HistoricalTrend> AnalyseHistoricalTrend(PermutationFactorAnalyseHistoricalTrendDto<byte> dto)
        {
            var trends = new List<HistoricalTrend>();

            if (dto.Numbers.Count < dto.AnalyseNumberCount)
                throw new Exception("分析历史趋势时，分析记录数量不能大于记录数量！");

            var analyseNumbers = dto.Numbers.OrderByDescending(n => n.TimesValue).Skip(0).Take(dto.AnalyseNumberCount).ToList();
            var factorResultDict = new Dictionary<int, List<PermutationFactorTrendAnalyseResult<byte>>>();

            //先记录分析结果
            for (int i = 0, maxCount = analyseNumbers.Count; i < maxCount; i++)
            {
                var timesValue = analyseNumbers[i].TimesValue;
                var numbers = dto.Numbers.Where(n => n.TimesValue < timesValue).Select(n => n.Number).ToList();

                var factorResults = Analyse(new PermutationFactorTrendAnalyseDto<byte>
                {
                    Numbers = numbers,
                    PermutationFactors = dto.PermutationFactors,
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


                        /*  分析结果为也作记录
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

                        trend.Items.Add(trendItem);    
                     */
                    }
                    trend.AnalyticalCount = resultCount;
                    trend.CorrectCount = successCount;
                    trend.CorrectRate = trend.AnalyticalCount == 0 ? 0 : (double)trend.CorrectCount / trend.AnalyticalCount;
                }
            }
            return trends;
        }

    }

}