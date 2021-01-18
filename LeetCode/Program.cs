using System;

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
            Console.WriteLine(solution.NumDecodings("1123"));
        }
    }
}
