using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Windows.Media;
using System.Drawing;

namespace RaceHUD
{
    /// <summary>
    /// Interaction logic for HUD.xaml
    /// </summary>
    public partial class HUD : Window
    {
        private GlobalHotKey ghkNext;
        private GlobalHotKey ghkLast;
        public int questIndex;
        private XmlDocument Doc = new XmlDocument();
        private XmlNodeList StepActions;
        public XmlNode node;

        public HUD(MainWindow window)

        {
            ghkNext = new GlobalHotKey(Key.F1, KeyModifier.None, NextStep);
            ghkLast = new GlobalHotKey(Key.F2, KeyModifier.None, LastStep);
            InitializeComponent();
            Doc.Load("RaceInstructions.xml");
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
                IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
                ClickThrough.makeTransparent(hwnd);
        }
        private void NextStep(GlobalHotKey hotKey)
        {
            questIndex += 1;
            ProcessSteps();

        }
        private void LastStep(GlobalHotKey hotKey)
        {
            questIndex -= 1;
            ProcessSteps();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            ghkNext.Unregister();
            ghkLast.Unregister();
        }

        private void ProcessSteps()
        {
            lstView.Items.Clear();
            StepActions = Doc.SelectNodes("//Step" + questIndex + "/Actions/*");
            foreach (XmlNode item in StepActions)
            {
                if (item.Name == "msg")
                {
                    System.Windows.Controls.TextBlock limsg = new System.Windows.Controls.TextBlock();
                    limsg.Text = item.InnerText;
                    lstView.Items.Add(limsg);
                    limsg.Foreground = System.Windows.Media.Brushes.Ivory;
                    limsg.FontSize = 16;
                    limsg.Width = 400;
                    limsg.TextWrapping = TextWrapping.Wrap;
                }

                if (item.Name == "go")
                {
                    System.Windows.Controls.TextBlock ligo = new System.Windows.Controls.TextBlock();
                    ligo.Text = item.InnerText;
                    lstView.Items.Add(ligo);
                    ligo.Foreground = System.Windows.Media.Brushes.White;
                    ligo.FontSize = 16;
                    ligo.Width = 400;
                    ligo.TextWrapping = TextWrapping.Wrap;
                }
                if (item.Name == "do")
                {
                    System.Windows.Controls.TextBlock lido = new System.Windows.Controls.TextBlock();
                    lido.Text = item.InnerText;
                    lstView.Items.Add(lido);
                    lido.Foreground = System.Windows.Media.Brushes.LightBlue;
                    lido.FontSize = 16;
                    lido.Width = 400;
                    lido.TextWrapping = TextWrapping.Wrap;
                }
                if (item.Name == "kill")
                {
                    System.Windows.Controls.TextBlock likill = new System.Windows.Controls.TextBlock();
                    likill.Text = item.InnerText;
                    lstView.Items.Add(likill);
                    likill.Foreground = System.Windows.Media.Brushes.OrangeRed;
                    likill.FontSize = 16;
                    likill.Width = 400;
                    likill.TextWrapping = TextWrapping.Wrap;
                }
                if (item.Name == "take")
                {
                    System.Windows.Controls.TextBlock litake = new System.Windows.Controls.TextBlock();
                    litake.Text = item.InnerText;
                    lstView.Items.Add(litake);
                    litake.Foreground = System.Windows.Media.Brushes.LightGreen;
                    litake.FontSize = 16;
                    litake.Width = 400;
                    litake.TextWrapping = TextWrapping.Wrap;
                }
            }
        }

    }
}
