using System.Windows.Forms;
using GTA;
using GTA.UI;

namespace NucleiLite
{
    public class Main : Script
    {
        public Main()
        {
            KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.T) Notification.Show("Hello Compiled Script!");
        }
    }
}