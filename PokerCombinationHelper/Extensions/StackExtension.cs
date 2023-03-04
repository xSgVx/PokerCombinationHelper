using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameBase.Extensions
{
    public static class StackExtension
    {
        public static IEnumerable<T> PopRange<T>(this Stack<T> stack, int amount)
        {
            var result = new List<T>(amount);
            while (amount-- > 0 && stack.Count > 0)
            {
                result.Add(stack.Pop());
            }
            return result;
        }
    }
}
