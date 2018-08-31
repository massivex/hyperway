using System;
using System.Collections.Generic;

namespace Mx.Tools
{
    using System.Collections;

    public class ImmutableList<TItem> : IList<TItem>
    {
        private readonly IList<TItem> list;

        public ImmutableList(IList<TItem> list)
        {
            this.list = list;
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.list).GetEnumerator();
        }

        public void Add(TItem item)
        {
            throw new NotSupportedException("Operation not permitted. This is an immutable list!");
        }

        public void Clear()
        {
            throw new NotSupportedException("Operation not permitted. This is an immutable list!");
        }

        public bool Contains(TItem item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(TItem[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public bool Remove(TItem item)
        {
            throw new NotSupportedException("Operation not permitted. This is an immutable list!");
        }

        public int Count => this.list.Count;

        public bool IsReadOnly => this.list.IsReadOnly;

        public int IndexOf(TItem item)
        {
            return this.list.IndexOf(item);
        }

        public void Insert(int index, TItem item)
        {
            this.list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            this.list.RemoveAt(index);
        }

        public TItem this[int index]
        {
            get => this.list[index];
            set => throw new NotSupportedException("Operation not permitted. This is an immutable list!");
        }
    }
}
