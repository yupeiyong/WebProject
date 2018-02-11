using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendAnalysis.DataTransferObject;
using TrendAnalysis.Models.Trend;
using TrendAnalysis.Service.Trend;


namespace TrendAnalysis.Service.Test.Trend
{

    [TestClass]
    public class UnitTestPermutationFactorTrend
    {

        [TestMethod]
        public void TestPermutationFactorTrendAnalyseResult_Two_Element()
        {
            var fac1 = new Factor<int> { Left = new List<int> { 1, 2 }, Right = new List<int> { 3, 4 } };
            var fac2 = new Factor<int> { Left = new List<int> { 5, 6 }, Right = new List<int> { 7, 8 } };
            var ls = new List<Factor<int>> { fac1, fac2 };

            var lss = new List<List<Factor<int>>> { ls, ls };
            var result = PermutationFactorTrend.TraversePermutationFactor(lss);
            Assert.IsTrue(result.Count == 4 * 4);
        }


        [TestMethod]
        public void TestPermutationFactorTrendAnalyseResult_One_And_Two_Element()
        {
            var fac1 = new Factor<int> { Left = new List<int> { 1, 2 }, Right = new List<int> { 3, 4 } };
            var fac2 = new Factor<int> { Left = new List<int> { 5, 6 }, Right = new List<int> { 7, 8 } };
            var ls1 = new List<Factor<int>> { fac1 };
            var ls = new List<Factor<int>> { fac1, fac2 };

            var lss = new List<List<Factor<int>>> { ls1, ls };
            var result = PermutationFactorTrend.TraversePermutationFactor(lss);
            Assert.IsTrue(result.Count == 2 * 4);
        }


        [TestMethod]
        public void TestCountConsecutive()
        {
            var numbers = new List<byte>
            {
                1, 3, 6, 9, 1, 4, 2, 3, 1, 2, 5, 6, 8, 2, 3, 1
            };

            var factors = new List<List<byte>>
            {
                new List<byte> {1, 2},
                new List<byte> {3, 4}
            };

            var predictiveFactor = new List<byte> { 5, 6 };

            var cutCount = 6;
            var result = PermutationFactorTrend.CountConsecutive(numbers, factors, predictiveFactor, cutCount);

            //有两个连续次数
            Assert.IsTrue(result.HistoricalConsecutiveTimes.Count == 2);

            //连续次数＝1出现一次
            Assert.IsTrue(result.HistoricalConsecutiveTimes[1] == 1);

            //连续次数＝2出现一次
            Assert.IsTrue(result.HistoricalConsecutiveTimes[2] == 1);


            numbers = new List<byte>
            {
                1, 3, 9, 1, 3, 1, 3, 1, 3, 6, 9, 1, 4, 2, 3, 1, 4, 5, 6, 8, 2, 3, 1
            };

            result = PermutationFactorTrend.CountConsecutive(numbers, factors, predictiveFactor, cutCount);

            //有两个连续次数
            Assert.IsTrue(result.HistoricalConsecutiveTimes.Count == 2);

            //连续次数＝1出现一次
            Assert.IsTrue(result.HistoricalConsecutiveTimes[1] == 1);

            //连续次数＝3出现2次
            Assert.IsTrue(result.HistoricalConsecutiveTimes[3] == 2);



            numbers = new List<byte>
            {
                1, 3, 9, 1, 3, 1, 3, 1, 3, 6, 9, 1, 4, 2, 3, 1, 4,1, 4, 2, 3, 1, 4,1, 4, 2, 3, 1, 4, 5, 6, 8, 2, 3, 1
            };

            result = PermutationFactorTrend.CountConsecutive(numbers, factors, predictiveFactor, cutCount);

            //有3个连续次数
            Assert.IsTrue(result.HistoricalConsecutiveTimes.Count == 3);

            //连续次数＝1出现一次
            Assert.IsTrue(result.HistoricalConsecutiveTimes[1] == 1);

            //连续次数＝3出现一次
            Assert.IsTrue(result.HistoricalConsecutiveTimes[3] == 1);

            //连续次数＝9出现一次
            Assert.IsTrue(result.HistoricalConsecutiveTimes[9] == 1);

        }

