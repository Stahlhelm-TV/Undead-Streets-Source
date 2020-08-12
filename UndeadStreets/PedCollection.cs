namespace UndeadStreets
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [Serializable]
    public class PedCollection : IList<PedData>, ICollection<PedData>, IEnumerable<PedData>, IEnumerable
    {
        private readonly List<PedData> peds = new List<PedData>();

        public event ListChangedEvent ListChanged;

        public void Add(PedData item)
        {
            this.peds.Add(item);
            if (this.ListChanged == null)
            {
                ListChangedEvent listChanged = this.ListChanged;
            }
            else
            {
                this.ListChanged(this);
            }
        }

        public void Clear()
        {
            this.peds.Clear();
            if (this.ListChanged == null)
            {
                ListChangedEvent listChanged = this.ListChanged;
            }
            else
            {
                this.ListChanged(this);
            }
        }

        public bool Contains(PedData item) => 
            this.peds.Contains(item);

        public void CopyTo(PedData[] array, int arrayIndex)
        {
            this.peds.CopyTo(array, arrayIndex);
        }

        public IEnumerator<PedData> GetEnumerator() => 
            this.peds.GetEnumerator();

        public int IndexOf(PedData item) => 
            this.peds.IndexOf(item);

        public void Insert(int index, PedData item)
        {
            this.peds.Insert(index, item);
        }

        public bool Remove(PedData item)
        {
            bool flag = this.peds.Remove(item);
            if (this.ListChanged == null)
            {
                ListChangedEvent listChanged = this.ListChanged;
            }
            else
            {
                this.ListChanged(this);
            }
            return flag;
        }

        public void RemoveAt(int index)
        {
            this.peds.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public int Count =>
            this.peds.Count;

        public bool IsReadOnly =>
            this.peds.IsReadOnly;

        public PedData this[int index]
        {
            get => 
                this.peds[index];
            set => 
                (this.peds[index] = value);
        }

        public delegate void ListChangedEvent(PedCollection sender);
    }
}

