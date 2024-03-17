using Beep.Compilers.Module;
using Beep.IDE.Winform.Controls;
using Beep.Vis.Module;
using ScintillaNET;
using System.Reflection;
using TheTechIdea;
using TheTechIdea.Beep;
using TheTechIdea.Beep.FileManager;
using TheTechIdea.Util;


namespace Beep.IDE
{
    public class IDEManager:IDisposable
    {
        public IDEManager()
        {
            Init();
        }
        public void Init()
        {
            Tabs.MouseClick += Tabs_MouseClick;
            Tabs.SelectedIndexChanged += Tabs_SelectedIndexChanged;
            Tabs.Selected += Tabs_Selected;
            Tabs.DragOver += Tabs_DragOver;
            Tabs.DragDrop += Tabs_DragDrop;
            Tabs.ControlRemoved += Tabs_ControlRemoved;
            Tabs.TabIndexChanged += Tabs_TabIndexChanged;
            Tabs.CloseButtonClick += Tabs_CloseButtonClick;
            Tabs.NextButtonClick += Tabs_NextButtonClick;
            Tabs.PrevButtonClick += Tabs_PrevButtonClick;
            Tabs.TabRemoved += Tabs_TabRemoved;
            if (Tabs.TabPages.Count > 0)
            {
                Tabs.TabPages.Remove(Tabs.TabPages[0]);
            }
            menuManager = new MenuManager(this);
            menuManager.CreateMenu(Menustrip);
            GetCompilers();
        }

      

        public IDEManager(IDMEEditor peditor, IPassedArgs e, Control parent, BeepTabControl tab, MenuStrip menu, CancellationToken cancellationToken, IProgress<PassedArgs> progress)
        {
            DMEEditor = peditor;
            Tabs = tab;
            Menustrip = menu;
            Parent = parent;
            CancellationToken = cancellationToken;
            Progress = progress;
            Passedarguments = e;
            Visutil = (IVisManager)e.Objects.Where(c => c.Name == "VISUTIL").FirstOrDefault().obj;
            Init();
            // register the hotkeys with the WaitForm
            HotKeyManager.AddHotKey(Parent, OpenSearch, Keys.F, true);
            HotKeyManager.AddHotKey(Parent, OpenFindDialog, Keys.F, true, false, true);
            HotKeyManager.AddHotKey(Parent, OpenReplaceDialog, Keys.R, true);
            HotKeyManager.AddHotKey(Parent, OpenReplaceDialog, Keys.H, true);
            HotKeyManager.AddHotKey(Parent, Uppercase, Keys.U, true);
            HotKeyManager.AddHotKey(Parent, Lowercase, Keys.L, true);
            HotKeyManager.AddHotKey(Parent, ZoomIn, Keys.Oemplus, true);
            HotKeyManager.AddHotKey(Parent, ZoomOut, Keys.OemMinus, true);
            HotKeyManager.AddHotKey(Parent, ZoomDefault, Keys.D0, true);
            HotKeyManager.AddHotKey(Parent, CloseSearch, Keys.Escape);

        }
        #region "Properties"
        public event EventHandler<CursorMoveEvent> CursorPositionChanged;
        public event EventHandler<ProjectUpdateEvent> FileAddedToProject;
        CancellationToken CancellationToken;
        IProgress<PassedArgs> Progress;
        public IPassedArgs Passedarguments { get; }
        ListofMethods listofMethods = new ListofMethods();
        static int idxuntitled = 0;
        IVisManager Visutil;
        public MenuManager menuManager { get; set; }

