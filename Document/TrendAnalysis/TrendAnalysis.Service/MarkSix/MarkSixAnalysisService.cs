using System;
using System.Collections.Generic;
using System.Linq;
using TrendAnalysis.Data;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.DataTransferObject.Trend;
using TrendAnalysis.Models;
using TrendAnalysis.Models.MarkSix;
using TrendAnalysis.Models.Trend;
using TrendAnalysis.Service.Trend;


namespace TrendAnalysis.Service.MarkSix
{

    public class MarkSixAnalysisService
    {

        //public static int FactorIndex { get; set; }

        #region  普通方法分析指定位置号码

        /// <summary>
        ///     分析指定位置号码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<byte> AnalyseSpecifiedLocation(MarkSixAnalyseSpecifiedLocationDto dto)
        {
            using (var dao = new TrendDbContext())
            {
                var source = dao.Set<MarkSixRecord>().AsQueryable();
                if (!string.IsNullOrWhiteSpace(dto.Times))
                {
                    var record = dao.Set<MarkSixRecord>().FirstOrDefault(m => m.Times == dto.Times);
                    if (record == null)
                        throw new Exception("错误，指定期次的记录不存在！");
                    source = source.Where(m => m.TimesValue < record.TimesValue);
                }

                //按期次值升序排列
                source = source.OrderBy(m => m.TimesValue);
                List<byte> numbers;
                switch (dto.Location)
                {
                    case 1:
                        numbers = source.Select(m => m.FirstNum).ToList();
                        break;
                    case 2:
                        numbers = source.Select(m => m.SecondNum).ToList();
                        break;
                    case 3:
                        numbers = source.Select(m => m.ThirdNum).ToList();
                        break;
                    case 4:
                        numbers = source.Select(m => m.FourthNum).ToList();
                        break;
                    case 5:
                        numbers = source.Select(m => m.FifthNum).ToList();
                        break;
                    case 6:
                        numbers = source.Select(m => m.SixthNum).ToList();
                        break;
                    case 7:
                        numbers = source.Select(m => m.SeventhNum).ToList();
                        break;
                    default:
                        throw new Exception("错误，指定的位置不是有效的号码位置！");
                }

                var factorHistoricalTrend = new FactorsCompareTrend();

                //十位数号码列表
                var tensDigitNumbers = numbers.Select(n => n.ToString("00").Substring(0, 1)).Select(byte.Parse).ToList();

                //十位因子
                var tensDigitFactors = FactorGenerator.Create(new List<byte> { 0, 1, 2, 3, 4 }.ToList());

                //按数字位置分析（十位/个位）
                //十位
                var tensDigitResult = factorHistoricalTrend.Analyse(new FactorTrendAnalyseDto<byte>
                {
                    Numbers = tensDigitNumbers,
                    Factors = tensDigitFactors,
                    AllowMinTimes = dto.TensAllowMinTimes,
                    NumbersTailCutCount = dto.TensNumbersTailCutCount,
                    AllowMinFactorCurrentConsecutiveTimes = dto.TensAllowMinFactorCurrentConsecutiveTimes,
                    AllowMaxInterval = dto.TensAllowMaxInterval
                });

                //个位数号码列表
                var onesDigitNumbers = numbers.Select(n => n.ToString("00").Substring(1)).Select(byte.Parse).ToList();

                //个位因子
                var onesDigitFactors = FactorGenerator.Create(new List<byte> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.ToList());

                //个位
                var onesDigitResult = factorHistoricalTrend.Analyse(new FactorTrendAnalyseDto<byte>
                {
                    Numbers = onesDigitNumbers,
                    Factors = onesDigitFactors,
                    AllowMinTimes = dto.OnesAllowMinTimes,
                    NumbersTailCutCount = dto.OnesNumbersTailCutCount,
                    AllowMinFactorCurrentConsecutiveTimes = dto.OnesAllowMinFactorCurrentConsecutiveTimes,
                    AllowMaxInterval = dto.OnesAllowMaxInterval
                });

                if (tensDigitResult.Count > 0 && onesDigitResult.Count > 0)
                {
                    //选择最多连续次数
                    var maxTens = tensDigitResult.OrderByDescending(t => t.FactorCurrentConsecutiveTimes).FirstOrDefault();
                    var maxOnes = onesDigitResult.OrderByDescending(t => t.FactorCurrentConsecutiveTimes).FirstOrDefault();
                    if (maxTens != null && maxOnes != null)
                    {
                        var tenFactor = maxTens.PredictiveFactor;
                        var onesFactor = maxOnes.PredictiveFactor;
                        return GetNumbers(tenFactor, onesFactor);
                    }
                }
                return new List<byte>();
            }
        }


