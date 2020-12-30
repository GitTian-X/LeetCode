using System;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();
            ListNode l1 = new ListNode(1);
            l1.next = new ListNode(2);
            l1.next.next = new ListNode(4);
            ListNode l2 = new ListNode(1);
            l2.next = new ListNode(3);
            l2.next.next = new ListNode(4);
            Console.WriteLine(solution.UniquePaths(3, 7));
        }
    }
}
