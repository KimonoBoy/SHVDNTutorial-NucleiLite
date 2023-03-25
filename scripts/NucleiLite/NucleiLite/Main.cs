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

        public Main()
        {
            CreateMainMenu();
            CreatePlayerMenu();
            CreateVehicleSpawnerMenu();
            CreateWeaponsMenu();

            AddMenusToPool();

            KeyDown += OnKeyDown;
            Tick += OnTick;
        }

        private void CreateMainMenu()
        {
            mainMenu.AddSubMenu(playerMenu);
            mainMenu.AddSubMenu(vehicleSpawnerMenu);
            mainMenu.AddSubMenu(weaponsMenu);
        }

        private void CreatePlayerMenu()
        {
            // Fix Player
            NativeItem itemFixPlayer = new NativeItem("Fix Player", "Restores Player's Health and Armor");
            itemFixPlayer.Activated += (sender, args) =>
            {
                Game.Player.Character.Health = Game.Player.Character.MaxHealth;
                Game.Player.Character.Armor = Game.Player.MaxArmor;
                Notification.Show("Health and Armor Restored!");
            };
            playerMenu.Add(itemFixPlayer);

            // Invincible
            NativeCheckboxItem checkBoxInvincible = new NativeCheckboxItem("Invincible", "Your character can no longer die.");
            checkBoxInvincible.CheckboxChanged += (sender, args) =>
            {
                Game.Player.Character.IsInvincible = checkBoxInvincible.Checked;
                Notification.Show($"Invincible: {Game.Player.Character.IsInvincible}");
            };
            playerMenu.Add(checkBoxInvincible);

            // Wanted Level
            NativeListItem<int> listItemWantedLevel = new NativeListItem<int>("Wanted Level", "Adjust Player's Wanted Level.", 0, 1, 2, 3, 4, 5);
            listItemWantedLevel.ItemChanged += (sender, args) =>
            {
                Game.Player.WantedLevel = args.Object;
            };
            playerMenu.Add(listItemWantedLevel);
        }

        private void CreateWeaponsMenu()
        {
        }

        private void CreateVehicleSpawnerMenu()
        {
        }

        private void AddMenusToPool()
        {
            menuPool.Add(mainMenu);
            menuPool.Add(playerMenu);
            menuPool.Add(vehicleSpawnerMenu);
            menuPool.Add(weaponsMenu);
        }

        private void OnTick(object sender, EventArgs e)
        {
            menuPool.Process();
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