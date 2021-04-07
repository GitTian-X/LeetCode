namespace LeetCode.Q10_Q19
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
    }
    class Q19
    {
        //给你一个链表，删除链表的倒数第 n 个结点，并且返回链表的头结点。
        //进阶：你能尝试使用一趟扫描实现吗？
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            ListNode fast = head, slow = head;
            for (int i = 0; i < n; i++)
            {
                fast = fast.next;
            }
            if (fast == null)
            {
                return head.next;
            }
            while (fast.next != null)
            {
                fast = fast.next;
                slow = slow.next;
            }
            slow.next = slow.next?.next;
            return head;
        }
    }
}
