namespace UndeadStreets
{
    using GTA.Native;
    using System;

    public interface IWeapon
    {
        int Ammo { get; set; }

        WeaponHash Hash { get; set; }

        WeaponComponent[] Components { get; set; }
    }
}

