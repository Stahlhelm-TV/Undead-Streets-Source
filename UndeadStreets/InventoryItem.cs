namespace UndeadStreets
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class InventoryItem
    {
        public InventoryItem(string name, string description, int amount, int maxAmount)
        {
            this.Name = name;
            this.Description = description;
            this.Amount = amount;
            this.MaxAmount = maxAmount;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Amount { get; set; }

        public int MaxAmount { get; set; }
    }
}

