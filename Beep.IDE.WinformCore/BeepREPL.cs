using Beep.Compilers.Module;
using Beep.Vis.Module;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TheTechIdea;
using TheTechIdea.Beep;
using TheTechIdea.Beep.DataBase;
using TheTechIdea.Logger;
using TheTechIdea.Util;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Beep.IDE
{

    public class BeepREPL : TableLayoutPanel,IDM_Addin
    {
        private List<TextBox> codeCells;
        private ICompiler Compiler;

        public string ParentName { get  ; set  ; }
        public string ObjectName { get  ; set  ; }
        public string ObjectType { get  ; set  ; }
        public string AddinName { get  ; set  ; }
        public string Description { get  ; set  ; }
        public bool DefaultCreate { get  ; set  ; }
        public string DllPath { get  ; set  ; }
        public string DllName { get  ; set  ; }
        public string NameSpace { get  ; set  ; }
        public IErrorsInfo ErrorObject { get  ; set  ; }
        public IDMLogger Logger { get  ; set  ; }
        public IDMEEditor DMEEditor { get  ; set  ; }
        public EntityStructure EntityStructure { get  ; set  ; }
        public string EntityName { get  ; set  ; }
        public IPassedArgs Passedarg { get  ; set  ; }

        public BeepREPL()
        {
            codeCells = new List<TextBox>();
            //Compiler= compiler;
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            AddNewCell();
        }
        
        public void AddNewCell()
        {
            TextBox newCell = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill
            };
            codeCells.Add(newCell);

            RowCount += 1;
            RowStyles.Add(new RowStyle(SizeType.AutoSize));

            Controls.Add(newCell, 0, RowCount - 1);
            Controls.Add(CreateButtonsPanel(), 1, RowCount - 1);
        }

        private Panel CreateButtonsPanel()
        {
            Panel panel = new Panel();
            Button btnRun = new Button { Text = "Run", Dock = DockStyle.Top };
            Button btnSave = new Button { Text = "Save", Dock = DockStyle.Top };

            btnRun.Click += (sender, e) => ExecuteCurrentCell();
            // Add your logic for btnSave.Click event here.

            panel.Controls.Add(btnRun);
            panel.Controls.Add(btnSave);
            return panel;
        }

        public async void ExecuteCurrentCell()
        {
            TextBox currentCell = codeCells[RowCount - 1];
            string code = currentCell.Text;
            List<CompileResults> result = Compiler.Run(code,null,null,progress,token);
          //  AddOutput(result);
            AddNewCell();
        }

        private void AddOutput(string output)
        {
            TextBox outputBox = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                Text = output,
                Dock = DockStyle.Fill
            };

            RowCount += 1;
            RowStyles.Add(new RowStyle(SizeType.AutoSize));
            Controls.Add(outputBox, 0, RowCount - 1);
        }
        IProgress<PassedArgs> progress;
        CancellationToken token;
        public IVisManager Visutil { get; set; }
        public void Run(IPassedArgs pPassedarg)
        {
          
        }
        public void SetupCompiler(ICompiler compiler)
        {
            Compiler = compiler;
        }
        public void SetConfig(IDMEEditor pbl, IDMLogger plogger, IUtil putil, string[] args, IPassedArgs e, IErrorsInfo per)
        {
            Passedarg = e;
            Logger = plogger;
            ErrorObject = per;
            DMEEditor = pbl;

            Visutil = (IVisManager)e.Objects.Where(c => c.Name == "VISUTIL").FirstOrDefault().obj;

          
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
          
        }
    }

}