        [TestMethod]
        public void TestCountCountConsecutivesByPermutationFactors()
        {
            var numbers = new List<byte>
            {
                1, 3, 6, 9, 1, 4, 2, 3, 1, 2, 5, 6, 8, 2, 3, 1
            };

            var fac1 = new Factor<byte> { Left = new List<byte> { 1, 2 }, Right = new List<byte> { 3, 4 } };
            var fac2 = new Factor<byte> { Left = new List<byte> { 5, 6 }, Right = new List<byte> { 7, 8 } };
            //var fac3 = new Factor<byte> { Left = new List<byte> { 1, 2 }, Right = new List<byte> { 3, 4 } };
            var permutationFactors = new List<List<Factor<byte>>> { new List<Factor<byte>> { fac1 }, new List<Factor<byte>> { fac2 } };


            var cutCount = 6;
            var results = PermutationFactorTrend.CountConsecutives(numbers, permutationFactors, cutCount);

            Assert.IsTrue(results.Count == 4);
            var result1 = results[0];
            Assert.IsTrue(result1.Factors.Count == 2);
            Assert.IsTrue(string.Join(",", result1.Factors[0]) == "1,2");
            Assert.IsTrue(string.Join(",", result1.Factors[1]) == "5,6");
            //有0个连续次数
            Assert.IsTrue(result1.HistoricalConsecutiveTimes.Count == 0);


            var result2 = results[1];
            Assert.IsTrue(result2.Factors.Count == 2);
            Assert.IsTrue(string.Join(",", result2.Factors[0]) == "1,2");
            Assert.IsTrue(string.Join(",", result2.Factors[1]) == "7,8");
            //有0个连续次数
            Assert.IsTrue(result2.HistoricalConsecutiveTimes.Count == 0);


            var result3 = results[2];
            Assert.IsTrue(result3.Factors.Count == 2);
            Assert.IsTrue(string.Join(",", result3.Factors[0]) == "3,4");
            Assert.IsTrue(string.Join(",", result3.Factors[1]) == "5,6");
            //有1个连续次数
            Assert.IsTrue(result3.HistoricalConsecutiveTimes.Count == 1);

            //连续次数＝1出现一次
            Assert.IsTrue(result3.HistoricalConsecutiveTimes[1] == 1);


            var result4 = results[3];
            Assert.IsTrue(result4.Factors.Count == 2);
            Assert.IsTrue(string.Join(",", result4.Factors[0]) == "3,4");
            Assert.IsTrue(string.Join(",", result4.Factors[1]) == "7,8");
            //有0个连续次数
            Assert.IsTrue(result4.HistoricalConsecutiveTimes.Count == 0);



            numbers = new List<byte>
            {
                1, 3, 9, 1, 3, 1, 3, 1, 3, 6, 4, 5, 4, 6, 3, 5, 4,5, 4, 2, 3, 1, 4,1, 4, 2, 3, 1, 4, 5, 6, 8, 2, 3, 1
            };

            results = results = PermutationFactorTrend.CountConsecutives(numbers, permutationFactors, cutCount);

            Assert.IsTrue(results.Count == 4);
            result1 = results[0];
            Assert.IsTrue(result1.Factors.Count == 2);
            Assert.IsTrue(string.Join(",", result1.Factors[0]) == "1,2");
            Assert.IsTrue(string.Join(",", result1.Factors[1]) == "5,6");
            //有0个连续次数
            Assert.IsTrue(result1.HistoricalConsecutiveTimes.Count == 0);


            result2 = results[1];
            Assert.IsTrue(result2.Factors.Count == 2);
            Assert.IsTrue(string.Join(",", result2.Factors[0]) == "1,2");
            Assert.IsTrue(string.Join(",", result2.Factors[1]) == "7,8");
            //有0个连续次数
            Assert.IsTrue(result2.HistoricalConsecutiveTimes.Count == 0);


            result3 = results[2];
            Assert.IsTrue(result3.Factors.Count == 2);
            Assert.IsTrue(string.Join(",", result3.Factors[0]) == "3,4");
            Assert.IsTrue(string.Join(",", result3.Factors[1]) == "5,6");
            //有1个连续次数
            Assert.IsTrue(result3.HistoricalConsecutiveTimes.Count == 1);

            //连续次数＝1出现一次
            Assert.IsTrue(result3.HistoricalConsecutiveTimes[5] == 1);


            result4 = results[3];
            Assert.IsTrue(result4.Factors.Count == 2);
            Assert.IsTrue(string.Join(",", result4.Factors[0]) == "3,4");
            Assert.IsTrue(string.Join(",", result4.Factors[1]) == "7,8");
            //有0个连续次数
            Assert.IsTrue(result4.HistoricalConsecutiveTimes.Count == 0);
        }


        [TestMethod]
        public void TestAnalyse_Empty()
        {
            var numbers = new List<byte>
            {
                1, 3, 6, 9, 1, 4, 2, 3, 1, 2, 5, 6, 8, 2, 3, 1
            };

            var fac1 = new Factor<byte> { Left = new List<byte> { 1, 2 }, Right = new List<byte> { 3, 4 } };
            var fac2 = new Factor<byte> { Left = new List<byte> { 5, 6 }, Right = new List<byte> { 7, 8 } };
            //var fac3 = new Factor<byte> { Left = new List<byte> { 1, 2 }, Right = new List<byte> { 3, 4 } };
            var permutationFactors = new List<List<Factor<byte>>> { new List<Factor<byte>> { fac1 }, new List<Factor<byte>> { fac2 } };


            var result = new PermutationFactorTrend().Analyse(new PermutationFactorTrendAnalyseDto<byte>
            {
                Numbers = numbers,
                PermutationFactors = permutationFactors,
                NumbersTailCutCount = 6
            });
            //没有符合条件
            Assert.IsTrue(result.Count == 0);
        }


