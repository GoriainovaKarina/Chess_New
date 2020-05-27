using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Elements
{
    public class GameTime
    {
        public GameTime()
        {
            StartDate =
            FrameDate = DateTime.Now;
        }

       
        public DateTime StartDate { get; set; }
        
        public DateTime FrameDate { get; set; }
        
        public TimeSpan TotalTime { get { return FrameDate - StartDate; } }
       
        public int FrameMilliseconds { get; set; }
    }
}
