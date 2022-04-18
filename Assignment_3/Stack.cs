using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    class Stack<T>: ICloneable
    {

        public int Size { get; private set; }
        public Node<T> Head { get; private set; }
        public Stack()
        {
            Size = 0;
            Head = null;
        }

        public void Push(T element)
        {
            Size++;
            Head = new Node<T>(element, Head, null);
        }
        public T Top()
        {
            BadEmpty();
            return Head.Element;
        }

        public T Pop()
        {
            BadEmpty();
            Node<T> oldHead = Head;
            Head = oldHead.Previous;
            Size--;
            return oldHead.Element;
        }

        public void Clear()
        {
            Size = 0;
            Head = null;
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

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}


/// This was the stack used for test 2
//public class Stack<T>
//{
//    T[] data;
//    int head;

//    public int Size;

//    public Stack()
//    {
//        data = new T[100];
//        Size = 0;
//        head = 0;
//    }

//    public bool IsEmpty()
//    {
//        return Size == 0;
//    }

//    public T Top()
//    {
//        if (IsEmpty())
//        {
//            throw new Exception();
//        }
//        return data[head - 1];
//    }

//    public void Push(T element)
//    {
//        data[head] = element;
//        head++;
//        Size++;
//    }

//    public T Pop()
//    {
//        T oldHead = Top();
//        T[] newData = new T[100];
//        head--;

//        if (head == 0)
//            throw new ArgumentOutOfRangeException();
//        for (int i = 0; i < head; i++)
//        {
//            newData[i] = data[i];
//        }       
//        Size --;

//        data = newData;

//        return oldHead;
//    }

//}



