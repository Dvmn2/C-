namespace Lab4Part1
{
    /// <summary>
    /// Узел однонаправленного списка.
    /// </summary>
    public class SinglyLinkedNode
    {
        private int _value;
        private SinglyLinkedNode _next;

        public SinglyLinkedNode(int value)
        {
            _value = value;
            _next = null;
        }

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public SinglyLinkedNode Next
        {
            get { return _next; }
            set { _next = value; }
        }
    }
}
