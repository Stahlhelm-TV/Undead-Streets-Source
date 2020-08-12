namespace UndeadStreets
{
    using GTA;
    using GTA.Native;
    using NativeUI;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [Serializable]
    public class Inventory : Script
    {
        private List<Ped> lootedPeds = new List<Ped>();
        public static List<InventoryItem> playerItemInventory = new List<InventoryItem>();
        public static InventoryFoodItem bottleWater = new InventoryFoodItem("Water Bottle", "A bottle of water", 0, 10, FoodType.Drink, 0.2f);
        public static InventoryFoodItem bottleSoda = new InventoryFoodItem("Soda Bottle", "A bottle of soda", 0, 10, FoodType.Drink, 0.15f);
        public static InventoryFoodItem chocolateBar = new InventoryFoodItem("Chocolate Bar", "A bar of chocolate", 0, 20, FoodType.Food, 0.1f);
        public static InventoryFoodItem canFood = new InventoryFoodItem("Canned Food", "A can containing uncooked food", 0, 10, FoodType.Food, 0.15f);
        public static List<InventoryMaterial> playerMaterialInventory = new List<InventoryMaterial>();
        public static InventoryMaterial metal = new InventoryMaterial("Metal", "Scraps of metal", 0, 50);
        public static InventoryMaterial wood = new InventoryMaterial("Wood", "Scraps of wood", 0, 50);
        public static InventoryMaterial plastic = new InventoryMaterial("Plastic", "Scraps of plastic", 0, 50);
        public static InventoryMaterial rawMeat = new InventoryMaterial("Raw Meat", "Uncooked animal meat", 0, 20);
        public static InventoryCraftableFoodItem cookedMeat;
        [NonSerialized]
        public static UIMenu inventoryMenu;
        [NonSerialized]
        public static UIMenu craftCampfireMenu;
        [NonSerialized]
        public static UIMenu itemsSubMenu;
        [NonSerialized]
        public static UIMenu materialsSubMenu;

        static Inventory()
        {
            MaterialCraftable[] requiredMaterials = new MaterialCraftable[] { new MaterialCraftable(rawMeat, 1) };
            cookedMeat = new InventoryCraftableFoodItem("Cooked Meat", "Meat that has been cooked", 0, 10, FoodType.Food, 0.25f, requiredMaterials);
        }

        public Inventory()
        {
            base.Tick += new EventHandler(this.OnTick);
            playerItemInventory.Add(bottleWater);
            playerItemInventory.Add(bottleSoda);
            playerItemInventory.Add(chocolateBar);
            playerItemInventory.Add(canFood);
            playerItemInventory.Add(cookedMeat);
            playerMaterialInventory.Add(metal);
            playerMaterialInventory.Add(wood);
            playerMaterialInventory.Add(plastic);
            playerMaterialInventory.Add(rawMeat);
            inventoryMenu = new UIMenu("Inventory", "");
            craftCampfireMenu = new UIMenu("Campfire Crafting", "");
            this.AddItemsSubMenu(inventoryMenu);
            this.AddMaterialsSubMenu(inventoryMenu);
            this.AddCraftingCooking(craftCampfireMenu);
            UIResRectangle rectangle = new UIResRectangle();
            UIResRectangle rectangle2 = rectangle;
            rectangle2.Color = Color.FromArgb(0xff, Color.DarkGreen);
            inventoryMenu.SetBannerType(rectangle2);
            UIResRectangle rectangle3 = rectangle;
            rectangle3.Color = Color.FromArgb(0xff, Color.OrangeRed);
            craftCampfireMenu.SetBannerType(rectangle3);
            Main.MasterMenuPool.Add(inventoryMenu);
            Main.MasterMenuPool.Add(craftCampfireMenu);
            Main.MasterMenuPool.RefreshIndex();
            this.KeyDown += delegate (object o, KeyEventArgs e) {
                if (((e.KeyCode == Main.InventoryKey) && Main.ModActive) && !Main.MasterMenuPool.IsAnyMenuOpen())
                {
                    inventoryMenu.Visible = !inventoryMenu.Visible;
                }
            };
        }

        public void AddCraftingCooking(UIMenu menu)
        {
            UIMenuItem cookRawMeat = new UIMenuItem("Cook Raw Meat", "Cook Raw Meat so it can be eaten");
            cookRawMeat.SetRightLabel("(" + playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount + ")");
            menu.AddItem(cookRawMeat);
            craftCampfireMenu.OnItemSelect += delegate (UIMenu sender, UIMenuItem item, int index) {
                int amount = playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount;
                int num2 = playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount;
                int maxAmount = playerMaterialInventory.Find(material => material.Name == "Raw Meat").MaxAmount;
                if (amount <= 0)
                {
                    UI.Notify("You have no ~b~Raw Meat~w~ to cook");
                }
                else if (num2 == maxAmount)
                {
                    UI.Notify("Your inventory is full!");
                }
                else
                {
                    InventoryMaterial local5 = playerMaterialInventory.Find(material => material.Name == "Raw Meat");
                    local5.Amount--;
                    InventoryItem local7 = playerItemInventory.Find(items => items.Name == "Cooked Meat");
                    local7.Amount++;
                    cookRawMeat.SetRightLabel("(" + playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount + ")");
                    object[] objArray1 = new object[5];
                    objArray1[0] = "(";
                    objArray1[1] = playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount;
                    object[] local11 = objArray1;
                    local11[2] = "/";
                    local11[3] = playerMaterialInventory.Find(material => material.Name == "Raw Meat").MaxAmount;
                    object[] local13 = local11;
                    local13[4] = ")";
                    materialsSubMenu.MenuItems.Find(material => material.Text == "Raw Meat").SetRightLabel(string.Concat(local13));
                    object[] objArray2 = new object[5];
                    objArray2[0] = "(";
                    objArray2[1] = playerItemInventory.Find(items => items.Name == "Cooked Meat").Amount;
                    object[] local16 = objArray2;
                    local16[2] = "/";
                    local16[3] = playerItemInventory.Find(items => items.Name == "Cooked Meat").MaxAmount;
                    object[] local18 = local16;
                    local18[4] = ")";
                    itemsSubMenu.MenuItems.Find(items => items.Text == "Cooked Meat").SetRightLabel(string.Concat(local18));
                    UI.Notify("You've made some ~b~Cooked Meat~w~!");
                }
            };
        }

        public void AddItemsSubMenu(UIMenu menu)
        {
            itemsSubMenu = Main.MasterMenuPool.AddSubMenu(menu, "Items", "See what useful items you are carrying");
            UIResRectangle rectangle1 = new UIResRectangle();
            rectangle1.Color = Color.FromArgb(0xff, Color.DarkGreen);
            UIResRectangle rectangle = rectangle1;
            itemsSubMenu.SetBannerType(rectangle);
            int num = 0;
            while (true)
            {
                if (num >= playerItemInventory.Count)
                {
                    itemsSubMenu.OnItemSelect += delegate (UIMenu sender, UIMenuItem item, int index) {
                        if (playerItemInventory[index].Amount <= 0)
                        {
                            UI.Notify("Cannot use ~b~" + item.Text + "~w~ as you are not carrying any.");
                        }
                        else if (playerItemInventory[index].GetType() == typeof(InventoryFoodItem))
                        {
                            InventoryFoodItem item2 = (InventoryFoodItem) playerItemInventory[index];
                            if (item2.FoodType == FoodType.Food)
                            {
                                if (Character.currentHungerLevel >= 1f)
                                {
                                    UI.Notify("Cannot use ~b~" + item.Text + "~w~ as your Hunger levels are full.");
                                }
                                else
                                {
                                    Character.currentHungerLevel += item2.Restore;
                                    if (Character.currentHungerLevel > 1f)
                                    {
                                        Character.currentHungerLevel = 1f;
                                    }
                                    InventoryItem local1 = playerItemInventory[index];
                                    local1.Amount--;
                                    object[] objArray1 = new object[] { "(", playerItemInventory[index].Amount, "/", playerItemInventory[index].MaxAmount, ")" };
                                    item.SetRightLabel(string.Concat(objArray1));
                                    UI.Notify("Used ~b~" + item.Text + "~w~ to replenish Hunger levels.");
                                }
                            }
                            else if (item2.FoodType == FoodType.Drink)
                            {
                                if (Character.currentThirstLevel >= 1f)
                                {
                                    UI.Notify("Cannot use ~b~" + item.Text + "~w~ as your Thirst levels are full.");
                                }
                                else
                                {
                                    Character.currentThirstLevel += item2.Restore;
                                    if (Character.currentThirstLevel > 1f)
                                    {
                                        Character.currentThirstLevel = 1f;
                                    }
                                    InventoryItem local2 = playerItemInventory[index];
                                    local2.Amount--;
                                    object[] objArray2 = new object[] { "(", playerItemInventory[index].Amount, "/", playerItemInventory[index].MaxAmount, ")" };
                                    item.SetRightLabel(string.Concat(objArray2));
                                    UI.Notify("Used ~b~" + item.Text + "~w~ to replenish Thirst levels.");
                                }
                            }
                        }
                        else if (playerItemInventory[index].GetType() == typeof(InventoryCraftableFoodItem))
                        {
                            InventoryCraftableFoodItem item3 = (InventoryCraftableFoodItem) playerItemInventory[index];
                            if (item3.FoodType == FoodType.Food)
                            {
                                if (Character.currentHungerLevel >= 1f)
                                {
                                    UI.Notify("Cannot use ~b~" + item.Text + "~w~ as your Hunger levels are full.");
                                }
                                else
                                {
                                    Character.currentHungerLevel += item3.Restore;
                                    if (Character.currentHungerLevel > 1f)
                                    {
                                        Character.currentHungerLevel = 1f;
                                    }
                                    InventoryItem local3 = playerItemInventory[index];
                                    local3.Amount--;
                                    object[] objArray3 = new object[] { "(", playerItemInventory[index].Amount, "/", playerItemInventory[index].MaxAmount, ")" };
                                    item.SetRightLabel(string.Concat(objArray3));
                                    UI.Notify("Used ~b~" + item.Text + "~w~ to replenish Hunger levels.");
                                }
                            }
                            else if (item3.FoodType == FoodType.Drink)
                            {
                                if (Character.currentThirstLevel >= 1f)
                                {
                                    UI.Notify("Cannot use ~b~" + item.Text + "~w~ as your Thirst levels are full.");
                                }
                                else
                                {
                                    Character.currentThirstLevel += item3.Restore;
                                    if (Character.currentThirstLevel > 1f)
                                    {
                                        Character.currentThirstLevel = 1f;
                                    }
                                    InventoryItem local4 = playerItemInventory[index];
                                    local4.Amount--;
                                    object[] objArray4 = new object[] { "(", playerItemInventory[index].Amount, "/", playerItemInventory[index].MaxAmount, ")" };
                                    item.SetRightLabel(string.Concat(objArray4));
                                    UI.Notify("Used ~b~" + item.Text + "~w~ to replenish Thirst levels.");
                                }
                            }
                        }
                    };
                    return;
                }
                itemsSubMenu.AddItem(new UIMenuItem(playerItemInventory[num].Name, playerItemInventory[num].Description));
                object[] objArray1 = new object[] { "(", playerItemInventory[num].Amount, "/", playerItemInventory[num].MaxAmount, ")" };
                itemsSubMenu.MenuItems[itemsSubMenu.MenuItems.Count - 1].SetRightLabel(string.Concat(objArray1));
                num++;
            }
        }

        public void AddMaterialsSubMenu(UIMenu menu)
        {
            materialsSubMenu = Main.MasterMenuPool.AddSubMenu(menu, "Materials", "See what materials that can be used for crafting you are carrying");
            UIResRectangle rectangle1 = new UIResRectangle();
            rectangle1.Color = Color.FromArgb(0xff, Color.DarkGreen);
            UIResRectangle rectangle = rectangle1;
            materialsSubMenu.SetBannerType(rectangle);
            for (int i = 0; i < playerMaterialInventory.Count; i++)
            {
                materialsSubMenu.AddItem(new UIMenuItem(playerMaterialInventory[i].Name, playerMaterialInventory[i].Description));
                object[] objArray1 = new object[] { "(", playerMaterialInventory[i].Amount, "/", playerMaterialInventory[i].MaxAmount, ")" };
                materialsSubMenu.MenuItems[materialsSubMenu.MenuItems.Count - 1].SetRightLabel(string.Concat(objArray1));
            }
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Game.Player.Character.IsDead)
            {
                inventoryMenu.Visible = false;
                craftCampfireMenu.Visible = false;
            }
            if (Main.ModActive)
            {
                Game.DisableControlThisFrame(2, GTA.Control.Phone);
                if (Game.IsDisabledControlJustPressed(2, GTA.Control.Phone) && !Main.MasterMenuPool.IsAnyMenuOpen())
                {
                    inventoryMenu.Visible = !inventoryMenu.Visible;
                }
            }
            if (Main.ModActive && !Game.Player.Character.IsInVehicle())
            {
                try
                {
                    Prop closest = World.GetClosest<Prop>(Game.Player.Character.Position, World.GetNearbyProps(Game.Player.Character.Position, 2.5f));
                    if (closest == Character.tent)
                    {
                        Extensions.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to sleep in Tent");
                        Game.DisableControlThisFrame(2, GTA.Control.Context);
                        if (Game.IsDisabledControlJustPressed(2, GTA.Control.Context))
                        {
                            Population.Sleep(Game.Player.Character.Position);
                        }
                    }
                    else if (closest == Character.campFire)
                    {
                        Extensions.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to craft using Campfire");
                        Game.DisableControlThisFrame(2, GTA.Control.Context);
                        if (Game.IsDisabledControlJustPressed(2, GTA.Control.Context))
                        {
                            craftCampfireMenu.Visible = !craftCampfireMenu.Visible;
                        }
                    }
                    Game.Player.CanControlCharacter = !craftCampfireMenu.Visible;
                    Ped ped = World.GetClosest<Ped>(Game.Player.Character.Position, World.GetNearbyPeds(Game.Player.Character, 1.5f));
                    if (((ped == null) || (!ped.IsDead || !ped.IsHuman)) || this.lootedPeds.Contains(ped))
                    {
                        if (((ped != null) && (ped.IsDead && !ped.IsHuman)) && !this.lootedPeds.Contains(ped))
                        {
                            Extensions.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to harvest meat from animal corpse");
                            Game.DisableControlThisFrame(2, GTA.Control.Context);
                            if (Game.IsDisabledControlJustPressed(2, GTA.Control.Context))
                            {
                                if (!Game.Player.Character.Weapons.HasWeapon((WeaponHash) (-1716189206)))
                                {
                                    UI.Notify("You need a knife to harvest ~b~Raw Meat~w~ from dead animals!", true);
                                }
                                else if (Inventory.playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount == Inventory.playerMaterialInventory.Find(material => material.Name == "Raw Meat").MaxAmount)
                                {
                                    UI.Notify("You cannot carry any more ~b~Raw Meat~w~!", true);
                                }
                                else
                                {
                                    Game.Player.Character.Weapons.Select((WeaponHash) (-1716189206), true);
                                    Game.Player.Character.Task.PlayAnimation("pickup_object", "pickup_low", 8f, 0xbb8, AnimationFlags.None);
                                    InventoryMaterial local7 = Inventory.playerMaterialInventory.Find(material => material.Name == "Raw Meat");
                                    local7.Amount++;
                                    object[] objArray5 = new object[5];
                                    objArray5[0] = "(";
                                    objArray5[1] = Inventory.playerMaterialInventory.Find(item => item.Name == "Raw Meat").Amount;
                                    object[] local10 = objArray5;
                                    local10[2] = "/";
                                    local10[3] = Inventory.playerMaterialInventory.Find(item => item.Name == "Raw Meat").MaxAmount;
                                    object[] local12 = local10;
                                    local12[4] = ")";
                                    materialsSubMenu.MenuItems.Find(item => item.Text == "Raw Meat").SetRightLabel(string.Concat(local12));
                                    craftCampfireMenu.MenuItems[0].SetRightLabel("(" + Inventory.playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount + ")");
                                    object[] objArray6 = new object[5];
                                    objArray6[0] = "You have harvested ~b~Raw Meat ~g~(";
                                    objArray6[1] = Inventory.playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount;
                                    object[] local15 = objArray6;
                                    local15[2] = "/";
                                    local15[3] = Inventory.playerMaterialInventory.Find(material => material.Name == "Raw Meat").MaxAmount;
                                    object[] local17 = local15;
                                    local17[4] = ")";
                                    UI.Notify(string.Concat(local17), true);
                                    this.lootedPeds.Add(ped);
                                }
                            }
                        }
                    }
                    else
                    {
                        Extensions.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to search corpse");
                        Game.DisableControlThisFrame(2, GTA.Control.Context);
                        if (Game.IsDisabledControlJustPressed(2, GTA.Control.Context))
                        {
                            Game.Player.Character.Task.PlayAnimation("pickup_object", "pickup_low");
                            int num = RandoMath.CachedRandom.Next(0, 10);
                            if (num < 3)
                            {
                                List<InventoryItem> playerItemInventory = Inventory.playerItemInventory;
                                int num4 = 0;
                                while (true)
                                {
                                    if (num4 >= playerItemInventory.Count)
                                    {
                                        InventoryItem itemFound = RandoMath.GetRandomElementFromList<InventoryItem>(playerItemInventory);
                                        if (Inventory.playerItemInventory.Find(item => item.Name == itemFound.Name).Amount == Inventory.playerItemInventory.Find(item => item.Name == itemFound.Name).MaxAmount)
                                        {
                                            UI.Notify("Found ~b~" + itemFound.Name + "~w~ but inventory is full!", true);
                                        }
                                        else
                                        {
                                            InventoryItem local1 = Inventory.playerItemInventory.Find(item => item.Name == itemFound.Name);
                                            local1.Amount++;
                                            object[] objArray1 = new object[] { "Found ~b~", itemFound.Name, "~g~ (", Inventory.playerItemInventory.Find(item => item.Name == itemFound.Name).Amount, "/", Inventory.playerItemInventory.Find(item => item.Name == itemFound.Name).MaxAmount, ")" };
                                            UI.Notify(string.Concat(objArray1), true);
                                            this.lootedPeds.Add(ped);
                                            object[] objArray2 = new object[] { "(", Inventory.playerItemInventory.Find(item => item.Name == itemFound.Name).Amount, "/", Inventory.playerItemInventory.Find(item => item.Name == itemFound.Name).MaxAmount, ")" };
                                            itemsSubMenu.MenuItems.Find(item => item.Text == itemFound.Name).SetRightLabel(string.Concat(objArray2));
                                        }
                                        break;
                                    }
                                    if (playerItemInventory[num4].GetType() == typeof(InventoryCraftableFoodItem))
                                    {
                                        playerItemInventory.Remove(playerItemInventory[num4]);
                                    }
                                    num4++;
                                }
                            }
                            else if (num <= 5)
                            {
                                UI.Notify("Found nothing", true);
                                this.lootedPeds.Add(ped);
                            }
                            else
                            {
                                List<InventoryMaterial> playerMaterialInventory = Inventory.playerMaterialInventory;
                                int num8 = 0;
                                while (true)
                                {
                                    if (num8 >= playerMaterialInventory.Count)
                                    {
                                        InventoryItem materialFound = RandoMath.GetRandomElementFromList<InventoryMaterial>(playerMaterialInventory);
                                        if (Inventory.playerMaterialInventory.Find(material => material.Name == materialFound.Name).Amount == Inventory.playerMaterialInventory.Find(material => material.Name == materialFound.Name).MaxAmount)
                                        {
                                            UI.Notify("Found ~b~" + materialFound.Name + "~w~ but inventory is full!", true);
                                        }
                                        else
                                        {
                                            InventoryMaterial local2 = Inventory.playerMaterialInventory.Find(material => material.Name == materialFound.Name);
                                            local2.Amount++;
                                            object[] objArray3 = new object[] { "Found ~b~", materialFound.Name, "~g~ (", Inventory.playerMaterialInventory.Find(material => material.Name == materialFound.Name).Amount, "/", Inventory.playerMaterialInventory.Find(material => material.Name == materialFound.Name).MaxAmount, ")" };
                                            UI.Notify(string.Concat(objArray3), true);
                                            this.lootedPeds.Add(ped);
                                            object[] objArray4 = new object[] { "(", Inventory.playerMaterialInventory.Find(item => item.Name == materialFound.Name).Amount, "/", Inventory.playerMaterialInventory.Find(item => item.Name == materialFound.Name).MaxAmount, ")" };
                                            materialsSubMenu.MenuItems.Find(item => item.Text == materialFound.Name).SetRightLabel(string.Concat(objArray4));
                                            craftCampfireMenu.MenuItems.Find(item => item.Text == materialFound.Name).SetRightLabel("(" + Inventory.playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount + ")");
                                        }
                                        break;
                                    }
                                    if ((playerMaterialInventory[num8].GetType() == typeof(InventoryCraftableMaterial)) || (playerMaterialInventory[num8].Name == "Raw Meat"))
                                    {
                                        playerMaterialInventory.Remove(playerMaterialInventory[num8]);
                                    }
                                    num8++;
                                }
                            }
                        }
                    }
                }
                catch (Exception exception1)
                {
                    Debug.Log(exception1.ToString());
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Inventory.<>c <>9 = new Inventory.<>c();
            public static KeyEventHandler <>9__16_0;
            public static Predicate<InventoryMaterial> <>9__17_16;
            public static Predicate<InventoryMaterial> <>9__17_18;
            public static Predicate<InventoryMaterial> <>9__17_19;
            public static Predicate<InventoryMaterial> <>9__17_26;
            public static Predicate<UIMenuItem> <>9__17_20;
            public static Predicate<InventoryMaterial> <>9__17_21;
            public static Predicate<InventoryMaterial> <>9__17_22;
            public static Predicate<InventoryMaterial> <>9__17_23;
            public static Predicate<InventoryMaterial> <>9__17_24;
            public static Predicate<InventoryMaterial> <>9__17_25;
            public static Predicate<InventoryMaterial> <>9__18_0;
            public static Predicate<InventoryMaterial> <>9__18_2;
            public static Predicate<InventoryMaterial> <>9__18_3;
            public static Predicate<InventoryMaterial> <>9__18_4;
            public static Predicate<InventoryMaterial> <>9__18_12;
            public static Predicate<InventoryItem> <>9__18_13;
            public static Predicate<InventoryMaterial> <>9__18_5;
            public static Predicate<UIMenuItem> <>9__18_6;
            public static Predicate<InventoryMaterial> <>9__18_7;
            public static Predicate<InventoryMaterial> <>9__18_8;
            public static Predicate<UIMenuItem> <>9__18_9;
            public static Predicate<InventoryItem> <>9__18_10;
            public static Predicate<InventoryItem> <>9__18_11;
            public static ItemSelectEvent <>9__19_0;

            internal void <.ctor>b__16_0(object o, KeyEventArgs e)
            {
                if (((e.KeyCode == Main.InventoryKey) && Main.ModActive) && !Main.MasterMenuPool.IsAnyMenuOpen())
                {
                    Inventory.inventoryMenu.Visible = !Inventory.inventoryMenu.Visible;
                }
            }

            internal bool <AddCraftingCooking>b__18_0(InventoryMaterial material) => 
                (material.Name == "Raw Meat");

            internal bool <AddCraftingCooking>b__18_10(InventoryItem items) => 
                (items.Name == "Cooked Meat");

            internal bool <AddCraftingCooking>b__18_11(InventoryItem items) => 
                (items.Name == "Cooked Meat");

            internal bool <AddCraftingCooking>b__18_12(InventoryMaterial material) => 
                (material.Name == "Raw Meat");

            internal bool <AddCraftingCooking>b__18_13(InventoryItem items) => 
                (items.Name == "Cooked Meat");

            internal bool <AddCraftingCooking>b__18_2(InventoryMaterial material) => 
                (material.Name == "Raw Meat");

            internal bool <AddCraftingCooking>b__18_3(InventoryMaterial material) => 
                (material.Name == "Raw Meat");

            internal bool <AddCraftingCooking>b__18_4(InventoryMaterial material) => 
                (material.Name == "Raw Meat");

            internal bool <AddCraftingCooking>b__18_5(InventoryMaterial material) => 
                (material.Name == "Raw Meat");

            internal bool <AddCraftingCooking>b__18_6(UIMenuItem material) => 
                (material.Text == "Raw Meat");

            internal bool <AddCraftingCooking>b__18_7(InventoryMaterial material) => 
                (material.Name == "Raw Meat");

            internal bool <AddCraftingCooking>b__18_8(InventoryMaterial material) => 
                (material.Name == "Raw Meat");

            internal bool <AddCraftingCooking>b__18_9(UIMenuItem items) => 
                (items.Text == "Cooked Meat");

            internal void <AddItemsSubMenu>b__19_0(UIMenu sender, UIMenuItem item, int index)
            {
                if (Inventory.playerItemInventory[index].Amount <= 0)
                {
                    UI.Notify("Cannot use ~b~" + item.Text + "~w~ as you are not carrying any.");
                }
                else if (Inventory.playerItemInventory[index].GetType() == typeof(InventoryFoodItem))
                {
                    InventoryFoodItem item2 = (InventoryFoodItem) Inventory.playerItemInventory[index];
                    if (item2.FoodType == FoodType.Food)
                    {
                        if (Character.currentHungerLevel >= 1f)
                        {
                            UI.Notify("Cannot use ~b~" + item.Text + "~w~ as your Hunger levels are full.");
                        }
                        else
                        {
                            Character.currentHungerLevel += item2.Restore;
                            if (Character.currentHungerLevel > 1f)
                            {
                                Character.currentHungerLevel = 1f;
                            }
                            InventoryItem local1 = Inventory.playerItemInventory[index];
                            local1.Amount--;
                            object[] objArray1 = new object[] { "(", Inventory.playerItemInventory[index].Amount, "/", Inventory.playerItemInventory[index].MaxAmount, ")" };
                            item.SetRightLabel(string.Concat(objArray1));
                            UI.Notify("Used ~b~" + item.Text + "~w~ to replenish Hunger levels.");
                        }
                    }
                    else if (item2.FoodType == FoodType.Drink)
                    {
                        if (Character.currentThirstLevel >= 1f)
                        {
                            UI.Notify("Cannot use ~b~" + item.Text + "~w~ as your Thirst levels are full.");
                        }
                        else
                        {
                            Character.currentThirstLevel += item2.Restore;
                            if (Character.currentThirstLevel > 1f)
                            {
                                Character.currentThirstLevel = 1f;
                            }
                            InventoryItem local2 = Inventory.playerItemInventory[index];
                            local2.Amount--;
                            object[] objArray2 = new object[] { "(", Inventory.playerItemInventory[index].Amount, "/", Inventory.playerItemInventory[index].MaxAmount, ")" };
                            item.SetRightLabel(string.Concat(objArray2));
                            UI.Notify("Used ~b~" + item.Text + "~w~ to replenish Thirst levels.");
                        }
                    }
                }
                else if (Inventory.playerItemInventory[index].GetType() == typeof(InventoryCraftableFoodItem))
                {
                    InventoryCraftableFoodItem item3 = (InventoryCraftableFoodItem) Inventory.playerItemInventory[index];
                    if (item3.FoodType == FoodType.Food)
                    {
                        if (Character.currentHungerLevel >= 1f)
                        {
                            UI.Notify("Cannot use ~b~" + item.Text + "~w~ as your Hunger levels are full.");
                        }
                        else
                        {
                            Character.currentHungerLevel += item3.Restore;
                            if (Character.currentHungerLevel > 1f)
                            {
                                Character.currentHungerLevel = 1f;
                            }
                            InventoryItem local3 = Inventory.playerItemInventory[index];
                            local3.Amount--;
                            object[] objArray3 = new object[] { "(", Inventory.playerItemInventory[index].Amount, "/", Inventory.playerItemInventory[index].MaxAmount, ")" };
                            item.SetRightLabel(string.Concat(objArray3));
                            UI.Notify("Used ~b~" + item.Text + "~w~ to replenish Hunger levels.");
                        }
                    }
                    else if (item3.FoodType == FoodType.Drink)
                    {
                        if (Character.currentThirstLevel >= 1f)
                        {
                            UI.Notify("Cannot use ~b~" + item.Text + "~w~ as your Thirst levels are full.");
                        }
                        else
                        {
                            Character.currentThirstLevel += item3.Restore;
                            if (Character.currentThirstLevel > 1f)
                            {
                                Character.currentThirstLevel = 1f;
                            }
                            InventoryItem local4 = Inventory.playerItemInventory[index];
                            local4.Amount--;
                            object[] objArray4 = new object[] { "(", Inventory.playerItemInventory[index].Amount, "/", Inventory.playerItemInventory[index].MaxAmount, ")" };
                            item.SetRightLabel(string.Concat(objArray4));
                            UI.Notify("Used ~b~" + item.Text + "~w~ to replenish Thirst levels.");
                        }
                    }
                }
            }

            internal bool <OnTick>b__17_16(InventoryMaterial material) => 
                (material.Name == "Raw Meat");

            internal bool <OnTick>b__17_18(InventoryMaterial material) => 
                (material.Name == "Raw Meat");

            internal bool <OnTick>b__17_19(InventoryMaterial material) => 
                (material.Name == "Raw Meat");

            internal bool <OnTick>b__17_20(UIMenuItem item) => 
                (item.Text == "Raw Meat");

            internal bool <OnTick>b__17_21(InventoryMaterial item) => 
                (item.Name == "Raw Meat");

            internal bool <OnTick>b__17_22(InventoryMaterial item) => 
                (item.Name == "Raw Meat");

            internal bool <OnTick>b__17_23(InventoryMaterial material) => 
                (material.Name == "Raw Meat");

            internal bool <OnTick>b__17_24(InventoryMaterial material) => 
                (material.Name == "Raw Meat");

            internal bool <OnTick>b__17_25(InventoryMaterial material) => 
                (material.Name == "Raw Meat");

            internal bool <OnTick>b__17_26(InventoryMaterial material) => 
                (material.Name == "Raw Meat");
        }
    }
}

