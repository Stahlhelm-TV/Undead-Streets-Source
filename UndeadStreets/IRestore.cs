namespace UndeadStreets
{
    using System;

    public interface IRestore
    {
        UndeadStreets.RestoreType RestoreType { get; set; }

        float Restore { get; set; }
    }
}

