using Beep.IDE.Extensions;
using Beep.Vis.Module;
using System.Data;
using TheTechIdea;
using TheTechIdea.Beep;
using TheTechIdea.Beep.DataBase;
using TheTechIdea.Beep.Vis;
using TheTechIdea.Logger;
using TheTechIdea.Util;


namespace Beep.IDE
{
    [AddinAttribute(Caption = "Beep IDE", Name = "BeepIDE", misc = "App", ObjectType = "Beep.Admin", addinType = AddinType.Control, displayType = DisplayType.InControl)]
    public partial class uc_BeepIDE : UserControl, IDM_Addin
    {
        public uc_BeepIDE()
        {
            InitializeComponent();
            this.beepTabControl1.NextButtonClick += BeepTabControl1_NextButtonClick;
            this.beepTabControl1.PrevButtonClick += BeepTabControl1_PrevButtonClick;
            this.beepTabControl1.CloseButtonClick += BeepTabControl1_CloseButtonClick;

            progressBar1.Style = ProgressBarStyle.Blocks;
        }

        private void BeepTabControl1_MouseClick(object? sender, MouseEventArgs e)
        {
            // Convert the click point to beepTabControl's coordinate space
            Point clickPoint = beepTabControl1.PointToClient(new Point(e.X, e.Y));

            // Assuming beepTabControl exposes NextButtonRectangle and PrevButtonRectangle
            if (beepTabControl1.nextButton.Contains(clickPoint))
            {
                beepTabControl1.OnNextButtonClick();
            }
            else if (beepTabControl1.prevButton.Contains(clickPoint))
            {
                beepTabControl1.OnPrevButtonClick();
            }
        }

        private void BeepTabControl1_CloseButtonClick(object sender, TabsDataEventarg e)
        {
           
        }

        private void BeepTabControl1_PrevButtonClick(object sender, TabsDataEventarg e)
        {
           
        }

        private void BeepTabControl1_NextButtonClick(object sender, TabsDataEventarg e)
        {
           
        }

        public string ParentName { get; set; }
        public string AddinName { get; set; } = "Beep IDE";
        public string Description { get; set; } = "Beep IDE";
        public string ObjectName { get; set; }
        public string ObjectType { get; set; } = "UserControl";
        public bool DefaultCreate { get; set; } = true;
        public string DllPath { get; set; }
        public string DllName { get; set; }
        public string NameSpace { get; set; }
        public DataSet Dset { get; set; }
        public IErrorsInfo ErrorObject { get; set; }
        public IDMLogger Logger { get; set; }
        public IDMEEditor DMEEditor { get; set; }
        public EntityStructure EntityStructure { get; set; }
        public string EntityName { get; set; }
        public IPassedArgs Passedarg { get; set; }


        //   public iDEManager iDEManager { get; set; }
        public IDEManager iDEManager { get; set; }
   
