using Game.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Elements
{
    
    public class Board : Sprite
    {
       
        public Board(Image boardImage, Image moveTileImage) : base(boardImage, new Point())
        {
            this.Move_Image = moveTileImage;

            Cells = new BoardCell[8, 8];

            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                {
                    int _x = (x * 100) + 5 * (x + 1);
                    int _y = (y * 100) + 5 * (y + 1);

                    Cells[x, y] = new BoardCell()
                    {
                        ScreenPosition = new Point(_x, _y),
                    };
                    // указывает положение каждой ячейки доски
                }
        }
        

       
        private Image Move_Image { get; set; }
        
        public BoardCell[,] Cells { get; set; }
       

       
        public void Clear_EnabledMoves()
        {
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    Cells[x, y].CanMove = false;
        }
        
        public override void Draw(DrawHandler drawHandler)
        {
            drawHandler.Draw(this.Image, this.Position);

            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                {
                    if (Cells[x, y].CanMove)
                        drawHandler.Draw(this.Move_Image, Cells[x, y].ScreenPosition);
                }
        }
       
    }

    
    public class BoardCell
    {
        
        public Point ScreenPosition { get; set; }
        
        public bool CanMove { get; set; }
    }
}
