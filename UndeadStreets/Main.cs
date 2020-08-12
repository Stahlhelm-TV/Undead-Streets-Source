namespace UndeadStreets
{
    using GTA;
    using GTA.Math;
    using GTA.Native;
    using NativeUI;
    using System;
    using System.Windows.Forms;

    public class Main : Script
    {
        public static bool ModActive = false;
        public static UndeadStreets.Map Map;
        public static UndeadStreets.Character Character;
        public static UndeadStreets.Population Population;
        public static UndeadStreets.Stats Stats;
        public static Keys MenuKey = Keys.F10;
        public static Keys InventoryKey = Keys.I;
        public static MenuPool MasterMenuPool = new MenuPool();

        public Main()
        {
            base.Tick += new EventHandler(this.OnTick);
            base.KeyDown += new KeyEventHandler(this.OnKeyDown);
            Map = new UndeadStreets.Map();
            Character = new UndeadStreets.Character();
            Population = new UndeadStreets.Population();
            Config.LoadSettings();
        }

        public void Custom_Respawn()
        {
            InputArgument[] arguments = new InputArgument[] { "respawn_controller" };
            Function.Call(Hash._0x9DC711BC69C548DF, arguments);
            InputArgument[] argumentArray2 = new InputArgument[] { true };
            Function.Call(Hash._0x21FFB63D8C615361, argumentArray2);
            InputArgument[] argumentArray3 = new InputArgument[] { true };
            Function.Call(Hash._0x2C2B3493FBF51C71, argumentArray3);
            if (Game.Player.Character.IsDead)
            {
                Vector3 vector = new Vector3(478.8616f, -921.53f, 38.77953f);
                while (true)
                {
                    if (Game.IsScreenFadedOut)
                    {
                        InputArgument[] argumentArray4 = new InputArgument[] { "respawn_controller" };
                        Function.Call(Hash._0x9DC711BC69C548DF, argumentArray4);
                        Game.TimeScale = 1f;
                        Function.Call(Hash._0xB4EDDC19532BFB85, Array.Empty<InputArgument>());
                        InputArgument[] argumentArray5 = new InputArgument[] { Game.Player.Character.Handle };
                        Function.Call(Hash._0xB69317BF5E782347, argumentArray5);
                        float num = 266f;
                        InputArgument[] argumentArray6 = new InputArgument[] { vector.X, vector.Y, vector.Z, num, false, false };
                        Function.Call(Hash._0xEA23C49EAA83ACFB, argumentArray6);
                        Wait(0x7d0);
                        Character.ResetCharacter();
                        Game.FadeScreenIn(0xdac);
                        Function.Call(Hash._0xC0AA53F866B3134D, Array.Empty<InputArgument>());
                        InputArgument[] argumentArray7 = new InputArgument[] { Game.Player.Character.Handle };
                        Function.Call(Hash._0x2D03E13C460760D6, argumentArray7);
                        InputArgument[] argumentArray8 = new InputArgument[] { true };
                        Function.Call(Hash._0xA6294919E56FF02A, argumentArray8);
                        Game.Player.Character.FreezePosition = false;
                        break;
                    }
                    Wait(0);
                }
            }
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (ModActive)
            {
                Character.Update();
                Map.Update();
                Population.Populate();
                Relationships.Update();
                Population.Update();
                this.Custom_Respawn();
            }
            MasterMenuPool.ProcessMenus();
        }

        public static void StartMod()
        {
            Game.FadeScreenOut(0x3e8);
            Wait(0x7d0);
            ModActive = true;
            Map.Setup();
            Character.Setup();
            Wait(0xbb8);
            Game.FadeScreenIn(0x3e8);
            BigMessageThread.MessageInstance.ShowOldMessage("~r~Undead Streets", 0x1388);
        }
    }
}

