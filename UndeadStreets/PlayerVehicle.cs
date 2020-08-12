namespace UndeadStreets
{
    using GTA;
    using System;

    public class PlayerVehicle
    {
        public static VehicleCollection PlayerVehicleCollection = new VehicleCollection();

        public static void AddVehicleData(Vehicle vehicle)
        {
            VehicleData item = new VehicleData(vehicle.Handle, vehicle.Model.Hash, vehicle.Rotation, vehicle.Position, vehicle.PrimaryColor, vehicle.SecondaryColor, vehicle.Health, vehicle.EngineHealth, vehicle.Heading);
            PlayerVehicleCollection.Add(item);
        }

        public static void LoadVehicleFromVehicleData(VehicleData vehicleData)
        {
            Model model = new Model(vehicleData.Hash);
            Vehicle vehicle = Extensions.SpawnVehicle(model, vehicleData.Position, vehicleData.Heading);
            if (vehicle != null)
            {
                vehicle.Rotation = vehicleData.Rotation;
                vehicle.PrimaryColor = vehicleData.PrimaryColor;
                vehicle.SecondaryColor = vehicleData.SecondaryColor;
                vehicle.Health = vehicleData.Health;
                vehicle.EngineHealth = vehicleData.EngineHealth;
            }
            Character.playerVehicle = vehicle;
        }
    }
}

