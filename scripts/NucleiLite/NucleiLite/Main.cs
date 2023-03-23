using System;
using System.Windows.Forms;
using GTA;
using GTA.UI;
using LemonUI;
using LemonUI.Menus;

namespace NucleiLite
{
    public class Main : Script
    {
        ObjectPool menuPool = new ObjectPool();
        NativeMenu mainMenu = new NativeMenu("NucleiLite", "Main Menu");
        NativeMenu playerMenu = new NativeMenu("NucleiLite", "Player Menu");
        NativeMenu vehicleSpawnerMenu = new NativeMenu("NucleiLite", "Vehicle Spawner Menu");
        NativeMenu weaponsMenu = new NativeMenu("NucleiLite", "Weapons Menu");

        bool CanSuperJump;

        public Main()
        {
            // Main Menu
            CreateMainMenu();

            // Player Menu
            CreatePlayerMenu();

            // Vehicle Spawner Menu
            CreateVehicleSpawnerMenu();

            // Weapons Menu
            CreateWeaponsMenu();

            KeyDown += OnKeyDown;
            Tick += OnTick;
        }

        private void CreateMainMenu()
        {
            menuPool.Add(mainMenu);
            mainMenu.AddSubMenu(playerMenu);
            mainMenu.AddSubMenu(vehicleSpawnerMenu);
            mainMenu.AddSubMenu(weaponsMenu);
        }

        private void CreateWeaponsMenu()
        {
            menuPool.Add(weaponsMenu);
            var itemGiveAllWeapons = new NativeItem("Give All Weapons", "Gives all Weapons to the Player");
            itemGiveAllWeapons.Activated += (sender, args) =>
            {
                foreach (WeaponHash weaponHash in Enum.GetValues(typeof(WeaponHash)))
                {
                    Game.Player.Character.Weapons.Give(weaponHash, 10, true, true);
                    Game.Player.Character.Weapons[weaponHash].Ammo = Game.Player.Character.Weapons[weaponHash].MaxAmmo;
                    Game.Player.Character.Weapons[weaponHash].AmmoInClip = Game.Player.Character.Weapons[weaponHash].MaxAmmoInClip;
                }
                Notification.Show("All Weapons Given.");
            };
            weaponsMenu.Add(itemGiveAllWeapons);
        }

        private void CreateVehicleSpawnerMenu()
        {
            menuPool.Add(vehicleSpawnerMenu);
            foreach (VehicleHash vehicleHash in Enum.GetValues(typeof(VehicleHash)))
            {
                var itemVehicle = new NativeItem(vehicleHash.ToString(), $"Spawn the {vehicleHash.ToString()}");
                itemVehicle.Activated += (sender, args) => 
                {
                    var vehicleModel = new Model(vehicleHash);
                    vehicleModel.Request();

                    var vehicle = World.CreateVehicle(vehicleModel, Game.Player.Character.Position + Game.Player.Character.ForwardVector * 3.0f, Game.Player.Character.Heading + 90.0f);

                    vehicleModel.MarkAsNoLongerNeeded();

                    Notification.Show($"Vehicle: {vehicleHash.ToString()} Spawned!");
                };
                vehicleSpawnerMenu.Add(itemVehicle);
            }
        }

        private void CreatePlayerMenu()
        {
            menuPool.Add(playerMenu);

            // Fix Player
            var itemFixPlayer = new NativeItem("Fix Player");
            itemFixPlayer.Activated += (sender, args) => 
            {
                Game.Player.Character.Health = Game.Player.Character.MaxHealth;
                Game.Player.Character.Armor = Game.Player.MaxArmor;
                Notification.Show("Health and Armor Restored!");
            };
            playerMenu.Add(itemFixPlayer);

            // Invincible
            var checkBoxInvincible = new NativeCheckboxItem("Invincible", "Set The Player Invincible", Game.Player.Character.IsInvincible);
            checkBoxInvincible.CheckboxChanged += (sender, args) => 
            {
                Game.Player.Character.IsInvincible = !Game.Player.Character.IsInvincible;
                Notification.Show($"Player Invincible: {Game.Player.Character.IsInvincible}");
            };
            playerMenu.Add(checkBoxInvincible);

            // Change Wanted Level
            var listItemWantedLevel = new NativeListItem<int>("Wanted Level", "Sets the Player's Wanted Level", 0, 1, 2, 3, 4, 5);
            listItemWantedLevel.ItemChanged += (sender, args) => 
            {
                Game.Player.WantedLevel = args.Object;
            };
            listItemWantedLevel.SelectedItem = Game.Player.WantedLevel;
            playerMenu.Add(listItemWantedLevel);

            // Super Jump
            var checkBoxSuperJump = new NativeCheckboxItem("Super Jump", "Allows the Player to jump higher than a building!", CanSuperJump);
            checkBoxSuperJump.CheckboxChanged += (sender, args) => 
            { 
                CanSuperJump = !CanSuperJump;
                Notification.Show($"Super Jump Enabled: {CanSuperJump}");
            };
            playerMenu.Add(checkBoxSuperJump);
        }

        private void OnTick(object sender, EventArgs e)
        {
            menuPool.Process();

            if (CanSuperJump)
            {
                Game.Player.SetSuperJumpThisFrame();
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                mainMenu.Visible = !mainMenu.Visible;
                
            }
        }
    }
}