namespace LeetCode.Q20_Q29
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
    class Q21
    {
        //将两个升序链表合并为一个新的 升序 链表并返回。新链表是通过拼接给定的两个链表的所有节点组成的。 
        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            ListNode ans = new ListNode(0);
            ListNode dumy = ans;
            while (l1 != null && l2 != null)
            {
                if (l1.val < l2.val)
                {
                    dumy.next = new ListNode(l1.val);
                    l1 = l1.next;
                }
                else
                {
                    dumy.next = new ListNode(l2.val);
                    l2 = l2.next;
                }
                dumy = dumy.next;
            }
            if (l1 != null)
            {
                dumy.next = l1;
            }
            else if(l2 != null)
            {
                dumy.next = l2;
            }
            return ans.next;
        }
    }
}
