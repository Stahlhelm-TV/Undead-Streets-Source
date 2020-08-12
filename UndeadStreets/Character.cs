namespace UndeadStreets
{
    using GTA;
    using GTA.Math;
    using GTA.Native;
    using NativeUI;
    using System;

    public class Character
    {
        public static Vehicle playerVehicle;
        public static Gender playerGender;
        public Model playerOldModel;
        public int playerOldMaxWantedLevel;
        public int playerOldMoney;
        public Vector3 playerOldPosition;
        public float playerOldHeading;
        public WeaponCollection playerOldWeapons;
        public static Prop campFire;
        public static Prop tent;
        public static Prop barrier;
        public static Prop dumpster;
        public static float currentHungerLevel = 1f;
        public static float maxHungerLevel = 1f;
        public static float hungerDecrease = 0.0004f;
        public static int hungerTicksBetweenUpdates = 200;
        public static int hungerTicksSinceLastUpdate;
        public static int lowHungerTicksBetweenUpdates = 100;
        public static int lowHungerTicksSinceLastUpdate;
        public static float currentThirstLevel = 1f;
        public static float maxThirstLevel = 1f;
        public static float thirstDecrease = 0.0008f;
        public static int thirstTicksBetweenUpdates = 200;
        public static int thirstTicksSinceLastUpdate;
        public static int lowThirstTicksBetweenUpdates = 100;
        public static int lowThirstTicksSinceLastUpdate;
        public static float currentEnergyLevel = 1f;
        public static float maxEnergyLevel = 1f;
        public static float energyDecrease = 0.0002f;
        public static int energyTicksBetweenUpdates = 200;
        public static int energyTicksSinceLastUpdate;
        public static int lowEnergyTicksBetweenUpdates = 100;
        public static int lowEnergyTicksSinceLastUpdate;

        public void ResetCharacter()
        {
            Game.Player.Money = 0;
            Game.Player.Character.Weapons.RemoveAll();
            Game.Player.Character.Weapons.Give(WeaponHash.Pistol, 120, false, true);
            Game.Player.Character.Weapons.Give(WeaponHash.SMG, 300, false, true);
            Game.Player.Character.Weapons.Give((WeaponHash) (-1716189206), 0, false, true);
            Game.Player.Character.Weapons.Give((WeaponHash) (-1569615261), 0, true, true);
            Game.Player.Character.Health = Game.Player.Character.MaxHealth;
            Game.Player.Character.Armor = 0;
            foreach (InventoryItem item in Inventory.playerItemInventory)
            {
                item.Amount = 0;
                object[] objArray1 = new object[] { "(", item.Amount, "/", item.MaxAmount, ")" };
                Inventory.itemsSubMenu.MenuItems.Find(menuItem => menuItem.Text == item.Name).SetRightLabel(string.Concat(objArray1));
            }
            foreach (InventoryMaterial material in Inventory.playerMaterialInventory)
            {
                material.Amount = 0;
                object[] objArray2 = new object[] { "(", material.Amount, "/", material.MaxAmount, ")" };
                Inventory.materialsSubMenu.MenuItems.Find(menuItem => menuItem.Text == material.Name).SetRightLabel(string.Concat(objArray2));
            }
            hungerTicksSinceLastUpdate = 0;
            thirstTicksSinceLastUpdate = 0;
            maxHungerLevel = 1f;
            currentHungerLevel = maxHungerLevel;
            maxThirstLevel = 1f;
            currentThirstLevel = maxThirstLevel;
            Ped[] allPeds = World.GetAllPeds();
            if (allPeds.Length != 0)
            {
                foreach (Ped ped in allPeds)
                {
                    ped.Delete();
                }
            }
            Vehicle[] allVehicles = World.GetAllVehicles();
            if (allVehicles.Length != 0)
            {
                foreach (Vehicle vehicle in allVehicles)
                {
                    vehicle.Delete();
                }
            }
            Population.suvLastSpawnTime = DateTime.UtcNow;
            playerVehicle = null;
        }

        public static void RestoreEnergy(float amount)
        {
            currentEnergyLevel += amount;
            if (currentEnergyLevel > maxEnergyLevel)
            {
                currentEnergyLevel = maxEnergyLevel;
            }
            if (currentEnergyLevel > 0.1f)
            {
                lowEnergyTicksSinceLastUpdate = 0;
            }
        }

        public static void RestoreHunger(float amount)
        {
            currentHungerLevel += amount;
            if (currentHungerLevel > maxHungerLevel)
            {
                currentHungerLevel = maxHungerLevel;
            }
            if (currentHungerLevel > 0.1f)
            {
                lowHungerTicksSinceLastUpdate = 0;
            }
        }

        public static void RestoreThirst(float amount)
        {
            currentThirstLevel += amount;
            if (currentThirstLevel > maxThirstLevel)
            {
                currentThirstLevel = maxThirstLevel;
            }
            if (currentThirstLevel > 0.1f)
            {
                lowThirstTicksSinceLastUpdate = 0;
            }
        }

        public void Revert()
        {
            Game.MaxWantedLevel = this.playerOldMaxWantedLevel;
            Game.Player.Money = 0;
            Model model = new Model(this.playerOldModel.Hash);
            model.Request(500);
            if (model.IsInCdImage && model.IsValid)
            {
                while (true)
                {
                    if (model.IsLoaded)
                    {
                        InputArgument[] arguments = new InputArgument[] { Game.Player, model.Hash };
                        Function.Call(Hash._0x00A1CADD00108836, arguments);
                        InputArgument[] argumentArray2 = new InputArgument[] { Game.Player.Character.Handle };
                        Function.Call(Hash._0x45EEE61580806D63, argumentArray2);
                        break;
                    }
                    Script.Wait(100);
                }
            }
            model.MarkAsNoLongerNeeded();
            Game.Player.Money = this.playerOldMoney;
            Game.Player.Character.Position = this.playerOldPosition;
            Game.Player.Character.Heading = this.playerOldHeading;
            campFire.Delete();
        }

        public void Setup()
        {
            this.playerOldMaxWantedLevel = Game.MaxWantedLevel;
            this.playerOldMoney = Game.Player.Money;
            this.playerOldPosition = Game.Player.Character.Position;
            this.playerOldHeading = Game.Player.Character.Heading;
            this.playerOldModel = Game.Player.Character.Model;
            this.playerOldWeapons = Game.Player.Character.Weapons;
            Game.MaxWantedLevel = 0;
            Game.Player.WantedLevel = 0;
            Game.Player.Money = 0;
            PedHash[] hashArray = new PedHash[] { PedHash.FreemodeMale01 };
            PedHash[] hashArray2 = new PedHash[] { (PedHash) (-1667301416) };
            PedHash[] theArray = (playerGender != Gender.Male) ? hashArray2 : hashArray;
            Model model = new Model(RandoMath.GetRandomElementFromArray<PedHash>(theArray));
            model.Request(500);
            if (model.IsInCdImage && model.IsValid)
            {
                while (true)
                {
                    if (model.IsLoaded)
                    {
                        InputArgument[] arguments = new InputArgument[] { Game.Player, model.Hash };
                        Function.Call(Hash._0x00A1CADD00108836, arguments);
                        if (playerGender != Gender.Male)
                        {
                            InputArgument[] argumentArray15 = new InputArgument[11];
                            argumentArray15[0] = Game.Player.Character.Handle;
                            argumentArray15[1] = 0x21;
                            argumentArray15[2] = 0x21;
                            argumentArray15[3] = 0;
                            argumentArray15[4] = 0x21;
                            argumentArray15[5] = 0x21;
                            argumentArray15[6] = 0;
                            argumentArray15[7] = 1f;
                            argumentArray15[8] = 1f;
                            argumentArray15[9] = 0f;
                            argumentArray15[10] = true;
                            Function.Call(Hash._0x9414E18B9434C2FE, argumentArray15);
                            InputArgument[] argumentArray16 = new InputArgument[] { Game.Player.Character.Handle, 2, 4, 0, 0 };
                            Function.Call(Hash._0x262B14F48D29DE80, argumentArray16);
                            InputArgument[] argumentArray17 = new InputArgument[] { Game.Player.Character.Handle, 11, 0xf3, 0, 0 };
                            Function.Call(Hash._0x262B14F48D29DE80, argumentArray17);
                            InputArgument[] argumentArray18 = new InputArgument[] { Game.Player.Character.Handle, 3, 7, 0, 0 };
                            Function.Call(Hash._0x262B14F48D29DE80, argumentArray18);
                            InputArgument[] argumentArray19 = new InputArgument[] { Game.Player.Character.Handle, 8, 0, 0, 0 };
                            Function.Call(Hash._0x262B14F48D29DE80, argumentArray19);
                            InputArgument[] argumentArray20 = new InputArgument[] { Game.Player.Character.Handle, 6, 0x18, 0, 0 };
                            Function.Call(Hash._0x262B14F48D29DE80, argumentArray20);
                            InputArgument[] argumentArray21 = new InputArgument[] { Game.Player.Character.Handle, 4, 0x54, 0, 0 };
                            Function.Call(Hash._0x262B14F48D29DE80, argumentArray21);
                            InputArgument[] argumentArray22 = new InputArgument[] { Game.Player.Character.Handle, 11, 11 };
                            Function.Call(Hash._0x4CFFC65454C93A49, argumentArray22);
                            InputArgument[] argumentArray23 = new InputArgument[] { Game.Player.Character.Handle, 3 };
                            Function.Call(Hash._0x50B56988B170AFDF, argumentArray23);
                            InputArgument[] argumentArray24 = new InputArgument[] { Game.Player.Character.Handle, 2, 1, 1f };
                            Function.Call(Hash._0x48F44967FA05CC1E, argumentArray24);
                            InputArgument[] argumentArray25 = new InputArgument[] { Game.Player.Character.Handle, 2, 1, 11, 11 };
                            Function.Call(Hash._0x497BF74A7B9CB952, argumentArray25);
                        }
                        else
                        {
                            InputArgument[] argumentArray2 = new InputArgument[11];
                            argumentArray2[0] = Game.Player.Character.Handle;
                            argumentArray2[1] = 0x2c;
                            argumentArray2[2] = 0x2c;
                            argumentArray2[3] = 0;
                            argumentArray2[4] = 0x2c;
                            argumentArray2[5] = 0x2c;
                            argumentArray2[6] = 0;
                            argumentArray2[7] = 1f;
                            argumentArray2[8] = 1f;
                            argumentArray2[9] = 0f;
                            argumentArray2[10] = true;
                            Function.Call(Hash._0x9414E18B9434C2FE, argumentArray2);
                            InputArgument[] argumentArray3 = new InputArgument[] { Game.Player.Character.Handle, 3, 12, 0, 0 };
                            Function.Call(Hash._0x262B14F48D29DE80, argumentArray3);
                            InputArgument[] argumentArray4 = new InputArgument[] { Game.Player.Character.Handle, 4, 1, 0, 0 };
                            Function.Call(Hash._0x262B14F48D29DE80, argumentArray4);
                            InputArgument[] argumentArray5 = new InputArgument[] { Game.Player.Character.Handle, 8, 0, 0, 0 };
                            Function.Call(Hash._0x262B14F48D29DE80, argumentArray5);
                            InputArgument[] argumentArray6 = new InputArgument[] { Game.Player.Character.Handle, 11, 0xe9, 0, 0 };
                            Function.Call(Hash._0x262B14F48D29DE80, argumentArray6);
                            InputArgument[] argumentArray7 = new InputArgument[] { Game.Player.Character.Handle, 6, 0x18, 0, 0 };
                            Function.Call(Hash._0x262B14F48D29DE80, argumentArray7);
                            InputArgument[] argumentArray8 = new InputArgument[] { Game.Player.Character.Handle, 2, 12, 0, 0 };
                            Function.Call(Hash._0x262B14F48D29DE80, argumentArray8);
                            InputArgument[] argumentArray9 = new InputArgument[] { Game.Player.Character.Handle, 0x39, 0x3a };
                            Function.Call(Hash._0x4CFFC65454C93A49, argumentArray9);
                            InputArgument[] argumentArray10 = new InputArgument[] { Game.Player.Character.Handle, 3 };
                            Function.Call(Hash._0x50B56988B170AFDF, argumentArray10);
                            InputArgument[] argumentArray11 = new InputArgument[] { Game.Player.Character.Handle, 1, 0, 1f };
                            Function.Call(Hash._0x48F44967FA05CC1E, argumentArray11);
                            InputArgument[] argumentArray12 = new InputArgument[] { Game.Player.Character.Handle, 2, 1, 1f };
                            Function.Call(Hash._0x48F44967FA05CC1E, argumentArray12);
                            InputArgument[] argumentArray13 = new InputArgument[] { Game.Player.Character.Handle, 1, 1, 0x39, 0x3a };
                            Function.Call(Hash._0x497BF74A7B9CB952, argumentArray13);
                            InputArgument[] argumentArray14 = new InputArgument[] { Game.Player.Character.Handle, 2, 1, 0x39, 0x3a };
                            Function.Call(Hash._0x497BF74A7B9CB952, argumentArray14);
                        }
                        break;
                    }
                    Script.Wait(100);
                }
            }
            model.MarkAsNoLongerNeeded();
            Game.Player.Money = 0;
            Game.Player.Character.Position = new Vector3(478.8616f, -921.53f, 38.77953f);
            Game.Player.Character.Heading = 266f;
            Model model2 = new Model("prop_beach_fire");
            model2.Request(250);
            if (model2.IsInCdImage && model2.IsValid)
            {
                while (true)
                {
                    if (model2.IsLoaded)
                    {
                        Vector3 position = new Vector3(482.3683f, -921.3369f, 37.2f);
                        campFire = World.CreateProp(model2, position, true, false);
                        break;
                    }
                    Script.Wait(50);
                }
            }
            model2.MarkAsNoLongerNeeded();
            campFire.AddBlip();
            campFire.CurrentBlip.Sprite = BlipSprite.HotProperty;
            campFire.CurrentBlip.Color = BlipColor.Yellow;
            campFire.CurrentBlip.Name = "Campfire";
            Model model3 = new Model("prop_skid_tent_01");
            model3.Request(250);
            if (model3.IsInCdImage && model3.IsValid)
            {
                while (true)
                {
                    if (model3.IsLoaded)
                    {
                        Vector3 position = new Vector3(478.2682f, -925.3043f, 36.8f);
                        tent = World.CreateProp(model3, position, true, false);
                        tent.Heading = 135f;
                        break;
                    }
                    Script.Wait(50);
                }
            }
            model3.MarkAsNoLongerNeeded();
            tent.AddBlip();
            tent.CurrentBlip.Sprite = BlipSprite.CaptureHouse;
            tent.CurrentBlip.Color = BlipColor.Blue;
            tent.CurrentBlip.Name = "Tent";
            Model model4 = new Model("prop_const_fence02a");
            model4.Request(250);
            if (model4.IsInCdImage && model4.IsValid)
            {
                while (true)
                {
                    if (model4.IsLoaded)
                    {
                        Vector3 position = new Vector3(418.9457f, -890.5727f, 28.4f);
                        barrier = World.CreateProp(model4, position, true, false);
                        barrier.Heading = 270f;
                        break;
                    }
                    Script.Wait(50);
                }
            }
            model4.MarkAsNoLongerNeeded();
            Model model5 = new Model("prop_dumpster_02a");
            model5.Request(250);
            if (model5.IsInCdImage && model5.IsValid)
            {
                while (true)
                {
                    if (model5.IsLoaded)
                    {
                        Vector3 position = new Vector3(459.4905f, -933.745f, 31.2f);
                        dumpster = World.CreateProp(model5, position, true, false);
                        dumpster.Heading = 270f;
                        break;
                    }
                    Script.Wait(50);
                }
            }
            model5.MarkAsNoLongerNeeded();
            this.ResetCharacter();
        }

        public void StatsUpdate()
        {
            hungerTicksSinceLastUpdate++;
            if (hungerTicksSinceLastUpdate >= hungerTicksBetweenUpdates)
            {
                currentHungerLevel -= hungerDecrease;
                if ((currentHungerLevel <= 0.1f) && (currentHungerLevel > 0f))
                {
                    UI.Notify("~r~WARNING:~w~ Hunger levels are getting low! You need to eat something to keep up your strength and avoid loss of health.");
                }
                else if (currentHungerLevel < 0.01f)
                {
                    UI.Notify("~r~WARNING:~w~ Hunger levels are dangerously low! You need to eat something to regain your strength and raise your health.");
                }
                if (currentHungerLevel < 0f)
                {
                    currentHungerLevel = 0f;
                }
                hungerTicksSinceLastUpdate = 0;
            }
            thirstTicksSinceLastUpdate++;
            if (thirstTicksSinceLastUpdate >= thirstTicksBetweenUpdates)
            {
                currentThirstLevel -= thirstDecrease;
                if ((currentThirstLevel <= 0.1f) && (currentThirstLevel > 0f))
                {
                    UI.Notify("~r~WARNING:~w~ Thirst levels are getting low! You need to drink something to keep up your strength and avoid loss of health.");
                }
                else if (currentThirstLevel < 0.01f)
                {
                    UI.Notify("~r~WARNING:~w~ Thirst levels are dangerously low! You need to drink something to regain your strength and raise your health.");
                }
                if (currentThirstLevel < 0f)
                {
                    currentThirstLevel = 0f;
                }
                thirstTicksSinceLastUpdate = 0;
            }
            if (currentHungerLevel < 0.01f)
            {
                lowHungerTicksSinceLastUpdate++;
                if (lowHungerTicksSinceLastUpdate >= lowHungerTicksBetweenUpdates)
                {
                    Ped character = Game.Player.Character;
                    character.Health--;
                    lowHungerTicksSinceLastUpdate = 0;
                }
            }
            if (currentThirstLevel < 0.01f)
            {
                lowThirstTicksSinceLastUpdate++;
                if (lowThirstTicksSinceLastUpdate >= lowThirstTicksBetweenUpdates)
                {
                    Ped character = Game.Player.Character;
                    character.Health--;
                    lowThirstTicksSinceLastUpdate = 0;
                }
            }
            energyTicksSinceLastUpdate++;
            if (energyTicksSinceLastUpdate >= energyTicksBetweenUpdates)
            {
                currentEnergyLevel -= energyDecrease;
                if ((currentEnergyLevel <= 0.1f) && (currentEnergyLevel > 0f))
                {
                    UI.Notify("~r~WARNING:~w~ Energy levels are getting low! You need to find somewhere safe to keep up your strength and avoid loss of health.");
                }
                else if (currentEnergyLevel < 0.01f)
                {
                    UI.Notify("~r~WARNING:~w~ Energy levels are dangerously low! You need to find somewhere safe to sleep to regain your strength and raise your health.");
                }
                if (currentEnergyLevel < 0f)
                {
                    currentEnergyLevel = 0f;
                }
                energyTicksSinceLastUpdate = 0;
                energyTicksSinceLastUpdate = 0;
            }
            if (currentEnergyLevel < 0.01f)
            {
                lowEnergyTicksSinceLastUpdate++;
                if (lowEnergyTicksSinceLastUpdate >= lowEnergyTicksBetweenUpdates)
                {
                    Ped character = Game.Player.Character;
                    character.Health--;
                    lowEnergyTicksSinceLastUpdate = 0;
                }
            }
        }

        public void Update()
        {
            Game.Player.WantedLevel = 0;
            this.StatsUpdate();
        }
    }
}

