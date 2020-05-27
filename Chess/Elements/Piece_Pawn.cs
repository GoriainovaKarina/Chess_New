using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Elements
{
    public class Piece_Pawn : Piece_Base
    {
       
        public Piece_Pawn(Image image, Piece_Color color) : base(image, color)
        {
            Moves = new Piece_Move[]
            {
                new Piece_Move(0,-1, false, Move_Type.Special), // может двигаться , но не  атаковать
                new Piece_Move(0,-2, false, Move_Type.Special), //может двигаться на 2 клетки вперёд, но не атаковать, и это можно сделать, если это первый ход пешки

                new Piece_Move(-1,-1, false, Move_Type.Special), // по диагонали иожет только отаковать (влево),но не двигаться
                new Piece_Move(1,-1, false, Move_Type.Special), // по диагонали иожет только отаковать (вправо),но не двигаться
            };
        }
        
    }
}
