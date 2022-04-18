using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_3
{
    public class Node<T>
    {
        public T Element { get; set; }
        public Node<T> Previous { get; set; }
        public Node<T> Next { get; set; }

        public Node(T element, Node<T> previous, Node<T> next)
        {
            Element = element;
            Previous = previous;
            Next = next;
        }

        public Node(T element):this(element, null, null) {}
        public Node() : this(default(T)) { }


    }
}
