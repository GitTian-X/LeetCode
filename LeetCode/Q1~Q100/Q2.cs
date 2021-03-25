using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Q1_Q100
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
        public ListNode(int x)
        {
            val = x;
            next = null;
        }
    }
    class Q2
    {
        /*
         * 给你两个 非空 的链表，表示两个非负的整数。它们每位数字都是按照 逆序 的方式存储的，并且每个节点只能存储 一位 数字。
         * 请你将两个数相加，并以相同形式返回一个表示和的链表。
         * 你可以假设除了数字 0 之外，这两个数都不会以 0 开头。
         */
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            return GetListNode(l1, l2, 0);
        }
        private ListNode GetListNode(ListNode l1, ListNode l2, int carry)
        {
            int sum = carry;
            sum += l1 == null ? 0 : l1.val;
            sum += l2 == null ? 0 : l2.val;
            if (l1 != null || l2 != null)
            {
                return new ListNode(sum % 10, GetListNode(l1?.next, l2?.next, sum / 10));
            }
            else
            {
                return carry > 0 ? new ListNode(1) : null;
            }
        }
    }
}
