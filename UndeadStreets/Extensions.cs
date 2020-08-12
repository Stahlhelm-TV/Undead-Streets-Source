namespace UndeadStreets
{
    using GTA;
    using GTA.Math;
    using GTA.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class Extensions
    {
        public static void ClearAllHelpText()
        {
            Function.Call(Hash._0x6178F68A87A4D3A0, Array.Empty<InputArgument>());
        }

        public static void DisplayHelpTextThisFrame(string helpText)
        {
            InputArgument[] arguments = new InputArgument[] { "CELL_EMAIL_BCON" };
            Function.Call(Hash._0x8509B634FBE7DA11, arguments);
            int startIndex = 0;
            while (true)
            {
                if (startIndex >= helpText.Length)
                {
                    InputArgument[] argumentArray3 = new InputArgument[4];
                    argumentArray3[0] = 0;
                    argumentArray3[1] = 0;
                    argumentArray3[2] = !Function.Call<bool>(Hash._0x4D79439A6B55AC67, Array.Empty<InputArgument>()) ? 1 : 0;
                    InputArgument[] local1 = argumentArray3;
                    local1[3] = -1;
                    Function.Call(Hash._0x238FFE5C7B0498A6, local1);
                    return;
                }
                InputArgument[] argumentArray2 = new InputArgument[] { helpText.Substring(startIndex, Math.Min(0x63, helpText.Length - startIndex)) };
                Function.Call(Hash._0x6C188BE134E074AA, argumentArray2);
                startIndex += 0x63;
            }
        }

        public static double DistanceBetween(Entity p1, Entity p2)
        {
            Vector3 position = p1.Position;
            Vector3 vector2 = p2.Position;
            double num2 = vector2.X - position.X;
            double num3 = vector2.Y - position.Y;
            double num4 = vector2.Z - position.Z;
            return Math.Sqrt(((num2 * num2) + (num3 * num3)) + (num4 * num4));
        }

        public static double DistanceBetweenV3(Vector3 p1, Vector3 p2)
        {
            Vector3 vector = p1;
            Vector3 vector2 = p2;
            double num2 = vector2.X - vector.X;
            double num3 = vector2.Y - vector.Y;
            double num4 = vector2.Z - vector.Z;
            return Math.Sqrt(((num2 * num2) + (num3 * num3)) + (num4 * num4));
        }

        public static double DistanceTo(Entity p1, Vector3 p2)
        {
            Vector3 position = p1.Position;
            Vector3 vector2 = p2;
            double num2 = vector2.X - position.X;
            double num3 = vector2.Y - position.Y;
            double num4 = vector2.Z - position.Z;
            return Math.Sqrt(((num2 * num2) + (num3 * num3)) + (num4 * num4));
        }

        [Extension]
        public static bool HasClearLineOfSight(this Entity entity, Entity target, float visionDistance)
        {
            InputArgument[] arguments = new InputArgument[] { entity.Handle, target.Handle };
            return (Function.Call<bool>(Hash._0xFCDFF7B72D23A1AC, arguments) && (entity.Position.DistanceTo(target.Position) < visionDistance));
        }

        public static bool IsAnyHelpTextOnScreen() => 
            Function.Call<bool>(Hash._0xDAD37F45428801AE, Array.Empty<InputArgument>());

        [Extension]
        public static bool IsCurrentWeaponSileced(this Ped ped)
        {
            InputArgument[] arguments = new InputArgument[] { ped.Handle };
            return Function.Call<bool>(Hash._0x65F0C5AE05943EC7, arguments);
        }

        [Extension]
        public static bool IsOnScreen(this Vector3 vector3)
        {
            Vector3 position = GameplayCamera.Position;
            Vector3 direction = GameplayCamera.Direction;
            float fieldOfView = GameplayCamera.FieldOfView;
            return (Vector3.Angle(Vector3.Subtract(vector3, position), direction) < fieldOfView);
        }

        [Extension]
        public static void Recruit(this Ped ped, Ped leader)
        {
            if (leader != null)
            {
                PedGroup currentPedGroup = leader.CurrentPedGroup;
                ped.LeaveGroup();
                InputArgument[] arguments = new InputArgument[] { ped.Handle, false };
                Function.Call(Hash._0xF0A4F1BBF4FA7497, arguments);
                ped.Task.ClearAll();
                currentPedGroup.SeparationRange = 2.147484E+09f;
                if (!currentPedGroup.Contains(leader))
                {
                    currentPedGroup.Add(leader, true);
                }
                if (!currentPedGroup.Contains(ped))
                {
                    currentPedGroup.Add(ped, false);
                }
                ped.IsPersistent = true;
                ped.RelationshipGroup = leader.RelationshipGroup;
                ped.NeverLeavesGroup = true;
                Blip currentBlip = ped.CurrentBlip;
                if (currentBlip.Type != 0)
                {
                    currentBlip.Remove();
                }
                Blip blip2 = ped.AddBlip();
                blip2.Color = BlipColor.Green;
                blip2.Scale = 0.65f;
                blip2.Name = "Group";
                PlayerGroup.SetPedTasks(ped, PedTasks.Follow);
            }
        }

        public static Vehicle SpawnVehicle(Model model, Vector3 position, float heading = 0f)
        {
            Vehicle vehicle2;
            if (!model.IsVehicle || !model.Request(0x3e8))
            {
                vehicle2 = null;
            }
            else
            {
                InputArgument[] arguments = new InputArgument[] { model.Hash, position.X, position.Y, position.Z, heading, false, false };
                Vehicle vehicle = Function.Call<Vehicle>(Hash._0xAF35D0D2583051B0, arguments);
                InputArgument[] argumentArray2 = new InputArgument[] { vehicle };
                Function.Call(Hash._0x49733E92263139D1, argumentArray2);
                InputArgument[] argumentArray3 = new InputArgument[] { vehicle, true, false };
                Function.Call(Hash._0xAD738C3085FE7E11, argumentArray3);
                vehicle2 = vehicle;
            }
            return vehicle2;
        }
    }
}