        [TestMethod]
        public void TestAnalyse_ConsecutiveTimes_One()
        {
            var numbers = new List<byte>
            {
                1, 3, 6, 9, 1, 4, 2, 3, 1, 2, 5, 6, 8, 2, 3, 6,4
            };

            var fac1 = new Factor<byte> { Left = new List<byte> { 1, 2 }, Right = new List<byte> { 3, 4 } };
            var fac2 = new Factor<byte> { Left = new List<byte> { 5, 6 }, Right = new List<byte> { 7, 8 } };
            //var fac3 = new Factor<byte> { Left = new List<byte> { 1, 2 }, Right = new List<byte> { 3, 4 } };
            var permutationFactors = new List<List<Factor<byte>>> { new List<Factor<byte>> { fac1 }, new List<Factor<byte>> { fac2 } };


            var result = new PermutationFactorTrend().Analyse(new PermutationFactorTrendAnalyseDto<byte>
            {
                Numbers = numbers,
                PermutationFactors = permutationFactors,
                NumbersTailCutCount = 6
            });
            Assert.IsTrue(result.Count == 1);
            var result1 = result[0];
            Assert.IsTrue(result1.Factors.Count == 2);
            Assert.IsTrue(string.Join(",", result1.Factors[0]) == "3,4");
            Assert.IsTrue(string.Join(",", result1.Factors[1]) == "5,6");
            //有0个连续次数
            Assert.IsTrue(result1.HistoricalConsecutiveTimes.Count == 1);

            //连续次数＝1出现一次
            Assert.IsTrue(result1.HistoricalConsecutiveTimes[1] == 1);

            //历史连续次数＝1
            Assert.IsTrue(result1.FactorCurrentConsecutiveTimes == 2);
        }

        [TestMethod]
        public void TestAnalyse_ConsecutiveTimes_Three()
        {
            var numbers = new List<byte>
            {
                1, 3, 6, 9, 1, 4, 2, 3, 1, 2, 4, 6, 4, 5, 3
            };

            var fac1 = new Factor<byte> { Left = new List<byte> { 1, 2 }, Right = new List<byte> { 3, 4 } };
            var fac2 = new Factor<byte> { Left = new List<byte> { 5, 6 }, Right = new List<byte> { 7, 8 } };
            //var fac3 = new Factor<byte> { Left = new List<byte> { 1, 2 }, Right = new List<byte> { 3, 4 } };
            var permutationFactors = new List<List<Factor<byte>>> { new List<Factor<byte>> { fac1 }, new List<Factor<byte>> { fac2 } };


            var result = new PermutationFactorTrend().Analyse(new PermutationFactorTrendAnalyseDto<byte>
            {
                Numbers = numbers,
                PermutationFactors = permutationFactors,
                NumbersTailCutCount = 6
            });
            Assert.IsTrue(result.Count == 1);
            var result1 = result[0];
            Assert.IsTrue(result1.Factors.Count == 2);
            Assert.IsTrue(string.Join(",", result1.Factors[0]) == "3,4");
            Assert.IsTrue(string.Join(",", result1.Factors[1]) == "5,6");
            //有0个连续次数
            Assert.IsTrue(result1.HistoricalConsecutiveTimes.Count == 1);

            //连续次数＝1出现一次
            Assert.IsTrue(result1.HistoricalConsecutiveTimes[1] == 1);

            //历史连续次数＝1
            Assert.IsTrue(result1.FactorCurrentConsecutiveTimes == 3);
        }

        [TestMethod]
        public void TestAnalyse_ConsecutiveTimes_Four()
        {
            var numbers = new List<byte>
            {
                1, 3, 6, 9, 1, 4, 2, 3, 1, 3,5, 4, 6, 4, 5, 3
            };

            var fac1 = new Factor<byte> { Left = new List<byte> { 1, 2 }, Right = new List<byte> { 3, 4 } };
            var fac2 = new Factor<byte> { Left = new List<byte> { 5, 6 }, Right = new List<byte> { 7, 8 } };
            //var fac3 = new Factor<byte> { Left = new List<byte> { 1, 2 }, Right = new List<byte> { 3, 4 } };
            var permutationFactors = new List<List<Factor<byte>>> { new List<Factor<byte>> { fac1 }, new List<Factor<byte>> { fac2 } };


            var result = new PermutationFactorTrend().Analyse(new PermutationFactorTrendAnalyseDto<byte>
            {
                Numbers = numbers,
                PermutationFactors = permutationFactors,
                NumbersTailCutCount = 6
            });
            Assert.IsTrue(result.Count == 1);
            var result1 = result[0];
            Assert.IsTrue(result1.Factors.Count == 2);
            Assert.IsTrue(string.Join(",", result1.Factors[0]) == "3,4");
            Assert.IsTrue(string.Join(",", result1.Factors[1]) == "5,6");
            //有0个连续次数
            Assert.IsTrue(result1.HistoricalConsecutiveTimes.Count == 1);

            //连续次数＝1出现2次
            Assert.IsTrue(result1.HistoricalConsecutiveTimes[1] == 2);

            //历史连续次数＝1
            Assert.IsTrue(result1.FactorCurrentConsecutiveTimes == 4);
        }

    }

}