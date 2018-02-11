using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winform.Common
{
    public class ComboBoxItem<T>
    {
        public string Text { get; set; }

        public T Value { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
}
