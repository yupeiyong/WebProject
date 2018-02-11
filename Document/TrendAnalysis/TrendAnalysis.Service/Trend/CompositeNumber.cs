using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace TrendAnalysis.Service.Trend
{

    /// <summary>
    ///     合数
    /// </summary>
    public class CompositeNumber
    {

        /// <summary>
        ///     结束数
        /// </summary>
        private readonly uint _endNumber;


        /// <summary>
        ///     开始数
        /// </summary>
        private readonly uint _startNumber;

        private SortedDictionary<uint, List<uint>> _dictCompositeNumber;


        /// <summary>
        ///     构造器
        /// </summary>
        /// <param name="stratNumber">开始数</param>
        /// <param name="endNumber">结束数</param>
        public CompositeNumber(uint stratNumber, uint endNumber)
        {
            _startNumber = stratNumber;
            _endNumber = endNumber;

            //生成合数
            CompositeNumbers = Create();
        }


        public List<uint> CompositeNumbers { get; private set; }


        /// <summary>
        ///     通过最大数，创建合数集合
        /// </summary>
        /// <returns></returns>
        private List<uint> Create()
        {
            if (_endNumber < _startNumber)
                throw new Exception("生成合数失败，最大数不能小于最小数！");

            _dictCompositeNumber = new SortedDictionary<uint, List<uint>>();
            var formatString = _endNumber.ToString();
            formatString = Regex.Replace(formatString, @"\d", "0");
            for (var i = _startNumber; i <= _endNumber; i++)
            {
                var numberString = i.ToString(formatString);
                var sum = (uint) numberString.ToList().Select(item => Convert.ToInt32(item.ToString())).Sum();
                if (_dictCompositeNumber.ContainsKey(sum))
                {
                    _dictCompositeNumber[sum].Add(i);
                }
                else
                {
                    _dictCompositeNumber.Add(sum, new List<uint> {i});
                }
            }
            return _dictCompositeNumber.Keys.ToList();
        }


        /// <summary>
        ///     通过合数，获取数字列表
        /// </summary>
        /// <param name="compositeNumber"></param>
        /// <returns></returns>
        public List<uint> GetNumbers(uint compositeNumber)
        {
            if (_dictCompositeNumber == null)
                throw new Exception("合数字典为空！");
            return _dictCompositeNumber[compositeNumber];
        }


        /// <summary>
        ///     通过合数，获取数字列表
        /// </summary>
        /// <param name="compositeNumbers"></param>
        /// <returns></returns>
        public List<uint> GetNumbers(List<uint> compositeNumbers)
        {
            if (_dictCompositeNumber == null)
                throw new Exception("合数字典为空！");

            var numbers = new List<uint>();
            foreach (var composite in compositeNumbers)
            {
                numbers.AddRange(_dictCompositeNumber[composite]);
            }
            return numbers.Distinct().OrderBy(n => n).ToList();
        }

    }

}