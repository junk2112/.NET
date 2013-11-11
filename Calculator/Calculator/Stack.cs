using System;

namespace Calculator
{
    class Stack<T>
    {
        private const int DefaultSize = 50; 
        private T[] _array;
        private int _size, _capacity;

        public Stack ()
        {
            _capacity = DefaultSize;
            _array = new T[_capacity];
            _size = 0;
        }
        public Stack(T[] input) //передача параметра ссылочного типа input по значению
        {
            _size = input.Length;
            _capacity = _size * 2;
            _array = new T[_capacity];
            for (int i = 0; i < _size; ++i)
            {
                _array[i] = input[i];
            }
        }
        public int GetSize () 
        {
            return _size;
        }
        public T[] GetItems () 
        {
            var temp = new T[_size];
            Array.Copy(_array, temp, _size);
            return temp;
        }
        public void Push(T input)
        {
            if (++_size >= _capacity)
                Array.Resize(ref _array, _capacity = _size * 2);
            _array[_size - 1] = input;
        }
        
        public T Pop()
        {
            if (_size != 0)
                return _array[--_size];
            throw new IndexOutOfRangeException();
        }

        public T Top()
        {
            if (_size != 0)
                return _array[_size - 1];
            throw new IndexOutOfRangeException();
        }

        public void Print()
        {
            Console.WriteLine("Stack data is:");
            for (int i = 0; i < _size; ++i)
                Console.WriteLine(_array[i]);
        }

        public bool Empty()
        {
            return (_size == 0);
        }
    }
}
