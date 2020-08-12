namespace UndeadStreets
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class MaterialCraftable
    {
        public MaterialCraftable(InventoryMaterial material, int requiredAmount)
        {
            this.Material = material;
            this.RequiredAmount = requiredAmount;
        }

        public InventoryMaterial Material { get; set; }

        public int RequiredAmount { get; set; }
    }
}

