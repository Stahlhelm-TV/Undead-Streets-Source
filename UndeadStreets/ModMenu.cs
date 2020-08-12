namespace UndeadStreets
{
    using GTA;
    using NativeUI;
    using System;
    using System.Drawing;

    public class ModMenu : Script
    {
        private UIMenu mainMenu = new UIMenu("Undead Streets", "");

        public ModMenu()
        {
            Main.MasterMenuPool.Add(this.mainMenu);
            this.AddMenuSaveInventory(this.mainMenu);
            this.AddMenuLoadInventory(this.mainMenu);
            this.AddMenuSaveWeapons(this.mainMenu);
            this.AddMenuLoadWeapons(this.mainMenu);
            this.AddMenuRegisterVehicle(this.mainMenu);
            this.AddMenuSaveVehicle(this.mainMenu);
            this.AddMenuLoadVehicle(this.mainMenu);
            this.AddMenuSaveGroup(this.mainMenu);
            this.AddMenuLoadGroup(this.mainMenu);
            UIResRectangle rectangle1 = new UIResRectangle();
            rectangle1.Color = Color.FromArgb(0xff, Color.DarkRed);
            UIResRectangle rectangle = rectangle1;
            this.mainMenu.SetBannerType(rectangle);
            Main.MasterMenuPool.RefreshIndex();
            base.Tick += new EventHandler(this.OnTick);
            base.KeyDown += delegate (object o, KeyEventArgs e) {
                if (((e.KeyCode == Main.MenuKey) && Main.ModActive) && !Main.MasterMenuPool.IsAnyMenuOpen())
                {
                    this.mainMenu.Visible = !this.mainMenu.Visible;
                }
            };
        }

        public void AddMenuLoadGroup(UIMenu menu)
        {
            UIMenuItem newitem = new UIMenuItem("Load Group", "Load saved group (This will wipe your current group)");
            menu.AddItem(newitem);
            menu.OnItemSelect += delegate (UIMenu sender, UIMenuItem item, int index) {
                if (ReferenceEquals(item, newitem))
                {
                    Config.LoadGroup();
                }
            };
        }

        public void AddMenuLoadInventory(UIMenu menu)
        {
            UIMenuItem newitem = new UIMenuItem("Load Inventory", "Load saved inventory (This will wipe your current inventory)");
            menu.AddItem(newitem);
            menu.OnItemSelect += delegate (UIMenu sender, UIMenuItem item, int index) {
                if (ReferenceEquals(item, newitem))
                {
                    Config.LoadInventory();
                }
            };
        }

        public void AddMenuLoadVehicle(UIMenu menu)
        {
            UIMenuItem newitem = new UIMenuItem("Load Vehicle", "Load saved Personal Vehicle (This will wipe your current Personal Vehicle)");
            menu.AddItem(newitem);
            menu.OnItemSelect += delegate (UIMenu sender, UIMenuItem item, int index) {
                if (ReferenceEquals(item, newitem))
                {
                    Config.LoadVehicle();
                }
            };
        }

        public void AddMenuLoadWeapons(UIMenu menu)
        {
            UIMenuItem newitem = new UIMenuItem("Load Weapons", "Load saved Weapons and Ammo (This will wipe your current Weapons and Ammo)");
            menu.AddItem(newitem);
            menu.OnItemSelect += delegate (UIMenu sender, UIMenuItem item, int index) {
                if (ReferenceEquals(item, newitem))
                {
                    Config.LoadWeapons();
                }
            };
        }

        public void AddMenuRegisterVehicle(UIMenu menu)
        {
            UIMenuItem newitem = new UIMenuItem("Register Vehicle", "Set current vehicle as Personal Vehicle");
            menu.AddItem(newitem);
            menu.OnItemSelect += delegate (UIMenu sender, UIMenuItem item, int index) {
                if (ReferenceEquals(item, newitem))
                {
                    Config.RegisterVehicle();
                }
            };
        }

        public void AddMenuSaveGroup(UIMenu menu)
        {
            UIMenuItem newitem = new UIMenuItem("Save Group", "Save your current group");
            menu.AddItem(newitem);
            menu.OnItemSelect += delegate (UIMenu sender, UIMenuItem item, int index) {
                if (ReferenceEquals(item, newitem))
                {
                    Config.SaveGroup();
                }
            };
        }

        public void AddMenuSaveInventory(UIMenu menu)
        {
            UIMenuItem newitem = new UIMenuItem("Save Inventory", "Save your current inventory");
            menu.AddItem(newitem);
            menu.OnItemSelect += delegate (UIMenu sender, UIMenuItem item, int index) {
                if (ReferenceEquals(item, newitem))
                {
                    Config.SaveInventory();
                }
            };
        }

        public void AddMenuSaveVehicle(UIMenu menu)
        {
            UIMenuItem newitem = new UIMenuItem("Save Vehicle", "Save your current Personal Vehicle");
            menu.AddItem(newitem);
            menu.OnItemSelect += delegate (UIMenu sender, UIMenuItem item, int index) {
                if (ReferenceEquals(item, newitem))
                {
                    Config.SaveVehicle();
                }
            };
        }

        public void AddMenuSaveWeapons(UIMenu menu)
        {
            UIMenuItem newitem = new UIMenuItem("Save Weapons", "Save your current Weapons and Ammo");
            menu.AddItem(newitem);
            menu.OnItemSelect += delegate (UIMenu sender, UIMenuItem item, int index) {
                if (ReferenceEquals(item, newitem))
                {
                    Config.SaveWeapons();
                }
            };
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Game.Player.Character.IsDead)
            {
                this.mainMenu.Visible = false;
            }
            if (Main.ModActive)
            {
                Game.DisableControlThisFrame(2, Control.CharacterWheel);
                if (Game.IsDisabledControlJustPressed(2, Control.CharacterWheel) && !Main.MasterMenuPool.IsAnyMenuOpen())
                {
                    this.mainMenu.Visible = !this.mainMenu.Visible;
                }
            }
        }
    }
}

