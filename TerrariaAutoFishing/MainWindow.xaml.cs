using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TerrariaTool;

namespace TerrariaAutoFishing
{
 
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public static implicit operator Point(POINT point)
        {
            return new Point(point.X, point.Y);
        }
    }
    public partial class MainWindow : Window
    {
        ThreadTimer threadTimer;
        KeyHook keyHook;
        CScreen cScreen = new CScreen();
        POINT pOINT = new POINT();
        WinApiWindow winApiWindow = new WinApiWindow();
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += (o, e) =>
            {
                winApiWindow.Set(this);

                winApiWindow.MakeTransparent();
            };
            cScreen.SizeRender = new Size(60, 10);

            border.Width = cScreen.SizeRender.Width + 2;
            border.Height = cScreen.SizeRender.Height + 2;

            threadTimer = new ThreadTimer(this);
            threadTimer.TickTime = 500;
            threadTimer.Tick += EventTimer;
            threadTimer.initialize();

            keyHook = new KeyHook(KeyInterop.VirtualKeyFromKey(Key.C));
            keyHook.KeyPressed += (o, e) =>
            {
                cScreen.MakeScreenshot();
            };
            keyHook.SetHook();


            
        }
        bool act_ = true;
        double last_t = 0;
        void EventTimer(int i, int e)
        {
            if (Keyboard.IsKeyDown(Key.K))
            {

                WinApi.GetCursorPos(out pOINT);
                cScreen.ScreenLocation = new Point(pOINT.X, pOINT.Y);


                Console.WriteLine($"{cScreen.ScreenLocation}");

                this.Left = cScreen.ScreenLocation.X - 1;
                this.Top = cScreen.ScreenLocation.Y - 1;

            }
            double d = cScreen.Get();
            if (keyHook.is_down)
            {
                
                 

                if (d >= 0.05 && act_)
                {

                    


                    Mosue.mouse_event((int)(MouseFlags.LEFTDOWN), 0, 0, 0, 0);
                    Thread.Sleep(100);
                    Mosue.mouse_event((int)(MouseFlags.LEFTUP), 0, 0, 0, 0);

                    Thread.Sleep(1000);
                     
                    Mosue.mouse_event((int)(MouseFlags.LEFTDOWN), 0, 0, 0, 0);
                    Thread.Sleep(100);
                    Mosue.mouse_event((int)(MouseFlags.LEFTUP), 0, 0, 0, 0);

                    
                   

                    act_ = false;

                }
                if(act_ == false)
                {
                    Thread.Sleep(1000);
                    act_ = true;
                }
            }
            lll.Content = $"D: {d} KeyState: {keyHook.is_down} Sleep: {act_}";
            Console.WriteLine(d);
            winApiWindow.SetMostTop(true);
        }
    }
   
}
