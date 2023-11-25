using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beep.IDE
{
    public class TabsData
    {
        public int Tabidx { get; set; } 
        public Scintilla EditorControl { get; set; } 
        public string filename { get; set; }
        public string filepath { get; set; }
        public string ext { get; set; }
        public string lexer { get; set; }
        public bool isSaved { get; set; }=false;
        public bool isEdited { get; set; }=true;
        public bool isClosed { get; set; } = false;   
        public bool isRunning { get; set; } = false;
        public bool isDebugging { get; set; } = false;
        public bool isBreakpoint { get; set; } = false;
        public bool isDebuggingPaused { get; set; } = false;
        public int line { get; set; }
        public int column { get; set; }
        public int linecount { get; set; }
        public int charcount { get; set; }
        public int selstart { get; set; }
        public int selend { get; set; }
        public int selcount { get; set; }
        public int  CompilerIndex { get; set; }
        public string ProjectName { get; set; }
        public string ProjectPath { get; set; }
        public string ProjectRoot { get; set; }



    }
    public class TabsDataEventarg : EventArgs
    {
        public int Tabidx { get; set; }
        public string filename { get; set; }
        public bool Cancel { get; set; }=false;
    }
}
