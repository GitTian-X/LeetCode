using System;
using System.Collections.Generic;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();
            ListNode l1 = new ListNode(1);
            l1.next = new ListNode(1);
            l1.next.next = new ListNode(2);
            Console.WriteLine(solution.MaxProfit(2, new int[] { 2, 4,1}));
        }
    }
}
