using LeetCode.Q10_Q19;
using System;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Q18 q = new Q18();
            Console.WriteLine(q.FourSum(new int[] { 1, 0, -1, 0, -2, 2 }, 0));
            int a = 1, b = 2;
            a = b + (b = a) * 0;
            Console.WriteLine($"a:{a}     b:{b}");
        }
    }
}
