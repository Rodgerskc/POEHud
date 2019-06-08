using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace RaceHUD
{
    //Code for click through window found here.
    //https://stackoverflow.com/questions/2842667/how-to-create-a-semi-transparent-window-in-wpf-that-allows-mouse-events-to-pass?rq=1

    class ClickThrough
    {
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int GWL_EXSTYLE = (-20);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd,
        int index);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd,
        int index, int newStyle);

        public static void makeTransparent(IntPtr hwnd)
        {
            int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            ClickThrough.SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle |
            WS_EX_TRANSPARENT);

        }
    }
}
