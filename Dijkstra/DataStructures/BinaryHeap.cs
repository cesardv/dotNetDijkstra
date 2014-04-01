namespace Dijkstra.DataStructures
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /*
     * The following c# binary heap implementation is adapted from http://content.gpwiki.org/index.php/C_sharp:BinaryHeapOfT
     * 
     *      
     */

    /// <summary>
    /// A binary heap, useful for sorting data and priority queues.
    /// </summary>
    /// <typeparam name="T"><![CDATA[IComparable<T> type of item in the heap]]>.</typeparam>
    public class BinaryHeap<T> : ICollection<T> where T : IComparable<T>
    {
        // Constants
        private const int DEFAULT_SIZE = 4;
        // Fields
        private T[] _data = new T[DEFAULT_SIZE];
        private int _count = 0;
        private int _capacity = DEFAULT_SIZE;
        private bool _sorted;
 

        // Properties

        /// <summary>
        /// Gets the number of values in the heap. 
        /// </summary>
        public int Count
        {
            get { return _count; }
        }
        /// <summary>
        /// Gets or sets the capacity of the heap.
        /// </summary>
        public int Capacity
        {
            get { return _capacity; }
            set
            {
                int previousCapacity = _capacity;
                _capacity = Math.Max(value, _count);
                if (_capacity != previousCapacity)
                {
                    T[] temp = new T[_capacity];
                    Array.Copy(_data, temp, _count);
                    _data = temp;
                }
            }
        }
        
        // Methods

        /// <summary>
        /// Initializes a new binary heap with no arguments passed.
        /// </summary>
        public BinaryHeap()
        {
        }

        /// <summary>
        /// Initializes a new binary heap with some data passed.
        /// </summary>
        private BinaryHeap(T[] data, int count)
        {
            this.Capacity = count;
            this._count = count;
            Array.Copy(data, this._data, count);
        }

        /// <summary>
        /// Gets the first value in the heap without removing it.
        /// </summary>
        /// <returns>The lowest value of type TValue.</returns>
        public T Peek()
        {
            return this._data[0];
        }
 
        /// <summary>
        /// Removes all items from the heap.
        /// </summary>
        public void Clear()
        {
            this._count = 0;
            this._data = new T[this._capacity];
        }
        /// <summary>
        /// Adds a key and value to the heap.
        /// </summary>
        /// <param name="item">The item to add to the heap.</param>
        public void Add(T item)
        {
            if (this._count == this._capacity)
            {
                this.Capacity *= 2;
            }

            this._data[this._count] = item;
            this.UpHeap();
            this._count++;
        }
 
        /// <summary>
        /// Removes and returns the first item in the heap.
        /// </summary>
        /// <returns>The next value in the heap.</returns>
        public T Remove()
        {
            if (this._count == 0)
            {
                throw new InvalidOperationException("Cannot remove item, heap is empty.");
            }
            
            T v = _data[0];
            
            this._count--;
            this._data[0] = this._data[this._count];
            this._data[this._count] = default(T); // Clears the Last Node
            this.DownHeap();
            return v;
        }

        /// <summary>
        /// helper function that performs up-heap bubbling
        /// </summary>
        private void UpHeap()
        {
            _sorted = false;
            int p = this._count;
            T item = this._data[p];
            int par = Parent(p);
            while (par > -1 && item.CompareTo(this._data[par]) < 0)
            {
                this._data[p] = this._data[par]; // Swap nodes
                p = par;
                par = Parent(p);
            }

            this._data[p] = item;
        }


        /// <summary>
        /// helper function that performs down-heap bubbling
        /// </summary>
        private void DownHeap()
        {
            this._sorted = false;
            int n;
            int p = 0;
            T item = this._data[p];
            
            while (true)
            {
                int ch1 = Child1(p);
                
                if (ch1 >= this._count)
                {
                    break;
                }

                int ch2 = Child2(p);
                
                if (ch2 >= this._count)
                {
                    n = ch1;
                }
                else
                {
                    n = _data[ch1].CompareTo(_data[ch2]) < 0 ? ch1 : ch2;
                }

                if (item.CompareTo(this._data[n]) > 0)
                {
                    this._data[p] = this._data[n]; //Swap nodes
                    p = n;
                }
                else
                {
                    break;
                }
            }

            this._data[p] = item;
        }

        private void EnsureSort()
        {
            if (_sorted) return;
            Array.Sort(_data, 0, _count);
            _sorted = true;
        }

        /// <summary>
        /// helper function that calculates the parent of a node
        /// </summary>
        /// <param name="index">Index of the node of which we want the parent</param>
        /// <returns>Index of the parent node</returns>
        private static int Parent(int index)
        {
            return (index - 1) >> 1;
        }

        /// <summary>
        /// helper function that calculates the first child of a node
        /// </summary>
        /// <param name="index">Index of the node of which we want the child</param>
        /// <returns>Index of the left child</returns>
        private static int Child1(int index)
        {
            return (index << 1) + 1;
        }

        /// <summary>
        /// helper function that calculates the second child of a node
        /// </summary>
        /// <param name="index">Index of the node of which we want the child</param>
        /// <returns>Index of the right child</returns>
        private static int Child2(int index)
        {
            return (index << 1) + 2;
        }
 
        /// <summary>
        /// Creates a new instance of an identical binary heap.
        /// </summary>
        /// <returns>A BinaryHeap.</returns>
        public BinaryHeap<T> Copy()
        {
            return new BinaryHeap<T>(this._data, this._count);
        }
 
        /// <summary>
        /// Gets an enumerator for the binary heap.
        /// </summary>
        /// <returns>An IEnumerator of type T.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            EnsureSort();
            for (int i = 0; i < this._count; i++)
            {
                yield return this._data[i];
            }
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
 
        /// <summary>
        /// Checks to see if the binary heap contains the specified item.
        /// </summary>
        /// <param name="item">The item to search the binary heap for.</param>
        /// <returns>A boolean, true if binary heap contains item.</returns>
        public bool Contains(T item)
        {
            this.EnsureSort();
            return Array.BinarySearch<T>(this._data, 0, this._count, item) >= 0;
        }

        /// <summary>
        /// Copies the binary heap to an array at the specified index.
        /// </summary>
        /// <param name="array">One dimensional array that is the destination of the copied elements.</param>
        /// <param name="arrayIndex">The zero-based index at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            EnsureSort();
            Array.Copy(this._data, array, this._count);
        }

        /// <summary>
        /// Gets whether or not the binary heap is readonly.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes an item from the binary heap. This utilizes the type T's Comparer and will not remove duplicates.
        /// </summary>
        /// <param name="item">The item to be removed.</param>
        /// <returns>Boolean true if the item was removed.</returns>
        public bool Remove(T item)
        {
            this.EnsureSort();

            int i = Array.BinarySearch<T>(this._data, 0, this._count, item);
            if (i < 0)
            {
                return false;
            }

            Array.Copy(this._data, i + 1, this._data, i, this._count - i);
            this._data[this._count] = default(T);
            this._count--;
            return true;
        }
 
    }
}
