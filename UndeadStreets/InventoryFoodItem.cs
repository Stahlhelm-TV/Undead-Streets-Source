namespace UndeadStreets
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class InventoryFoodItem : InventoryItem, IFood
    {
        public InventoryFoodItem(string name, string description, int amount, int maxAmount, UndeadStreets.FoodType foodType, float restore) : base(name, description, amount, maxAmount)
        {
            this.FoodType = foodType;
            this.Restore = restore;
        }

        public UndeadStreets.FoodType FoodType { get; set; }

        public float Restore { get; set; }
    }
}

