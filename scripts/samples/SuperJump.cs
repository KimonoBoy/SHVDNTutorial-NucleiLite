using System;
using System.Windows.Forms;
using GTA;
using GTA.UI;

public class SuperJump : Script
{
    bool canSuperJump = false;

    public SuperJump()
    {
        KeyDown += OnKeyDown;
        Tick += OnTick;
    }

    public void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.H && e.Control)
        {
            if (canSuperJump == false)
            {
                canSuperJump = true;
            } else
            {
                canSuperJump = false;
            }
            Notification.Show("Super Jump: " + canSuperJump.ToString());
        }
    }

    public void OnTick(object sender, EventArgs e)
    {
        if (canSuperJump)
        {
            Game.Player.SetSuperJumpThisFrame();
        }
    }
}