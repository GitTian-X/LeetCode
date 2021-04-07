using System.Linq;

namespace LeetCode.Q20_Q29
{
    class Q23
    {
        //给你一个链表数组，每个链表都已经按升序排列。
        //请你将所有链表合并到一个升序链表中，返回合并后的链表。
        public ListNode MergeKLists(ListNode[] lists)
        {
            ListNode MergeK(ListNode[] lists, int left, int right)
            {
                if (left == right) return lists[left];
                if (left > right) return null;
                int mid = (left + right) / 2;
                return Merge2Lists(MergeK(lists, left, mid), MergeK(lists, mid + 1, right));
            }
            ListNode Merge2Lists(ListNode node1, ListNode node2)
            {
                ListNode dummy = new ListNode();
                ListNode newHead = dummy;
                while (node1 != null && node2 != null)
                {
                    if (node1.val <= node2.val)
                    {
                        dummy.next = node1;
                        dummy = dummy.next;
                        node1 = node1.next;
                    }
                    else
                    {
                        dummy.next = node2;
                        dummy = dummy.next;
                        node2 = node2.next;
                    }
                }
                //处理剩余的结点
                while (node1 != null)
                {
                    dummy.next = node1;
                    dummy = dummy.next;
                    node1 = node1.next;
                }
                while (node2 != null)
                {
                    dummy.next = node2;
                    dummy = dummy.next;
                    node2 = node2.next;
                }
                return newHead.next;
            }
            return MergeK(lists, 0, lists.Length - 1);
        }
    }
}
