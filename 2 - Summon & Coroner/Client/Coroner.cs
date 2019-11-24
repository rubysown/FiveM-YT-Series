using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using System;
using System.Threading.Tasks;

namespace Client
{
    class Coroner
    {
        private static Vehicle cVanEntity;
        private static Ped cVanPed1;
        private static Ped cVanPed2;
        private static int cVanBlip;

        private static bool eventSpawned;

        private static readonly Random random = new Random();

        public async static void Summon()
        {
            Ped player = Game.Player.Character;
            Screen.ShowNotification("The coroner is on his way to you now!");

            // Van
            Vector3 spawnLocation = new Vector3();
            float spawnHeading = 0F;
            int unusedVar = 0;
            API.GetNthClosestVehicleNodeWithHeading(player.Position.X, player.Position.Y, player.Position.Z, 80, ref spawnLocation, ref spawnHeading, ref unusedVar, 9, 3.0F, 2.5F);
            await LoadModel((uint)VehicleHash.Burrito3);
            cVanEntity = await World.CreateVehicle(VehicleHash.Burrito3, spawnLocation, spawnHeading);
            cVanEntity.Mods.PrimaryColor = VehicleColor.MetallicBlack;
            cVanEntity.Mods.LicensePlate = $"SA C {random.Next(10)}";
            cVanEntity.Mods.LicensePlateStyle = LicensePlateStyle.BlueOnWhite3;

            // Van Blip
            cVanBlip = API.AddBlipForEntity(cVanEntity.Handle);
            API.SetBlipColour(cVanBlip, 40);
            API.BeginTextCommandSetBlipName("STRING");
            API.AddTextComponentString("Coroner");
            API.EndTextCommandSetBlipName(cVanBlip);

            // Driver
            await LoadModel((uint)PedHash.Doctor01SMM);
            cVanPed1 = await World.CreatePed(PedHash.Doctor01SMM, spawnLocation);
            cVanPed1.SetIntoVehicle(cVanEntity, VehicleSeat.Driver);
            cVanPed1.CanBeTargetted = false;

            // Passenger
            await LoadModel((uint)PedHash.Scientist01SMM);
            cVanPed2 = await World.CreatePed(PedHash.Scientist01SMM, spawnLocation);
            cVanPed2.SetIntoVehicle(cVanEntity, VehicleSeat.Passenger);
            cVanPed2.CanBeTargetted = false;

            // Configuration
            Vector3 targetLocation = new Vector3();
            float targetHeading = 0F;
            API.GetClosestVehicleNodeWithHeading(player.Position.X, player.Position.Y, player.Position.Z, ref targetLocation, ref targetHeading, 1, 3.0F, 0);
            cVanPed1.Task.DriveTo(cVanEntity, targetLocation, 10F, 20F, 262972);
            eventSpawned = true;
        }

        public static async Task<bool> LoadModel(uint model)
        {
            if (!API.IsModelInCdimage(model))
            {
                Debug.WriteLine($"Invalid model {model} was supplied to LoadModel.");
                return false;
            }
            API.RequestModel(model);
            while (!API.HasModelLoaded(model))
            {
                Debug.WriteLine($"Waiting for model {model} to load");
                await BaseScript.Delay(100);
            }
            return true;
        }
    }
}
