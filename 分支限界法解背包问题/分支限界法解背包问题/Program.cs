using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 分支限界法解背包问题
{
    class Program
    {
        static void Main(string[] args)
        {
            var bags = new List<Knapsack>() { new Knapsack(2, 13),
                new Knapsack(1, 10), new Knapsack(3, 24), new Knapsack(2, 15),
                new Knapsack(4, 28), new Knapsack(5, 33), new Knapsack(3, 20),
                new Knapsack(1, 8) };
            int totalWeight = 12;
            var problem = new FZXJ();

            var maxValue=problem.GetBestValue(totalWeight, bags);
            Console.WriteLine("Max value:{0}", maxValue);
            Console.ReadKey();
        }
    }
    public class Knapsack : IComparable<Knapsack>
    {
        public int Weight { get; set; }

        public int Value { get; set; }

        public int UnitValue
        {
            get
            {
                return Value / Weight;
            }
        }
        public Knapsack(int weight,int value)
        {
            this.Weight = weight;
            this.Value = value;
        }
        public int CompareTo(Knapsack other)
        {
            int v = other.UnitValue;
            if (UnitValue > v) return 1;
            else if (UnitValue < v) return -1;
            return 0;
        }
    }

    public class FZXJ
    {
        private List<Knapsack> _knapsacks = null;

        public int TotalWeight { get; set; }

        private int MaxValue { get; set; }

        public FZXJ()
        {
        }

        public int GetBestValue(int totalWeight, List<Knapsack> knapsacks)
        {
            this.TotalWeight = totalWeight;
            this._knapsacks = knapsacks;
            _knapsacks.Sort();


            return solve();
        }
        private int solve()
        {
            var queue = new Queue<Node>();
            queue.Enqueue(new Node(0, 0, 0));
            var length = _knapsacks.Count;
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if(node.upboundValue>=MaxValue && node.index < length)
                {
                    var index = node.index;
                    var currentWeight = node.currWeight + _knapsacks[index].Weight;
                    var currentValue = node.currValue + _knapsacks[index].Value;
                    var left = new Node(currentWeight, currentValue, index + 1);
                    var upboundValue = GetUpboundValue(left);
                    if(upboundValue>MaxValue && left.currWeight <= TotalWeight)
                    {
                        queue.Enqueue(left);
                        if (left.currValue > MaxValue)
                        {
                            MaxValue = left.currValue;
                        }
                    }

                    var right = new Node(node.currWeight, node.currValue, index + 1);
                    upboundValue = GetUpboundValue(right);
                    if (right.upboundValue >= MaxValue)
                    {
                        queue.Enqueue(right);
                    }
                }
            }
            return MaxValue;
        }
        class Node
        {
            // 当前放入物品的重量  
            public int currWeight;
            // 当前放入物品的价值  
            public int currValue;
            // 不放入当前物品可能得到的价值上限  
            public int upboundValue;
            // 当前操作的索引  
            public int index;

            public Node(int currWeight, int currValue, int index)
            {
                this.currWeight = currWeight;
                this.currValue = currValue;
                this.index = index;
            }
        }
        private int GetUpboundValue(Node n)
        {
            var balanceWeight = TotalWeight - n.currWeight;
            var v = n.currValue;
            var i = n.index;
            while(i<_knapsacks.Count && _knapsacks[i].Weight <= balanceWeight)
            {
                balanceWeight -= _knapsacks[i].Weight;
                v += _knapsacks[i].Value;
                i++;
            }
            if (i < _knapsacks.Count)
            {
                v += _knapsacks[i].UnitValue * balanceWeight;
            }
            return v;
        }
    }
}
