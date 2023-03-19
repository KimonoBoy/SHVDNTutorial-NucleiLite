using System;
using System.Windows.Forms;
using GTA;
using GTA.UI;

public class FixPlayer : Script
{
    public FixPlayer()
    {
        KeyDown += OnKeyDown;
    }

    public void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F && e.Control)
        {
            Game.Player.Character.Health = Game.Player.Character.MaxHealth;
            Game.Player.Character.Armor = Game.Player.MaxArmor;
            Notification.Show("Health and Armor Restored!");
        }
    }
}