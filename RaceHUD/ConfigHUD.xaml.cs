using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RaceHUD
{
    /// <summary>
    /// Interaction logic for ConfigHUD.xaml
    /// </summary>

    public partial class ConfigHUD : Window
    {
        public System.Drawing.Point point;
        private HUD Hud;

        public ConfigHUD(HUD hud)
        {
            //breakout the hud form
            Hud = hud;

            InitializeComponent();

            //Fullscreen
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            this.Left = 0;
            this.Top = 0;
            this.WindowState = WindowState.Normal;
        }
        private void TxtHud_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //make the window draggable.
            DragMove();

            //Pass the Left and Top to the Hud as new Left and Top values.
            Hud.lstView.SetValue(Canvas.LeftProperty, this.Left);
            Hud.lstView.SetValue(Canvas.TopProperty, this.Top);
        }
    }
}

