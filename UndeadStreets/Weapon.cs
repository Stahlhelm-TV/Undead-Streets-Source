﻿namespace UndeadStreets
{
    using GTA.Native;
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class Weapon : IWeapon
    {
        public Weapon(int ammo, WeaponHash hash, WeaponComponent[] components)
        {
            this.Ammo = ammo;
            this.Hash = hash;
            this.Components = components;
        }

        public int Ammo { get; set; }

        public WeaponHash Hash { get; set; }

        public WeaponComponent[] Components { get; set; }
    }
}

