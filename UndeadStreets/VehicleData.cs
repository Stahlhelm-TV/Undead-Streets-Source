namespace UndeadStreets
{
    using GTA;
    using GTA.Math;
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class VehicleData
    {
        public VehicleData(int handle, int hash, Vector3 rotation, Vector3 position, VehicleColor primaryColor, VehicleColor secondaryColor, int health, float engineHealth, float heading)
        {
            this.Handle = handle;
            this.Hash = hash;
            this.Rotation = rotation;
            this.Position = position;
            this.PrimaryColor = primaryColor;
            this.SecondaryColor = secondaryColor;
            this.Health = health;
            this.EngineHealth = engineHealth;
            this.Heading = heading;
        }

        public int Handle { get; set; }

        public int Hash { get; set; }

        public Vector3 Rotation { get; set; }

        public Vector3 Position { get; set; }

        public int Health { get; set; }

        public float EngineHealth { get; set; }

        public VehicleColor PrimaryColor { get; set; }

        public VehicleColor SecondaryColor { get; set; }

        public float Heading { get; set; }
    }
}

