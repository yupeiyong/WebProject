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
            var bags = new List<Bag>() { new Bag(2, 13),
                new Bag(1, 10), new Bag(3, 24), new Bag(2, 15),
                new Bag(4, 28), new Bag(5, 33), new Bag(3, 20),
                new Bag(1, 8) };
            const int totalWeight = 12;
            var problem = new Fzxj();
            var maxValue=problem.GetMaxValue(bags, totalWeight);
            Console.WriteLine("max value:{0}",maxValue);
            Console.ReadKey();
        }
    }

    public class Bag:IComparable<Bag>
    {

        public int Weight { get; set; }

        public int Value { get; set; }

        public int UnitValue { get { return Value / Weight; } }


        public Bag(int weight, int value)
        {
            this.Weight = weight;
            this.Value = value;
        }


        public int CompareTo(Bag y)
        {
            return y == null ? 1 : this.UnitValue.CompareTo(y.UnitValue);
        }

    }

    public class Fzxj
    {
        private List<Bag> _bags;
        private int _totalWeight;
        public Fzxj()
        {
        }


        public int GetMaxValue(List<Bag>bags,int totalWeight)
        {
            var maxValue = 0;
            _totalWeight = totalWeight;
            _bags = bags;
            _bags.Sort();
            var queue=new Queue<Node>();
           queue.Enqueue(new Node(0,0,0));
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                var index = node.Index;
                if (node.UpboundValue<maxValue ||node.Index>=_bags.Count) continue;
                var left=new Node(node.CurWeight+bags[index].Weight,node.CurValue+bags[index].Value,index+1);
                left.UpboundValue = GetUpboundValue(left);
                if (left.UpboundValue > maxValue && left.CurWeight <= totalWeight)
                {
                    queue.Enqueue(left);
                    if (left.CurValue > maxValue)
                    {
                        maxValue = left.CurValue;
                    }
                }
                var right=new Node(node.CurWeight,node.CurValue,index+1);
                right.UpboundValue = GetUpboundValue(right);
                if (right.UpboundValue >= maxValue)
                {
                    queue.Enqueue(right);
                }
            }
            return maxValue;
        }


        private int GetUpboundValue(Node node)
        {
            var i = node.Index;
            var balanceWeight = _totalWeight - node.CurWeight;
            var v = node.CurValue;
            var length = _bags.Count;
            while (i < length && _bags[i].Weight <= balanceWeight)
            {
                balanceWeight -= _bags[i].Weight;
                v += _bags[i].Value;
                i++;
            }
            if (i < length)
            {
                v += _bags[i].UnitValue * balanceWeight;
            }
            return v;
        }
        public class Node
        {
            public int CurWeight { get; set; }

            public int CurValue { get; set; }

            public int UpboundValue { get; set; }

            public int Index { get; set; }


            public Node(int curWeight, int curValue, int index)
            {
                this.CurValue = curValue;
                this.CurWeight = curWeight;
                this.Index = index;
            }
        }
    }
}
