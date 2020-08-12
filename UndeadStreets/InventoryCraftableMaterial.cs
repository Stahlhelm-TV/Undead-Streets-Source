namespace UndeadStreets
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class InventoryCraftableMaterial : InventoryItem, ICraftable
    {
        public InventoryCraftableMaterial(string name, string description, int amount, int maxAmount, MaterialCraftable[] requiredMaterials) : base(name, description, amount, maxAmount)
        {
            base.Name = name;
            base.Description = description;
            base.Amount = amount;
            base.MaxAmount = maxAmount;
            this.RequiredMaterials = requiredMaterials;
        }

        public MaterialCraftable[] RequiredMaterials { get; set; }
    }
}

