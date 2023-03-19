using System;
using System.Windows.Forms;
using GTA;
using GTA.UI;
using GTA.Native;
using GTA.Math;

public class VehicleSpawner : Script
{
    public VehicleSpawner()
    {
        KeyDown += OnKeyDown;
    }

    public void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.D1 && e.Control)
        {
            SpawnVehicle(VehicleHash.Adder);
        } else if (e.KeyCode == Keys.D2 && e.Control)
        {
            SpawnVehicle(VehicleHash.Annihilator);
        } else if (e.KeyCode == Keys.D3 && e.Control)
        {
            SpawnVehicle(VehicleHash.Formula);
        }
    }

    public void SpawnVehicle(VehicleHash vehicleHash)
    {
        Model vehicleModel = new Model(vehicleHash);
        vehicleModel.Request();

        Vector3 spawnPosition = Game.Player.Character.Position + Game.Player.Character.ForwardVector * 3.0f;
        float vehicleHeading = Game.Player.Character.Heading + 90.0f;

        GTA.Vehicle vehicle = World.CreateVehicle(vehicleModel, spawnPosition, vehicleHeading);

        vehicleModel.MarkAsNoLongerNeeded();
    }
}