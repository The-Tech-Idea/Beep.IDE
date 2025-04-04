
using System.Data;
using TheTechIdea.Beep.Addin;
using TheTechIdea.Beep.ConfigUtil;
using TheTechIdea.Beep.Container.Services;
using TheTechIdea.Beep.DataBase;
using TheTechIdea.Beep.Editor;
using TheTechIdea.Beep.Logger;
using TheTechIdea.Beep.Utilities;
using TheTechIdea.Beep.Vis;
using TheTechIdea.Beep.Vis.Modules;
using TheTechIdea.Beep.Winform.Default.Views.Template;


namespace Beep.IDE
{
    [AddinAttribute(Caption = "Beep IDE", Name = "BeepIDE", misc = "App", ObjectType = "Beep", addinType = AddinType.Control, displayType = DisplayType.InControl)]
    public partial class uc_BeepIDE :  TemplateUserControl
    {
        public uc_BeepIDE(IBeepService service):base(service)
        {
            InitializeComponent();
           Details.AddinName = "Beep IDE";
            Details.ObjectName = "uc_BeepIDE";
            Details.ObjectType = "UserControl";
            Details.ParentName = "Beep IDE";
        
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
        public IAppManager Visutil { get; set; }

        IProgress<PassedArgs> progress;
        CancellationToken token;
        IBranch RootAppBranch;
        IBranch branch;

       // FunctionandExtensionsHelpers extensionsHelpers;
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

            Visutil = (IAppManager)e.Objects.Where(c => c.Name == "VISUTIL").FirstOrDefault().obj;

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
           // extensionsHelpers = new FunctionandExtensionsHelpers();
            iDEManager = new IDEManager(this, tabControl1, menuStrip1);

        }


    }
}
