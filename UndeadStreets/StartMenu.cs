namespace UndeadStreets
{
    using GTA;
    using NativeUI;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class StartMenu : Script
    {
        private bool runners = false;
        private UIMenu mainMenu = new UIMenu("Undead Streets", "Starting Settings");

        public StartMenu()
        {
            this.AddMenuRunners(this.mainMenu);
            this.AddMenuGender(this.mainMenu);
            this.AddMenuStart(this.mainMenu);
            UIResRectangle rectangle1 = new UIResRectangle();
            rectangle1.Color = Color.FromArgb(0xff, Color.DarkRed);
            UIResRectangle rectangle = rectangle1;
            this.mainMenu.SetBannerType(rectangle);
            Main.MasterMenuPool.Add(this.mainMenu);
            Main.MasterMenuPool.RefreshIndex();
            base.Tick += new EventHandler(this.OnTick);
            base.KeyDown += delegate (object o, KeyEventArgs e) {
                if (((e.KeyCode == Main.MenuKey) && !Main.ModActive) && !Main.MasterMenuPool.IsAnyMenuOpen())
                {
                    this.mainMenu.Visible = !this.mainMenu.Visible;
                }
            };
        }

        public void AddMenuGender(UIMenu menu)
        {
            List<object> list1 = new List<object>();
            list1.Add("Male");
            list1.Add("Female");
            List<object> items = list1;
            UIMenuListItem newitem = new UIMenuListItem("Gender", items, 0, "Select gender for character");
            menu.AddItem(newitem);
            menu.OnListChange += delegate (UIMenu sender, UIMenuListItem item, int index) {
                if (ReferenceEquals(item, newitem))
                {
                    Character.playerGender = (item.Items[index].ToString() != "Male") ? Gender.Female : Gender.Male;
                }
            };
        }

        public void AddMenuRunners(UIMenu menu)
        {
            UIMenuCheckboxItem newitem = new UIMenuCheckboxItem("Fast Zombies", this.runners, "Enable/Disable fast zombies");
            menu.AddItem(newitem);
            menu.OnCheckboxChange += delegate (UIMenu sender, UIMenuCheckboxItem item, bool checked_) {
                if (ReferenceEquals(item, newitem))
                {
                    this.runners = checked_;
                    Population.zombieRunners = this.runners;
                }
            };
        }

        public void AddMenuStart(UIMenu menu)
        {
            UIMenuItem newitem = new UIMenuItem("Start", "Begin playing Undead Streets");
            newitem.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            menu.AddItem(newitem);
            menu.OnItemSelect += delegate (UIMenu sender, UIMenuItem item, int index) {
                if (ReferenceEquals(item, newitem))
                {
                    this.mainMenu.Visible = !this.mainMenu.Visible;
                    Main.StartMod();
                }
            };
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Game.Player.Character.IsDead)
            {
                this.mainMenu.Visible = false;
            }
        }
    }
}