        public string searchtext { get; set; }
        public IDMEEditor DMEEditor { get; }
        public BeepTabControl Tabs { get; set; } = new BeepTabControl();
        public MenuStrip Menustrip { get; }
        public Control Parent { get; }
        public List<TabsData> Files { get; set; } = new List<TabsData>();
        public int CurrentFileIndex { get; set; } = -1;
        public TabsData CurrentFile
        {
            get
            {
                if (CurrentFileIndex >= 0)
                {
                    return Files[CurrentFileIndex];
                }
                else
                    return null;
            }
        }
        public string CurrentFileName
        {
            get
            {
                if (CurrentFileIndex >= 0)
                {
                    return Files[CurrentFileIndex].filename;
                }
                else
                    return null;
            }
        }
        public SearchManager SearchManager { get; set; }
        public List<ICompiler> Compilers { get; set; } = new List<ICompiler>();
        #endregion "Properties"
        #region "Compiler Methods"
        public void GetCompilers()
        {
            Compilers = new List<ICompiler>();
            listofMethods = new ListofMethods();
            foreach (AssemblyClassDefinition definition in DMEEditor.ConfigEditor.AppComponents.Where(p => p.componentType == "ICompiler").ToList())
            {
                // DMEEditor.assemblyHandler.CreateInstanceFromString(definition.className);

                ICompiler compiler = (ICompiler)Activator.CreateInstance(definition.type, new object[] { DMEEditor, Progress, CancellationToken, Passedarguments });
                CreateMethodsforCompiler(definition, compiler);
                Compilers.Add(compiler);
            }
          //  SetupCompilers();

        }
        private void CreateMethodsforCompiler(AssemblyClassDefinition c, ICompiler compiler)
        {

            ToolStripMenuItem HeadmenuItem = new ToolStripMenuItem();
            HeadmenuItem.Text = compiler.Name;
            HeadmenuItem.Name = compiler.Name;
            HeadmenuItem.Tag = c;

            Menustrip.Items.Add(HeadmenuItem);
            ToolStripMenuItem InitmenuItem = new ToolStripMenuItem();

            InitmenuItem.Text = "Init";
            InitmenuItem.Name = compiler.Name + ".Init";
            InitmenuItem.Tag = compiler.Name + ".Init";
            InitmenuItem.Click -= CompilerInitMethod;
            InitmenuItem.Click += CompilerInitMethod;
            InitmenuItem.Image = global::Beep.IDE.Properties.Resources.RunAll;

            HeadmenuItem.DropDownItems.Add(InitmenuItem);
            ToolStripMenuItem SetupmenuItem = new ToolStripMenuItem();

            SetupmenuItem.Text = "Configuration";
            SetupmenuItem.Name = compiler.Name + ".Configuration";
            SetupmenuItem.Tag = compiler.Name + ".Configuration";
            SetupmenuItem.Click -= ConfigurationSetupConfigMethod;
            SetupmenuItem.Click += ConfigurationSetupConfigMethod;
            SetupmenuItem.Image = global::Beep.IDE.Properties.Resources.ConfigurationEditor;
            HeadmenuItem.DropDownItems.Add(SetupmenuItem);
            foreach (var item in c.Methods)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem();
                listofMethods.Methods.Add(new CompilerMethod() { CompilerName = compiler.Name, MethodName = item.Name, AssemblyClass = c, MClass = item });
                menuItem.Text = item.Caption;
                menuItem.Name = compiler.Name + "." + item.Name;
                menuItem.Tag = item;
                menuItem.Click -= MenuItem_Click;
                menuItem.Click += MenuItem_Click;
                if (item.iconimage != null)
                {
                    // item.iconimage = Visutil.visHelper.GetImageIndex(item.iconimage, IconsSize.Width);
                }
                HeadmenuItem.DropDownItems.Add(menuItem);

            }


        }
        private void ConfigurationSetupConfigMethod(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;

            if (toolStripMenuItem != null)
            {
                string compilername = toolStripMenuItem.Name.Split('.')[0];
                ICompiler compiler = Compilers.Where(c => c.Name == compilername).FirstOrDefault();
                if (compiler != null)
                {
                    if (compiler.IsCompilerAvailable)
                    {
                        if (Visutil.Controlmanager.InputBoxYesNo("Beep IDE", "Already the Compiler is Initialized , woudl you like to shutdown and open new one?") == Beep.Vis.Module.DialogResult.Yes)
                        {
                            compiler.Shutdown();
                        }
                        else
                        {
                            return;
                        }

                    }

                    if (compiler.SetConfig(new PassedArgs() { }).Flag == Errors.Ok)
                    {
                        DMEEditor.AddLogMessage("IDE", $"Compiler Configuration", DateTime.Now, 0, null, Errors.Ok);
                    };
                }
            }
        }
        private void CompilerInitMethod(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;

            if (toolStripMenuItem != null)
            {
                string compilername = toolStripMenuItem.Name.Split('.')[0];
                ICompiler compiler = Compilers.Where(c => c.Name == compilername).FirstOrDefault();
                if (compiler != null)
                {
                    if (compiler.InitConfig(new PassedArgs() { }).Flag == Errors.Ok)
                    {
                        DMEEditor.AddLogMessage("IDE", $"Compiler Loaded", DateTime.Now, 0, null, Errors.Ok);
                    };
                }
            }
        }
        private void MenuItem_Click(object sender, EventArgs e)
        {
           
            RunFunction(sender, e);
        }
        public bool SetupCompiler(ICompiler compiler)
        {

            if (compiler.SetConfig((PassedArgs)DMEEditor.Passedarguments).Flag == Errors.Ok)
            {
                if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), compiler.Extension)))
                {
                    Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), compiler.Extension));
                }
                return true;
            }
            else return false;

        }
        public bool SetupCompilers()
        {
            if (Compilers.Count > 0)
            {
                foreach (ICompiler compiler in Compilers)
                {
                    compiler.SetConfig((PassedArgs)DMEEditor.Passedarguments);
                    if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), compiler.Extension)))
                    {
                        Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), compiler.Extension));
                    }
                }
            }
            return true;
        }
        public bool IsCompilerAvailable(string compilername)
        {
            if (Compilers.Count > 0)
            {
                foreach (ICompiler compiler in Compilers)
                {
                    if (compiler.Name == compilername)
                    {
                        return compiler.IsCompilerAvailable;
                    }
                }
            }
            return false;

        }
        public bool IsCompilerReady(string compilername)
        {
            if (Compilers.Count > 0)
            {
                foreach (ICompiler compiler in Compilers)
                {
                    if (compiler.Name == compilername)
                    {
                        return compiler.IsCompilerReady;
                    }
                }
            }
            return false;

        }
        public ICompiler GetCompiler(string name)
        {
            if (Compilers.Count > 0)
            {
                foreach (ICompiler compiler in Compilers)
                {
                    if (compiler.Name == name)
                    {
                        return compiler;
                    }
                }
            }
            else return null;
            return null;
        }
        public void RunFunction(object sender, EventArgs e)
        {


            AssemblyClassDefinition assemblydef = new AssemblyClassDefinition();
            AssemblyClassDefinition cls = null;
            MethodInfo method = null;
            MethodsClass methodsClass;
            string compilername = "";
            string MethodName = "";
            if (sender == null) { return; }
            if (sender.GetType() == typeof(ToolStripButton))
            {
                ToolStripButton item = (ToolStripButton)sender;

                MethodName = item.Name.Split('.')[1]; ;
                compilername = item.Name.Split('.')[0];
            }
            if (sender.GetType() == typeof(ToolStripMenuItem))
            {
                ToolStripMenuItem item = (ToolStripMenuItem)sender;

                MethodName = item.Name.Split('.')[1]; ;
                compilername = item.Name.Split('.')[0];
            }

            if (!IsCompilerAvailable(compilername))
            {
                string mes = $"Compiler is not Available : {compilername}";
                DMEEditor.AddLogMessage(mes, "Beep IDE", DateTime.Now, 0, compilername, Errors.Failed);
                MessageBox.Show(mes, "Beep IDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsCompilerReady(compilername))
            {
                string mes = $"Compiler is not Ready nor Initilized : {compilername}";
                DMEEditor.AddLogMessage(mes, "Beep IDE", DateTime.Now, 0, compilername, Errors.Failed);
                MessageBox.Show(mes, "Beep IDE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //dynamic fc = DMEEditor.assemblyHandler.CreateInstanceFromString(assemblydef.type.ToString(), new object[] { DMEEditor, Visutil, this });
            dynamic fc = GetCompiler(compilername);


            methodsClass = listofMethods.Methods.Where(x => x.MethodName == MethodName && x.CompilerName == compilername).FirstOrDefault().MClass;

            if (DMEEditor.Passedarguments == null)
            {
                DMEEditor.Passedarguments = new PassedArgs();
            }
            if (methodsClass != null)
            {
                method = methodsClass.Info;
                if (method.GetParameters().Length > 0)
                {
                    method.Invoke(fc, new object[] { DMEEditor.Passedarguments });
                }
                else
                    method.Invoke(fc, null);
            }
        }
        public bool RunCode(int tabindx = -1)
        {
            bool retval = false;
            try
            {
                if (Tabs.TabPages.Count > 0)
                {
                    if (tabindx >= 0)
                    {
                        if (Tabs.TabPages[tabindx] != null)
                        {
                            TabsData p = Files[tabindx];
                            ICompiler compiler = Compilers[p.CompilerIndex];
                            if (compiler != null)
                            {
                                if (compiler.IsCompilerReady)
                                {
                                    PassedArgs args = new PassedArgs();
                                    args.ParameterString1 = $"Running Code File {p.filename}";
                                    Visutil.ShowWaitForm(args);
                                    compiler.Run(p.EditorControl.Text, null, null, Progress, CancellationToken);
                                    args.ParameterString1 = $"Finished Run for {p.filename}";
                                    Visutil.PasstoWaitForm(args);
                                    Visutil.CloseWaitForm();
                                }
                                else
                                    MessageBox.Show($"Please Initialize Compiler {compiler.Name}", "Beep IDE");

                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Visutil.CloseWaitForm();
                retval = false;
                DMEEditor.AddLogMessage("IDE", $"Run Error {ex.Message}", DateTime.Now, 0, null, Errors.Failed);
            }
            return retval;
        }
        public bool RunCode()
        {
            bool retval = false;
            try
            {
                if (Tabs.TabPages.Count > 0)
                {

                }

            }
            catch (Exception ex)
            {
                retval = false;
                DMEEditor.AddLogMessage("IDE", $"Run Error {ex.Message}", DateTime.Now, 0, null, Errors.Failed);
            }
            return retval;
        }
        #endregion "Compiler Methods"
        #region "Tab Events"
        private void Tabs_PrevButtonClick(object sender, TabsDataEventarg e)
        {
            SetCurrentFileToPrevious();
        }

        private void Tabs_NextButtonClick(object sender, TabsDataEventarg e)
        {
            SetCurrentFileToNext();
        }
        private void Tabs_TabRemoved(object sender, TabsDataEventarg e)
        {
            if (Files.Count == 0)
            {
                return;
            }

            Files.RemoveAt(e.Tabidx);
        }
        private void Tabs_CloseButtonClick(object sender, TabsDataEventarg e)
        {
            TabsData t = Files[e.Tabidx];
            if (t.isSaved)
            {
                e.Cancel = false;
                return;
            }
            else
            {
                if (Visutil.Controlmanager.InputBoxYesNo("Beep IDE", "File not Saved , Would you like to save it") == Beep.Vis.Module.DialogResult.Yes)
                {
                    string retval = Visutil.Controlmanager.SaveFileDialog(t.ext, Environment.CurrentDirectory, null);

                }
            }
            e.Cancel = false;
        }
        private void Tabs_TabIndexChanged(object sender, EventArgs e)
        {
            if (CurrentFileIndex >= 0)
            {
                CurrentFileIndex = Tabs.SelectedIndex;
            }

        }
        private void Tabs_ControlRemoved(object sender, ControlEventArgs e)
        {


        }
        private void Tabs_DragDrop(object? sender, DragEventArgs e)
        {
            InitDragDropFile();
        }
        private void Tabs_DragOver(object? sender, DragEventArgs e)
        {

        }
        private void Tabs_Selected(object? sender, TabControlEventArgs e)
        {
            CurrentFileIndex = e.TabPageIndex;
        }
        private void Tabs_SelectedIndexChanged(object? sender, EventArgs e)
        {
        }
        private void Tabs_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

            }
        }
        #endregion "Tab Events"
        #region "File Operations"
        public IErrorsInfo Save(string filename)
        {
            DMEEditor.ErrorObject.Flag = Errors.Ok;
            TabsData data = null;
            try
            {
                int idx = Files.FindIndex(p => p.filename.Equals(filename));
                if (idx < 0)
                {
                    DMEEditor.AddLogMessage("IDE", "Error file not found", DateTime.Now, 0, null, Errors.Failed);
                }
                else
                {
                    data = Files[idx];
                }
                if (data.isSaved)
                {
                    File.WriteAllText(Path.Combine(data.filepath, data.filename), data.EditorControl.Text);
                    data.isSaved = true;
                    data.isEdited = false;
                    DMEEditor.AddLogMessage("IDE", "File Saved", DateTime.Now, 0, null, Errors.Ok);
                }
                else
                {
                    
                    SaveAs(filename);
                }
            }
            catch (Exception ex)
            {

                DMEEditor.AddLogMessage("IDE", "Error file not found", DateTime.Now, 0, null, Errors.Failed);
            }
            return DMEEditor.ErrorObject;
        }
        public IErrorsInfo SaveAs(string filename)
        {
            DMEEditor.ErrorObject.Flag = Errors.Ok;
            TabsData data = null;
            string projectfolder = string.Empty;
            if (!string.IsNullOrEmpty(DMEEditor.ConfigEditor.Config.ProjectsPath))
            {
                projectfolder = DMEEditor.ConfigEditor.Config.ProjectsPath;
            }
            else
                projectfolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


            try
            {
                int idx = Files.FindIndex(p => p.filename.Equals(filename));
                if (idx < 0)
                {
                    DMEEditor.AddLogMessage("IDE", "Error file not found", DateTime.Now, 0, null, Errors.Failed);
                }
                else
                {
                    data = Files[idx];
                }
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = Path.Combine(projectfolder, data.ext); ;
                saveFileDialog1.Title = $"Save {data.lexer} Files";
                //saveFileDialog1.CheckFileExists = true;
                saveFileDialog1.CheckPathExists = true;
                saveFileDialog1.DefaultExt = "txt";
                saveFileDialog1.Filter = $"{data.lexer} files (*{data.ext})|*{data.ext}";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    data.filepath = Path.GetDirectoryName(saveFileDialog1.FileName);
                    data.filename = Path.GetFileName(saveFileDialog1.FileName);
                    File.WriteAllText(Path.Combine(data.filepath, data.filename), data.EditorControl.Text);
                    data.isSaved = true;
                    data.isEdited = false;
                }

            }
            catch (Exception ex)
            {
                DMEEditor.AddLogMessage("IDE", "Error file not found", DateTime.Now, 0, null, Errors.Failed);
            }
            return DMEEditor.ErrorObject;
        }
        #region "Save to Project"
        public IErrorsInfo SaveToProject(string filename)
        {
            DMEEditor.ErrorObject.Flag = Errors.Ok;
            TabsData data = null;
            try
            {
                int idx = Files.FindIndex(p => p.filename.Equals(filename));
                if (idx < 0)
                {
                    DMEEditor.AddLogMessage("IDE", "Error file not found", DateTime.Now, 0, null, Errors.Failed);
                }
                else
                {
                    data = Files[idx];
                }
                if (data.isSaved)
                {
                    string fi = Path.Combine(data.filepath, data.filename);
                    File.WriteAllText(fi, data.EditorControl.Text);
                    data.isSaved = true;
                     
                   
                }else
                {
                    SaveAsToProject(filename);
                }
            }
            catch (Exception ex)
            {

                DMEEditor.AddLogMessage("IDE", "Error file not found", DateTime.Now, 0, null, Errors.Failed);
            }
            return DMEEditor.ErrorObject;
        }
        public IErrorsInfo SaveAsToProject(string filename)
        {
            TabsData data = null;
            DMEEditor.ErrorObject.Flag = Errors.Ok;
            try
            {
                int idx = Files.FindIndex(p => p.filename.Equals(filename));
                if (idx < 0)
                {
                    DMEEditor.AddLogMessage("IDE", "Error file not found", DateTime.Now, 0, null, Errors.Failed);
                }
                else
                {
                    data = Files[idx];
                }
                List<string> projectlist = new List<string>();
                string selectedproject = string.Empty;
                projectlist = DMEEditor.ConfigEditor.Projects.Select(p => p.Name).ToList();
                bool firstproject = false;
                bool ok = true;
                bool newproject = false;
                string projectname = string.Empty;
                string rootprojectpath = string.Empty;
                firstproject = true;
                RootFolder projectFolder = null;
                if (projectlist.Count == 0)
                {
                    firstproject = true;
                }
               if (string.IsNullOrEmpty(DMEEditor.ConfigEditor.Config.ProjectsPath))
               {
                  if (Visutil.Controlmanager.InputBox("Beep", "Pick Projects Root Path (Path to host all your Projects)", ref rootprojectpath) == Beep.Vis.Module.DialogResult.OK)
                  {
                       if (!string.IsNullOrEmpty(rootprojectpath))
                       {
                           DMEEditor.ConfigEditor.Config.ProjectsPath= rootprojectpath;
                            DMEEditor.ConfigEditor.SaveConfigValues();
                             ok =true;
                        }
                        else ok = false;
                   }
                   else ok = false;
               }
               else ok = true;
               newproject = false;
               projectname= string.Empty;
               if (ok || firstproject)
               {
                    if (Visutil.Controlmanager.InputBoxYesNo("Beep", "Would you like to Create New Project?") == Beep.Vis.Module.DialogResult.Yes)
                    {
                        if (Visutil.Controlmanager.InputBox("Beep", "Pick Project Name", ref projectname) == Beep.Vis.Module.DialogResult.OK)
                        {
                            if (!string.IsNullOrEmpty(projectname))
                            {

                                int id;//= DMEEditor.ConfigEditor.Projects.Select(p => p.ID).Max() + 1;
                                if (DMEEditor.ConfigEditor.Projects.Count > 0)
                                {
                                    id = DMEEditor.ConfigEditor.Projects.Select(p => p.ID).Max() + 1;
                                }
                                else
                                    id = 1;
                                projectFolder = new RootFolder() { ID = id, Url = Path.Combine(DMEEditor.ConfigEditor.Config.ProjectsPath, projectname), Name = projectname };
                                if(!Directory.Exists(Path.Combine(DMEEditor.ConfigEditor.Config.ProjectsPath, projectname)))
                                {
                                    Directory.CreateDirectory(Path.Combine(DMEEditor.ConfigEditor.Config.ProjectsPath, projectname));
                                }
                              

                                DMEEditor.ConfigEditor.Projects.Add(projectFolder);
                                DMEEditor.ConfigEditor.SaveProjects();
                                newproject = true;
                                ok = true;
                            }
                        }
                    }
                }
                if (projectlist.Count == 0 && newproject==false)
                {
                    ok = false;
                    MessageBox.Show("Error file not found", "Beep");
                    DMEEditor.AddLogMessage("IDE", "Error file not found", DateTime.Now, 0, null, Errors.Failed);
                    return DMEEditor.ErrorObject;

                }
                if (!firstproject && ok && !newproject)
                {
                    if (Visutil.Controlmanager.InputComboBox("Beep", "Pick Project Folder", projectlist, ref projectname) == Beep.Vis.Module.DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(projectname))
                        {
                            projectFolder = DMEEditor.ConfigEditor.Projects.FirstOrDefault(p => p.Name.Equals(projectname, StringComparison.InvariantCultureIgnoreCase));
                       
                        }
                    }
                }
              
               if (projectFolder != null)
                {
                    string fi = Path.Combine(projectFolder.Url, data.filename);
                    File.WriteAllText(fi, data.EditorControl.Text);
                    data.filepath = projectFolder.Url;
                    data.isSaved = true;
                    data.ProjectName = projectname;
                    data.ProjectPath=projectFolder.Url;
                    data.isEdited = false;
                   
                }
                return DMEEditor.ErrorObject;
            }
            catch (Exception ex)
            {
                DMEEditor.AddLogMessage("IDE", $"Error file not found {ex.Message}", DateTime.Now, 0, null, Errors.Failed);
            }
            return DMEEditor.ErrorObject;
        }
        #endregion
        public IErrorsInfo OpenFile(string pfilenamewithpath)
        {
            DMEEditor.ErrorObject.Flag = Errors.Ok;
            TabsData data = null;
            string projectfolder = string.Empty;
            string filename = Path.GetFileName(pfilenamewithpath);
            string filepath= Path.GetDirectoryName(pfilenamewithpath);
            TabsData tab =null;
            bool fileexist=false;
            try
            {
                if(Files.Count>0)
                {
                    tab = Files.FirstOrDefault(p=>p.filename.Equals(filename,StringComparison.InvariantCultureIgnoreCase));
                }
                if(tab!=null)
                {
                    fileexist = true;
                    SetCurrentFile(filename);
                   
                }
                else
                {
                    AddFile(pfilenamewithpath);
                }

            }
            catch (Exception ex)
            {
                DMEEditor.AddLogMessage("IDE", $"Error in opening File {ex.Message}", DateTime.Now, 0, null, Errors.Failed);
            }
            return DMEEditor.ErrorObject;
        }
        public void NewFile(Lexer lexer = Lexer.Python)
        {
            string filename = "";
            if (Visutil.Controlmanager.InputBox("Beep IDE", "Enter File Name", ref filename) == Beep.Vis.Module.DialogResult.OK)
            {
                List<string> c = Compilers.Select(p => p.Extension).ToList();
                string retval = string.Empty;
                if (Visutil.Controlmanager.InputComboBox("Compiler Select", "Please Selct File Type", c, ref retval) == Beep.Vis.Module.DialogResult.OK)
                {
                    NewFile(filename, retval);
                }

            }
            else
            {
                idxuntitled += 1;
                AddFile("Untitled" + idxuntitled, lexer);
            }

        }
        public void NewFile(string filename, string ext)
        {
            TabsData data = new TabsData();
            Scintilla ctl = new Scintilla();

            createnewFileTab(filename + $"{ext}", null, ctl, data);
        }
        public void AddFile(string file, Lexer lexer)
        {
            TabsData data = new TabsData();
            Scintilla ctl = new Scintilla();
            string fileame = System.IO.Path.GetFileName(file);
            string filepath = System.IO.Path.GetDirectoryName(file);

            // ctl.LexerName= lexer;
            createnewFileTab(fileame, filepath, ctl, data);

        }
        public void AddFile(string file)
        {
            TabsData data = new TabsData();
            Scintilla ctl = new Scintilla();
            string fileame = System.IO.Path.GetFileName(file);
            string filepath = System.IO.Path.GetDirectoryName(file);
            string[] lexs = Enum.GetNames(typeof(Lexer));
            createnewFileTab(fileame, filepath, ctl, data);

        }
        private void createnewFileTab(string filename, string filepath, Scintilla ctl, TabsData data)
        {
            try
            {
                string filext = System.IO.Path.GetExtension(filename);
                string[] lexs = Enum.GetNames(typeof(Lexer));
                //  List<string> c = Compilers.FirstOrDefault(p => p.Extension).ToList();
                Tabs.TabPages.Add(filename, filename);
                int idx = Tabs.TabPages.IndexOfKey(filename);
                TabPage page = Tabs.TabPages[idx];
                page.Text = filename;
                page.Controls.Add(ctl);
                ctl.Name = filename;
                ctl.Dock = System.Windows.Forms.DockStyle.Fill;

                data.EditorControl = ctl;
                data.Tabidx = idx;
                data.filename = filename;
                data.isSaved = false;
                data.isEdited = false;
                data.filepath = filepath;
                if (!string.IsNullOrEmpty(filepath))
                {
                    data.isSaved = true;
                }
                else
                    data.isSaved = false;
                data.ext = filext;
                data.CompilerIndex = Compilers.FindIndex(p => p.Extension.Equals(filext, StringComparison.InvariantCultureIgnoreCase));
                if (Files.Contains(data))
                {
                    Files.Remove(data);
                }
                Files.Add(data);
                CurrentFileIndex = Files.Count - 1;
                ctl.LexerName = Compilers[data.CompilerIndex].Lexer;
                ctl.UpdateUI += Ctl_UpdateUI;
                
               
                InitHotkeys();
                // STYLING
                //  InitColors();
                InitSyntaxColoring();

                // NUMBER MARGIN
                InitNumberMargin();

                // BOOKMARK MARGIN
                InitBookmarkMargin();

                // CODE FOLDING MARGIN
                InitCodeFolding();

                // DRAG DROP
                InitDragDropFile();
            }
            catch (Exception ex)
            {

                //throw;
            }

        }
        private void Ctl_UpdateUI(object sender, UpdateUIEventArgs e)
        {
            ScintillaNET.Scintilla scintilla = (Scintilla)sender;
            switch (e.Change)
            {
                case UpdateChange.Content:
                    CursorPositionChanged?.Invoke(this, new CursorMoveEvent() { CurrentColumn = scintilla.CurrentPosition, CurrentLine = scintilla.CurrentLine, TotalLines = scintilla.Lines.Count });
                    CurrentFile.isEdited = true;
                    break; 
                case UpdateChange.Selection:
                    CursorPositionChanged?.Invoke(this, new CursorMoveEvent() { CurrentColumn = scintilla.CurrentPosition, CurrentLine = scintilla.CurrentLine, TotalLines = scintilla.Lines.Count });
                    break;
                 case UpdateChange.VScroll:
                    CursorPositionChanged?.Invoke(this, new CursorMoveEvent() { CurrentColumn = scintilla.CurrentPosition, CurrentLine = scintilla.CurrentLine, TotalLines = scintilla.Lines.Count });
                    break;
                case UpdateChange.HScroll:
                    CursorPositionChanged?.Invoke(this, new CursorMoveEvent() { CurrentColumn = scintilla.CurrentPosition, CurrentLine = scintilla.CurrentLine, TotalLines = scintilla.Lines.Count });
                    break;
            }
         
            
        }
        public void RemoveFile(string file)
        {
            string fileame = System.IO.Path.GetFileName(file);

            int pageidx = Tabs.TabPages.IndexOfKey(fileame);
            int ctlidx = Files.FindIndex(p => p.filename == fileame);
            TabPage page = Tabs.TabPages[pageidx];

            Tabs.TabPages.RemoveByKey(fileame);
            Files.RemoveAt(ctlidx);
            CurrentFileIndex = Files.Count - 1;
        }
        public void RemoveFile(int index)
        {
            TabPage page = Tabs.TabPages[index];
            Tabs.TabPages.RemoveAt(index);
            Files.RemoveAt(index);
            CurrentFileIndex = Files.Count - 1;
        }
        public void SetCurrentFile(string file)
        {
            int pageidx = Tabs.TabPages.IndexOfKey(file);
            int ctlidx = Files.FindIndex(p => p.filename == file);
            CurrentFileIndex = ctlidx;
            Tabs.SelectedIndex = pageidx;
        }
        public void SetCurrentFile(int index)
        {

            CurrentFileIndex = index;
            
            int pageidx = Tabs.TabPages.IndexOfKey(CurrentFile.filename);
            Tabs.SelectedIndex = pageidx;
        }
        public void SetCurrentFileToPrevious()
        {
            if (CurrentFileIndex > 0)
            {
                CurrentFileIndex--;
                SetCurrentFile(CurrentFileIndex);
            }
        }
        public void SetCurrentFileToNext()
        {
            if (CurrentFileIndex < Files.Count - 1)
            {
                CurrentFileIndex++;
                SetCurrentFile(CurrentFileIndex);
            }
        }
        public void SetCurrentFileToFirst()
        {
            CurrentFileIndex = 0;
            SetCurrentFile(CurrentFileIndex);
        }
        public void SetCurrentFileToLast()
        {
            CurrentFileIndex = Files.Count - 1;
            SetCurrentFile(CurrentFileIndex);
        }
        public void SetCurrentFileToNone()
        {
            CurrentFileIndex = -1;
            SetCurrentFile(CurrentFileIndex);
        }
        public void SetCurrentFileToNew()
        {
            CurrentFileIndex = Files.Count;
            SetCurrentFile(CurrentFileIndex);

        }
        #endregion "File Operations"
        #region "Control Visualization"
        #region Uppercase / Lowercase

        public void Lowercase()
        {

            // save the selection
            int start = CurrentFile.EditorControl.SelectionStart;
            int end = CurrentFile.EditorControl.SelectionEnd;

            // modify the selected text
            CurrentFile.EditorControl.ReplaceSelection(CurrentFile.EditorControl.GetTextRange(start, end - start).ToLower());

            // preserve the original selection
            CurrentFile.EditorControl.SetSelection(start, end);
        }

        public void Uppercase()
        {

            // save the selection
            int start = CurrentFile.EditorControl.SelectionStart;
            int end = CurrentFile.EditorControl.SelectionEnd;

            // modify the selected text
            CurrentFile.EditorControl.ReplaceSelection(CurrentFile.EditorControl.GetTextRange(start, end - start).ToUpper());

            // preserve the original selection
            CurrentFile.EditorControl.SetSelection(start, end);
        }

        #endregion
        #region Indent / Outdent

        public void Indent()
        {
            // we use this hack to send "Shift+Tab" to scintilla, since there is no known API to indent,
            // although the indentation function exists. Pressing TAB with the editor focused confirms this.
            GenerateKeystrokes("{TAB}");
        }

        public void Outdent()
        {
            // we use this hack to send "Shift+Tab" to scintilla, since there is no known API to outdent,
            // although the indentation function exists. Pressing Shift+Tab with the editor focused confirms this.
            GenerateKeystrokes("+{TAB}");
        }

        public void GenerateKeystrokes(string keys)
        {
            HotKeyManager.Enable = false;
            CurrentFile.EditorControl.Focus();
            SendKeys.Send(keys);
            HotKeyManager.Enable = true;
        }

        #endregion
        #region Zoom

        public void ZoomIn()
        {
            CurrentFile.EditorControl.ZoomIn();
        }

        public void ZoomOut()
        {
            CurrentFile.EditorControl.ZoomOut();
        }

        public void ZoomDefault()
        {
            CurrentFile.EditorControl.Zoom = 0;
        }


        #endregion
        #region Quick Search Bar

        bool SearchIsOpen = false;
        private bool disposedValue;

        public void OpenSearch()
        {


            SearchManager.Find(CurrentFile.EditorControl, searchtext, false, true);

            //if (!SearchIsOpen)
            //{
            //    SearchIsOpen = true;

            //    PanelSearch.Visible = true;
            //    TxtSearch.Text = SearchManager.LastSearch;
            //    TxtSearch.Focus();
            //    TxtSearch.SelectAll();

            //}
            //else
            //{

            //    TxtSearch.Focus();
            //    TxtSearch.SelectAll();

            //}
        }
        public void CloseSearch()
        {
            if (SearchIsOpen)
            {
                SearchIsOpen = false;

                //PanelSearch.Visible = false;

            }
        }

        public void BtnClearSearch_Click(object sender, EventArgs e)
        {
            CloseSearch();
        }

        public void BtnPrevSearch_Click(object sender, EventArgs e)
        {
            SearchManager.Find(CurrentFile.EditorControl, searchtext, false, false);
        }
        public void BtnNextSearch_Click(object sender, EventArgs e)
        {
            SearchManager.Find(CurrentFile.EditorControl, searchtext, true, false);
        }
        public void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchManager.Find(CurrentFile.EditorControl, searchtext, true, true);
        }

        public void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (HotKeyManager.IsHotkey(e, Keys.Enter))
            {
                SearchManager.Find(CurrentFile.EditorControl, searchtext, true, false);
            }
            if (HotKeyManager.IsHotkey(e, Keys.Enter, true) || HotKeyManager.IsHotkey(e, Keys.Enter, false, true))
            {
                SearchManager.Find(CurrentFile.EditorControl, searchtext, false, false);
            }
        }

        #endregion
        #region Find & Replace Dialog

        public void OpenFindDialog()
        {

        }
        public void OpenReplaceDialog()
        {


        }

        #endregion 
        #region Utils

        public Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        public void InvokeIfNeeded(Action action)
        {
            if (CurrentFile.EditorControl.InvokeRequired)
            {
                CurrentFile.EditorControl.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        #endregion
        public void SetupCurrentTextArea()
        {

            // BASIC CONFIG
            CurrentFile.EditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            CurrentFile.EditorControl.TextChanged += (this.OnTextChanged);

            // INITIAL VIEW CONFIG
            CurrentFile.EditorControl.WrapMode = WrapMode.None;
            CurrentFile.EditorControl.IndentationGuides = IndentView.LookBoth;

            // STYLING
            InitColors();
            InitSyntaxColoring();

            // NUMBER MARGIN
            InitNumberMargin();

            // BOOKMARK MARGIN
            InitBookmarkMargin();

            // CODE FOLDING MARGIN
            InitCodeFolding();

            // DRAG DROP
            InitDragDropFile();

            // DEFAULT FILE
            //  LoadDataFromFile("../../MainForm.cs");
            //  CurrentFile.Text="";
        }
        private void OnTextChanged(object sender, EventArgs e)
        {

        }
        private void InitSyntaxColoring()
        {

            // Configure the default style
            CurrentFile.EditorControl.StyleResetDefault();
            CurrentFile.EditorControl.StyleClearAll();
            CurrentFile.EditorControl.Styles[Style.Default].Font = "Consolas";
            CurrentFile.EditorControl.Styles[Style.Default].Size = 10;
            CurrentFile.EditorControl.Styles[Style.Default].BackColor = IntToColor(0xFFFFFF); 
            CurrentFile.EditorControl.Styles[Style.Default].ForeColor = IntToColor(0x212121);
           

            // Configure the CPP (C#) lexer styles
            switch (CurrentFile.lexer)
            {
                case "Python":
                    CurrentFile.EditorControl.Styles[Style.Python.Identifier].ForeColor = IntToColor(0xD0DAE2);
                    CurrentFile.EditorControl.Styles[Style.Python.String].ForeColor = IntToColor(0xBD758B);
                    CurrentFile.EditorControl.Styles[Style.Python.Number].ForeColor = IntToColor(0x40BF57);
                    CurrentFile.EditorControl.Styles[Style.Python.CommentBlock].ForeColor = IntToColor(0x2FAE35);
                    CurrentFile.EditorControl.Styles[Style.Python.ClassName].ForeColor = IntToColor(0xFFFF00);
                    CurrentFile.EditorControl.Styles[Style.Python.CommentLine].ForeColor = IntToColor(0xFFFF00);
                    CurrentFile.EditorControl.Styles[Style.Python.Character].ForeColor = IntToColor(0xE95454);
                    CurrentFile.EditorControl.Styles[Style.Python.Decorator].ForeColor = IntToColor(0x8AAFEE);
                    CurrentFile.EditorControl.Styles[Style.Python.Operator].ForeColor = IntToColor(0xE0E0E0);
                    CurrentFile.EditorControl.Styles[Style.Python.Word].ForeColor = IntToColor(0x48A8EE);
                    CurrentFile.EditorControl.Styles[Style.Python.Word2].ForeColor = IntToColor(0xF98906);
                    CurrentFile.EditorControl.Styles[Style.Python.Default].ForeColor = IntToColor(0xB3D991);
                    CurrentFile.EditorControl.Styles[Style.Python.DefName].ForeColor = IntToColor(0xFF0000);
                    CurrentFile.EditorControl.Styles[Style.Python.Triple].ForeColor = IntToColor(0x48A8EE);
                    break;
                case "R":
                    CurrentFile.EditorControl.Styles[Style.R.Identifier].ForeColor = IntToColor(0xD0DAE2);
                    CurrentFile.EditorControl.Styles[Style.R.Comment].ForeColor = IntToColor(0xBD758B);
                    CurrentFile.EditorControl.Styles[Style.R.String2].ForeColor = IntToColor(0x40BF57);
                    CurrentFile.EditorControl.Styles[Style.R.KWord].ForeColor = IntToColor(0x2FAE35);
                    CurrentFile.EditorControl.Styles[Style.R.BaseKWord].ForeColor = IntToColor(0xFFFF00);
                    CurrentFile.EditorControl.Styles[Style.R.String].ForeColor = IntToColor(0xFFFF00);
                    CurrentFile.EditorControl.Styles[Style.R.Default].ForeColor = IntToColor(0xE95454);
                    CurrentFile.EditorControl.Styles[Style.R.Infix].ForeColor = IntToColor(0x8AAFEE);
                    CurrentFile.EditorControl.Styles[Style.R.Operator].ForeColor = IntToColor(0xE0E0E0);
                    CurrentFile.EditorControl.Styles[Style.R.Identifier].ForeColor = IntToColor(0xff00ff);
                    CurrentFile.EditorControl.Styles[Style.R.Comment].ForeColor = IntToColor(0x77A7DB);
                    CurrentFile.EditorControl.Styles[Style.R.Number].ForeColor = IntToColor(0x48A8EE);
                    CurrentFile.EditorControl.Styles[Style.R.OtherKWord].ForeColor = IntToColor(0xF98906);
                    CurrentFile.EditorControl.Styles[Style.R.InfixEol].ForeColor = IntToColor(0xB3D991);

                    break;
                case "Cpp":
                    CurrentFile.EditorControl.Styles[Style.Cpp.Identifier].ForeColor = IntToColor(0xD0DAE2);
                    CurrentFile.EditorControl.Styles[Style.Cpp.Comment].ForeColor = IntToColor(0xBD758B);
                    CurrentFile.EditorControl.Styles[Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
                    CurrentFile.EditorControl.Styles[Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
                    CurrentFile.EditorControl.Styles[Style.Cpp.Number].ForeColor = IntToColor(0xFFFF00);
                    CurrentFile.EditorControl.Styles[Style.Cpp.String].ForeColor = IntToColor(0xFFFF00);
                    CurrentFile.EditorControl.Styles[Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
                    CurrentFile.EditorControl.Styles[Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
                    CurrentFile.EditorControl.Styles[Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
                    CurrentFile.EditorControl.Styles[Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
                    CurrentFile.EditorControl.Styles[Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
                    CurrentFile.EditorControl.Styles[Style.Cpp.Word].ForeColor = IntToColor(0x48A8EE);
                    CurrentFile.EditorControl.Styles[Style.Cpp.Word2].ForeColor = IntToColor(0xF98906);
                    CurrentFile.EditorControl.Styles[Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
                    CurrentFile.EditorControl.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
                    CurrentFile.EditorControl.Styles[Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);
                    break;
                default:
                    break;
            }


            CurrentFile.EditorControl.Name = CurrentFile.filename;

            CurrentFile.EditorControl.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            CurrentFile.EditorControl.SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");

        }
        #region Numbers, Bookmarks, Code Folding

        /// <summary>
        /// the background color of the text area
        /// </summary>
        private const int BACK_COLOR = 0x2A211C;

        /// <summary>
        /// default text color of the text area
        /// </summary>
        private const int FORE_COLOR = 0xB7B7B7;

        /// <summary>
        /// change this to whatever margin you want the line numbers to show in
        /// </summary>
        private const int NUMBER_MARGIN = 1;

        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;

        /// <summary>
        /// change this to whatever margin you want the code folding tree (+/-) to show in
        /// </summary>
        private const int FOLDING_MARGIN = 3;

        /// <summary>
        /// set this true to show circular buttons for code folding (the [+] and [-] buttons on the margin)
        /// </summary>
        private const bool CODEFOLDING_CIRCULAR = true;

        private void InitNumberMargin()
        {

            CurrentFile.EditorControl.Styles[Style.LineNumber].BackColor = IntToColor(BACK_COLOR);
            CurrentFile.EditorControl.Styles[Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
            CurrentFile.EditorControl.Styles[Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
            CurrentFile.EditorControl.Styles[Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);

            var nums = CurrentFile.EditorControl.Margins[NUMBER_MARGIN];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;

            CurrentFile.EditorControl.MarginClick += TextArea_MarginClick;
        }

        private void InitBookmarkMargin()
        {

            //CurrentFile.SetFoldMarginColor(true, IntToColor(BACK_COLOR));

            var margin = CurrentFile.EditorControl.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER);
            //margin.Cursor = MarginCursor.Arrow;

            var marker = CurrentFile.EditorControl.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(IntToColor(0xFF003B));
            marker.SetForeColor(IntToColor(0x000000));
            marker.SetAlpha(100);

        }

        private void InitCodeFolding()
        {

            CurrentFile.EditorControl.SetFoldMarginColor(true, IntToColor(BACK_COLOR));
            CurrentFile.EditorControl.SetFoldMarginHighlightColor(true, IntToColor(BACK_COLOR));

            // Enable code folding
            CurrentFile.EditorControl.SetProperty("fold", "1");
            CurrentFile.EditorControl.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            CurrentFile.EditorControl.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            CurrentFile.EditorControl.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            CurrentFile.EditorControl.Margins[FOLDING_MARGIN].Sensitive = true;
            CurrentFile.EditorControl.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                CurrentFile.EditorControl.Markers[i].SetForeColor(IntToColor(BACK_COLOR)); // styles for [+] and [-]
                CurrentFile.EditorControl.Markers[i].SetBackColor(IntToColor(FORE_COLOR)); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            CurrentFile.EditorControl.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            CurrentFile.EditorControl.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            CurrentFile.EditorControl.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            CurrentFile.EditorControl.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            CurrentFile.EditorControl.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            CurrentFile.EditorControl.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            CurrentFile.EditorControl.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            CurrentFile.EditorControl.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

        }

        private void TextArea_MarginClick(object sender, MarginClickEventArgs e)
        {
            if (e.Margin == BOOKMARK_MARGIN)
            {
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = CurrentFile.EditorControl.Lines[CurrentFile.EditorControl.LineFromPosition(e.Position)];
                if ((line.MarkerGet() & mask) > 0)
                {
                    // Remove existing bookmark
                    line.MarkerDelete(BOOKMARK_MARKER);
                }
                else
                {
                    // Add bookmark
                    line.MarkerAdd(BOOKMARK_MARKER);
                }
            }
        }

        #endregion
        #region Drag & Drop File

        public void InitDragDropFile()
        {

            CurrentFile.EditorControl.AllowDrop = true;
            CurrentFile.EditorControl.DragEnter += delegate (object sender, DragEventArgs e) {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            };
            CurrentFile.EditorControl.DragDrop += delegate (object sender, DragEventArgs e) {

                // get file drop
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {

                    Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
                    if (a != null)
                    {

                        string path = a.GetValue(0).ToString();

                        LoadDataFromFile(path);

                    }
                }
            };

        }

        private void LoadDataFromFile(string path)
        {
            if (File.Exists(path))
            {
                //setupCurrentFilename(Path.GetFileName(path));
                //CurrentFile.Text = File.ReadAllText(path);
                AddFile(path);
            }
        }
        #endregion
        private void InitColors()
        {
            CurrentFile.EditorControl.SetSelectionBackColor(true, IntToColor(0x114D9C));
        }
        private void InitHotkeys()
        {
            // remove conflicting hotkeys from scintilla
            CurrentFile.EditorControl.ClearCmdKey(Keys.Control | Keys.F);
            CurrentFile.EditorControl.ClearCmdKey(Keys.Control | Keys.R);
            CurrentFile.EditorControl.ClearCmdKey(Keys.Control | Keys.H);
            CurrentFile.EditorControl.ClearCmdKey(Keys.Control | Keys.L);
            CurrentFile.EditorControl.ClearCmdKey(Keys.Control | Keys.U);

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (Tabs.TabCount > 0)
                    {
                        Tabs.TabPages.Clear();
                    }
                    foreach (ICompiler cmp in Compilers)
                    {
                        if(cmp != null) {

                            cmp.Shutdown();
                        }
                       
                    }
                    if (Files.Count > 0)
                    {
                        Files.Clear();
                        
                    }
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~IDEManager()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion "Control Visualization"

    }
}