        bool IDM_Addin.DefaultCreate { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        IProgress<PassedArgs> progress;
        CancellationToken token;
        IBranch RootAppBranch;
        IBranch branch;
        public IVisManager Visutil { get; set; }
        FunctionandExtensionsHelpers extensionsHelpers;
        public void Run(IPassedArgs pPassedarg)
        {
            if (pPassedarg != null)
            {
                if (pPassedarg.ObjectType != null)
                {
                    if (pPassedarg.ObjectType.Equals("CODE", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (!string.IsNullOrEmpty(pPassedarg.ObjectName))
                        {
                            iDEManager.OpenFile(pPassedarg.ObjectName);
                        }
                    }

                }
           
            }
        }

        public void SetConfig(IDMEEditor pbl, IDMLogger plogger, IUtil putil, string[] args, IPassedArgs e, IErrorsInfo per)
        {
            Passedarg = e;
            Logger = plogger;
            ErrorObject = per;
            DMEEditor = pbl;
            //Python = new PythonHandler(pbl,TextArea,OutputtextBox, griddatasource);

            Visutil = (IVisManager)e.Objects.Where(c => c.Name == "VISUTIL").FirstOrDefault().obj;

            if (e.Objects.Where(c => c.Name == "Branch").Any())
            {
                branch = (IBranch)e.Objects.Where(c => c.Name == "Branch").FirstOrDefault().obj;
            }
            if (e.Objects.Where(c => c.Name == "RootAppBranch").Any())
            {
                RootAppBranch = (IBranch)e.Objects.Where(c => c.Name == "RootAppBranch").FirstOrDefault().obj;
            }
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            progress = new Progress<PassedArgs>(percent =>
            {
                progressBar1.Style = ProgressBarStyle.Marquee;
                progressBar1.MarqueeAnimationSpeed = 30; // Adjust as needed
                if (!String.IsNullOrEmpty(percent.Messege) && percent.EventType != "CODEFINISH")
                {

                    //Add the text to the collected output.

                    this.OutputtextBox.BeginInvoke(new Action(() =>
                    {
                        this.OutputtextBox.AppendText(Environment.NewLine +
                        $">{percent.Messege}");
                        OutputtextBox.SelectionStart = OutputtextBox.Text.Length;
                        OutputtextBox.ScrollToCaret();
                    }));

                }

                if (!string.IsNullOrEmpty(percent.ParameterString1) && percent.EventType != "CODEFINISH")
                {
                    DMEEditor.AddLogMessage(percent.Messege);
                }
                if (percent.EventType == "CODEFINISH")
                {
                    progressBar1.Style = ProgressBarStyle.Blocks;
                }

            });
            extensionsHelpers = new FunctionandExtensionsHelpers(DMEEditor, Visutil, (ITree)Visutil.Tree);
            iDEManager = new IDEManager(DMEEditor ,e  , this, beepTabControl1,menuStrip1, token, progress);
            this.RuntoolStripButton.Click += RuntoolStripButton_Click;
            this.newFileToolStripMenuItem.Click+= NewFiletoolStripButton_Click;
            this.NewFiletoolStripButton.Click += NewFiletoolStripButton_Click;

            this.saveAsToolStripMenuItem.Click += SaveAsToolStripMenuItem_Click;
            this.SaveAstoolStripButton.Click += SaveAsToolStripMenuItem_Click;

            this.saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            this.SaveFiletoolStripButton.Click += SaveToolStripMenuItem_Click;

            this.SaveAsProjecttoolStripButton.Click += SaveAsProjecttoolStripButton_Click;
            this.SaveToProjecttoolStripMenuItem.Click += SaveAsProjecttoolStripButton_Click;
            iDEManager.CursorPositionChanged += IDEManager_CursorPositionChanged;
            iDEManager.FileAddedToProject += IDEManager_FileAddedToProject;
            this.ClearOutputtoolStripButton.Click += ClearOutputtoolStripButton_Click;
            this.Disposed += Uc_BeepIDE_Disposed;
          

        }

        private void Uc_BeepIDE_Disposed(object sender, EventArgs e)
        {
            iDEManager.Dispose();
          

        }

        private void ClearOutputtoolStripButton_Click(object sender, EventArgs e)
        {
            OutputtextBox.Clear();
        }

        private void IDEManager_FileAddedToProject(object sender, ProjectUpdateEvent e)
        {
            ITree tree= (ITree)Visutil.Tree;
            IBranch proj=  tree.Branches.FirstOrDefault(p=>p.BranchText.Equals(e.ProjectName, StringComparison.OrdinalIgnoreCase) && p.BranchClass=="PROJECT");
            proj.CreateChildNodes();
        }

        private void SaveAsProjecttoolStripButton_Click(object sender, EventArgs e)
        {

            iDEManager.SaveAsToProject(iDEManager.CurrentFileName);
            if(DMEEditor.ErrorObject.Flag== Errors.Ok)
            {
                //---- Referesh Projects
                ITree tree = (ITree)Visutil.Tree;
                IBranch projects = tree.Branches.FirstOrDefault(p => p.BranchClass == "PROJECT" && p.BranchType == EnumPointType.Root);
                projects.CreateChildNodes();
            }

        }

        private void IDEManager_CursorPositionChanged(object sender, CursorMoveEvent e)
        {
            this.CurrentLineValue.Text = e.CurrentLine.ToString();
            this.TotalLinesValue.Text = e.TotalLines.ToString();
            this.ColumnPositionLabel.Text=e.CurrentColumn.ToString();
        }

        private void SaveAstoolStripButton_Click(object sender, EventArgs e)
        {
            iDEManager.SaveAs(iDEManager.CurrentFileName);

        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (beepTabControl1.SelectedIndex == -1)
            {
                return;
            }


            iDEManager.Save(iDEManager.CurrentFileName);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (beepTabControl1.SelectedIndex == -1)
            {
                return;
            }

            iDEManager.SaveAs(iDEManager.CurrentFileName);
        }

        private void NewFiletoolStripButton_Click(object sender, EventArgs e)
        {
           iDEManager.NewFile();
        }

        private void RuntoolStripButton_Click(object sender, EventArgs e)
        {
            if(beepTabControl1.SelectedIndex == -1)
            {
                return;
            }
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30; // Adjust as needed
            if(beepTabControl1.SelectedIndex<0 && beepTabControl1.TabCount>0)
            {
                beepTabControl1.SelectedIndex = 0;
            }
            var retval = iDEManager.GetCodeFromTab(beepTabControl1.SelectedIndex);
            if (string.IsNullOrEmpty(retval.Item2))
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                return;
            }
            if (retval.Item1 < 0)
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                return;
            }
            iDEManager.RunCode(retval.Item1, retval.Item2);
            
            progressBar1.Style = ProgressBarStyle.Blocks;
           
            //          ret.Wait();


        }
       
    }
}
