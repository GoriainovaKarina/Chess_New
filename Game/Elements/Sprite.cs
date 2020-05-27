using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Elements
{
    
    public class Sprite
    {
       
        public Sprite(Image image, Point position)
        {
            this.Image = image;
            this.Position = position;

            Visible = true;
        }
        
        public Image Image { get; set; }
       
        public Point Position { get; set; }
        
        public bool Visible { get; set; }
        
        public virtual void Draw(DrawHandler drawHandler)
        {
            if (this.Visible)
                drawHandler.Draw(this.Image, this.Position);
        }
        
    }
}