        /// <summary>
        ///     分析一段时期指定位置号码个位数趋势
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<HistoricalTrend> AnalyseOnesHistoricalTrend(MarkSixAnalyseHistoricalTrendDto dto)
        {
            using (var dao = new TrendDbContext())
            {
                var source = dao.Set<MarkSixRecord>().AsQueryable();
                if (!string.IsNullOrWhiteSpace(dto.Times))
                {
                    var record = dao.Set<MarkSixRecord>().FirstOrDefault(m => m.Times == dto.Times);
                    if (record == null)
                        throw new Exception("错误，指定期次的记录不存在！");
                    source = source.Where(m => m.TimesValue < record.TimesValue);
                }

                //按期次值升序排列
                source = source.OrderBy(m => m.TimesValue);
                List<TemporaryRecord<byte>> records;
                switch (dto.Location)
                {
                    case 1:
                        records = source.Select(m => new { Number = m.FirstNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 2:
                        records = source.Select(m => new { Number = m.SecondNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 3:
                        records = source.Select(m => new { Number = m.ThirdNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 4:
                        records = source.Select(m => new { Number = m.FourthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 5:
                        records = source.Select(m => new { Number = m.FifthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 6:
                        records = source.Select(m => new { Number = m.SixthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 7:
                        records = source.Select(m => new { Number = m.SeventhNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    default:
                        throw new Exception("错误，指定的位置不是有效的号码位置！");
                }

                var factorHistoricalTrend = new FactorsCompareTrend();

                //个位因子
                var onesDigitFactors = FactorGenerator.Create(new List<byte> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.ToList());

                //onesDigitFactors = onesDigitFactors.Skip(FactorIndex * 20).Take(20).ToList();
                var trendDto = new AnalyseHistoricalTrendDto<byte>
                {
                    Numbers = records,
                    Factors = onesDigitFactors,
                    Location = dto.Location,
                    AnalyseNumberCount = dto.AnalyseNumberCount,
                    StartAllowMaxInterval = dto.StartAllowMaxInterval,
                    EndAllowMaxInterval = dto.EndAllowMaxInterval,
                    StartAllowMinFactorCurrentConsecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes,
                    EndAllowMinFactorCurrentConsecutiveTimes = dto.EndAllowMinFactorCurrentConsecutiveTimes,
                    AllowMinTimes = dto.AllowMinTimes,
                    NumbersTailCutCount = dto.NumbersTailCutCount,
                    HistoricalTrendType = HistoricalTrendTypeEnum.MarkSixOnesNormal
                };
                //先删除旧数据
                var removeDto = new HistoricalTrendServiceRemoveDto
                {
                    Location = dto.Location,
                    StartTimesValue = records[0].TimesValue,
                    EndTimesValue = records[records.Count - 1].TimesValue,
                    StartAllowConsecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes,
                    EndAllowConsecutiveTimes = dto.EndAllowMinFactorCurrentConsecutiveTimes,
                    StartAllowMaxInterval = dto.StartAllowMaxInterval,
                    EndAllowMaxInterval = dto.EndAllowMaxInterval,
                    HistoricalTrendType = HistoricalTrendTypeEnum.MarkSixOnesNormal
                };
                HistoricalTrendService.Remove(removeDto);
                var historicalTrends = factorHistoricalTrend.AnalyseHistoricalTrend(trendDto);

                //保存到数据库
                HistoricalTrendService.AddRange(historicalTrends);
                return historicalTrends;
            }
        }


        /// <summary>
        ///     分析一段时期指定位置号码十位数趋势
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<HistoricalTrend> AnalyseTensHistoricalTrend(MarkSixAnalyseHistoricalTrendDto dto)
        {
            using (var dao = new TrendDbContext())
            {
                var source = dao.Set<MarkSixRecord>().AsQueryable();
                if (!string.IsNullOrWhiteSpace(dto.Times))
                {
                    var record = dao.Set<MarkSixRecord>().FirstOrDefault(m => m.Times == dto.Times);
                    if (record == null)
                        throw new Exception("错误，指定期次的记录不存在！");
                    source = source.Where(m => m.TimesValue < record.TimesValue);
                }

                //按期次值升序排列
                source = source.OrderBy(m => m.TimesValue);
                var records = new List<TemporaryRecord<byte>>();
                switch (dto.Location)
                {
                    case 1:
                        records = source.Select(m => new { Number = m.FirstNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 2:
                        records = source.Select(m => new { Number = m.SecondNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 3:
                        records = source.Select(m => new { Number = m.ThirdNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 4:
                        records = source.Select(m => new { Number = m.FourthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 5:
                        records = source.Select(m => new { Number = m.FifthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 6:
                        records = source.Select(m => new { Number = m.SixthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 7:
                        records = source.Select(m => new { Number = m.SeventhNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    default:
                        throw new Exception("错误，指定的位置不是有效的号码位置！");
                }

                var factorHistoricalTrend = new FactorsCompareTrend();

                //十位因子
                var tensDigitFactors = FactorGenerator.Create(new List<byte> { 0, 1, 2, 3, 4 }.ToList());

                var trendDto = new AnalyseHistoricalTrendDto<byte>
                {
                    Numbers = records,
                    Factors = tensDigitFactors,
                    Location = dto.Location,
                    AnalyseNumberCount = dto.AnalyseNumberCount,
                    StartAllowMaxInterval = dto.StartAllowMaxInterval,
                    EndAllowMaxInterval = dto.EndAllowMaxInterval,
                    StartAllowMinFactorCurrentConsecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes,
                    EndAllowMinFactorCurrentConsecutiveTimes = dto.EndAllowMinFactorCurrentConsecutiveTimes,
                    AllowMinTimes = dto.AllowMinTimes,
                    NumbersTailCutCount = dto.NumbersTailCutCount,
                    HistoricalTrendType = HistoricalTrendTypeEnum.MarkSixTensNormal
                };
                //先删除旧数据
                var removeDto = new HistoricalTrendServiceRemoveDto
                {
                    Location = dto.Location,
                    StartTimesValue = records[0].TimesValue,
                    EndTimesValue = records[records.Count - 1].TimesValue,
                    StartAllowConsecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes,
                    EndAllowConsecutiveTimes = dto.EndAllowMinFactorCurrentConsecutiveTimes,
                    StartAllowMaxInterval = dto.StartAllowMaxInterval,
                    EndAllowMaxInterval = dto.EndAllowMaxInterval,
                    HistoricalTrendType = HistoricalTrendTypeEnum.MarkSixTensNormal
                };
                HistoricalTrendService.Remove(removeDto);


                var historicalTrends = factorHistoricalTrend.AnalyseHistoricalTrend(trendDto);

                //保存到数据库
                HistoricalTrendService.AddRange(historicalTrends);
                return historicalTrends;
            }
        }
        #endregion

        #region  排列因子分析指定位置号码

        /// <summary>
        ///     通过排列因子分析指定位置号码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<byte> AnalyseSpecifiedLocationByPermutationFactors(MarkSixAnalyseSpecifiedLocationDto dto)
        {
            using (var dao = new TrendDbContext())
            {
                var source = dao.Set<MarkSixRecord>().AsQueryable();
                if (!string.IsNullOrWhiteSpace(dto.Times))
                {
                    var record = dao.Set<MarkSixRecord>().FirstOrDefault(m => m.Times == dto.Times);
                    if (record == null)
                        throw new Exception("错误，指定期次的记录不存在！");
                    source = source.Where(m => m.TimesValue < record.TimesValue);
                }

                //按期次值升序排列
                source = source.OrderBy(m => m.TimesValue);
                List<byte> numbers;
                switch (dto.Location)
                {
                    case 1:
                        numbers = source.Select(m => m.FirstNum).ToList();
                        break;
                    case 2:
                        numbers = source.Select(m => m.SecondNum).ToList();
                        break;
                    case 3:
                        numbers = source.Select(m => m.ThirdNum).ToList();
                        break;
                    case 4:
                        numbers = source.Select(m => m.FourthNum).ToList();
                        break;
                    case 5:
                        numbers = source.Select(m => m.FifthNum).ToList();
                        break;
                    case 6:
                        numbers = source.Select(m => m.SixthNum).ToList();
                        break;
                    case 7:
                        numbers = source.Select(m => m.SeventhNum).ToList();
                        break;
                    default:
                        throw new Exception("错误，指定的位置不是有效的号码位置！");
                }

                //十位数号码列表
                var tensDigitNumbers = numbers.Select(n => n.ToString("00").Substring(0, 1)).Select(byte.Parse).ToList();

                //十位因子
                var tensDigitFactors = FactorGenerator.Create(new List<byte> { 0, 1, 2, 3, 4 }.ToList());
                var tensResults = new List<List<PermutationFactorTrendAnalyseResult<byte>>>();
                for (var i = 0; i < tensDigitFactors.Count; i++)
                {
                    var ls = new List<List<Factor<byte>>> { new List<Factor<byte>> { tensDigitFactors[i] }, tensDigitFactors };
                    var trend = new PermutationFactorTrend();
                    var curResult = trend.Analyse(new PermutationFactorTrendAnalyseDto<byte>
                    {
                        Numbers = tensDigitNumbers,
                        PermutationFactors = ls,
                        AllowMinTimes = dto.TensAllowMinTimes,
                        NumbersTailCutCount = dto.TensNumbersTailCutCount,
                        AllowMinFactorCurrentConsecutiveTimes = dto.TensAllowMinFactorCurrentConsecutiveTimes,
                        AllowMaxInterval = dto.TensAllowMaxInterval
                    });
                    if (curResult.Count > 0)
                    {
                        tensResults.Add(curResult);
                    }
                }


                //个位数号码列表
                var onesDigitNumbers = numbers.Select(n => n.ToString("00").Substring(1)).Select(byte.Parse).ToList();

                //个位因子
                var onesDigitFactors = FactorGenerator.Create(new List<byte> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.ToList());

                var onesResults = new List<List<PermutationFactorTrendAnalyseResult<byte>>>();
                for (var i = 0; i < onesDigitFactors.Count; i++)
                {
                    var ls = new List<List<Factor<byte>>> { new List<Factor<byte>> { onesDigitFactors[i] }, onesDigitFactors };
                    var trend = new PermutationFactorTrend();
                    var curResult = trend.Analyse(new PermutationFactorTrendAnalyseDto<byte>
                    {
                        Numbers = onesDigitNumbers,
                        PermutationFactors = ls,
                        AllowMinTimes = dto.OnesAllowMinTimes,
                        NumbersTailCutCount = dto.OnesNumbersTailCutCount,
                        AllowMinFactorCurrentConsecutiveTimes = dto.OnesAllowMinFactorCurrentConsecutiveTimes,
                        AllowMaxInterval = dto.OnesAllowMaxInterval
                    });
                    if (curResult.Count > 0)
                    {
                        onesResults.Add(curResult);
                    }
                }

                //if (tensDigitResult.Count > 0 && onesDigitResult.Count > 0)
                //{
                //    //选择最多连续次数
                //    var maxTens = tensDigitResult.OrderByDescending(t => t.FactorCurrentConsecutiveTimes).FirstOrDefault();
                //    var maxOnes = onesDigitResult.OrderByDescending(t => t.FactorCurrentConsecutiveTimes).FirstOrDefault();
                //    if (maxTens != null && maxOnes != null)
                //    {
                //        var tenFactor = maxTens.predictiveFactor;
                //        var onesFactor = maxOnes.predictiveFactor;
                //        return GetNumbers(tenFactor, onesFactor);
                //    }
                //}
                var test = onesResults.OrderBy(p => p.First().Interval).ThenByDescending(p => p.First().FactorCurrentConsecutiveTimes).ToList();
                return new List<byte>();
            }
        }

        /// <summary>
        ///     分析一段时期指定位置号码个位数趋势(用排列因子方法)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<HistoricalTrend> AnalyseOnesHistoricalTrendByPermutationFactors(MarkSixAnalyseHistoricalTrendDto dto)
        {
            List<TemporaryRecord<byte>> records;
            using (var dao = new TrendDbContext())
            {
                var source = dao.Set<MarkSixRecord>().AsQueryable();
                if (!string.IsNullOrWhiteSpace(dto.Times))
                {
                    var record = dao.Set<MarkSixRecord>().FirstOrDefault(m => m.Times == dto.Times);
                    if (record == null)
                        throw new Exception("错误，指定期次的记录不存在！");
                    source = source.Where(m => m.TimesValue < record.TimesValue);
                }

                //按期次值升序排列
                source = source.OrderBy(m => m.TimesValue);

                switch (dto.Location)
                {
                    case 1:
                        records = source.Select(m => new { Number = m.FirstNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 2:
                        records = source.Select(m => new { Number = m.SecondNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 3:
                        records = source.Select(m => new { Number = m.ThirdNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 4:
                        records = source.Select(m => new { Number = m.FourthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 5:
                        records = source.Select(m => new { Number = m.FifthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 6:
                        records = source.Select(m => new { Number = m.SixthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 7:
                        records = source.Select(m => new { Number = m.SeventhNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    default:
                        throw new Exception("错误，指定的位置不是有效的号码位置！");
                }
            }

            if(records==null||records.Count==0)
                throw new Exception("错误，号码记录为空！");

            var factorHistoricalTrend = new PermutationFactorTrend();

            //先删除旧数据
            var removeDto = new HistoricalTrendServiceRemoveDto
            {
                Location = dto.Location,
                StartTimesValue = records[0].TimesValue,
                EndTimesValue = records[records.Count - 1].TimesValue,
                StartAllowConsecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes,
                EndAllowConsecutiveTimes = dto.EndAllowMinFactorCurrentConsecutiveTimes,
                StartAllowMaxInterval = dto.StartAllowMaxInterval,
                EndAllowMaxInterval = dto.EndAllowMaxInterval,
                HistoricalTrendType = HistoricalTrendTypeEnum.MarkSixOnesPermutationFactor
            };

            HistoricalTrendService.Remove(removeDto);
            //个位因子
            var onesDigitFactors = FactorGenerator.Create(new List<byte> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.ToList());

            var onesResults = new List<HistoricalTrend>();
            for (var i = 0; i < onesDigitFactors.Count; i++)
            {
                var permutationFactors = new List<List<Factor<byte>>> { new List<Factor<byte>> { onesDigitFactors[i] }, onesDigitFactors };
                var trendDto = new PermutationFactorAnalyseHistoricalTrendDto<byte>
                {
                    Numbers = records,
                    PermutationFactors = permutationFactors,
                    Location = dto.Location,
                    AnalyseNumberCount = dto.AnalyseNumberCount,
                    StartAllowMaxInterval = dto.StartAllowMaxInterval,
                    EndAllowMaxInterval = dto.EndAllowMaxInterval,
                    StartAllowMinFactorCurrentConsecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes,
                    EndAllowMinFactorCurrentConsecutiveTimes = dto.EndAllowMinFactorCurrentConsecutiveTimes,
                    AllowMinTimes = dto.AllowMinTimes,
                    NumbersTailCutCount = dto.NumbersTailCutCount,
                    HistoricalTrendType = HistoricalTrendTypeEnum.MarkSixOnesPermutationFactor,
                    TypeDescription = $"排列因子:[{i}]0[1]*",//第一维排列因子只取索引位置为i的元素，第二维排列因子取所有元素
                };
                var historicalTrends = factorHistoricalTrend.AnalyseHistoricalTrend(trendDto);

                //保存到数据库
                HistoricalTrendService.AddRange(historicalTrends);
                //onesResults.AddRange(historicalTrends);
            }

            ////暂时只分析第一个因子
            //var permutationFactors= new List<List<Factor<byte>>> { new List<Factor<byte>> { onesDigitFactors[0] }, onesDigitFactors };
            //var trendDto = new PermutationFactorAnalyseHistoricalTrendDto<byte>
            //{
            //    Numbers = records,
            //    PermutationFactors = permutationFactors,
            //    Location = dto.Location,
            //    AnalyseNumberCount = dto.AnalyseNumberCount,
            //    StartAllowMaxInterval = dto.StartAllowMaxInterval,
            //    EndAllowMaxInterval = dto.EndAllowMaxInterval,
            //    StartAllowMinFactorCurrentConsecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes,
            //    EndAllowMinFactorCurrentConsecutiveTimes = dto.EndAllowMinFactorCurrentConsecutiveTimes,
            //    AllowMinTimes = dto.AllowMinTimes,
            //    NumbersTailCutCount = dto.NumbersTailCutCount,
            //    HistoricalTrendType = HistoricalTrendTypeEnum.MarkSixOnesPermutationFactor,
            //    TypeDescription = "[0]0[1]*"//第一维排列因子只取索引位置为0的元素，第二维排列因子取所有元素
            //};
            //var historicalTrends = factorHistoricalTrend.AnalyseHistoricalTrend(trendDto);

            return onesResults;
        }


        /// <summary>
        ///     分析一段时期指定位置号码十位数趋势(用排列因子方法)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<HistoricalTrend> AnalyseTensHistoricalTrendByPermutationFactors(MarkSixAnalyseHistoricalTrendDto dto)
        {
            using (var dao = new TrendDbContext())
            {
                var source = dao.Set<MarkSixRecord>().AsQueryable();
                if (!string.IsNullOrWhiteSpace(dto.Times))
                {
                    var record = dao.Set<MarkSixRecord>().FirstOrDefault(m => m.Times == dto.Times);
                    if (record == null)
                        throw new Exception("错误，指定期次的记录不存在！");
                    source = source.Where(m => m.TimesValue < record.TimesValue);
                }

                //按期次值升序排列
                source = source.OrderBy(m => m.TimesValue);
                var records = new List<TemporaryRecord<byte>>();
                switch (dto.Location)
                {
                    case 1:
                        records = source.Select(m => new { Number = m.FirstNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 2:
                        records = source.Select(m => new { Number = m.SecondNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 3:
                        records = source.Select(m => new { Number = m.ThirdNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 4:
                        records = source.Select(m => new { Number = m.FourthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 5:
                        records = source.Select(m => new { Number = m.FifthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 6:
                        records = source.Select(m => new { Number = m.SixthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 7:
                        records = source.Select(m => new { Number = m.SeventhNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    default:
                        throw new Exception("错误，指定的位置不是有效的号码位置！");
                }

                var factorHistoricalTrend = new PermutationFactorTrend();

                //十位因子
                var tensDigitFactors = FactorGenerator.Create(new List<byte> { 0, 1, 2, 3, 4 }.ToList());

                //var onesResults = new List<List<PermutationFactorTrendAnalyseResult<byte>>>();
                //for (var i = 0; i < onesDigitFactors.Count; i++)
                //{
                //    var ls = new List<List<Factor<byte>>> { new List<Factor<byte>> { onesDigitFactors[i] }, onesDigitFactors };
                //}

                //暂时只分析第一个因子
                var permutationFactors = new List<List<Factor<byte>>> { new List<Factor<byte>> { tensDigitFactors[0] }, tensDigitFactors };
                var trendDto = new PermutationFactorAnalyseHistoricalTrendDto<byte>
                {
                    Numbers = records,
                    PermutationFactors = permutationFactors,
                    Location = dto.Location,
                    AnalyseNumberCount = dto.AnalyseNumberCount,
                    StartAllowMaxInterval = dto.StartAllowMaxInterval,
                    EndAllowMaxInterval = dto.EndAllowMaxInterval,
                    StartAllowMinFactorCurrentConsecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes,
                    EndAllowMinFactorCurrentConsecutiveTimes = dto.EndAllowMinFactorCurrentConsecutiveTimes,
                    AllowMinTimes = dto.AllowMinTimes,
                    NumbersTailCutCount = dto.NumbersTailCutCount,
                    HistoricalTrendType = HistoricalTrendTypeEnum.MarkSixTensPermutationFactor,
                    TypeDescription = $"[0]0[1]*"//第一维排列因子只取索引位置为0的元素，第二维排列因子取所有元素
                };
                var historicalTrends = factorHistoricalTrend.AnalyseHistoricalTrend(trendDto);

                //保存到数据库
                HistoricalTrendService.AddRange(historicalTrends);
                return historicalTrends;
            }
        }
        #endregion

        #region  多号码结合分析指定位置号码
        /// <summary>
        ///     通过多号码结合分析指定位置号码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<byte> AnalyseSpecifiedLocationByMultiNumber(MarkSixAnalyseSpecifiedLocationDto dto)
        {
            using (var dao = new TrendDbContext())
            {
                var source = dao.Set<MarkSixRecord>().AsQueryable();
                if (!string.IsNullOrWhiteSpace(dto.Times))
                {
                    var record = dao.Set<MarkSixRecord>().FirstOrDefault(m => m.Times == dto.Times);
                    if (record == null)
                        throw new Exception("错误，指定期次的记录不存在！");
                    source = source.Where(m => m.TimesValue < record.TimesValue);
                }

                //按期次值升序排列
                source = source.OrderBy(m => m.TimesValue);
                List<byte> numbers;
                switch (dto.Location)
                {
                    case 1:
                        numbers = source.Select(m => m.FirstNum).ToList();
                        break;
                    case 2:
                        numbers = source.Select(m => m.SecondNum).ToList();
                        break;
                    case 3:
                        numbers = source.Select(m => m.ThirdNum).ToList();
                        break;
                    case 4:
                        numbers = source.Select(m => m.FourthNum).ToList();
                        break;
                    case 5:
                        numbers = source.Select(m => m.FifthNum).ToList();
                        break;
                    case 6:
                        numbers = source.Select(m => m.SixthNum).ToList();
                        break;
                    case 7:
                        numbers = source.Select(m => m.SeventhNum).ToList();
                        break;
                    default:
                        throw new Exception("错误，指定的位置不是有效的号码位置！");
                }

                var factorHistoricalTrend = new MultiNumberFactorTrend();

                //十位数号码列表
                var tensDigitNumbers = numbers.Select(n => n.ToString("00").Substring(0, 1)).Select(byte.Parse).ToList();

                //十位因子
                var tensDigitFactors = FactorGenerator.Create(new List<byte> { 0, 1, 2, 3, 4 }.ToList());

                //按数字位置分析（十位/个位）
                //十位
                //累加号码的偏移位置
                var offsetIndex = 1;
                //最多结合多少个号码
                var multiNumberMaxCount = 30;
                var tensDigitResult = factorHistoricalTrend.Analyse(new MultiNumberFactorTrendAnalyseDto<byte>
                {
                    Numbers = tensDigitNumbers,
                    Factors = tensDigitFactors,
                    AllowMinTimes = dto.TensAllowMinTimes,
                    NumbersTailCutCount = dto.TensNumbersTailCutCount,
                    AllowMinFactorCurrentConsecutiveTimes = dto.TensAllowMinFactorCurrentConsecutiveTimes,
                    AllowMaxInterval = dto.TensAllowMaxInterval,
                    AnalyseConsecutiveCompareFunc = (nums, factors, i) =>
                    {
                        var sum = nums[i] + nums[i - offsetIndex];
                        var curNumber = (byte)(sum % 5);
                        return factors.Contains(curNumber);
                    },
                    MultiNumberMaxCount = multiNumberMaxCount,
                    PredictiveConsecutivesCompareFunc = (nums, factors, i) =>
                    {
                        var sum = nums[i] + nums[i - offsetIndex];
                        var curNumber = (byte)(sum % 5);
                        return factors.Contains(curNumber);
                    },
                    PredictiveFactorAction = (nums, factor) =>
                    {
                        //反向累加
                        var currentSum = 0;
                        var lastIndex = nums.Count - 1;
                        currentSum += nums[lastIndex - (offsetIndex - 1)];
                        //取5的模
                        var sum = (byte)(currentSum % 5);

                        //当前因子数-累加数取5的模（分析数字可能区域）
                        for (var n = 0; n < factor.Count; n++)
                        {
                            factor[n] = (byte)((factor[n] + 5 - sum) % 5);
                        }
                    }
                });

                //个位数号码列表
                var onesDigitNumbers = numbers.Select(n => n.ToString("00").Substring(1)).Select(byte.Parse).ToList();

                //个位因子
                var onesDigitFactors = FactorGenerator.Create(new List<byte> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.ToList());

                //个位
                var onesDigitResult = factorHistoricalTrend.Analyse(new MultiNumberFactorTrendAnalyseDto<byte>
                {
                    Numbers = onesDigitNumbers,
                    Factors = onesDigitFactors,
                    AllowMinTimes = dto.TensAllowMinTimes,
                    NumbersTailCutCount = dto.TensNumbersTailCutCount,
                    AllowMinFactorCurrentConsecutiveTimes = dto.TensAllowMinFactorCurrentConsecutiveTimes,
                    AllowMaxInterval = dto.TensAllowMaxInterval,
                    AnalyseConsecutiveCompareFunc = (nums, factors, i) =>
                    {
                        var sum = nums[i] + nums[i - offsetIndex];
                        var curNumber = (byte)(sum % 10);
                        return factors.Contains(curNumber);
                    },
                    MultiNumberMaxCount = 30,
                    PredictiveConsecutivesCompareFunc = (nums, factors, i) =>
                    {
                        var sum = nums[i] + nums[i - offsetIndex];
                        var curNumber = (byte)(sum % 10);
                        return factors.Contains(curNumber);
                    },
                    PredictiveFactorAction = (nums, factor) =>
                    {
                        //反向累加
                        var currentSum = 0;
                        var lastIndex = nums.Count - 1;
                        currentSum += nums[lastIndex - (offsetIndex - 1)];
                        //取10的模
                        var sum = (byte)(currentSum % 10);

                        //当前因子数-累加数取10的模（分析数字可能区域）
                        for (var n = 0; n < factor.Count; n++)
                        {
                            factor[n] = (byte)((factor[n] + 10 - sum) % 10);
                        }
                    }
                });

                if (tensDigitResult.Count > 0 && onesDigitResult.Count > 0)
                {
                    //选择最多连续次数
                    var maxTens = tensDigitResult.OrderByDescending(t => t.FactorCurrentConsecutiveTimes).FirstOrDefault();
                    var maxOnes = onesDigitResult.OrderByDescending(t => t.FactorCurrentConsecutiveTimes).FirstOrDefault();
                    if (maxTens != null && maxOnes != null)
                    {
                        var tenFactor = maxTens.PredictiveFactor;
                        var onesFactor = maxOnes.PredictiveFactor;
                        return GetNumbers(tenFactor, onesFactor);
                    }
                }
                return new List<byte>();
            }
        }

        /// <summary>
        ///     分析一段时期指定位置号码个位数趋势(用多号码结合方法)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<HistoricalTrend> AnalyseOnesHistoricalTrendByMultiNumber(MarkSixAnalyseHistoricalTrendDto dto)
        {
            using (var dao = new TrendDbContext())
            {
                var source = dao.Set<MarkSixRecord>().AsQueryable();
                if (!string.IsNullOrWhiteSpace(dto.Times))
                {
                    var record = dao.Set<MarkSixRecord>().FirstOrDefault(m => m.Times == dto.Times);
                    if (record == null)
                        throw new Exception("错误，指定期次的记录不存在！");
                    source = source.Where(m => m.TimesValue < record.TimesValue);
                }

                //按期次值升序排列
                source = source.OrderBy(m => m.TimesValue);
                List<TemporaryRecord<byte>> records;
                switch (dto.Location)
                {
                    case 1:
                        records = source.Select(m => new { Number = m.FirstNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 2:
                        records = source.Select(m => new { Number = m.SecondNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 3:
                        records = source.Select(m => new { Number = m.ThirdNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 4:
                        records = source.Select(m => new { Number = m.FourthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 5:
                        records = source.Select(m => new { Number = m.FifthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 6:
                        records = source.Select(m => new { Number = m.SixthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 7:
                        records = source.Select(m => new { Number = m.SeventhNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    default:
                        throw new Exception("错误，指定的位置不是有效的号码位置！");
                }

                var factorHistoricalTrend = new MultiNumberFactorTrend();

                //个位因子
                var onesDigitFactors = FactorGenerator.Create(new List<byte> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.ToList());

                //累加号码的偏移位置
                var offsetIndex = 1;
                //最多结合多少个号码
                var multiNumberMaxCount = 30;
                var trendDto = new MultiNumberAnalyseHistoricalTrendDto<byte>
                {
                    Numbers = records,
                    Factors = onesDigitFactors,
                    Location = dto.Location,
                    AnalyseNumberCount = dto.AnalyseNumberCount,
                    StartAllowMaxInterval = dto.StartAllowMaxInterval,
                    EndAllowMaxInterval = dto.EndAllowMaxInterval,
                    StartAllowMinFactorCurrentConsecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes,
                    EndAllowMinFactorCurrentConsecutiveTimes = dto.EndAllowMinFactorCurrentConsecutiveTimes,
                    AllowMinTimes = dto.AllowMinTimes,
                    NumbersTailCutCount = dto.NumbersTailCutCount,
                    AnalyseConsecutiveCompareFunc = (nums, factors, i) =>
                    {
                        var sum = nums[i] + nums[i - offsetIndex];
                        var curNumber = (byte)(sum % 10);
                        return factors.Contains(curNumber);
                    },
                    MultiNumberMaxCount = multiNumberMaxCount,
                    PredictiveConsecutivesCompareFunc = (nums, factors, i) =>
                    {
                        var sum = nums[i] + nums[i - offsetIndex];
                        var curNumber = (byte)(sum % 10);
                        return factors.Contains(curNumber);
                    },
                    PredictiveFactorAction = (nums, factor) =>
                    {
                        //反向累加
                        var currentSum = 0;
                        var lastIndex = nums.Count - 1;
                        currentSum += nums[lastIndex - (offsetIndex - 1)];
                        //取10的模
                        var sum = (byte)(currentSum % 10);

                        //当前因子数-累加数取10的模（分析数字可能区域）
                        for (var n = 0; n < factor.Count; n++)
                        {
                            factor[n] = (byte)((factor[n] + 10 - sum) % 10);
                        }
                    },
                    HistoricalTrendType = HistoricalTrendTypeEnum.MarkSixOnesMultiNumber,
                    TypeDescription = "i+1"
                };
                var historicalTrends = factorHistoricalTrend.AnalyseHistoricalTrend(trendDto);

                //保存到数据库
                HistoricalTrendService.AddRange(historicalTrends);
                return historicalTrends;
            }
        }


        /// <summary>
        ///     分析一段时期指定位置号码十位数趋势(用多号码结合方法)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<HistoricalTrend> AnalyseTensHistoricalTrendByMultiNumber(MarkSixAnalyseHistoricalTrendDto dto)
        {
            using (var dao = new TrendDbContext())
            {
                var source = dao.Set<MarkSixRecord>().AsQueryable();
                if (!string.IsNullOrWhiteSpace(dto.Times))
                {
                    var record = dao.Set<MarkSixRecord>().FirstOrDefault(m => m.Times == dto.Times);
                    if (record == null)
                        throw new Exception("错误，指定期次的记录不存在！");
                    source = source.Where(m => m.TimesValue < record.TimesValue);
                }

                //按期次值升序排列
                source = source.OrderBy(m => m.TimesValue);
                var records = new List<TemporaryRecord<byte>>();
                switch (dto.Location)
                {
                    case 1:
                        records = source.Select(m => new { Number = m.FirstNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 2:
                        records = source.Select(m => new { Number = m.SecondNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 3:
                        records = source.Select(m => new { Number = m.ThirdNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 4:
                        records = source.Select(m => new { Number = m.FourthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 5:
                        records = source.Select(m => new { Number = m.FifthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 6:
                        records = source.Select(m => new { Number = m.SixthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 7:
                        records = source.Select(m => new { Number = m.SeventhNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = byte.Parse(m.Number.ToString("00").Substring(0, 1)), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    default:
                        throw new Exception("错误，指定的位置不是有效的号码位置！");
                }

                var factorHistoricalTrend = new MultiNumberFactorTrend();

                //十位因子
                var tensDigitFactors = FactorGenerator.Create(new List<byte> { 0, 1, 2, 3, 4 }.ToList());

                //累加号码的偏移位置
                var offsetIndex = 1;
                //最多结合多少个号码
                var multiNumberMaxCount = 30;

                var trendDto = new MultiNumberAnalyseHistoricalTrendDto<byte>
                {
                    Numbers = records,
                    Factors = tensDigitFactors,
                    Location = dto.Location,
                    AnalyseNumberCount = dto.AnalyseNumberCount,
                    StartAllowMaxInterval = dto.StartAllowMaxInterval,
                    EndAllowMaxInterval = dto.EndAllowMaxInterval,
                    StartAllowMinFactorCurrentConsecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes,
                    EndAllowMinFactorCurrentConsecutiveTimes = dto.EndAllowMinFactorCurrentConsecutiveTimes,
                    AllowMinTimes = dto.AllowMinTimes,
                    NumbersTailCutCount = dto.NumbersTailCutCount,
                    AnalyseConsecutiveCompareFunc = (nums, factors, i) =>
                    {
                        var sum = nums[i] + nums[i - offsetIndex];
                        var curNumber = (byte)(sum % 5);
                        return factors.Contains(curNumber);
                    },
                    MultiNumberMaxCount = multiNumberMaxCount,
                    PredictiveConsecutivesCompareFunc = (nums, factors, i) =>
                    {
                        var sum = nums[i] + nums[i - offsetIndex];
                        var curNumber = (byte)(sum % 5);
                        return factors.Contains(curNumber);
                    },
                    PredictiveFactorAction = (nums, factor) =>
                    {
                        //反向累加
                        var currentSum = 0;
                        var lastIndex = nums.Count - 1;
                        currentSum += nums[lastIndex - (offsetIndex - 1)];
                        //取10的模
                        var sum = (byte)(currentSum % 5);

                        //当前因子数-累加数取5的模（分析数字可能区域）
                        for (var n = 0; n < factor.Count; n++)
                        {
                            factor[n] = (byte)((factor[n] + 5 - sum) % 5);
                        }
                    },
                    HistoricalTrendType = HistoricalTrendTypeEnum.MarkSixTensMultiNumber
                };
                var historicalTrends = factorHistoricalTrend.AnalyseHistoricalTrend(trendDto);

                return historicalTrends;
            }
        }


        #endregion

        #region 合数方法分析指定位置号码
        /// <summary>
        ///     单独分析指定位置号码合数
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<HistoricalTrend> AnalyseCompositeHistoricalTrend(MarkSixAnalyseHistoricalTrendDto dto)
        {
            using (var dao = new TrendDbContext())
            {
                var source = dao.Set<MarkSixRecord>().AsQueryable();
                if (!string.IsNullOrWhiteSpace(dto.Times))
                {
                    var record = dao.Set<MarkSixRecord>().FirstOrDefault(m => m.Times == dto.Times);
                    if (record == null)
                        throw new Exception("错误，指定期次的记录不存在！");
                    source = source.Where(m => m.TimesValue < record.TimesValue);
                }

                //按期次值升序排列
                source = source.OrderBy(m => m.TimesValue);
                var records = new List<TemporaryRecord<byte>>();
                switch (dto.Location)
                {
                    case 1:
                        records = source.Select(m => new { Number = m.FirstNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = (byte)(byte.Parse(m.Number.ToString("00").Substring(0, 1)) + byte.Parse(m.Number.ToString("00").Substring(1))), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 2:
                        records = source.Select(m => new { Number = m.SecondNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = (byte)(byte.Parse(m.Number.ToString("00").Substring(0, 1)) + byte.Parse(m.Number.ToString("00").Substring(1))), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 3:
                        records = source.Select(m => new { Number = m.ThirdNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = (byte)(byte.Parse(m.Number.ToString("00").Substring(0, 1)) + byte.Parse(m.Number.ToString("00").Substring(1))), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 4:
                        records = source.Select(m => new { Number = m.FourthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = (byte)(byte.Parse(m.Number.ToString("00").Substring(0, 1)) + byte.Parse(m.Number.ToString("00").Substring(1))), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 5:
                        records = source.Select(m => new { Number = m.FifthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = (byte)(byte.Parse(m.Number.ToString("00").Substring(0, 1)) + byte.Parse(m.Number.ToString("00").Substring(1))), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 6:
                        records = source.Select(m => new { Number = m.SixthNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = (byte)(byte.Parse(m.Number.ToString("00").Substring(0, 1)) + byte.Parse(m.Number.ToString("00").Substring(1))), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    case 7:
                        records = source.Select(m => new { Number = m.SeventhNum, m.Times, m.TimesValue }).ToList()
                            .Select(m => new TemporaryRecord<byte> { Number = (byte)(byte.Parse(m.Number.ToString("00").Substring(0, 1)) + byte.Parse(m.Number.ToString("00").Substring(1))), Times = m.Times, TimesValue = m.TimesValue }).ToList();
                        break;
                    default:
                        throw new Exception("错误，指定的位置不是有效的号码位置！");
                }

                var factorHistoricalTrend = new FactorsCompareTrend();

                var compositeService = new CompositeNumber(1, 49);
                var compositeNumber = compositeService.CompositeNumbers.Select(n => (byte)n).ToList();

                //合数因子
                var compositeDigitFactors = FactorGenerator.Create(compositeNumber);

                var trendDto = new AnalyseHistoricalTrendDto<byte>
                {
                    Numbers = records,
                    Factors = compositeDigitFactors,
                    Location = dto.Location,
                    AnalyseNumberCount = dto.AnalyseNumberCount,
                    StartAllowMaxInterval = dto.StartAllowMaxInterval,
                    EndAllowMaxInterval = dto.EndAllowMaxInterval,
                    StartAllowMinFactorCurrentConsecutiveTimes = dto.StartAllowMinFactorCurrentConsecutiveTimes,
                    EndAllowMinFactorCurrentConsecutiveTimes = dto.EndAllowMinFactorCurrentConsecutiveTimes,
                    AllowMinTimes = dto.AllowMinTimes,
                    NumbersTailCutCount = dto.NumbersTailCutCount
                };
                var historicalTrends = factorHistoricalTrend.AnalyseHistoricalTrend(trendDto);

                return historicalTrends;
            }
        }

        #endregion



        /// <summary>
        ///     通过10位和个位因子，获取最终数字
        /// </summary>
        /// <param name="tenFactor"></param>
        /// <param name="onesFactor"></param>
        /// <returns></returns>
        private List<byte> GetNumbers(List<byte> tenFactor, List<byte> onesFactor)
        {
            var result = new List<byte>();
            for (var i = 0; i < tenFactor.Count; i++)
            {
                for (var j = 0; j < onesFactor.Count; j++)
                {
                    var valueStr = tenFactor[i] + onesFactor[j];
                    byte number;
                    if (!byte.TryParse(valueStr.ToString(), out number))
                    {
                        throw new Exception($"错误，{valueStr}不是有效的byte类型数据！");
                    }
                    result.Add(number);
                }
            }
            return result;
        }


        ///// <summary>
        ///// 分析列表个位数
        ///// </summary>
        ///// <param name="numbers">记录集合</param>
        ///// <param name="tensDigitFactors">比较因子</param>
        ///// <param name="allowMinTimes">允许的最小连续次数，大于等于此数才记录</param>
        ///// <returns></returns>
        //public List<FactorTrendAnalyseResult<byte>> AnalyseOnesDigit(List<byte> onesDigitNumbers, List<Factor<byte>> onesDigitFactors, int allowMinTimes, int numbersTailCutCount)
        //{
        //    List<FactorTrendAnalyseResult<byte>> onesDigitResult;
        //    if (numbersTailCutCount > 0 && numbersTailCutCount < onesDigitNumbers.Count)
        //    {
        //        var numbers = onesDigitNumbers.Skip(0).Take(onesDigitNumbers.Count - numbersTailCutCount).ToList();
        //        onesDigitResult = FactorAnalysis.AnalyseConsecutives(numbers, onesDigitFactors, allowMinTimes);
        //    }
        //    else
        //    {
        //        onesDigitResult = FactorAnalysis.AnalyseConsecutives(onesDigitNumbers, onesDigitFactors, allowMinTimes);
        //    }
        //    onesDigitResult = onesDigitResult.Where(t => t.HistoricalConsecutiveTimes.Count > 0).ToList();
        //    foreach (var item in onesDigitResult)
        //    {
        //        var times = 0;
        //        for (var i = onesDigitNumbers.Count - 1; i >= 0; i--)
        //        {
        //            if (!item.Factor.Contains(onesDigitNumbers[i]))
        //                break;
        //            times++;
        //        }
        //        item.FactorCurrentConsecutiveTimes = times;
        //    }
        //    //先按最大连续次数然后按最小间隔次数排序
        //    onesDigitResult = onesDigitResult
        //        .OrderByDescending(t => t.FactorCurrentConsecutiveTimes)
        //        .ThenBy(t => t.Interval).ToList();

        //    return onesDigitResult;
        //}


        ///// <summary>
        ///// 分析列表十位数
        ///// </summary>
        ///// <param name="numbers">记录集合</param>
        ///// <param name="tensDigitFactors">比较因子</param>
        ///// <param name="allowMinTimes">允许的最小连续次数，大于等于此数才记录</param>
        ///// <returns></returns>
        //public List<FactorTrendAnalyseResult<byte>> AnalyseTensDigit(List<byte> tensDigitNumbers, List<Factor<byte>> tensDigitFactors, int allowMinTimes, int numbersTailCutCount)
        //{
        //    List<FactorTrendAnalyseResult<byte>> tensDigitResult;
        //    if (numbersTailCutCount > 0 && tensDigitNumbers.Count > 0)
        //    {
        //        var numbers = tensDigitNumbers.Skip(0).Take(tensDigitNumbers.Count - numbersTailCutCount).ToList();
        //        tensDigitResult = FactorAnalysis.AnalyseConsecutives(numbers, tensDigitFactors, allowMinTimes);
        //    }
        //    else
        //    {
        //        tensDigitResult = FactorAnalysis.AnalyseConsecutives(tensDigitNumbers, tensDigitFactors, allowMinTimes);
        //    }
        //    tensDigitResult = tensDigitResult.Where(t => t.HistoricalConsecutiveTimes.Count > 0).ToList();
        //    foreach (var item in tensDigitResult)
        //    {
        //        var times = 0;
        //        for (var i = tensDigitNumbers.Count - 1; i >= 0; i--)
        //        {
        //            if (!item.Factor.Contains(tensDigitNumbers[i]))
        //                break;
        //            times++;
        //        }
        //        item.FactorCurrentConsecutiveTimes = times;
        //    }

        //    //先按最大连续次数然后按最小间隔次数排序
        //    tensDigitResult = tensDigitResult
        //        .OrderByDescending(t => t.FactorCurrentConsecutiveTimes)
        //        .ThenBy(t => t.Interval).ToList();

        //    return tensDigitResult;
        //}


        ///// <summary>
        ///// 分析合数
        ///// </summary>
        ///// <param name="compositeNumbers"></param>
        ///// <param name="factors"></param>
        ///// <param name="allowMinTimes"></param>
        ///// <param name="numbersTailCutCount"></param>
        ///// <returns></returns>
        //public List<FactorTrendAnalyseResult<byte>> AnalyseCompositeNumber(List<byte> compositeNumbers, List<Factor<byte>> factors, int allowMinTimes, int numbersTailCutCount)
        //{
        //    List<FactorTrendAnalyseResult<byte>> results;
        //    if (numbersTailCutCount > 0 && compositeNumbers.Count > 0)
        //    {
        //        var numbers = compositeNumbers.Skip(0).Take(compositeNumbers.Count - numbersTailCutCount).ToList();
        //        results = FactorAnalysis.AnalyseConsecutives(numbers, factors, allowMinTimes);
        //    }
        //    else
        //    {
        //        results = FactorAnalysis.AnalyseConsecutives(compositeNumbers, factors, allowMinTimes);
        //    }
        //    results = results.Where(t => t.HistoricalConsecutiveTimes.Count > 0).ToList();
        //    foreach (var item in results)
        //    {
        //        var times = 0;
        //        for (var i = compositeNumbers.Count - 1; i >= 0; i--)
        //        {
        //            if (!item.Factor.Contains(compositeNumbers[i]))
        //                break;
        //            times++;
        //        }
        //        item.FactorCurrentConsecutiveTimes = times;
        //    }

        //    //先按最大连续次数然后按最小间隔次数排序
        //    results = results
        //        .OrderByDescending(t => t.FactorCurrentConsecutiveTimes)
        //        .ThenBy(t => t.Interval).ToList();

        //    return results;
        //}

        ///// <summary>
        ///// 分析列表十位数(前后几期一起分析)
        ///// </summary>
        ///// <param name="numbers">记录集合</param>
        ///// <param name="tensDigitFactors">比较因子</param>
        ///// <param name="around">后面连续期次</param>
        ///// <param name="allowMinTimes">允许的最小连续次数，大于等于此数才记录</param>
        ///// <returns></returns>
        //public List<FactorTrendAnalyseResult<byte>> AnalyseTensDigitAround(List<byte> tensDigitNumbers, List<Factor<byte>> tensDigitFactors, int around, int allowMinTimes, int numbersTailCutCount)
        //{
        //    /*
        //     十位数相加组合
        //     0+0=0，0+1=1，0+2=2，0+3=3，0+4=4
        //     1+0=1，1+1=2，1+2=3，1+3=4，1+4=0
        //     2+0=2，2+1=3，2+2=4，2+3=0，2+4=1
        //     3+0=3，3+1=4，3+2=0，3+3=1，3+4=2
        //     4+0=4，4+1=0，4+2=1，4+3=2，4+4=3
        //     */
        //    //用于分析历史记录的比较因子的委托方法,参数为历史记录列表，因子列表和当前索引，返回结果为bool
        //    Func<IReadOnlyList<byte>, List<byte>, int, bool> compareFunc = (tenNumbers, factor, index) =>
        //     {
        //         var length = tenNumbers.Count;
        //         if (index > length - around)
        //         {
        //             return false;
        //         }
        //         var currentSum = 0;
        //         for (var i = 0; i < around; i++)
        //         {
        //             currentSum += tenNumbers[index + i];
        //         }
        //         //取5的模
        //         var currentItem = (byte)(currentSum % 5);
        //         var exists = factor.Exists(m => m.Equals(currentItem));
        //         return exists;
        //     };
        //    //用于预测当前期次的比较因子的委托方法,参数为历史记录列表，因子列表和当前索引，返回结果为bool
        //    Func<IReadOnlyList<byte>, List<byte>, int, bool> curTimesCompareFunc = (tenNumbers, factor, index) =>
        //   {
        //       var currentSum = 0;
        //       for (var i = 0; i < around; i++)
        //       {
        //           currentSum += tenNumbers[index - i];
        //       }
        //       //取5的模
        //       var sum = (byte)(currentSum % 5);
        //       return factor.Contains(sum);
        //   };
        //    //分析结果
        //    List<FactorTrendAnalyseResult<byte>> tensDigitResult;
        //    if (numbersTailCutCount > 0 && tensDigitNumbers.Count > 0)
        //    {
        //        var numbers = tensDigitNumbers.Skip(0).Take(tensDigitNumbers.Count - numbersTailCutCount).ToList();
        //        tensDigitResult = FactorAnalysis.Consecutives(numbers, tensDigitFactors, compareFunc, allowMinTimes);

        //    }
        //    else
        //    {
        //        tensDigitResult = FactorAnalysis.Consecutives(tensDigitNumbers, tensDigitFactors, compareFunc, allowMinTimes);
        //    }
        //    tensDigitResult = tensDigitResult.Where(t => t.HistoricalConsecutiveTimes.Count > 0).ToList();
        //    foreach (var item in tensDigitResult)
        //    {
        //        var times = 0;
        //        for (var i = tensDigitNumbers.Count - 1; i >= 0; i--)
        //        {
        //            if (!curTimesCompareFunc(tensDigitNumbers, item.Factor, i))
        //                break;
        //            times++;
        //        }
        //        item.FactorCurrentConsecutiveTimes = times;
        //    }
        //    tensDigitResult = tensDigitResult
        //        .OrderByDescending(t => t.FactorCurrentConsecutiveTimes)
        //        .ThenBy(t => t.Interval).ToList();

        //    return tensDigitResult;
        //}


        /// <summary>
        ///     分析指定位置当前期之前的号码
        /// </summary>
        /// <param name="location">指定位置</param>
        /// <param name="times">期次</param>
        /// <param name="beforeCount">之前多少期</param>
        /// <returns></returns>
        public List<AnalysisBeforeResult> AnalyseBeforeSpecifiedLocation(int location, string times, int beforeCount)
        {
            return null;
        }

    }

}