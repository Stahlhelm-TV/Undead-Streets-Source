namespace UndeadStreets
{
    using System;

    public interface ICraftable
    {
        MaterialCraftable[] RequiredMaterials { get; set; }
    }
}

