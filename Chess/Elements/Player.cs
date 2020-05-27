using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Elements
{
    
    public class Player
    {
       
        public Player(Piece_Color color, Player_Type type, int number)
        {
            this.Color = color;
            this.Type = type;
            this.Number = number;
        }

       
        public Piece_Color Color { get; set; }
       
        public Player_Type Type { get; set; }
       
        public int Number { get; set; }
    }

    public enum Player_Type
    {
        Cpu, // управление(компьютер)
        Human // управление(человек)
    }
}
