using System;
using System.Windows.Forms;
using GTA;
using GTA.UI;

public class WantedLevel : Script
{
    public WantedLevel()
    {
        KeyDown += OnKeyDown;
    }

    public void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus)
        {
            AddOneStar();
        } else if (e.KeyCode == Keys.Subtract|| e.KeyCode == Keys.OemMinus)
        {
            RemoveOneStar();
        }
    }

    public void AddOneStar()
    {
        Game.Player.WantedLevel += 1;
    }

    public void RemoveOneStar()
    {
        Game.Player.WantedLevel -= 1;
    }
}