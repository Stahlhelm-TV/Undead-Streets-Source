namespace UndeadStreets
{
    using GTA;
    using GTA.Native;
    using NativeUI;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class PlayerGroup : Script
    {
        public static PedCollection PlayerPedCollection = new PedCollection();
        public static List<UndeadStreets.Weapon> PlayerWeapons = new List<UndeadStreets.Weapon>();
        private UIMenu mainMenu = new UIMenu("Manage Group", "");
        private Ped currentGroupPed;
        private PedTasks taskApply;

        public PlayerGroup()
        {
            Main.MasterMenuPool.Add(this.mainMenu);
            this.AddMenuTasks(this.mainMenu);
            this.AddMenuApplyToPed(this.mainMenu);
            this.AddMenuApplyToAll(this.mainMenu);
            UIResRectangle rectangle1 = new UIResRectangle();
            rectangle1.Color = Color.FromArgb(0xff, Color.Purple);
            UIResRectangle rectangle = rectangle1;
            this.mainMenu.SetBannerType(rectangle);
            Main.MasterMenuPool.RefreshIndex();
            base.Tick += new EventHandler(this.OnTick);
        }

        public void AddMenuApplyToAll(UIMenu menu)
        {
            UIMenuItem newitem = new UIMenuItem("Set Task (All)", "Set task for all Group members");
            menu.AddItem(newitem);
            menu.OnItemSelect += delegate (UIMenu sender, UIMenuItem item, int index) {
                if (ReferenceEquals(item, newitem))
                {
                    foreach (Ped ped in Game.Player.Character.CurrentPedGroup.ToList(false))
                    {
                        SetPedTasks(ped, this.taskApply);
                    }
                    UI.Notify("You have given seleted task to all ~p~Group ~w~members");
                }
                this.mainMenu.Visible = !this.mainMenu.Visible;
            };
        }

        public void AddMenuApplyToPed(UIMenu menu)
        {
            UIMenuItem newitem = new UIMenuItem("Set Task", "Set task for selected Group member");
            menu.AddItem(newitem);
            menu.OnItemSelect += delegate (UIMenu sender, UIMenuItem item, int index) {
                if (ReferenceEquals(item, newitem))
                {
                    if (this.currentGroupPed == null)
                    {
                        UI.Notify("You do not have a ~p~Group ~w~member seleted");
                    }
                    else
                    {
                        SetPedTasks(this.currentGroupPed, this.taskApply);
                        UI.Notify("You have given seleted task to selected ~p~Group ~w~member");
                    }
                    this.mainMenu.Visible = !this.mainMenu.Visible;
                }
            };
        }

        public void AddMenuTasks(UIMenu menu)
        {
            List<object> list1 = new List<object>();
            list1.Add("None");
            list1.Add("Wander");
            list1.Add("Guard");
            list1.Add("Follow");
            list1.Add("Leave");
            List<object> items = list1;
            UIMenuListItem newitem = new UIMenuListItem("Task", items, 0, "Select a task for your Group member(s)");
            menu.AddItem(newitem);
            menu.OnListChange += delegate (UIMenu sender, UIMenuListItem item, int index) {
                if (ReferenceEquals(item, newitem))
                {
                    string str = item.Items[index].ToString();
                    if (str == "None")
                    {
                        this.taskApply = PedTasks.None;
                    }
                    else if (str == "Wander")
                    {
                        this.taskApply = PedTasks.Wander;
                    }
                    else if (str == "Guard")
                    {
                        this.taskApply = PedTasks.Guard;
                    }
                    else if (str == "Follow")
                    {
                        this.taskApply = PedTasks.Follow;
                    }
                    else if (str == "Leave")
                    {
                        this.taskApply = PedTasks.Leave;
                    }
                }
            };
        }

        public static void AddPedData(Ped ped)
        {
            PedTasks task = Population.survivorList.Find(a => a.pedEntity == ped).tasks;
            IEnumerable<WeaponHash> source = from hash in (WeaponHash[]) Enum.GetValues(typeof(WeaponHash))
                where ped.Weapons.HasWeapon(hash)
                select hash;
            WeaponComponent[] componentHashes = (WeaponComponent[]) Enum.GetValues(typeof(WeaponComponent));
            PedData item = new PedData(ped.Handle, ped.Model.Hash, ped.Rotation, ped.Position, task, source.ToList<WeaponHash>().ConvertAll<UndeadStreets.Weapon>(delegate (WeaponHash hash) {
                GTA.Weapon weapon = ped.Weapons[hash];
                return new UndeadStreets.Weapon(weapon.Ammo, weapon.Hash, (from h in componentHashes
                    where weapon.IsComponentActive(h)
                    select h).ToArray<WeaponComponent>());
            }).ToList<UndeadStreets.Weapon>());
            PlayerPedCollection.Add(item);
        }

        public static void LoadPedFromPedData(PedData pedData)
        {
            Ped ped = World.CreatePed(pedData.Hash, pedData.Position);
            SurvivorPed item = new SurvivorPed(ped);
            Population.survivorList.Add(item);
            if (ped != null)
            {
                ped.Rotation = pedData.Rotation;
                pedData.Weapons.ForEach(w => ped.Weapons.Give(w.Hash, w.Ammo, true, true));
                pedData.Handle = ped.Handle;
                ped.Recruit(Game.Player.Character);
                SetPedTasks(ped, pedData.Task);
            }
        }

        public static void LoadPlayerWeapons()
        {
            PlayerWeapons.ForEach(w => Game.Player.Character.Weapons.Give(w.Hash, w.Ammo, true, true));
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Main.ModActive)
            {
                Ped closest = World.GetClosest<Ped>(Game.Player.Character.Position, World.GetNearbyPeds(Game.Player.Character, 1.5f));
                if (((closest != null) && (!closest.IsDead && closest.IsHuman)) && (closest.RelationshipGroup == Relationships.FriendlyGroup))
                {
                    Extensions.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to recruit");
                    Game.DisableControlThisFrame(2, Control.Context);
                    if (Game.IsDisabledControlJustPressed(2, Control.Context))
                    {
                        try
                        {
                            closest.Recruit(Game.Player.Character);
                        }
                        catch (Exception exception1)
                        {
                            Debug.Log(exception1.ToString());
                        }
                    }
                }
                else if (((closest != null) && (!closest.IsDead && closest.IsHuman)) && (closest.RelationshipGroup == Game.Player.Character.RelationshipGroup))
                {
                    Extensions.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to manage Group");
                    Game.DisableControlThisFrame(2, Control.Context);
                    if (Game.IsDisabledControlJustPressed(2, Control.Context) && !Main.MasterMenuPool.IsAnyMenuOpen())
                    {
                        this.currentGroupPed = closest;
                        this.mainMenu.Visible = !this.mainMenu.Visible;
                    }
                }
            }
        }

        public static void SavePlayerWeapons()
        {
            IEnumerable<WeaponHash> source = from hash in (WeaponHash[]) Enum.GetValues(typeof(WeaponHash))
                where Game.Player.Character.Weapons.HasWeapon(hash)
                select hash;
            WeaponComponent[] componentHashes = (WeaponComponent[]) Enum.GetValues(typeof(WeaponComponent));
            PlayerWeapons = source.ToList<WeaponHash>().ConvertAll<UndeadStreets.Weapon>(delegate (WeaponHash hash) {
                GTA.Weapon weapon = Game.Player.Character.Weapons[hash];
                return new UndeadStreets.Weapon(weapon.Ammo, weapon.Hash, (from h in componentHashes
                    where weapon.IsComponentActive(h)
                    select h).ToArray<WeaponComponent>());
            }).ToList<UndeadStreets.Weapon>();
        }

        public static void SetPedTasks(Ped ped, PedTasks task)
        {
            ped.Task.ClearAll();
            if (task != PedTasks.Follow)
            {
                if (task == PedTasks.Guard)
                {
                    ped.Task.GuardCurrentPosition();
                }
                else if (task == PedTasks.Wander)
                {
                    ped.Task.WanderAround(ped.Position, 100f);
                }
                else if (task == PedTasks.None)
                {
                    ped.Task.StandStill(-1);
                }
                else if (task == PedTasks.Leave)
                {
                    ped.LeaveGroup();
                    Blip currentBlip = ped.CurrentBlip;
                    if (currentBlip.Handle != 0)
                    {
                        currentBlip.Remove();
                    }
                    ped.RelationshipGroup = Relationships.FriendlyGroup;
                    ped.Task.ClearAll();
                    Blip blip2 = ped.AddBlip();
                    blip2.Color = BlipColor.Blue;
                    blip2.Scale = 0.65f;
                    blip2.Name = "Friendly";
                    task = PedTasks.Wander;
                    ped.Task.WanderAround(ped.Position, 100f);
                }
            }
            Population.survivorList.Find(a => a.pedEntity == ped).tasks = task;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PlayerGroup.<>c <>9 = new PlayerGroup.<>c();
            public static Func<WeaponHash, bool> <>9__8_0;
            public static Action<UndeadStreets.Weapon> <>9__9_0;

            internal void <LoadPlayerWeapons>b__9_0(UndeadStreets.Weapon w)
            {
                Game.Player.Character.Weapons.Give(w.Hash, w.Ammo, true, true);
            }

            internal bool <SavePlayerWeapons>b__8_0(WeaponHash hash) => 
                Game.Player.Character.Weapons.HasWeapon(hash);
        }
    }
}

