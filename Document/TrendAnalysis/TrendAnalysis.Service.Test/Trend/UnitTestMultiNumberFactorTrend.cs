using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendAnalysis.DataTransferObject.Trend;
using TrendAnalysis.Models.Trend;
using TrendAnalysis.Service.Trend;


namespace TrendAnalysis.Service.Test.Trend
{
    [TestClass]
    public class UnitTestMultiNumberFactorTrend
    {
        [TestMethod]
        public void TestAnalyseConsecutives()
        {
            var maxCount = 2;
            var dto = new MultiNumberFactorTrendAnalyseDto<byte>
            {
                Numbers = new List<byte>() { 1, 3, 9, 3, 8, 4, 7, 1, 9, 6, 4, 4, 6, 3, 2, 3, 6, 3, 3, 9, 2, 2, 4, 4, 0 },
                Factors = new List<Factor<byte>>() { new Factor<byte>() { Left = new List<byte>() { 1, 2 }, Right = new List<byte>() { 3, 4 } } },
                AllowMaxInterval = 0,
                AllowMinFactorCurrentConsecutiveTimes = 2,
                AllowMinTimes = 1,
                NumbersTailCutCount = 6,
                AnalyseConsecutiveCompareFunc = (nums, factors, i) =>
                {
                    var sum = nums[i] + nums[i - 1];
                    var curNumber = (byte)(sum % 10);
                    return factors.Contains(curNumber);
                },
                MultiNumberMaxCount = maxCount
            };
            var resultList = MultiNumberFactorTrend.AnalyseConsecutives(dto);
        }

        [TestMethod]
        public void TestAnalyse()
        {
            var maxCount = 2;
            var dto = new MultiNumberFactorTrendAnalyseDto<byte>
            {
                Numbers = new List<byte>() { 1, 3, 9, 3, 8, 4, 7, 1, 9, 6, 4, 4, 6, 3, 2, 3, 6, 3, 3, 9, 6, 5, 7, 4, 8 },
                Factors = new List<Factor<byte>>() { new Factor<byte>() { Left = new List<byte>() { 1, 2 }, Right = new List<byte>() { 3, 4 } } },
                AllowMaxInterval = 1,
                AllowMinFactorCurrentConsecutiveTimes = 2,
                AllowMinTimes = 1,
                NumbersTailCutCount = 6,
                AnalyseConsecutiveCompareFunc = (nums, factors, i) =>
                {
                    var sum = nums[i] + nums[i - 1];
                    var curNumber = (byte)(sum % 10);
                    return factors.Contains(curNumber);
                },
                MultiNumberMaxCount = maxCount,
                PredictiveConsecutivesCompareFunc = (nums, factors, i) =>
                 {
                     var sum = nums[i] + nums[i - 1];
                     var curNumber = (byte)(sum % 10);
                     return factors.Contains(curNumber);
                 },
                PredictiveFactorAction = (nums, factor) =>
                {
                    //反向累加
                    var currentSum = 0;
                    var around =0;
                    var lastIndex = nums.Count - 1;
                    for (var n = around; n >=0; n--)
                    {
                        currentSum += nums[lastIndex - n];
                    }
                    //取10的模
                    var sum = (byte)(currentSum % 10);

                    //当前因子数-累加数取10的模（分析数字可能区域）
                    for (var n = 0; n < factor.Count; n++)
                    {
                        factor[n] = (byte)((factor[n] + 10 - sum) % 10);
                    }
                }
            };
            var resultList = new MultiNumberFactorTrend().Analyse(dto);
        }

    }
}
