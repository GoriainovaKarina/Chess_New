using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Elements
{
    public class ActionLog
    {
        public ActionLog()
        {
            Moves = new List<MoveLog>();
            Removed_Pieces = new List<Piece_Base>();
        }

        public List<MoveLog> Moves { get; set; }
       
        public List<Piece_Base> Removed_Pieces { get; set; }
       
        public Piece_Base Added_Piece { get; set; }
    }
    public class MoveLog
    {
        
        public Piece_Base Piece { get; set; }
        
        public Point Original_Location { get; set; }
       
        public Point New_Location { get; set; }
    }
}
