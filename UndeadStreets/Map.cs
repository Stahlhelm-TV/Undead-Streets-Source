namespace UndeadStreets
{
    using GTA;
    using GTA.Native;
    using System;

    public class Map
    {
        public static Weather[] weathers = new Weather[] { Weather.Clear, Weather.Clearing, Weather.Clouds, Weather.ExtraSunny, Weather.Foggy, Weather.Neutral, Weather.Overcast, Weather.Raining, Weather.Smog, Weather.ThunderStorm };

        public void Setup()
        {
            InputArgument[] arguments = new InputArgument[10];
            arguments[0] = -10000f;
            arguments[1] = -10000f;
            arguments[2] = -1000f;
            arguments[3] = 10000f;
            arguments[4] = 10000f;
            arguments[5] = 1000f;
            arguments[6] = 0;
            arguments[7] = 1;
            arguments[8] = 1;
            arguments[9] = 1;
            Function.Call(Hash._0x1B5C85C612E5256E, arguments);
            InputArgument[] argumentArray2 = new InputArgument[] { Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z, 1000f, false, false, false, false };
            Function.Call(Hash._0xA56F01F3765B93A0, argumentArray2);
            InputArgument[] argumentArray3 = new InputArgument[] { false };
            Function.Call(Hash._0x102E68B2024D536D, argumentArray3);
            InputArgument[] argumentArray4 = new InputArgument[] { false };
            Function.Call(Hash._0x84436EC293B1415F, argumentArray4);
            InputArgument[] argumentArray5 = new InputArgument[] { false };
            Function.Call(Hash._0x80D9F74197EA47D9, argumentArray5);
            InputArgument[] argumentArray6 = new InputArgument[] { false };
            Function.Call(Hash._0x2AFD795EEAC8D30D, argumentArray6);
            Function.Call(Hash._0x736A718577F39C7D, Array.Empty<InputArgument>());
            InputArgument[] argumentArray7 = new InputArgument[] { 0 };
            Function.Call(Hash._0x8C95333CFC3340F3, argumentArray7);
            InputArgument[] argumentArray8 = new InputArgument[] { 0 };
            Function.Call(Hash._0xCB9E1EB3BE2AF4E9, argumentArray8);
            InputArgument[] argumentArray9 = new InputArgument[] { false };
            Function.Call(Hash._0x608207E7A8FB787C, argumentArray9);
            InputArgument[] argumentArray10 = new InputArgument[] { 0 };
            Function.Call(Hash._0xCAA15F13EBD417FF, argumentArray10);
            InputArgument[] argumentArray11 = new InputArgument[] { false };
            Function.Call(Hash._0xF796359A959DF65D, argumentArray11);
            InputArgument[] argumentArray12 = new InputArgument[] { true };
            Function.Call(Hash._0xC9F98AC1884E73A2, argumentArray12);
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
            World.SetBlackout(true);
            World.TransitionToWeather(RandoMath.GetRandomElementFromArray<Weather>(weathers), 0f);
            InputArgument[] argumentArray13 = new InputArgument[] { 7, 0, 0 };
            Function.Call(Hash._0x47C3B5848C3E45D8, argumentArray13);
            InputArgument[] argumentArray14 = new InputArgument[] { 1, 1, 20 };
            Function.Call(Hash._0xB096419DF0D06CE7, argumentArray14);
        }

        public void Update()
        {
            Game.DisableControlThisFrame(0x55, Control.VehicleRadioWheel);
            Game.DisableControlThisFrame(0x51, Control.VehicleNextRadio);
            Game.DisableControlThisFrame(0x52, Control.VehiclePrevRadio);
            Game.DisableControlThisFrame(0x14d, Control.RadioWheelLeftRight);
            Game.DisableControlThisFrame(0x14c, Control.RadioWheelUpDown);
            InputArgument[] arguments = new InputArgument[] { 2, 0x13, true };
            Function.Call(Hash._0xFE99B66D079CF6BC, arguments);
            Function.Call(Hash._0x3BC861DF703E5097, Array.Empty<InputArgument>());
            InputArgument[] argumentArray2 = new InputArgument[] { 0 };
            Function.Call(Hash._0xCB9E1EB3BE2AF4E9, argumentArray2);
            InputArgument[] argumentArray3 = new InputArgument[] { 0 };
            Function.Call(Hash._0x8C95333CFC3340F3, argumentArray3);
            InputArgument[] argumentArray4 = new InputArgument[] { 0f };
            Function.Call(Hash._0x7A556143A1C03898, argumentArray4);
            InputArgument[] argumentArray5 = new InputArgument[] { 0f };
            Function.Call(Hash._0x245A6883D966D537, argumentArray5);
            InputArgument[] argumentArray6 = new InputArgument[] { 0f };
            Function.Call(Hash._0xB3B3359379FE77D3, argumentArray6);
            InputArgument[] argumentArray7 = new InputArgument[] { 0f };
            Function.Call(Hash._0xEAE6DCC7EEE3DB1D, argumentArray7);
            InputArgument[] argumentArray8 = new InputArgument[] { "PRISON_ALARMS", true };
            Function.Call(Hash._0xA1CADDCD98415A41, argumentArray8);
            InputArgument[] argumentArray9 = new InputArgument[] { "PoliceScannerDisabled", true };
            Function.Call(Hash._0xB9EFD5C25018725A, argumentArray9);
            InputArgument[] argumentArray10 = new InputArgument[] { "re_prison" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray10);
            InputArgument[] argumentArray11 = new InputArgument[] { "am_prison" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray11);
            InputArgument[] argumentArray12 = new InputArgument[] { "gb_biker_free_prisoner" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray12);
            InputArgument[] argumentArray13 = new InputArgument[] { "re_prisonvanbreak" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray13);
            InputArgument[] argumentArray14 = new InputArgument[] { "am_vehicle_spawn" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray14);
            InputArgument[] argumentArray15 = new InputArgument[] { "am_taxi" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray15);
            InputArgument[] argumentArray16 = new InputArgument[] { "audiotest" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray16);
            InputArgument[] argumentArray17 = new InputArgument[] { "freemode" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray17);
            InputArgument[] argumentArray18 = new InputArgument[] { "re_prisonerlift" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray18);
            InputArgument[] argumentArray19 = new InputArgument[] { "am_prison" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray19);
            InputArgument[] argumentArray20 = new InputArgument[] { "re_lossantosintl" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray20);
            InputArgument[] argumentArray21 = new InputArgument[] { "re_armybase" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray21);
            InputArgument[] argumentArray22 = new InputArgument[] { "restrictedareas" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray22);
            InputArgument[] argumentArray23 = new InputArgument[] { "stripclub" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray23);
            InputArgument[] argumentArray24 = new InputArgument[] { "re_gangfight" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray24);
            InputArgument[] argumentArray25 = new InputArgument[] { "re_gang_intimidation" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray25);
            InputArgument[] argumentArray26 = new InputArgument[] { "spawn_activities" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray26);
            InputArgument[] argumentArray27 = new InputArgument[] { "am_vehiclespawn" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray27);
            InputArgument[] argumentArray28 = new InputArgument[] { "traffick_air" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray28);
            InputArgument[] argumentArray29 = new InputArgument[] { "traffick_ground" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray29);
            InputArgument[] argumentArray30 = new InputArgument[] { "emergencycall" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray30);
            InputArgument[] argumentArray31 = new InputArgument[] { "emergencycalllauncher" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray31);
            InputArgument[] argumentArray32 = new InputArgument[] { "clothes_shop_sp" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray32);
            InputArgument[] argumentArray33 = new InputArgument[] { "gb_rob_shop" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray33);
            InputArgument[] argumentArray34 = new InputArgument[] { "gunclub_shop" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray34);
            InputArgument[] argumentArray35 = new InputArgument[] { "hairdo_shop_sp" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray35);
            InputArgument[] argumentArray36 = new InputArgument[] { "re_shoprobbery" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray36);
            InputArgument[] argumentArray37 = new InputArgument[] { "shop_controller" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray37);
            InputArgument[] argumentArray38 = new InputArgument[] { "re_crashrescue" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray38);
            InputArgument[] argumentArray39 = new InputArgument[] { "re_rescuehostage" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray39);
            InputArgument[] argumentArray40 = new InputArgument[] { "fm_mission_controller" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray40);
            InputArgument[] argumentArray41 = new InputArgument[] { "player_scene_m_shopping" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray41);
            InputArgument[] argumentArray42 = new InputArgument[] { "shoprobberies" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray42);
            InputArgument[] argumentArray43 = new InputArgument[] { "re_atmrobbery" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray43);
            InputArgument[] argumentArray44 = new InputArgument[] { "ob_vend1" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray44);
            InputArgument[] argumentArray45 = new InputArgument[] { "ob_vend2" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray45);
            InputArgument[] argumentArray46 = new InputArgument[] { "cellphone_controller" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray46);
            InputArgument[] argumentArray47 = new InputArgument[] { "blip_controller" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray47);
            InputArgument[] argumentArray48 = new InputArgument[] { "ambientblimp" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray48);
            InputArgument[] argumentArray49 = new InputArgument[] { "blimptest" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray49);
            InputArgument[] argumentArray50 = new InputArgument[] { "re_abandonedcar" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray50);
            InputArgument[] argumentArray51 = new InputArgument[] { "director_mode" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray51);
            InputArgument[] argumentArray52 = new InputArgument[] { "replay_controller" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray52);
            InputArgument[] argumentArray53 = new InputArgument[] { "rerecord_recording" };
            Function.Call(Hash._0x9DC711BC69C548DF, argumentArray53);
            InputArgument[] argumentArray54 = new InputArgument[] { "FBI_HEIST_H5_MUTE_AMBIENCE_SCENE" };
            Function.Call(Hash._0x013A80FC08F6E4F2, argumentArray54);
            InputArgument[] argumentArray55 = new InputArgument[] { "MIC1_RADIO_DISABLE" };
            Function.Call(Hash._0x013A80FC08F6E4F2, argumentArray55);
            InputArgument[] argumentArray56 = new InputArgument[] { true };
            Function.Call(Hash._0x808519373FD336A3, argumentArray56);
            InputArgument[] argumentArray57 = new InputArgument[] { "AZ_AFB_ALARM_SPEECH", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray57);
            InputArgument[] argumentArray58 = new InputArgument[] { "AZ_COUNTRYSIDE_CHILEAD_CABLE_CAR_LINE", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray58);
            InputArgument[] argumentArray59 = new InputArgument[] { "AZ_COUNTRYSIDE_DISTANT_CARS_ZONE_01", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray59);
            InputArgument[] argumentArray60 = new InputArgument[] { "AZ_COUNTRYSIDE_DISTANT_CARS_ZONE_02", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray60);
            InputArgument[] argumentArray61 = new InputArgument[] { "AZ_COUNTRYSIDE_DISTANT_CARS_ZONE_03", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray61);
            InputArgument[] argumentArray62 = new InputArgument[] { "AZ_COUNTRYSIDE_PRISON_01_ANNOUNCER_ALARM", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray62);
            InputArgument[] argumentArray63 = new InputArgument[] { "AZ_COUNTRYSIDE_PRISON_01_ANNOUNCER_GENERAL", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray63);
            InputArgument[] argumentArray64 = new InputArgument[] { "AZ_COUNTRYSIDE_PRISON_01_ANNOUNCER_WARNING", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray64);
            InputArgument[] argumentArray65 = new InputArgument[] { "AZ_COUNTRY_SAWMILL", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray65);
            InputArgument[] argumentArray66 = new InputArgument[] { "AZ_DISTANT_SASQUATCH", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray66);
            InputArgument[] argumentArray67 = new InputArgument[] { "AZ_DISTANT_VEHICLES_CITY_CENTRE", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray67);
            InputArgument[] argumentArray68 = new InputArgument[] { "AZ_DLC_HEISTS_BIOLAB", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray68);
            InputArgument[] argumentArray69 = new InputArgument[] { "AZ_DLC_HEIST_BIOLAB_GARAGE", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray69);
            InputArgument[] argumentArray70 = new InputArgument[] { "AZ_DLC_HEIST_POLICE_STATION_BOOST", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray70);
            InputArgument[] argumentArray71 = new InputArgument[] { "AZ_DMOD_TRAILER_01", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray71);
            InputArgument[] argumentArray72 = new InputArgument[] { "AZ_EPSILONISM_01_HILLS", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray72);
            InputArgument[] argumentArray73 = new InputArgument[] { "AZ_FBI_HEIST_SPRINKLER_FIRES_A_01", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray73);
            InputArgument[] argumentArray74 = new InputArgument[] { "AZ_FIB_HEIST_JANITOR_WALKIE_TALKIE", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray74);
            InputArgument[] argumentArray75 = new InputArgument[] { "AZ_PAPARAZZO_02_AMBIENCE", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray75);
            InputArgument[] argumentArray76 = new InputArgument[] { "AZ_PORT_OF_LS_UNDERWATER_CREAKS", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray76);
            InputArgument[] argumentArray77 = new InputArgument[] { "AZ_SAWMILL_CONVEYOR_01", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray77);
            InputArgument[] argumentArray78 = new InputArgument[] { "AZ_SOL_1_FACTORY_AREA_CONSTRUCTIONS", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray78);
            InputArgument[] argumentArray79 = new InputArgument[] { "AZ_SPECIAL_UFO_01", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray79);
            InputArgument[] argumentArray80 = new InputArgument[] { "AZ_SPECIAL_UFO_02", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray80);
            InputArgument[] argumentArray81 = new InputArgument[] { "AZ_SPECIAL_UFO_03", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray81);
            InputArgument[] argumentArray82 = new InputArgument[] { "AZ_UNDERWATER_EXILE_01_PLANE_WRECK", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray82);
            InputArgument[] argumentArray83 = new InputArgument[] { "AZ_YANKTON_CEMETARY", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray83);
            InputArgument[] argumentArray84 = new InputArgument[] { "AZ_strp3stge_SP", 0, 0 };
            Function.Call(Hash._0x120C48C614909FA4, argumentArray84);
            Function.Call(Hash._0x2F9A292AD0A3BD89, Array.Empty<InputArgument>());
            Function.Call(Hash._0x5F3B7749C112D552, Array.Empty<InputArgument>());
            Prop[] nearbyProps = World.GetNearbyProps(Game.Player.Character.Position, 10000f);
            int index = 0;
            while (true)
            {
                if (index >= nearbyProps.Length)
                {
                    Ped[] allPeds = World.GetAllPeds();
                    if (allPeds.Length != 0)
                    {
                        foreach (Ped ped in allPeds)
                        {
                            bool flag3 = !ped.IsAlive;
                            if (flag3 && ped.CurrentBlip.Exists())
                            {
                                ped.CurrentBlip.Remove();
                            }
                            if ((Extensions.DistanceBetween(ped, Game.Player.Character) > (Population.maxSpawnDistance + 30)) && (ped.RelationshipGroup == Relationships.ZombieGroup))
                            {
                                if (ped.CurrentBlip.Exists())
                                {
                                    ped.CurrentBlip.Remove();
                                }
                                ped.Delete();
                            }
                            if ((Extensions.DistanceBetween(ped, Game.Player.Character) > 1000.0) && (ped.RelationshipGroup != Relationships.PlayerGroup))
                            {
                                if (ped.CurrentBlip.Exists())
                                {
                                    ped.CurrentBlip.Remove();
                                }
                                ped.Delete();
                            }
                        }
                    }
                    Vehicle[] allVehicles = World.GetAllVehicles();
                    if (allVehicles.Length != 0)
                    {
                        foreach (Vehicle vehicle in allVehicles)
                        {
                            bool flag10 = vehicle != Character.playerVehicle;
                            if (flag10 && (Extensions.DistanceBetween(vehicle, Game.Player.Character) > (Population.maxSpawnDistance + 30)))
                            {
                                if (vehicle.CurrentBlip.Exists())
                                {
                                    vehicle.CurrentBlip.Remove();
                                }
                                vehicle.Delete();
                            }
                        }
                    }
                    return;
                }
                Model model = nearbyProps[index].Model;
                InputArgument[] argumentArray85 = new InputArgument[] { model.Hash, nearbyProps[index].Position.X, nearbyProps[index].Position.Y, nearbyProps[index].Position.Z, false, 0, 0 };
                Function.Call(Hash._0xF82D8F1926A02C3D, argumentArray85);
                index++;
            }
        }
    }
}

