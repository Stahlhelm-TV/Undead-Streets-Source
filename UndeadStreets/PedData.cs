namespace UndeadStreets
{
    using GTA.Math;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class PedData
    {
        public PedData(int handle, int hash, Vector3 rotation, Vector3 position, PedTasks task, List<Weapon> weapons)
        {
            this.Handle = handle;
            this.Hash = hash;
            this.Rotation = rotation;
            this.Position = position;
            this.Task = task;
            this.Weapons = weapons;
        }

        public int Handle { get; set; }

        public int Hash { get; set; }

        public Vector3 Rotation { get; set; }

        public Vector3 Position { get; set; }

        public PedTasks Task { get; set; }

        public List<Weapon> Weapons { get; set; }
    }
}

