using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beep.IDE
{
    public class CursorMoveEvent:EventArgs
    {
        public int CurrentLine { get; set; }
        public int CurrentColumn { get; set; } = -1;
        public int TotalLines { get; set; } 
    }
    public class ProjectUpdateEvent : EventArgs
    {
        public string ProjectName { get; set; } = string.Empty; 
        public string ProjectPath { get; set; } = string.Empty;
        public string Filename { get; set; }= string.Empty;
    }
}
