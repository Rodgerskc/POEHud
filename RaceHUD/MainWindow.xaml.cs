using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Xml;
using System.IO;
using System.Drawing;

namespace RaceHUD
{
    /// <summary>
    /// RaceHUD Developed by Kevin Rodgers.
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyIcon rh;
        private ContextMenu contextMenu;
        private MenuItem lockHud;
        private MenuItem moveHud;
        private MenuItem stopHud;
        private MenuItem dashHud;
        private MenuItem exitHud;
        public HUD hud;
        public ConfigHUD confighud;

        public MainWindow()
        {

            InitializeComponent();

            //HUD inits
            hud = new HUD(this);
            confighud = new ConfigHUD(hud);

            //Systray init
            rh = new NotifyIcon();
            contextMenu = new ContextMenu();
            rh.Icon = new Icon("RaceHudIcon.ico");
            rh.Visible = true;
            rh.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
            BuildMenu(rh);
            BaloonTip(rh);
            rh.ContextMenu = contextMenu;
        }

        //main window left click to drag
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        //system tray baloon tip
        protected void BaloonTip(NotifyIcon rh)
        {
            rh.Text = "RaceHUD";
            rh.BalloonTipText = "RaceHUD";
            rh.BalloonTipTitle = "RaceHUD";
            rh.ShowBalloonTip(3000);
        }

        //Build the system tray menu
        protected void BuildMenu(NotifyIcon rh)
        {
            contextMenu = new ContextMenu();
            lockHud = new MenuItem();
            moveHud = new MenuItem();
            stopHud = new MenuItem();
            dashHud = new MenuItem();
            exitHud = new MenuItem();
            contextMenu.MenuItems.AddRange(new MenuItem[] { lockHud, moveHud, stopHud, dashHud, exitHud });
            lockHud.Index = 0;
            lockHud.Text = "Lock HUD";
            lockHud.Click += new EventHandler(lockHUD_Click);
            moveHud.Index = 1;
            moveHud.Text = "Move HUD Elements";
            moveHud.Click += new EventHandler(moveHUD_Click);
            stopHud.Index = 2;
            stopHud.Text = "Stop HUD";

            //Stop HUD does nothing if the hud isn't active
            if (hud.WindowState == WindowState.Maximized)
            {
                stopHud.Click += new EventHandler(stopHUD_Click);
            }

            dashHud.Index = 3;
            dashHud.Text = "-";
            exitHud.Index = 4;
            exitHud.Text = "Exit";
            exitHud.Click += new EventHandler(exit_Click);
        }

        //System Tray Events
        private void stopHUD_Click(object Sender, EventArgs e)
        {
            hud.Hide();
            confighud.Hide();
            this.Show();
            this.WindowState = WindowState.Normal;
        }
        private void moveHUD_Click(object Sender, EventArgs e)
        {
            hud.Hide();
            confighud.Show();
        }
        private void lockHUD_Click(object Sender, EventArgs e)
        {
            hud.Show();
            confighud.Hide();
            this.Hide();
        }
        private void exit_Click(object Sender, EventArgs e)
        {
            this.Close();
            hud.Close();
            confighud.Close();
        }

        //Placeholder window button event
        private void Button_Click(object sender, EventArgs e)
        {
            hud.Show();
            this.WindowState = WindowState.Minimized;
            this.Hide();
        }
        
    }

}

