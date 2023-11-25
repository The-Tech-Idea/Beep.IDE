﻿using Beep.IDE.Extensions;
using BeepEnterprize.Vis.Module;
using System.Data;
using TheTechIdea;
using TheTechIdea.Beep;
using TheTechIdea.Beep.DataBase;
using TheTechIdea.Beep.Vis;
using TheTechIdea.Logger;
using TheTechIdea.Util;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Beep.IDE
{
    [AddinAttribute(Caption = "Beep IDE", Name = "BeepIDE", misc = "App", ObjectType = "Beep", addinType = AddinType.Control, displayType = DisplayType.InControl)]
    public partial class uc_BeepIDE : UserControl, IDM_Addin
    {
        public uc_BeepIDE()
        {
            InitializeComponent();
        }


        public string ParentName { get; set; }
        public string AddinName { get; set; } = "Beep IDE";
        public string Description { get; set; } = "Beep IDE";
        public string ObjectName { get; set; }
        public string ObjectType { get; set; } = "UserControl";
        public Boolean DefaultCreate { get; set; } = true;
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
        public IVisManager Visutil { get; set; }

        IProgress<PassedArgs> progress;
        CancellationToken token;
        IBranch RootAppBranch;
        IBranch branch;

        FunctionandExtensionsHelpers extensionsHelpers;
        public void Run(IPassedArgs pPassedarg)
        {
            throw new NotImplementedException();
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

                //CPythonManager_SendMessege(this, percent.ParameterString1);
                DMEEditor.AddLogMessage(percent.ParameterString1);

            });
            extensionsHelpers = new FunctionandExtensionsHelpers(DMEEditor, Visutil, (ITree)Visutil.Tree);
            iDEManager = new IDEManager(this, tabControl1, menuStrip1);

        }


    }
}
