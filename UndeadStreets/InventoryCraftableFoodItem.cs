namespace UndeadStreets
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class InventoryCraftableFoodItem : InventoryFoodItem, IFood, ICraftable
    {
        public InventoryCraftableFoodItem(string name, string description, int amount, int maxAmount, FoodType foodType, float restore, MaterialCraftable[] requiredMaterials) : base(name, description, amount, maxAmount, foodType, restore)
        {
            this.RequiredMaterials = requiredMaterials;
        }

        public MaterialCraftable[] RequiredMaterials { get; set; }
    }
}

