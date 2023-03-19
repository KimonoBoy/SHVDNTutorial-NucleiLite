using System;
using System.Windows.Forms;
using GTA;
using GTA.UI;
using GTA.Native;

public class GivePlayerAllWeapons : Script
{
    public GivePlayerAllWeapons()
    {
        KeyDown += OnKeyDown;
    }

    public void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.NumPad0)
        {
            GiveAllWeapons();
        }
    }

    public void GiveAllWeapons()
    {
        foreach (WeaponHash weaponHash in Enum.GetValues(typeof(WeaponHash)))
        {
            Game.Player.Character.Weapons.Give(weaponHash, 100, true, true);
            Game.Player.Character.Weapons.Current.Ammo = Game.Player.Character.Weapons.Current.MaxAmmo;
        }
    }
}