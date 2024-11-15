using Beep.Python.Winform;
using Python.Python.Winform;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Beep.IDE
{
    public class IDEManager
    {
        public MenuManager menuManager { get; set; }
        public IDEManager(Control parent, TabControl tab, MenuStrip menu)
        {
            Tabs = tab;
            Menustrip = menu;
            Parent = parent;
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
        public string searchtext { get; set; }
        public IDEManager()
        {
            Init();
        }
        public TabControl Tabs { get; set; } = new TabControl();
        public MenuStrip Menustrip { get; }
        public Control Parent { get; }
        public List<Scintilla> Files { get; set; } = new List<Scintilla>();
        public int CurrentFileIndex { get; set; } = -1;
        public Scintilla CurrentFile
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
                    return Files[CurrentFileIndex].Text;
                }
                else
                    return null;
            }
        }
        public SearchManager SearchManager { get; set; }
        public void Init()
        {
            Tabs.MouseClick += Tabs_MouseClick;
            Tabs.SelectedIndexChanged += Tabs_SelectedIndexChanged;
            Tabs.Selected += Tabs_Selected;
            Tabs.DragOver += Tabs_DragOver;
            Tabs.DragDrop += Tabs_DragDrop;
            menuManager = new MenuManager(this);
            menuManager.CreateMenu(Menustrip);

        }
        #region "Tab Events"
        private void Tabs_DragDrop(object? sender, DragEventArgs e)
        {
            InitDragDropFile();
        }

        private void Tabs_DragOver(object? sender, DragEventArgs e)
        {

        }

        private void Tabs_Selected(object? sender, TabControlEventArgs e)
        {

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
        public void AddFile(string file, Lexer lexer)
        {
            Scintilla ctl = new Scintilla();
            string fileame = System.IO.Path.GetFileName(file);
            string filext = System.IO.Path.GetExtension(file);
            string[] lexs = Enum.GetNames(typeof(Lexer));
            // ctl.LexerName= lexer;
            TabPage page = new TabPage();
            Tabs.TabPages.Add(fileame, fileame);
            int idx = Tabs.TabPages.IndexOf(page);
            page = Tabs.TabPages[idx];
            page.Text = fileame;
            page.Controls.Add(ctl);
            ctl.Name = fileame;
            ctl.Dock = System.Windows.Forms.DockStyle.Fill;

            if (Files.Contains(ctl))
            {
                Files.Remove(ctl);
            }
            Files.Add(ctl);
            CurrentFileIndex = Files.Count - 1;
            InitHotkeys();
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

        }
        public void AddFile(string file)
        {
            Scintilla ctl = new Scintilla();
            string fileame = System.IO.Path.GetFileName(file);
            string filext = System.IO.Path.GetExtension(file);
            string[] lexs = Enum.GetNames(typeof(Lexer));
            // ctl.LexerName= lexer;
            TabPage page = new TabPage();
            Tabs.TabPages.Add(fileame, fileame);
            int idx = Tabs.TabPages.IndexOf(page);
            page = Tabs.TabPages[idx];
            page.Text = fileame;
            page.Controls.Add(ctl);
            ctl.Name = fileame;
            ctl.Dock = System.Windows.Forms.DockStyle.Fill;

            if (Files.Contains(ctl))
            {
                Files.Remove(ctl);
            }
            Files.Add(ctl);
            CurrentFileIndex = Files.Count - 1;
            InitHotkeys();
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

        }
        public void RemoveFile(string file)
        {
            string fileame = System.IO.Path.GetFileName(file);

            int pageidx = Tabs.TabPages.IndexOfKey(fileame);
            int ctlidx = Files.FindIndex(p => p.Text == fileame);
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
            int ctlidx = Files.FindIndex(p => p.Text == file);
            CurrentFileIndex = ctlidx;
        }
        public void SetCurrentFile(int index)
        {

            CurrentFileIndex = index;
        }
        public void SetCurrentFileToPrevious()
        {
            if (CurrentFileIndex > 0)
            {
                CurrentFileIndex--;
            }
        }
        public void SetCurrentFileToNext()
        {
            if (CurrentFileIndex < Files.Count - 1)
            {
                CurrentFileIndex++;
            }
        }
        public void SetCurrentFileToFirst()
        {
            CurrentFileIndex = 0;
        }
        public void SetCurrentFileToLast()
        {
            CurrentFileIndex = Files.Count - 1;
        }
        public void SetCurrentFileToNone()
        {
            CurrentFileIndex = -1;
        }
        public void SetCurrentFileToNew()
        {
            CurrentFileIndex = Files.Count;
        }
        #endregion "File Operations"
        #region Uppercase / Lowercase

        public void Lowercase()
        {

            // save the selection
            int start = CurrentFile.SelectionStart;
            int end = CurrentFile.SelectionEnd;

            // modify the selected text
            CurrentFile.ReplaceSelection(CurrentFile.GetTextRange(start, end - start).ToLower());

            // preserve the original selection
            CurrentFile.SetSelection(start, end);
        }

        public void Uppercase()
        {

            // save the selection
            int start = CurrentFile.SelectionStart;
            int end = CurrentFile.SelectionEnd;

            // modify the selected text
            CurrentFile.ReplaceSelection(CurrentFile.GetTextRange(start, end - start).ToUpper());

            // preserve the original selection
            CurrentFile.SetSelection(start, end);
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
            CurrentFile.Focus();
            SendKeys.Send(keys);
            HotKeyManager.Enable = true;
        }

        #endregion
        #region Zoom

        public void ZoomIn()
        {
            CurrentFile.ZoomIn();
        }

        public void ZoomOut()
        {
            CurrentFile.ZoomOut();
        }

        public void ZoomDefault()
        {
            CurrentFile.Zoom = 0;
        }


        #endregion
        #region Quick Search Bar

        bool SearchIsOpen = false;

        public void OpenSearch()
        {


            SearchManager.Find(CurrentFile, searchtext, false, true);

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
            SearchManager.Find(CurrentFile, searchtext, false, false);
        }
        public void BtnNextSearch_Click(object sender, EventArgs e)
        {
            SearchManager.Find(CurrentFile, searchtext, true, false);
        }
        public void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchManager.Find(CurrentFile, searchtext, true, true);
        }

        public void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (HotKeyManager.IsHotkey(e, Keys.Enter))
            {
                SearchManager.Find(CurrentFile, searchtext, true, false);
            }
            if (HotKeyManager.IsHotkey(e, Keys.Enter, true) || HotKeyManager.IsHotkey(e, Keys.Enter, false, true))
            {
                SearchManager.Find(CurrentFile, searchtext, false, false);
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
            if (CurrentFile.InvokeRequired)
            {
                CurrentFile.BeginInvoke(action);
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
            CurrentFile.Dock = System.Windows.Forms.DockStyle.Fill;
            CurrentFile.TextChanged += (this.OnTextChanged);

            // INITIAL VIEW CONFIG
            CurrentFile.WrapMode = WrapMode.None;
            CurrentFile.IndentationGuides = IndentView.LookBoth;

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
            CurrentFile.StyleResetDefault();
            CurrentFile.Styles[Style.Default].Font = "Consolas";
            CurrentFile.Styles[Style.Default].Size = 10;
            CurrentFile.Styles[Style.Default].BackColor = IntToColor(0x212121);
            CurrentFile.Styles[Style.Default].ForeColor = IntToColor(0xFFFFFF);
            CurrentFile.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            CurrentFile.Styles[Style.Cpp.Identifier].ForeColor = IntToColor(0xD0DAE2);
            CurrentFile.Styles[Style.Cpp.Comment].ForeColor = IntToColor(0xBD758B);
            CurrentFile.Styles[Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
            CurrentFile.Styles[Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            CurrentFile.Styles[Style.Cpp.Number].ForeColor = IntToColor(0xFFFF00);
            CurrentFile.Styles[Style.Cpp.String].ForeColor = IntToColor(0xFFFF00);
            CurrentFile.Styles[Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
            CurrentFile.Styles[Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            CurrentFile.Styles[Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
            CurrentFile.Styles[Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            CurrentFile.Styles[Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            CurrentFile.Styles[Style.Cpp.Word].ForeColor = IntToColor(0x48A8EE);
            CurrentFile.Styles[Style.Cpp.Word2].ForeColor = IntToColor(0xF98906);
            CurrentFile.Styles[Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            CurrentFile.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            CurrentFile.Styles[Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);

          //  CurrentFile.LexerLanguage = Lexer.Python;

            CurrentFile.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            CurrentFile.SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");

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

            CurrentFile.Styles[Style.LineNumber].BackColor = IntToColor(BACK_COLOR);
            CurrentFile.Styles[Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
            CurrentFile.Styles[Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
            CurrentFile.Styles[Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);

            var nums = CurrentFile.Margins[NUMBER_MARGIN];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;

            CurrentFile.MarginClick += TextArea_MarginClick;
        }

        private void InitBookmarkMargin()
        {

            //CurrentFile.SetFoldMarginColor(true, IntToColor(BACK_COLOR));

            var margin = CurrentFile.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER);
            //margin.Cursor = MarginCursor.Arrow;

            var marker = CurrentFile.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(IntToColor(0xFF003B));
            marker.SetForeColor(IntToColor(0x000000));
            marker.SetAlpha(100);

        }

        private void InitCodeFolding()
        {

            CurrentFile.SetFoldMarginColor(true, IntToColor(BACK_COLOR));
            CurrentFile.SetFoldMarginHighlightColor(true, IntToColor(BACK_COLOR));

            // Enable code folding
            CurrentFile.SetProperty("fold", "1");
            CurrentFile.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            CurrentFile.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            CurrentFile.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            CurrentFile.Margins[FOLDING_MARGIN].Sensitive = true;
            CurrentFile.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                CurrentFile.Markers[i].SetForeColor(IntToColor(BACK_COLOR)); // styles for [+] and [-]
                CurrentFile.Markers[i].SetBackColor(IntToColor(FORE_COLOR)); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            CurrentFile.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            CurrentFile.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            CurrentFile.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            CurrentFile.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            CurrentFile.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            CurrentFile.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            CurrentFile.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            CurrentFile.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

        }

        private void TextArea_MarginClick(object sender, MarginClickEventArgs e)
        {
            if (e.Margin == BOOKMARK_MARGIN)
            {
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = CurrentFile.Lines[CurrentFile.LineFromPosition(e.Position)];
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

            CurrentFile.AllowDrop = true;
            CurrentFile.DragEnter += delegate (object sender, DragEventArgs e)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            };
            CurrentFile.DragDrop += delegate (object sender, DragEventArgs e)
            {

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

            CurrentFile.SetSelectionBackColor(true, IntToColor(0x114D9C));

        }
        private void InitHotkeys()
        {


            // remove conflicting hotkeys from scintilla
            CurrentFile.ClearCmdKey(Keys.Control | Keys.F);
            CurrentFile.ClearCmdKey(Keys.Control | Keys.R);
            CurrentFile.ClearCmdKey(Keys.Control | Keys.H);
            CurrentFile.ClearCmdKey(Keys.Control | Keys.L);
            CurrentFile.ClearCmdKey(Keys.Control | Keys.U);

        }
    }
}
