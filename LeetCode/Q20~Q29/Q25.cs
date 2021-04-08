namespace LeetCode.Q20_Q29
{
    class Q25
    {
        //给你一个链表，每 k 个节点一组进行翻转，请你返回翻转后的链表。
        //k 是一个正整数，它的值小于或等于链表的长度。
        //如果节点总数不是 k 的整数倍，那么请将最后剩余的节点保持原有顺序。
        //进阶：
        //    你可以设计一个只使用常数额外空间的算法来解决此问题吗？
        //    你不能只是单纯的改变节点内部的值，而是需要实际进行节点交换。
        public ListNode ReverseKGroup(ListNode head, int k)
        {
            ListNode Reverse(ListNode head)
            {
                ListNode pre = null;
                ListNode curr = head;
                while (curr != null)
                {
                    ListNode next = curr.next;
                    curr.next = pre;
                    pre = curr;
                    curr = next;
                }
                return pre;
            }
            ListNode dummy = new ListNode(0);
            dummy.next = head;
            ListNode pre = dummy;
            ListNode end = dummy;
            while (end.next != null)
            {
                for (int i = 0; i < k && end != null; i++)
                {
                    end = end.next;
                }
                if (end == null)
                {
                    break;
                }
                ListNode start = pre.next;
                ListNode next = end.next;
                end.next = null;
                pre.next = Reverse(start);
                start.next = next;
                pre = start;
                end = pre;
            }
            return dummy.next;
        }
    }
}
