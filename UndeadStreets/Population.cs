namespace UndeadStreets
{
    using GTA;
    using GTA.Math;
    using GTA.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class Population
    {
        public static List<ZombiePed> zombieList;
        public static List<SurvivorPed> survivorList;
        public static List<Ped> animalList;
        public static Population instance;
        public int zombieCount = 0;
        public int vehicleCount = 0;
        public int animalCount = 0;
        public bool zomUpdateRanThisFrame = false;
        public bool zomPopRanThisFrame = false;
        public int zomPopTicksBetweenUpdates = 100;
        public int zomPopTicksSinceLastUpdate = 0;
        public bool vehPopRanThisFrame = false;
        public int vehPopTicksBetweenUpdates = 100;
        public int vehPopTicksSinceLastUpdate = 0;
        public bool aniUpdateRanThisFrame = false;
        public bool aniPopRanThisFrame = false;
        public int aniPopTicksBetweenUpdates = 100;
        public int aniPopTicksSinceLastUpdate = 0;
        public bool clearRanThisFrame = false;
        public int clearTicksBetweenUpdates = 100;
        public int clearTicksSinceLastUpdate = 0;
        public static DateTime suvLastSpawnTime = new DateTime();
        public Vector3 startingLoc;
        public static bool spawnVehicles = true;
        public static bool spawnSurvivors = true;
        public static bool spawnAnimals = true;
        public static bool zombieRunners = false;
        public static int maxZombies = 30;
        public static int maxVehicles = 10;
        public static int maxAnimals = 5;
        public static bool doubleCityPopulation = true;
        public static int survivorTime = 5;
        public static int zombieHealth = 750;
        public static int survivorHealth = 100;
        public static int minSpawnDistance = 50;
        public static int maxSpawnDistance = 100;
        public static bool customZombies = false;
        public static List<string> ZombieModels;
        public static bool customSurvivors;
        public static List<string> SurvivorModels;
        public static bool customCountryAnimals;
        public static List<string> CountryAnimalModels;
        public static bool customCityAnimals;
        public static List<string> CityAnimalModels;
        public static bool customCountryVehicles;
        public static List<string> CountryVehicleModels;
        public static bool customCityVehicles;
        public static List<string> CityVehicleModels;
        public static bool enableSafeZones;
        public static List<string> SafeZones;
        public static List<string> CityZones;

        static Population()
        {
            List<string> list1 = new List<string>();
            list1.Add("u_f_y_corpse_01");
            list1.Add("u_m_y_corpse_01");
            list1.Add("u_m_y_zombie_01");
            ZombieModels = list1;
            customSurvivors = false;
            List<string> list2 = new List<string>();
            list2.Add("mp_m_waremech_01");
            list2.Add("mp_m_weapexp_01");
            list2.Add("mp_m_weapwork_01");
            list2.Add("s_m_y_xmech_02");
            SurvivorModels = list2;
            customCountryAnimals = false;
            List<string> list3 = new List<string>();
            list3.Add("a_c_cat_01");
            list3.Add("a_c_boar");
            list3.Add("a_c_chickenhawk");
            list3.Add("a_c_coyote");
            list3.Add("a_c_deer");
            list3.Add("a_c_hen");
            list3.Add("a_c_husky");
            list3.Add("a_c_mtlion");
            list3.Add("a_c_poodle");
            list3.Add("a_c_pug");
            list3.Add("a_c_rabbit_01");
            list3.Add("a_c_rottweiler");
            list3.Add("a_c_shepherd");
            list3.Add("a_c_westy");
            CountryAnimalModels = list3;
            customCityAnimals = false;
            List<string> list4 = new List<string>();
            list4.Add("a_c_cat_01");
            list4.Add("a_c_husky");
            list4.Add("a_c_poodle");
            list4.Add("a_c_pug");
            list4.Add("a_c_rottweiler");
            list4.Add("a_c_shepherd");
            list4.Add("a_c_westy");
            CityAnimalModels = list4;
            customCountryVehicles = false;
            List<string> list5 = new List<string>();
            list5.Add("ambulance");
            list5.Add("firetruk");
            list5.Add("sheriff");
            list5.Add("sheriff2");
            list5.Add("biff");
            list5.Add("mule");
            list5.Add("mule2");
            list5.Add("mule3");
            list5.Add("mule4");
            list5.Add("pounder");
            list5.Add("pounder2");
            list5.Add("stockade");
            list5.Add("rhapsody");
            list5.Add("mixer");
            list5.Add("mixer2");
            list5.Add("rubble");
            list5.Add("tiptruck");
            list5.Add("tiptruck2");
            list5.Add("blade");
            list5.Add("buccaneer");
            list5.Add("chino");
            list5.Add("clique");
            list5.Add("coquette3");
            list5.Add("deviant");
            list5.Add("dukes");
            list5.Add("faction");
            list5.Add("faction2");
            list5.Add("faction3");
            list5.Add("ellie");
            list5.Add("impaler");
            list5.Add("imperator");
            list5.Add("picador");
            list5.Add("ratloader");
            list5.Add("tampa");
            list5.Add("tulip");
            list5.Add("vamos");
            list5.Add("vigero");
            list5.Add("virgo");
            list5.Add("virgo2");
            list5.Add("virgo3");
            list5.Add("voodoo");
            list5.Add("voodoo2");
            list5.Add("yosemite");
            list5.Add("bfinjection");
            list5.Add("bodhi2");
            list5.Add("dloader");
            list5.Add("everon");
            list5.Add("mesa3");
            list5.Add("rancherxl");
            list5.Add("rebel");
            list5.Add("rebel2");
            list5.Add("sandking");
            list5.Add("sandking2");
            list5.Add("dubsta2");
            list5.Add("granger");
            list5.Add("mesa");
            list5.Add("patriot");
            list5.Add("emperor");
            list5.Add("emperor2");
            list5.Add("glendale");
            list5.Add("ingot");
            list5.Add("regina");
            list5.Add("warrener");
            list5.Add("taxi");
            CountryVehicleModels = list5;
            customCityVehicles = false;
            List<string> list6 = new List<string>();
            list6.Add("ambulance");
            list6.Add("fbi");
            list6.Add("fbi2");
            list6.Add("firetruk");
            list6.Add("police1");
            list6.Add("police2");
            list6.Add("police3");
            list6.Add("police4");
            list6.Add("policet");
            list6.Add("riot");
            list6.Add("riot2");
            list6.Add("biff");
            list6.Add("mule");
            list6.Add("mule2");
            list6.Add("mule3");
            list6.Add("mule4");
            list6.Add("pounder");
            list6.Add("pounder2");
            list6.Add("stockade");
            list6.Add("asbo");
            list6.Add("blista");
            list6.Add("dilettante");
            list6.Add("kanjo");
            list6.Add("panto");
            list6.Add("prairie");
            list6.Add("cogcabrio");
            list6.Add("exemplar");
            list6.Add("f620");
            list6.Add("felon");
            list6.Add("jackal");
            list6.Add("oracle");
            list6.Add("oracle2");
            list6.Add("sentinel");
            list6.Add("sentinel2");
            list6.Add("windsor");
            list6.Add("windsor2");
            list6.Add("zion");
            list6.Add("guardian");
            list6.Add("deviant");
            list6.Add("dominator3");
            list6.Add("moonbeam");
            list6.Add("nightshade");
            list6.Add("dubsta3");
            list6.Add("freecrawler");
            list6.Add("hellion");
            list6.Add("mesa3");
            list6.Add("baller");
            list6.Add("baller2");
            list6.Add("baller3");
            list6.Add("baller4");
            list6.Add("baller5");
            list6.Add("baller6");
            list6.Add("bjxl");
            list6.Add("cavalcade");
            list6.Add("cavalcade2");
            list6.Add("contender");
            list6.Add("dubsta");
            list6.Add("dubsta2");
            list6.Add("granger");
            list6.Add("patriot");
            list6.Add("rebla");
            list6.Add("toros");
            list6.Add("asea");
            list6.Add("asterope");
            list6.Add("cog55");
            list6.Add("cog552");
            list6.Add("cognoscenti");
            list6.Add("cognoscenti2");
            list6.Add("emperor");
            list6.Add("fugitive");
            list6.Add("taxi");
            CityVehicleModels = list6;
            enableSafeZones = false;
            List<string> list7 = new List<string>();
            list7.Add("Los Santos International Airport");
            list7.Add("Fort Zancudo");
            list7.Add("Bolingbroke Penitentiary");
            list7.Add("Davis Quartz");
            list7.Add("Palmer-Taylor Power Station");
            list7.Add("RON Alternates Wind Farm");
            list7.Add("Terminal");
            list7.Add("Humane Labs and Research");
            SafeZones = list7;
            List<string> list8 = new List<string>();
            list8.Add("Los Santos International Airport");
            list8.Add("Elysian Island");
            list8.Add("Terminal");
            list8.Add("El Burro Heights");
            list8.Add("Murrieta Heights");
            list8.Add("Cypress Flats");
            list8.Add("Banning");
            list8.Add("Port of South Los Santos");
            list8.Add("Maze Bank Arena");
            list8.Add("La Puerta");
            list8.Add("Vespucci Beach");
            list8.Add("Vespucci");
            list8.Add("Vespucci Canals");
            list8.Add("Little Seoul");
            list8.Add("Strawberry");
            list8.Add("Chamberlain Hills");
            list8.Add("Davis");
            list8.Add("Rancho");
            list8.Add("La Mesa");
            list8.Add("Mission Row");
            list8.Add("Legion Square");
            list8.Add("Pillbox Hill");
            list8.Add("Del Perro Beach");
            list8.Add("Del Perro");
            list8.Add("Richards Majestic");
            list8.Add("Downtown");
            list8.Add("Downtown Vinewood");
            list8.Add("Vinewood");
            list8.Add("Mirror Park");
            list8.Add("East Vinewood");
            list8.Add("Alta");
            list8.Add("Hawick");
            list8.Add("Burton");
            list8.Add("Rockford Hills");
            list8.Add("Morningwood");
            list8.Add("Pacific Bluffs");
            list8.Add("Richman");
            list8.Add("GWC and Golfing Society");
            list8.Add("West Vinewood");
            list8.Add("Vinewood Racetrack");
            CityZones = list8;
        }

        public Population()
        {
            instance = this;
            this.startingLoc = new Vector3(478.8616f, -921.53f, 38.77953f);
            zombieList = new List<ZombiePed>();
            survivorList = new List<SurvivorPed>();
            animalList = new List<Ped>();
        }

        public void ClearUnlistedPeds()
        {
            this.clearRanThisFrame = false;
            this.clearTicksSinceLastUpdate++;
            if (!this.clearRanThisFrame && (this.clearTicksSinceLastUpdate >= this.clearTicksBetweenUpdates))
            {
                Ped[] allPeds = World.GetAllPeds();
                if (allPeds.Length != 0)
                {
                    foreach (Ped ped in allPeds)
                    {
                        bool flag4 = false;
                        int index = 0;
                        while (true)
                        {
                            if (index >= zombieList.Count)
                            {
                                break;
                            }
                            if ((zombieList[index].pedEntity == null) || zombieList[index].pedEntity.IsDead)
                            {
                                zombieList.RemoveAt(index);
                            }
                            else if (zombieList[index].pedEntity == ped)
                            {
                                flag4 = true;
                                break;
                            }
                            index++;
                        }
                        if (!flag4)
                        {
                            int num3 = 0;
                            while (true)
                            {
                                if (num3 >= survivorList.Count)
                                {
                                    break;
                                }
                                if ((survivorList[num3].pedEntity == null) || survivorList[num3].pedEntity.IsDead)
                                {
                                    survivorList.RemoveAt(num3);
                                }
                                else if (survivorList[num3].pedEntity == ped)
                                {
                                    flag4 = true;
                                    break;
                                }
                                num3++;
                            }
                        }
                        if (!flag4)
                        {
                            int num4 = 0;
                            while (true)
                            {
                                if (num4 >= animalList.Count)
                                {
                                    break;
                                }
                                if ((animalList[num4] == null) || animalList[num4].IsDead)
                                {
                                    animalList.RemoveAt(num4);
                                }
                                else if (animalList[num4] == ped)
                                {
                                    flag4 = true;
                                    break;
                                }
                                num4++;
                            }
                        }
                        if ((!flag4 && !ped.IsPlayer) && ped.IsAlive)
                        {
                            ped.Delete();
                        }
                    }
                }
                this.clearTicksSinceLastUpdate = 0;
                this.clearRanThisFrame = true;
            }
        }

        public static Model GetRandomVehicleModel()
        {
            Model model = new Model();
            if (IsCityZone(Game.Player.Character.Position))
            {
                model = new Model(RandoMath.GetRandomElementFromList<string>(CityVehicleModels));
            }
            else
            {
                model = new Model(RandoMath.GetRandomElementFromList<string>(CountryVehicleModels));
            }
            return (model.Request(0x5dc) ? model : 0);
        }

        public static Ped Infect(Ped ped)
        {
            ped.Task.ClearAllImmediately();
            ped.Weapons.Drop();
            ped.LeaveGroup();
            ped.RelationshipGroup = Relationships.ZombieGroup;
            ped.BlockPermanentEvents = true;
            InputArgument[] arguments = new InputArgument[] { ped.Handle, 0x2e, true };
            Function.Call(Hash._0x9F7794730795E019, arguments);
            InputArgument[] argumentArray2 = new InputArgument[] { ped.Handle, "BigHitByVehicle", 0, 1 };
            Function.Call(Hash._0x46DF918788CB093F, argumentArray2);
            InputArgument[] argumentArray3 = new InputArgument[] { ped.Handle, "HOSPITAL_8", 0, 1 };
            Function.Call(Hash._0x46DF918788CB093F, argumentArray3);
            InputArgument[] argumentArray4 = new InputArgument[] { ped.Handle, "HOSPITAL_9", 0, 1 };
            Function.Call(Hash._0x46DF918788CB093F, argumentArray4);
            InputArgument[] argumentArray5 = new InputArgument[] { ped.Handle, "Explosion_Med", 0, 1 };
            Function.Call(Hash._0x46DF918788CB093F, argumentArray5);
            ped.CanPlayGestures = false;
            InputArgument[] argumentArray6 = new InputArgument[] { ped.Handle, false };
            Function.Call(Hash._0x6373D1349925A70E, argumentArray6);
            InputArgument[] argumentArray7 = new InputArgument[] { ped.Handle, false };
            Function.Call(Hash._0x0EB0585D15254740, argumentArray7);
            InputArgument[] argumentArray8 = new InputArgument[] { ped.Handle, false };
            Function.Call(Hash._0x77A5B103C87F476E, argumentArray8);
            InputArgument[] argumentArray9 = new InputArgument[] { ped.Handle, false };
            Function.Call(Hash._0x6B7A646C242A7059, argumentArray9);
            InputArgument[] argumentArray10 = new InputArgument[] { ped.Handle, true };
            Function.Call(Hash._0x38FE1EC73743793C, argumentArray10);
            InputArgument[] argumentArray11 = new InputArgument[] { ped.Handle, true };
            Function.Call(Hash._0x4455517B28441E60, argumentArray11);
            InputArgument[] argumentArray12 = new InputArgument[] { ped.Handle, 0 };
            Function.Call(Hash._0xDBA71115ED9941A6, argumentArray12);
            InputArgument[] argumentArray13 = new InputArgument[] { ped.Handle, 0, 0 };
            Function.Call(Hash._0x70A2D1137C8ED7C9, argumentArray13);
            ped.DrownsInWater = true;
            ped.DiesInstantlyInWater = true;
            InputArgument[] argumentArray14 = new InputArgument[] { ped.Handle };
            if (Function.Call<bool>(Hash._0x9072C8B49907BFAD, argumentArray14))
            {
                InputArgument[] argumentArray15 = new InputArgument[] { ped.Handle };
                Function.Call(Hash._0xB8BEC0CA6F0EDB0F, argumentArray15);
            }
            InputArgument[] argumentArray16 = new InputArgument[] { ped.Handle, 1 };
            Function.Call(Hash._0x9D64D7405520E3D3, argumentArray16);
            InputArgument[] argumentArray17 = new InputArgument[] { ped.Handle, true };
            Function.Call(Hash._0xA9A41C1E940FB0E8, argumentArray17);
            int num = 0;
            TimeSpan currentDayTime = World.CurrentDayTime;
            num = ((currentDayTime.Hours < 20) && (currentDayTime.Hours > 3)) ? RandoMath.CachedRandom.Next(0, 50) : RandoMath.CachedRandom.Next(0, 100);
            bool flag = false;
            if (!zombieRunners)
            {
                num = 100;
            }
            if (num <= 10)
            {
                flag = true;
                InputArgument[] argumentArray18 = new InputArgument[] { "move_m@injured" };
                if (!Function.Call<bool>(Hash._0xC4EA073D86FB29B0, argumentArray18))
                {
                    InputArgument[] argumentArray19 = new InputArgument[] { "move_m@injured" };
                    Function.Call(Hash._0x6EA47DAE7FAD0EED, argumentArray19);
                }
                InputArgument[] argumentArray20 = new InputArgument[] { ped.Handle, "move_m@injured", 0x3e800000 };
                Function.Call(Hash._0xAF8A94EDE7712BEF, argumentArray20);
                InputArgument[] argumentArray21 = new InputArgument[] { ped.Handle, true };
                Function.Call(Hash._0x8E06A6FE76C9EFF4, argumentArray21);
            }
            else
            {
                flag = false;
                InputArgument[] argumentArray22 = new InputArgument[] { "move_m@drunk@verydrunk" };
                if (!Function.Call<bool>(Hash._0xC4EA073D86FB29B0, argumentArray22))
                {
                    InputArgument[] argumentArray23 = new InputArgument[] { "move_m@drunk@verydrunk" };
                    Function.Call(Hash._0x6EA47DAE7FAD0EED, argumentArray23);
                }
                InputArgument[] argumentArray24 = new InputArgument[] { ped.Handle, "move_m@drunk@verydrunk", 0x3e800000 };
                Function.Call(Hash._0xAF8A94EDE7712BEF, argumentArray24);
                InputArgument[] argumentArray25 = new InputArgument[] { ped.Handle, false };
                Function.Call(Hash._0x8E06A6FE76C9EFF4, argumentArray25);
            }
            ped.CanWrithe = false;
            ped.MaxHealth = zombieHealth;
            ped.Health = ped.MaxHealth;
            ped.Armor = 0;
            ped.Money = 0;
            InputArgument[] argumentArray26 = new InputArgument[] { ped.Handle, 150f };
            Function.Call(Hash._0xF29CF591C4BF6CEE, argumentArray26);
            InputArgument[] argumentArray27 = new InputArgument[] { ped.Handle, 300f };
            Function.Call(Hash._0x33A8F7F7D5F7F33C, argumentArray27);
            ped.AlwaysDiesOnLowHealth = false;
            ped.AlwaysKeepTask = true;
            ZombiePed item = new ZombiePed(ped);
            bool flag2 = false;
            int num2 = 0;
            while (true)
            {
                if (num2 < zombieList.Count)
                {
                    if (zombieList[num2].pedEntity != null)
                    {
                        num2++;
                        continue;
                    }
                    zombieList[num2].AttachData(ped);
                    zombieList[num2].isRunner = flag;
                    item = zombieList[num2];
                    flag2 = true;
                }
                if (flag2)
                {
                    UI.Notify("Zombie not added to list");
                }
                else
                {
                    zombieList.Add(item);
                    zombieList[zombieList.Count - 1].isRunner = flag;
                }
                return ped;
            }
        }

        public static bool IsCityZone(Vector3 position) => 
            CityZones.Exists(a => a.Equals(World.GetZoneName(position)));

        private static bool IsEnemy(Ped ped) => 
            ((!ped.IsHuman || (ped.IsDead || (ped.GetRelationshipWithPed(Game.Player.Character) < Relationship.Dislike))) ? ped.IsInCombatAgainst(Game.Player.Character) : true);

        public static bool IsSafeZone(Vector3 position) => 
            SafeZones.Exists(a => a.Equals(World.GetZoneName(position)));

        public void Populate()
        {
            this.ClearUnlistedPeds();
            this.PopulateZombies();
            if (spawnVehicles)
            {
                this.PopulateVehicles();
            }
            if (spawnSurvivors)
            {
                this.PopulateSurvivors();
            }
            if (spawnAnimals)
            {
                this.PopulateAnimals();
            }
        }

        public void PopulateAnimals()
        {
            int maxAnimals;
            int num2;
            Vector3 vector = new Vector3(0f, 0f, 0f);
            this.animalCount = animalList.Count;
            this.aniPopRanThisFrame = false;
            this.aniPopTicksSinceLastUpdate++;
            if (this.aniPopRanThisFrame || (this.aniPopTicksSinceLastUpdate < this.aniPopTicksBetweenUpdates))
            {
                return;
            }
            else
            {
                maxAnimals = Population.maxAnimals;
                if (IsCityZone(Game.Player.Character.Position))
                {
                    maxAnimals = Population.maxAnimals * 2;
                }
                num2 = 0;
            }
            while (true)
            {
                while (true)
                {
                    if (num2 < maxAnimals)
                    {
                        int num3 = RandoMath.CachedRandom.Next(1, 0x65);
                        bool flag4 = num3 <= 40;
                        vector = !flag4 ? World.GetNextPositionOnSidewalk(Game.Player.Character.Position.Around((float) maxSpawnDistance)) : World.GetNextPositionOnStreet(Game.Player.Character.Position.Around((float) maxSpawnDistance), true);
                        Vector3 vector2 = vector.Around(5f);
                        if (!((vector2.IsOnScreen() || (Extensions.DistanceTo(Game.Player.Character, vector) < minSpawnDistance)) || IsSafeZone(vector2)))
                        {
                            try
                            {
                                if (this.animalCount < maxAnimals)
                                {
                                    Model model;
                                    if (IsCityZone(vector))
                                    {
                                        model = new Model(RandoMath.GetRandomElementFromList<string>(CityAnimalModels));
                                    }
                                    else
                                    {
                                        model = new Model(RandoMath.GetRandomElementFromList<string>(CountryAnimalModels));
                                    }
                                    Ped item = World.CreatePed(model, vector);
                                    item.RelationshipGroup = Relationships.AnimalGroup;
                                    item.Task.WanderAround();
                                    animalList.Add(item);
                                    this.aniPopTicksSinceLastUpdate = 0 - RandoMath.CachedRandom.Next(this.aniPopTicksBetweenUpdates / 3);
                                    this.aniPopRanThisFrame = true;
                                }
                            }
                            catch (Exception exception1)
                            {
                                Debug.Log(exception1.ToString());
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                    break;
                }
                num2++;
            }
        }

        public void PopulateSurvivors()
        {
            Vector3 nextPositionOnStreet = World.GetNextPositionOnStreet(Game.Player.Character.Position.Around(500f), true);
            if (DateTime.UtcNow.Subtract(suvLastSpawnTime) >= TimeSpan.FromMinutes((double) survivorTime))
            {
                try
                {
                    SurvivorGroupSpawn(nextPositionOnStreet, GroupType.Random, -1, PedTasks.Wander);
                    suvLastSpawnTime = DateTime.UtcNow;
                }
                catch (Exception exception1)
                {
                    Debug.Log(exception1.ToString());
                }
            }
        }

        public void PopulateVehicles()
        {
            int maxVehicles;
            int num2;
            Vector3 nextPositionOnStreet = new Vector3(0f, 0f, 0f);
            Vehicle[] allVehicles = World.GetAllVehicles();
            this.vehicleCount = allVehicles.Length;
            this.vehPopRanThisFrame = false;
            this.vehPopTicksSinceLastUpdate++;
            if (this.vehPopRanThisFrame || (this.vehPopTicksSinceLastUpdate < this.vehPopTicksBetweenUpdates))
            {
                return;
            }
            else
            {
                maxVehicles = Population.maxVehicles;
                if (IsCityZone(Game.Player.Character.Position))
                {
                    maxVehicles = Population.maxVehicles * 2;
                }
                num2 = 0;
            }
            while (true)
            {
                while (true)
                {
                    if (num2 < maxVehicles)
                    {
                        Vector3 position = Game.Player.Character.Position;
                        nextPositionOnStreet = World.GetNextPositionOnStreet(position.Around((float) maxSpawnDistance), true);
                        Vector3 vector2 = nextPositionOnStreet.Around(5f);
                        if (!((vector2.IsOnScreen() || (Extensions.DistanceTo(Game.Player.Character, nextPositionOnStreet) < minSpawnDistance)) || IsSafeZone(vector2)))
                        {
                            try
                            {
                                this.VehicleSpawn(nextPositionOnStreet, RandoMath.RandomHeading());
                                this.vehPopTicksSinceLastUpdate = 0 - RandoMath.CachedRandom.Next(this.vehPopTicksBetweenUpdates / 3);
                                this.vehPopRanThisFrame = true;
                            }
                            catch (Exception exception1)
                            {
                                Debug.Log(exception1.ToString());
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                    break;
                }
                num2++;
            }
        }

        public void PopulateZombies()
        {
            int maxZombies;
            int num2;
            Vector3 vector = new Vector3(0f, 0f, 0f);
            this.zombieCount = zombieList.Count;
            this.zomPopRanThisFrame = false;
            this.zomPopTicksSinceLastUpdate++;
            if (this.zomPopRanThisFrame || (this.zomPopTicksSinceLastUpdate < this.zomPopTicksBetweenUpdates))
            {
                return;
            }
            else
            {
                maxZombies = Population.maxZombies;
                if (IsCityZone(Game.Player.Character.Position))
                {
                    maxZombies = Population.maxZombies * 2;
                }
                num2 = 0;
            }
            while (true)
            {
                while (true)
                {
                    if (num2 < maxZombies)
                    {
                        int num3 = RandoMath.CachedRandom.Next(1, 0x65);
                        bool flag4 = num3 <= 40;
                        vector = !flag4 ? World.GetNextPositionOnSidewalk(Game.Player.Character.Position.Around((float) maxSpawnDistance)) : World.GetNextPositionOnStreet(Game.Player.Character.Position.Around((float) maxSpawnDistance), true);
                        Vector3 vector2 = vector.Around(5f);
                        if (!((vector2.IsOnScreen() || (Extensions.DistanceTo(Game.Player.Character, vector) < minSpawnDistance)) || IsSafeZone(vector2)))
                        {
                            try
                            {
                                this.ZombieSpawn(vector);
                                this.zomPopTicksSinceLastUpdate = 0 - RandoMath.CachedRandom.Next(this.zomPopTicksBetweenUpdates / 3);
                                this.zomPopRanThisFrame = true;
                            }
                            catch (Exception exception1)
                            {
                                Debug.Log(exception1.ToString());
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                    break;
                }
                num2++;
            }
        }

        public static void Sleep(Vector3 position)
        {
            if (World.GetNearbyPeds(position, 50f).Where<Ped>(new Func<Ped, bool>(Population.IsEnemy)).ToArray<Ped>().Any<Ped>())
            {
                UI.Notify("You cannot sleep here as there are ~r~hostiles~w~ nearby!");
            }
            else
            {
                TimeSpan span = World.CurrentDayTime + new TimeSpan(0, 8, 0, 0);
                Game.Player.Character.IsVisible = false;
                Game.Player.CanControlCharacter = false;
                Game.Player.Character.FreezePosition = true;
                Game.FadeScreenOut(0x7d0);
                Script.Wait(0x7d0);
                World.CurrentDayTime = span;
                Game.Player.Character.IsVisible = true;
                Game.Player.CanControlCharacter = true;
                Game.Player.Character.FreezePosition = false;
                Game.Player.Character.ClearBloodDamage();
                World.Weather = RandoMath.GetRandomElementFromArray<Weather>(Map.weathers);
                Character.currentEnergyLevel = 1f;
                Character.currentHungerLevel -= 0.15f;
                Character.currentThirstLevel -= 0.25f;
                Script.Wait(0x7d0);
                Game.FadeScreenIn(0x7d0);
            }
        }

        public static void SurvivorGroupSpawn(Vector3 pos, GroupType groupType = 3, int groupSize = -1, PedTasks pedTasks = 1)
        {
            if (groupType == GroupType.Random)
            {
                int num = RandoMath.CachedRandom.Next(0, 3);
                if (num == 0)
                {
                    groupType = GroupType.Friendly;
                }
                if (num == 1)
                {
                    groupType = GroupType.Neutral;
                }
                if (num == 2)
                {
                    groupType = GroupType.Hostile;
                }
            }
            List<Ped> list = new List<Ped>();
            PedGroup group = new PedGroup();
            if (groupSize == -1)
            {
                groupSize = RandoMath.CachedRandom.Next(3, 9);
            }
            int num2 = 0;
            while (true)
            {
                if (num2 >= groupSize)
                {
                    foreach (Ped ped2 in list)
                    {
                        if (group.MemberCount < 1)
                        {
                            group.Add(ped2, true);
                            continue;
                        }
                        group.Add(ped2, false);
                    }
                    group.FormationType = FormationType.Default;
                    foreach (Ped ped3 in group.ToList(true))
                    {
                        PlayerGroup.SetPedTasks(ped3, pedTasks);
                    }
                    return;
                }
                SurvivorPed ped = SurvivorSpawn(pos);
                if (groupType == GroupType.Friendly)
                {
                    ped.pedEntity.RelationshipGroup = Relationships.FriendlyGroup;
                    ped.pedEntity.AddBlip();
                    ped.pedEntity.CurrentBlip.Color = BlipColor.Blue;
                    ped.pedEntity.CurrentBlip.Scale = 0.65f;
                    ped.pedEntity.CurrentBlip.Name = "Friendly";
                }
                else if (groupType == GroupType.Neutral)
                {
                    ped.pedEntity.RelationshipGroup = Relationships.NeutralGroup;
                    ped.pedEntity.AddBlip();
                    ped.pedEntity.CurrentBlip.Color = BlipColor.Yellow;
                    ped.pedEntity.CurrentBlip.Scale = 0.65f;
                    ped.pedEntity.CurrentBlip.Name = "Neutral";
                }
                else if (groupType == GroupType.Hostile)
                {
                    ped.pedEntity.RelationshipGroup = Relationships.HostileGroup;
                    ped.pedEntity.AddBlip();
                    ped.pedEntity.CurrentBlip.Color = BlipColor.Red;
                    ped.pedEntity.CurrentBlip.Scale = 0.65f;
                    ped.pedEntity.CurrentBlip.Name = "Hostile";
                }
                list.Add(ped.pedEntity);
                num2++;
            }
        }

        public static SurvivorPed SurvivorSpawn(Vector3 pos)
        {
            Ped ped;
            if (!customSurvivors)
            {
                ped = World.CreateRandomPed(pos.Around(5f));
            }
            else
            {
                Model model = new Model(RandoMath.GetRandomElementFromList<string>(SurvivorModels));
                ped = World.CreatePed(model, pos.Around(5f));
            }
            InputArgument[] arguments = new InputArgument[] { ped.Handle, 0, 0 };
            Function.Call(Hash._0x70A2D1137C8ED7C9, arguments);
            InputArgument[] argumentArray2 = new InputArgument[] { ped.Handle, true };
            Function.Call(Hash._0x8E06A6FE76C9EFF4, argumentArray2);
            InputArgument[] argumentArray3 = new InputArgument[] { ped.Handle, true };
            Function.Call(Hash._0x77A5B103C87F476E, argumentArray3);
            InputArgument[] argumentArray4 = new InputArgument[] { ped.Handle, true };
            Function.Call(Hash._0x6B7A646C242A7059, argumentArray4);
            ped.DiesInstantlyInWater = false;
            InputArgument[] argumentArray5 = new InputArgument[] { ped.Handle, 1, true };
            Function.Call(Hash._0x4D9CA1009AFBD057, argumentArray5);
            int num = RandoMath.CachedRandom.Next(0, 11);
            ped.MaxHealth = 100;
            ped.Health = ped.MaxHealth;
            ped.Armor = 70;
            if (num < 5)
            {
                ped.Weapons.Give(WeaponHash.SMG, 300, true, true);
                ped.Weapons.Give(WeaponHash.Pistol, 120, true, true);
                ped.Weapons.Give((WeaponHash) (-1716189206), 0, false, true);
            }
            if (num >= 5)
            {
                ped.Weapons.Give((WeaponHash) (-1074790547), 300, true, true);
                ped.Weapons.Give(WeaponHash.CombatPistol, 120, true, true);
                ped.Weapons.Give((WeaponHash) (-853065399), 0, false, true);
            }
            ped.Money = 0;
            ped.NeverLeavesGroup = true;
            SurvivorPed item = new SurvivorPed(ped);
            bool flag = false;
            int num2 = 0;
            while (true)
            {
                if (num2 < survivorList.Count)
                {
                    if (survivorList[num2].pedEntity != null)
                    {
                        num2++;
                        continue;
                    }
                    survivorList[num2].AttachData(ped);
                    item = survivorList[num2];
                    flag = true;
                }
                if (!flag)
                {
                    survivorList.Add(item);
                }
                else
                {
                    UI.Notify("Survivor not added to list");
                }
                return item;
            }
        }

        public void Update()
        {
            for (int i = 0; i < zombieList.Count; i++)
            {
                if ((!zombieList[i].pedEntity.IsAlive || (Extensions.DistanceBetween(zombieList[i].pedEntity, Game.Player.Character) > maxSpawnDistance)) || (zombieList[i].pedEntity == null))
                {
                    zombieList.RemoveAt(i);
                }
                else
                {
                    ZombiePed local1 = zombieList[i];
                    local1.ticksSinceLastUpdate++;
                    if (zombieList[i].ticksSinceLastUpdate >= zombieList[i].ticksBetweenUpdates)
                    {
                        zombieList[i].Update();
                        zombieList[i].ticksSinceLastUpdate = 0;
                    }
                }
            }
        }

        public void VehicleSpawn(Vector3 position, float heading)
        {
            int maxVehicles = Population.maxVehicles;
            if (IsCityZone(Game.Player.Character.Position))
            {
                maxVehicles = Population.maxVehicles * 2;
            }
            if (((this.vehicleCount < maxVehicles) && ((position != Vector3.Zero) && (Extensions.DistanceBetweenV3(position, this.startingLoc) >= minSpawnDistance))) && (Extensions.DistanceBetweenV3(position, Game.Player.Character.Position) >= minSpawnDistance))
            {
                Vehicle vehicle = Extensions.SpawnVehicle(GetRandomVehicleModel(), position, heading);
                vehicle.EngineHealth = (RandoMath.CachedRandom.Next(0, 100) > 10) ? 0f : 1000f;
                vehicle.DirtLevel = 14f;
                VehicleDoor[] doors = vehicle.GetDoors();
                int num3 = 0;
                while (true)
                {
                    if (num3 >= 5)
                    {
                        int item = 0;
                        while (true)
                        {
                            if (item >= 3)
                            {
                                break;
                            }
                            List<int> theList = new List<int>();
                            InputArgument[] arguments = new InputArgument[] { vehicle.Handle, item };
                            if (Function.Call<bool>(Hash._0x46E571A0E20D01F1, arguments))
                            {
                                theList.Add(item);
                            }
                            if (theList.Count > 0)
                            {
                                int randomElementFromList = RandoMath.GetRandomElementFromList<int>(theList);
                                InputArgument[] argumentArray2 = new InputArgument[] { vehicle.Handle, randomElementFromList };
                                Function.Call(Hash._0x9E5B5E4D2CCD2259, argumentArray2);
                            }
                            item++;
                        }
                        break;
                    }
                    VehicleDoor randomElementFromArray = RandoMath.GetRandomElementFromArray<VehicleDoor>(doors);
                    vehicle.OpenDoor(randomElementFromArray, false, true);
                    num3++;
                }
            }
        }

        public ZombiePed ZombieSpawn(Vector3 pos)
        {
            ZombiePed ped;
            int maxZombies = Population.maxZombies;
            if (IsCityZone(Game.Player.Character.Position))
            {
                maxZombies = Population.maxZombies * 2;
            }
            if (((this.zombieCount >= maxZombies) || ((pos == Vector3.Zero) || (Extensions.DistanceBetweenV3(pos, this.startingLoc) < minSpawnDistance))) || (Extensions.DistanceBetweenV3(pos, Game.Player.Character.Position) < minSpawnDistance))
            {
                ped = null;
            }
            else
            {
                Ped ped;
                if (!customZombies)
                {
                    ped = World.CreateRandomPed(pos);
                }
                else
                {
                    Model model = new Model(RandoMath.GetRandomElementFromList<string>(ZombieModels));
                    ped = World.CreatePed(model, pos);
                }
                Infect(ped);
                ZombiePed objA = zombieList.Find(a => a.pedEntity == ped);
                ped = !ReferenceEquals(objA, null) ? objA : null;
            }
            return ped;
        }
    }
}

