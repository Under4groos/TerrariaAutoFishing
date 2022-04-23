using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

    #region Flags
    [Flags]
    public enum MosueButtonFlags
    {
        MOUSE_LEFT = 0,
        MOUSE_MIDDL = 1,
        MOUSE_RIGHT = 2,
    }

    [Flags]
    public enum MouseFlags : uint
    {
        ABSOLUTE = 0x8000,
        LEFTDOWN = 0x0002,
        LEFTUP = 0x0004,
        MIDDLEDOWN = 0x0020,
        MIDDLEUP = 0x0040,
        MOVE = 0x0001,
        RIGHTDOWN = 0x0008,
        RIGHTUP = 0x0010,
        WHEEL = 0x0800,
        XDOWN = 0x0080,
        XUP = 0x0100,
        HWHEEL = 0x01000,
        XBUTTON1 = 0x0001,
        XBUTTON2 = 0x0002,
    }
    #endregion 



    public class Mosue
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);


        public bool LeftClick()
        {
            mouse_event((int)(MouseFlags.LEFTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseFlags.LEFTUP), 0, 0, 0, 0);
             
            return true;
        }

        public bool RightClick()
        {
            mouse_event((int)(MouseFlags.RIGHTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseFlags.RIGHTUP), 0, 0, 0, 0);
             
            return true;
        }

        public bool MiddleClick()
        {
            mouse_event((int)(MouseFlags.MIDDLEDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseFlags.MIDDLEUP), 0, 0, 0, 0);
             
            return true;
        }

    }

