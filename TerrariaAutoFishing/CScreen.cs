using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TerrariaAutoFishing
{
    public class CScreen
    {
        public System.Windows.Point ScreenLocation { get; set; }
        public System.Windows.Size SizeRender { get; set; }

        public Bitmap captureBitmap = null;
        public Bitmap LastBitmap = null;
        public double d = 0;
        public Bitmap MakeScreenshot()
        {
            Bitmap captureBitmap = new Bitmap(50, 50, PixelFormat.Format32bppArgb);          
            Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
            Graphics captureGraphics = Graphics.FromImage(captureBitmap);
            captureGraphics.CopyFromScreen((int)ScreenLocation.X, (int)ScreenLocation.Y, 0, 0, new Size((int)SizeRender.Width , (int)SizeRender.Height));
            return captureBitmap;
        }
        public double Get()
        {
            captureBitmap = MakeScreenshot();
            d = 0;
            if (LastBitmap == null)
            {
                LastBitmap = captureBitmap;
                return 0;
            }
            else
            {
                for (int x = 0; x < captureBitmap.Width; x++)
                {
                    for (int y = 0; y < captureBitmap.Height; y++)
                    {
                        if(captureBitmap.GetPixel(x,y) != LastBitmap.GetPixel(x, y))
                        {
                            d++;
                        }
                    }
                }

                d /= captureBitmap.Width * captureBitmap.Height;
                LastBitmap = captureBitmap;
                return d;
            }


            return 0;
        }
    }
}
