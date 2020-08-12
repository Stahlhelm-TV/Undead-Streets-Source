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
    public class VehicleCollection : IList<VehicleData>, ICollection<VehicleData>, IEnumerable<VehicleData>, IEnumerable
    {
        private readonly List<VehicleData> vehicles = new List<VehicleData>();

        public event ListChangedEvent ListChanged;

        public void Add(VehicleData item)
        {
            this.vehicles.Add(item);
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
            this.vehicles.Clear();
            if (this.ListChanged == null)
            {
                ListChangedEvent listChanged = this.ListChanged;
            }
            else
            {
                this.ListChanged(this);
            }
        }

        public bool Contains(VehicleData item) => 
            this.vehicles.Contains(item);

        public void CopyTo(VehicleData[] array, int arrayIndex)
        {
            this.vehicles.CopyTo(array, arrayIndex);
        }

        public IEnumerator<VehicleData> GetEnumerator() => 
            this.vehicles.GetEnumerator();

        public int IndexOf(VehicleData item) => 
            this.vehicles.IndexOf(item);

        public void Insert(int index, VehicleData item)
        {
            this.vehicles.Insert(index, item);
        }

        public bool Remove(VehicleData item)
        {
            bool flag = this.vehicles.Remove(item);
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
            this.vehicles.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public int Count =>
            this.vehicles.Count;

        public bool IsReadOnly =>
            this.vehicles.IsReadOnly;

        public VehicleData this[int index]
        {
            get => 
                this.vehicles[index];
            set => 
                (this.vehicles[index] = value);
        }

        public delegate void ListChangedEvent(VehicleCollection sender);
    }
}

