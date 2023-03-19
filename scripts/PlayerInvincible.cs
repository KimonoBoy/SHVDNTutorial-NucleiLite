using System;
using System.Windows.Forms;
using GTA;
using GTA.UI;

public class PlayerInvincible : Script
{
    public PlayerInvincible()
    {
        KeyDown += OnKeyDown;
    }

    public void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.I && e.Control)
        {
            if (Game.Player.Character.IsInvincible)
            {
                Game.Player.Character.IsInvincible = false;
                Notification.Show("You're no longer invincible.");
            } else
            {
                Game.Player.Character.IsInvincible = true;
                Notification.Show("You're now invincible.");
            }
        }
    }
}