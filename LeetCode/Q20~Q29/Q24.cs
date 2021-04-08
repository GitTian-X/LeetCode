namespace LeetCode.Q20_Q29
{
    class Q24
    {
        //给定一个链表，两两交换其中相邻的节点，并返回交换后的链表。
        //你不能只是单纯的改变节点内部的值，而是需要实际的进行节点交换。
        public ListNode SwapPairs(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }
            ListNode newHead = head.next;
            head.next = SwapPairs(newHead.next);
            newHead.next = head;
            return newHead;
        }
    }
}
