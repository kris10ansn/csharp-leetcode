namespace P2;

public class ListNode
{
    public int val;
    public ListNode? next;

    public ListNode(int val = 0, ListNode? next = null)
    {
        this.val = val;
        this.next = next;
    }
}

#nullable enable

public class Solution
{
    public ListNode AddTwoNumbers(ListNode? l1, ListNode? l2)
    {
        ListNode result = new(-1);
        ListNode current = result;

        int carry = 0;

        while (l1 != null || l2 != null || carry > 0)
        {
            int sum = (l1?.val ?? 0) + (l2?.val ?? 0) + carry;

            int digit = sum % 10;
            carry = (sum - digit) / 10;

            current = AppendValue(current, digit);

            l1 = l1?.next;
            l2 = l2?.next;
        }

        return result;
    }

    private ListNode AppendValue(ListNode node, int value)
    {
        if (node.val == -1)
        {
            node.val = value;
            return node;
        }

        if (node.next == null)
        {
            node.next = new ListNode(value, null);
            return node.next;
        }

        throw new ArgumentException("Cannot append node to ListNode with value and next-pointer");
    }
}