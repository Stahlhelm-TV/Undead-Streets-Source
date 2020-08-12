namespace UndeadStreets
{
    using GTA;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Windows.Forms;

    public static class Config
    {
        public static ScriptSettings Settings;

        public static void LoadGroup()
        {
            if (!File.Exists("./scripts/UndeadStreets/SaveGame/Group.sav"))
            {
                UI.Notify("No ~p~Group ~w~available to load!");
            }
            else
            {
                PlayerGroup.PlayerPedCollection = ReadFromBinaryFile<PedCollection>("./scripts/UndeadStreets/SaveGame/Group.sav");
                if (PlayerGroup.PlayerPedCollection.Count <= 0)
                {
                    UI.Notify("~p~Group ~w~load failed!");
                }
                else
                {
                    List<PedData> list = PlayerGroup.PlayerPedCollection.ToList<PedData>();
                    foreach (Ped ped in Game.Player.Character.CurrentPedGroup.ToList(false))
                    {
                        Blip currentBlip = ped.CurrentBlip;
                        if (currentBlip.Type != 0)
                        {
                            currentBlip.Remove();
                        }
                        ped.LeaveGroup();
                        int index = Population.survivorList.FindIndex(a => a.pedEntity == ped);
                        Population.survivorList.RemoveAt(index);
                        ped.MarkAsNoLongerNeeded();
                        ped.Delete();
                    }
                    foreach (PedData data in list)
                    {
                        PlayerGroup.LoadPedFromPedData(data);
                    }
                    UI.Notify("~p~Group ~w~loaded!");
                }
            }
        }

        public static void LoadInventory()
        {
            if (!(File.Exists("./scripts/UndeadStreets/SaveGame/Items.sav") && File.Exists("./scripts/UndeadStreets/SaveGame/Materials.sav")))
            {
                UI.Notify("No ~y~Inventory ~w~available to load!");
            }
            else
            {
                try
                {
                    Inventory.playerItemInventory = ReadFromBinaryFile<List<InventoryItem>>("./scripts/UndeadStreets/SaveGame/Items.sav");
                    int num = 0;
                    while (true)
                    {
                        if (num >= Inventory.playerItemInventory.Count)
                        {
                            Inventory.playerMaterialInventory = ReadFromBinaryFile<List<InventoryMaterial>>("./scripts/UndeadStreets/SaveGame/Materials.sav");
                            int num2 = 0;
                            while (true)
                            {
                                if (num2 >= Inventory.playerMaterialInventory.Count)
                                {
                                    UI.Notify("~y~Inventory ~w~loaded!");
                                    break;
                                }
                                object[] objArray2 = new object[] { "(", Inventory.playerMaterialInventory[num2].Amount, "/", Inventory.playerMaterialInventory[num2].MaxAmount, ")" };
                                Inventory.materialsSubMenu.MenuItems[num2].SetRightLabel(string.Concat(objArray2));
                                num2++;
                            }
                            break;
                        }
                        object[] objArray1 = new object[] { "(", Inventory.playerItemInventory[num].Amount, "/", Inventory.playerItemInventory[num].MaxAmount, ")" };
                        Inventory.itemsSubMenu.MenuItems[num].SetRightLabel(string.Concat(objArray1));
                        num++;
                    }
                }
                catch (Exception exception1)
                {
                    Debug.Log(exception1.ToString());
                }
            }
        }

        public static void LoadSettings()
        {
            if (!Directory.Exists("./scripts/UndeadStreets/"))
            {
                Directory.CreateDirectory("./scripts/UndeadStreets/");
            }
            if (!Directory.Exists("./scripts/UndeadStreets/SaveGame/"))
            {
                Directory.CreateDirectory("./scripts/UndeadStreets/SaveGame/");
            }
            if (!Directory.Exists("./scripts/UndeadStreets/Settings/"))
            {
                Directory.CreateDirectory("./scripts/UndeadStreets/Settings/");
            }
            if (!Directory.Exists("./scripts/UndeadStreets/Logs/"))
            {
                Directory.CreateDirectory("./scripts/UndeadStreets/Logs/");
            }
            if (!File.Exists("./scripts/UndeadStreets/Settings/CountryVehicles.lst"))
            {
                File.WriteAllLines("./scripts/UndeadStreets/Settings/CountryVehicles.lst", Population.CountryVehicleModels);
            }
            if (!File.Exists("./scripts/UndeadStreets/Settings/CityVehicles.lst"))
            {
                File.WriteAllLines("./scripts/UndeadStreets/Settings/CityVehicles.lst", Population.CityVehicleModels);
            }
            if (!File.Exists("./scripts/UndeadStreets/Settings/Zombies.lst"))
            {
                File.WriteAllLines("./scripts/UndeadStreets/Settings/Zombies.lst", Population.ZombieModels);
            }
            if (!File.Exists("./scripts/UndeadStreets/Settings/Survivors.lst"))
            {
                File.WriteAllLines("./scripts/UndeadStreets/Settings/Survivors.lst", Population.SurvivorModels);
            }
            if (!File.Exists("./scripts/UndeadStreets/Settings/CityAnimals.lst"))
            {
                File.WriteAllLines("./scripts/UndeadStreets/Settings/CityAnimals.lst", Population.CityAnimalModels);
            }
            if (!File.Exists("./scripts/UndeadStreets/Settings/CountryAnimals.lst"))
            {
                File.WriteAllLines("./scripts/UndeadStreets/Settings/CountryAnimals.lst", Population.CountryAnimalModels);
            }
            if (File.Exists("./scripts/UndeadStreets/Settings/Settings.ini"))
            {
                Settings = ScriptSettings.Load("./scripts/UndeadStreets/Settings/Settings.ini");
                Main.MenuKey = Settings.GetValue<Keys>("hotkeys", "menu_key", Keys.F10);
                Main.InventoryKey = Settings.GetValue<Keys>("hotkeys", "inventory_key", Keys.I);
                Population.spawnVehicles = Settings.GetValue<bool>("world", "enable_abandoned_vehicles", true);
                Population.spawnSurvivors = Settings.GetValue<bool>("world", "enable_survivors", true);
                Population.spawnAnimals = Settings.GetValue<bool>("world", "enable_animals", true);
                Population.zombieHealth = Settings.GetValue<int>("world", "zombie_health", 750);
                Population.survivorHealth = Settings.GetValue<int>("world", "survivor_health", 750);
                Population.maxZombies = Settings.GetValue<int>("world", "max_zombies", 30);
                Population.maxVehicles = Settings.GetValue<int>("world", "max_vehicles", 10);
                Population.maxAnimals = Settings.GetValue<int>("world", "max_animals", 5);
                Population.doubleCityPopulation = Settings.GetValue<bool>("world", "enable_double_city_population", true);
                Population.survivorTime = Settings.GetValue<int>("world", "survivor_spawn_time", 5);
                Population.minSpawnDistance = Settings.GetValue<int>("world", "min_spawn_distance", 50);
                Population.maxSpawnDistance = Settings.GetValue<int>("world", "max_spawn_distance", 100);
                Population.customCityVehicles = Settings.GetValue<bool>("world", "enable_custom_city_vehicles", false);
                Population.customCityVehicles = Settings.GetValue<bool>("world", "enable_custom_country_vehicles", false);
                Population.customZombies = Settings.GetValue<bool>("world", "enable_custom_zombies", false);
                Population.customSurvivors = Settings.GetValue<bool>("world", "enable_custom_survivors", false);
                Population.customCityAnimals = Settings.GetValue<bool>("world", "enable_custom_city_animals", false);
                Population.customCountryAnimals = Settings.GetValue<bool>("world", "enable_custom_country_animals", false);
                Character.hungerDecrease = Settings.GetValue<float>("player", "hunger_rate", 0.0004f);
                Character.thirstDecrease = Settings.GetValue<float>("player", "thirst_rate", 0.0008f);
                Character.energyDecrease = Settings.GetValue<float>("player", "energy_rate", 0.0002f);
            }
            else
            {
                Settings = ScriptSettings.Load("./scripts/UndeadStreets/Settings/Settings.ini");
                Settings.SetValue<Keys>("hotkeys", "menu_key", Keys.F10);
                Settings.SetValue<Keys>("hotkeys", "inventory_key", Keys.I);
                Settings.SetValue<bool>("world", "enable_abandoned_vehicles", true);
                Settings.SetValue<bool>("world", "enable_survivors", true);
                Settings.SetValue<bool>("world", "enable_animals", true);
                Settings.SetValue<int>("world", "zombie_health", 750);
                Settings.SetValue<int>("world", "survivor_health", 100);
                Settings.SetValue<int>("world", "max_zombies", 30);
                Settings.SetValue<int>("world", "max_vehicles", 10);
                Settings.SetValue<int>("world", "max_animals", 5);
                Settings.SetValue<bool>("world", "enable_double_city_population", true);
                Settings.SetValue<int>("world", "survivor_spawn_time", 5);
                Settings.SetValue<int>("world", "min_spawn_distance", 50);
                Settings.SetValue<int>("world", "max_spawn_distance", 100);
                Settings.SetValue<bool>("world", "enable_custom_city_vehicles", false);
                Settings.SetValue<bool>("world", "enable_custom_country_vehicles", false);
                Settings.SetValue<bool>("world", "enable_custom_zombies", false);
                Settings.SetValue<bool>("world", "enable_custom_survivors", false);
                Settings.SetValue<bool>("world", "enable_custom_city_animals", false);
                Settings.SetValue<bool>("world", "enable_custom_country_animals", false);
                Settings.SetValue<float>("player", "hunger_rate", 0.0004f);
                Settings.SetValue<float>("player", "thirst_rate", 0.0008f);
                Settings.SetValue<float>("player", "energy_rate", 0.0002f);
                Main.MenuKey = Settings.GetValue<Keys>("hotkeys", "menu_key", Keys.F10);
                Main.InventoryKey = Settings.GetValue<Keys>("hotkeys", "inventory_key", Keys.I);
                Population.spawnVehicles = Settings.GetValue<bool>("world", "enable_abandoned_vehicles", true);
                Population.spawnSurvivors = Settings.GetValue<bool>("world", "enable_survivors", true);
                Population.spawnAnimals = Settings.GetValue<bool>("world", "enable_animals", true);
                Population.zombieHealth = Settings.GetValue<int>("world", "zombie_health", 750);
                Population.zombieHealth = Settings.GetValue<int>("world", "zombie_health", 750);
                Population.survivorHealth = Settings.GetValue<int>("world", "survivor_health", 750);
                Population.maxZombies = Settings.GetValue<int>("world", "max_zombies", 30);
                Population.maxVehicles = Settings.GetValue<int>("world", "max_vehicles", 10);
                Population.maxAnimals = Settings.GetValue<int>("world", "max_animals", 5);
                Population.doubleCityPopulation = Settings.GetValue<bool>("world", "enable_double_city_population", true);
                Population.survivorTime = Settings.GetValue<int>("world", "survivor_spawn_time", 5);
                Population.minSpawnDistance = Settings.GetValue<int>("world", "min_spawn_distance", 50);
                Population.maxSpawnDistance = Settings.GetValue<int>("world", "max_spawn_distance", 100);
                Population.customCityVehicles = Settings.GetValue<bool>("world", "enable_custom_city_vehicles", false);
                Population.customCityVehicles = Settings.GetValue<bool>("world", "enable_custom_country_vehicles", false);
                Population.customZombies = Settings.GetValue<bool>("world", "enable_custom_zombies", false);
                Population.customSurvivors = Settings.GetValue<bool>("world", "enable_custom_survivors", false);
                Population.customCityAnimals = Settings.GetValue<bool>("world", "enable_custom_city_animals", false);
                Population.customCountryAnimals = Settings.GetValue<bool>("world", "enable_custom_country_animals", false);
                Character.hungerDecrease = Settings.GetValue<float>("player", "hunger_rate", 0.0004f);
                Character.thirstDecrease = Settings.GetValue<float>("player", "thirst_rate", 0.0008f);
                Character.energyDecrease = Settings.GetValue<float>("player", "energy_rate", 0.0002f);
                Settings.Save();
            }
            if (Population.customCityVehicles)
            {
                try
                {
                    Population.CityVehicleModels = File.ReadAllLines("./scripts/UndeadStreets/Settings/CityVehicles.lst").ToList<string>();
                }
                catch (Exception exception1)
                {
                    Debug.Log(exception1.ToString());
                }
            }
            if (Population.customCountryVehicles)
            {
                try
                {
                    Population.CountryVehicleModels = File.ReadAllLines("./scripts/UndeadStreets/Settings/CountryVehicles.lst").ToList<string>();
                }
                catch (Exception exception7)
                {
                    Debug.Log(exception7.ToString());
                }
            }
            if (Population.customZombies)
            {
                try
                {
                    Population.ZombieModels = File.ReadAllLines("./scripts/UndeadStreets/Settings/Zombies.lst").ToList<string>();
                }
                catch (Exception exception8)
                {
                    Debug.Log(exception8.ToString());
                }
            }
            if (Population.customSurvivors)
            {
                try
                {
                    Population.SurvivorModels = File.ReadAllLines("./scripts/UndeadStreets/Settings/Survivors.lst").ToList<string>();
                }
                catch (Exception exception9)
                {
                    Debug.Log(exception9.ToString());
                }
            }
            if (Population.customCountryAnimals)
            {
                try
                {
                    Population.CountryAnimalModels = File.ReadAllLines("./scripts/UndeadStreets/Settings/CountryAnimals.lst").ToList<string>();
                }
                catch (Exception exception10)
                {
                    Debug.Log(exception10.ToString());
                }
            }
            if (Population.customCityAnimals)
            {
                try
                {
                    Population.CityAnimalModels = File.ReadAllLines("./scripts/UndeadStreets/Settings/CityAnimals.lst").ToList<string>();
                }
                catch (Exception exception11)
                {
                    Debug.Log(exception11.ToString());
                }
            }
        }

        public static void LoadVehicle()
        {
            if (!File.Exists("./scripts/UndeadStreets/SaveGame/Vehicle.sav"))
            {
                UI.Notify("No ~o~Personal Vehicle ~w~available to load!");
            }
            else
            {
                PlayerVehicle.PlayerVehicleCollection = ReadFromBinaryFile<VehicleCollection>("./scripts/UndeadStreets/SaveGame/Vehicle.sav");
                if (PlayerVehicle.PlayerVehicleCollection.Count <= 0)
                {
                    UI.Notify("~o~Personal Vehicle ~w~load failed!");
                }
                else
                {
                    List<VehicleData> list = PlayerVehicle.PlayerVehicleCollection.ToList<VehicleData>();
                    if (Character.playerVehicle != null)
                    {
                        Character.playerVehicle.CurrentBlip.Remove();
                        Character.playerVehicle.MarkAsNoLongerNeeded();
                        Character.playerVehicle.Delete();
                    }
                    foreach (VehicleData data in list)
                    {
                        PlayerVehicle.LoadVehicleFromVehicleData(data);
                    }
                    Blip blip = Character.playerVehicle.AddBlip();
                    blip.Sprite = BlipSprite.GetawayCar;
                    blip.Color = BlipColor.Green;
                    blip.Name = "Personal Vehicle";
                    UI.Notify("~o~Personal Vehicle ~w~loaded!");
                }
            }
        }

        public static void LoadWeapons()
        {
            if (!File.Exists("./scripts/UndeadStreets/SaveGame/Weapons.sav"))
            {
                UI.Notify("No ~r~Weapons ~w~available to load!");
            }
            else
            {
                PlayerGroup.PlayerWeapons = ReadFromBinaryFile<List<UndeadStreets.Weapon>>("./scripts/UndeadStreets/SaveGame/Weapons.sav");
                if (PlayerGroup.PlayerWeapons.Count <= 0)
                {
                    UI.Notify("~r~Weapons ~w~load failed!");
                }
                else
                {
                    Game.Player.Character.Weapons.RemoveAll();
                    PlayerGroup.LoadPlayerWeapons();
                    UI.Notify("~r~Weapons ~w~loaded!");
                }
            }
        }

        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                return (T) new BinaryFormatter().Deserialize(stream);
            }
        }

        public static void RegisterVehicle()
        {
            if (!Game.Player.Character.IsInVehicle())
            {
                UI.Notify("You are not in a vehicle!");
            }
            else
            {
                Vehicle currentVehicle = Game.Player.Character.CurrentVehicle;
                if ((Character.playerVehicle != null) && (Character.playerVehicle.CurrentBlip.Type != 0))
                {
                    Character.playerVehicle.CurrentBlip.Remove();
                }
                Character.playerVehicle = currentVehicle;
                if (Character.playerVehicle.CurrentBlip.Type != 0)
                {
                    Character.playerVehicle.CurrentBlip.Remove();
                }
                Blip blip = Character.playerVehicle.AddBlip();
                blip.Sprite = BlipSprite.GetawayCar;
                blip.Color = BlipColor.Green;
                blip.Name = "Personal Vehicle";
                UI.Notify("Current vehicle now registered as ~o~Personal Vehicle");
            }
        }

        public static void SaveGroup()
        {
            List<Ped> list = Game.Player.Character.CurrentPedGroup.ToList(false);
            if (list.Count <= 0)
            {
                UI.Notify("You have no one in your group!");
                return;
            }
            else
            {
                int index = 0;
                while (true)
                {
                    if (index >= PlayerGroup.PlayerPedCollection.Count)
                    {
                        foreach (Ped ped in list)
                        {
                            PlayerGroup.AddPedData(ped);
                        }
                        break;
                    }
                    PlayerGroup.PlayerPedCollection.RemoveAt(index);
                    index++;
                }
            }
            try
            {
                WriteToBinaryFile<PedCollection>("./scripts/UndeadStreets/SaveGame/Group.sav", PlayerGroup.PlayerPedCollection, false);
                UI.Notify("~p~Group ~w~saved!");
            }
            catch (Exception exception1)
            {
                Debug.Log(exception1.ToString());
            }
        }

        public static void SaveInventory()
        {
            try
            {
                WriteToBinaryFile<List<InventoryItem>>("./scripts/UndeadStreets/SaveGame/Items.sav", Inventory.playerItemInventory, false);
                WriteToBinaryFile<List<InventoryMaterial>>("./scripts/UndeadStreets/SaveGame/Materials.sav", Inventory.playerMaterialInventory, false);
                UI.Notify("~y~Inventory ~w~saved!");
            }
            catch (Exception exception1)
            {
                Debug.Log(exception1.ToString());
            }
        }

        public static void SaveVehicle()
        {
            if (Character.playerVehicle == null)
            {
                UI.Notify("You don't have a ~o~Personal Vehicle~w~ to save!");
            }
            else
            {
                int index = 0;
                while (true)
                {
                    if (index >= PlayerVehicle.PlayerVehicleCollection.Count)
                    {
                        PlayerVehicle.AddVehicleData(Character.playerVehicle);
                        try
                        {
                            WriteToBinaryFile<VehicleCollection>("./scripts/UndeadStreets/SaveGame/Vehicle.sav", PlayerVehicle.PlayerVehicleCollection, false);
                            UI.Notify("~o~Personal Vehicle~w~ saved!");
                        }
                        catch (Exception exception1)
                        {
                            Debug.Log(exception1.ToString());
                        }
                        break;
                    }
                    PlayerVehicle.PlayerVehicleCollection.RemoveAt(index);
                    index++;
                }
            }
        }

        public static void SaveWeapons()
        {
            int index = 0;
            while (true)
            {
                if (index >= PlayerGroup.PlayerWeapons.Count)
                {
                    PlayerGroup.SavePlayerWeapons();
                    try
                    {
                        WriteToBinaryFile<List<UndeadStreets.Weapon>>("./scripts/UndeadStreets/SaveGame/Weapons.sav", PlayerGroup.PlayerWeapons, false);
                        UI.Notify("~r~Weapons~w~ saved!");
                    }
                    catch (Exception exception1)
                    {
                        Debug.Log(exception1.ToString());
                    }
                    return;
                }
                PlayerGroup.PlayerWeapons.RemoveAt(index);
                index++;
            }
        }

        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                new BinaryFormatter().Serialize(stream, objectToWrite);
            }
        }
    }
}

