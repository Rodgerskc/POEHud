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
            //register F1 and F1 keybinds
            ghkNext = new GlobalHotKey(Key.F1, KeyModifier.None, NextStep);
            ghkLast = new GlobalHotKey(Key.F2, KeyModifier.None, LastStep);

            InitializeComponent();

            //Load XML Document
            Doc.Load("RaceInstructions.xml");
        }

        //Code for click through window found here.
        //https://stackoverflow.com/questions/2842667/how-to-create-a-semi-transparent-window-in-wpf-that-allows-mouse-events-to-pass?rq=1

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
                IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
                ClickThrough.makeTransparent(hwnd);
        }

        //F1 Keybind function
        private void NextStep(GlobalHotKey hotKey)
        {
            //confine questIndex to the step count.
            if (questIndex < 73 && questIndex >= 0)
            {
            questIndex += 1;
            setListItems();
            }
        }

        //F2 Keybind function
        private void LastStep(GlobalHotKey hotKey)
        {
            if (questIndex <= 73 && questIndex >= 1)
            {
                questIndex -= 1;
                setListItems();
            }
        }

        //unregister low level keybinds.
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            ghkNext.Unregister();
            ghkLast.Unregister();
        }

        //Populate the ListView from the XML Document.
        private void setListItems()
        {
            //clear the listview
            lstView.Items.Clear();

            //populate node collection with the step based on the questIndex
            StepActions = Doc.SelectNodes("//Step" + questIndex + "/Actions/*");

            //transform nodes to stylized listviewitems
            foreach (XmlNode item in StepActions)
            {
                switch (item.Name)
                {
                    case "msg":
                        System.Windows.Controls.TextBlock limsg = new System.Windows.Controls.TextBlock();
                        limsg.Text = item.InnerText;
                        lstView.Items.Add(limsg);
                        limsg.Foreground = System.Windows.Media.Brushes.Ivory;
                        limsg.FontSize = 16;
                        limsg.Width = 400;
                        limsg.TextWrapping = TextWrapping.Wrap;
                        break;

                    case "go":
                        System.Windows.Controls.TextBlock ligo = new System.Windows.Controls.TextBlock();
                        ligo.Text = item.InnerText;
                        lstView.Items.Add(ligo);
                        ligo.Foreground = System.Windows.Media.Brushes.White;
                        ligo.FontSize = 16;
                        ligo.Width = 400;
                        ligo.TextWrapping = TextWrapping.Wrap;
                        break;

                    case "do":
                        System.Windows.Controls.TextBlock lido = new System.Windows.Controls.TextBlock();
                        lido.Text = item.InnerText;
                        lstView.Items.Add(lido);
                        lido.Foreground = System.Windows.Media.Brushes.LightBlue;
                        lido.FontSize = 16;
                        lido.Width = 400;
                        lido.TextWrapping = TextWrapping.Wrap;
                        break;

                    case "kill":
                        System.Windows.Controls.TextBlock likill = new System.Windows.Controls.TextBlock();
                        likill.Text = item.InnerText;
                        lstView.Items.Add(likill);
                        likill.Foreground = System.Windows.Media.Brushes.OrangeRed;
                        likill.FontSize = 16;
                        likill.Width = 400;
                        likill.TextWrapping = TextWrapping.Wrap;
                        break;

                    case "take":
                        System.Windows.Controls.TextBlock litake = new System.Windows.Controls.TextBlock();
                        litake.Text = item.InnerText;
                        lstView.Items.Add(litake);
                        litake.Foreground = System.Windows.Media.Brushes.LightGreen;
                        litake.FontSize = 16;
                        litake.Width = 400;
                        litake.TextWrapping = TextWrapping.Wrap;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
