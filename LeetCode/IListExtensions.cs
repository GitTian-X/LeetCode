using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    static class IListExtensions
    {
        public static List<int> SubList(this IList<int> lis, int index, int length)
        {
            List<int> ans = new List<int>();
            for (int i = 0; i < length; i++)
            {
                ans.Add(lis[i + index]);
            }
            return ans;
        }
    }
}
