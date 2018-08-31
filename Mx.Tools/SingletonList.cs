namespace Mx.Tools
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SingletonList<T> : IList<T>
    {
        private readonly T item;

        public SingletonList(T item)
        {
            this.item = item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            yield return this.item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(T itemToAdd)
        {
            throw new NotSupportedException("Add not supported.");
        }

        public void Clear()
        {
            throw new NotSupportedException("Clear not supported.");
        }

        public bool Contains(T itemToFind)
        {
            if (itemToFind == null) return this.item == null;

            return itemToFind.Equals(this.item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));

            array[arrayIndex] = this.item;
        }

        public bool Remove(T itemToRemove)
        {
            throw new NotSupportedException("Remove not supported.");
        }

        public int Count => 1;

        public bool IsReadOnly => true;

        public int IndexOf(T itemToFind)
        {
            return this.Contains(itemToFind) ? 0 : -1;
        }

        public void Insert(int index, T itemToAdd)
        {
            throw new NotSupportedException("Insert not supported.");
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException("RemoveAt not supported.");
        }

        public T this[int index]
        {
            get
            {
                if (index == 0) return this.item;

                throw new IndexOutOfRangeException();
            }
            set { throw new NotSupportedException("Set not supported."); }
        }
    }
}
