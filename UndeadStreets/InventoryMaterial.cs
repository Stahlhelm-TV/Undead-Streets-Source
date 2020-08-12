namespace UndeadStreets
{
    using System;

    [Serializable]
    public class InventoryMaterial : InventoryItem
    {
        public InventoryMaterial(string name, string description, int amount, int maxAmount) : base(name, description, amount, maxAmount)
        {
            base.Name = name;
            base.Description = description;
            base.Amount = amount;
            base.MaxAmount = maxAmount;
        }
    }
}

