using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Elements
{
   
    public class DrawHandler: IDisposable
    {
        public DrawHandler(int width, int height)
        {
            BaseImage = new Bitmap(width, height);
            Graphics = System.Drawing.Graphics.FromImage(BaseImage);
        }

      
        public Image BaseImage { get; private set; }
        
        private System.Drawing.Graphics Graphics { get; set; }

        public void Dispose()
        {
            Graphics.Dispose();
            BaseImage = null;
        }
       
        public void Draw(Image image, Point position)
        {
            Graphics.DrawImage(image, position.X, position.Y, image.Width, image.Height);
        }
    }
}
