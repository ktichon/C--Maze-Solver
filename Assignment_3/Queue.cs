using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    class Queue<T>
    {
        public int Size { get; private set; }
        public Node<T> Head { get; private set; }
        public Node<T> Tail { get; private set; }

        public Queue()
        {
            Clear();
        }

        public void Enqueue(T element)
        {
            Node<T> newNode = new Node<T>(element);
            if (Tail == null)
                Head = newNode;
            else
                 Tail.Next = newNode;
            Tail = newNode;
            Size++;
        }

        public T Front()
        {
            BadEmpty();
            return Head.Element;
        }

        public T Dequeue()
        {
            BadEmpty();
            T oldElement = Head.Element;
            if (Size == 1)
                Clear();
            else
            {
                Head = Head.Next;
                Size--;
            }
                


            return oldElement;
        }

        public void Clear()
        {
            Size = 0;
            Head = null;
            Tail = null;
        }
        
        
        public bool IsEmpty()
        {
            return Size == 0;
        }

        private void BadEmpty()
        {
            if (IsEmpty())
                throw new ApplicationException();
        }
    }
}
