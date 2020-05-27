using Game.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Elements
{
    
    public abstract class Piece_Base : Sprite
    {
       
        public Piece_Base(Image image, Piece_Color color) : base(image, new Point())
        {
            this.Color = color;
        }
       
        public Piece_Color Color { get; set; }
       
        public Point Location { get; set; }
       
        public bool Selected { get; set; }
        
        public Piece_Move[] Moves { get; set; }
       
        public Point[] EnabledMoves { get; set; }
        
        public Image SelectedImage { get; set; }
        

       
        public override void Draw(DrawHandler drawHandler)
        {
            if (this.Selected)
                drawHandler.Draw(this.SelectedImage, this.Position);

            base.Draw(drawHandler);
        }
        
    }
    
    public class Piece_Move
    {
        public Piece_Move(int x, int y, bool isLinear = true, Move_Type type = Move_Type.General)
        {
            this.Direction = new Point(x, y);
            this.Type = type;
            this.IsLinear = isLinear;
        }
        
        public Point Direction { get; set; }
        
        public Move_Type Type { get; set; }
        
        public bool IsLinear { get; set; }
    }
  
    public enum Move_Type
    {
        General = 1, // движение и атака нацелевую клетку
        Special = 2 // движение которое зависит от положения фигуры на доске
    }
   
    public enum Piece_Color
    {
        Black,
        White
    }
}
