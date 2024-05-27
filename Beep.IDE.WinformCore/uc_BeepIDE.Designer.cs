using Beep.IDE.Winform.Controls;
using System.Windows.Forms;

namespace Beep.IDE
{
    partial class uc_BeepIDE
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_BeepIDE));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newFileToolStripMenuItem = new ToolStripMenuItem();
            loadFileToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            SaveToProjecttoolStripMenuItem = new ToolStripMenuItem();
            configurationToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            NewFiletoolStripButton = new ToolStripButton();
            OpenFiletoolStripButton = new ToolStripButton();
            SaveFiletoolStripButton = new ToolStripButton();
            SaveAstoolStripButton = new ToolStripButton();
            SaveAsProjecttoolStripButton = new ToolStripButton();
            ConfigurationtoolStripButton = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            AligntoolStripButton = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            RuntoolStripButton = new ToolStripButton();
            PausetoolStripButton = new ToolStripButton();
            StoptoolStripButton = new ToolStripButton();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            beepTabControl1 = new BeepTabControl();
            tabPage2 = new TabPage();
            CodeStatusStrip = new StatusStrip();
            LinetoolStripStatusLabel = new ToolStripStatusLabel();
            CurrentLineValue = new ToolStripStatusLabel();
            ColumnPositionLabel = new ToolStripStatusLabel();
            TotalLinestoolStripStatusLabel = new ToolStripStatusLabel();
            TotalLinesValue = new ToolStripStatusLabel();
            progressBar1 = new ToolStripProgressBar();
            OutputtextBox = new TextBox();
            toolStrip2 = new ToolStrip();
            ClearOutputtoolStripButton = new ToolStripButton();
            OutputstatusStrip = new StatusStrip();
            toolStripProgressBar1 = new ToolStripProgressBar();
            OutputStatusLabel = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            beepTabControl1.SuspendLayout();
            CodeStatusStrip.SuspendLayout();
            toolStrip2.SuspendLayout();
            OutputstatusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1027, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newFileToolStripMenuItem, loadFileToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, SaveToProjecttoolStripMenuItem, configurationToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Image = Properties.Resources.FolderClosed;
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(53, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newFileToolStripMenuItem
            // 
            newFileToolStripMenuItem.Image = Properties.Resources.NewDocument;
            newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            newFileToolStripMenuItem.Size = new Size(153, 22);
            newFileToolStripMenuItem.Text = "New";
            // 
            // loadFileToolStripMenuItem
            // 
            loadFileToolStripMenuItem.Image = Properties.Resources.OpenFile;
            loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
            loadFileToolStripMenuItem.Size = new Size(153, 22);
            loadFileToolStripMenuItem.Text = "Load";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Image = Properties.Resources.SaveAs;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(153, 22);
            saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Image = Properties.Resources.SaveFileDialog;
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(153, 22);
            saveAsToolStripMenuItem.Text = "Save As";
            // 
            // SaveToProjecttoolStripMenuItem
            // 
            SaveToProjecttoolStripMenuItem.Image = Properties.Resources.SaveTable;
            SaveToProjecttoolStripMenuItem.Name = "SaveToProjecttoolStripMenuItem";
            SaveToProjecttoolStripMenuItem.Size = new Size(153, 22);
            SaveToProjecttoolStripMenuItem.Text = "Save To Project";
            // 
            // configurationToolStripMenuItem
            // 
            configurationToolStripMenuItem.Image = Properties.Resources.ConfigurationEditor;
            configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            configurationToolStripMenuItem.Size = new Size(153, 22);
            configurationToolStripMenuItem.Text = "Configuration";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Image = Properties.Resources.Exit;
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(153, 22);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { NewFiletoolStripButton, OpenFiletoolStripButton, SaveFiletoolStripButton, SaveAstoolStripButton, SaveAsProjecttoolStripButton, ConfigurationtoolStripButton, toolStripSeparator1, AligntoolStripButton, toolStripSeparator2, RuntoolStripButton, PausetoolStripButton, StoptoolStripButton });
            toolStrip1.Location = new Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1027, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // NewFiletoolStripButton
            // 
            NewFiletoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            NewFiletoolStripButton.Image = Properties.Resources.NewDocument;
            NewFiletoolStripButton.ImageTransparentColor = Color.Magenta;
            NewFiletoolStripButton.Name = "NewFiletoolStripButton";
            NewFiletoolStripButton.Size = new Size(23, 22);
            NewFiletoolStripButton.Text = "New File";
            // 
            // OpenFiletoolStripButton
            // 
            OpenFiletoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            OpenFiletoolStripButton.Image = Properties.Resources.OpenFile;
            OpenFiletoolStripButton.ImageTransparentColor = Color.Magenta;
            OpenFiletoolStripButton.Name = "OpenFiletoolStripButton";
            OpenFiletoolStripButton.Size = new Size(23, 22);
            OpenFiletoolStripButton.Text = "Open File";
            // 
            // SaveFiletoolStripButton
            // 
            SaveFiletoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            SaveFiletoolStripButton.Image = Properties.Resources.SaveFileDialog;
            SaveFiletoolStripButton.ImageTransparentColor = Color.Magenta;
            SaveFiletoolStripButton.Name = "SaveFiletoolStripButton";
            SaveFiletoolStripButton.Size = new Size(23, 22);
            SaveFiletoolStripButton.Text = "Save";
            // 
            // SaveAstoolStripButton
            // 
            SaveAstoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            SaveAstoolStripButton.Image = Properties.Resources.SaveAs;
            SaveAstoolStripButton.ImageTransparentColor = Color.Magenta;
            SaveAstoolStripButton.Name = "SaveAstoolStripButton";
            SaveAstoolStripButton.Size = new Size(23, 22);
            SaveAstoolStripButton.Text = "Save As";
            // 
            // SaveAsProjecttoolStripButton
            // 
            SaveAsProjecttoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            SaveAsProjecttoolStripButton.Image = Properties.Resources.SaveTable;
            SaveAsProjecttoolStripButton.ImageTransparentColor = Color.Magenta;
            SaveAsProjecttoolStripButton.Name = "SaveAsProjecttoolStripButton";
            SaveAsProjecttoolStripButton.Size = new Size(23, 22);
            SaveAsProjecttoolStripButton.Text = "Save As in Project";
            // 
            // ConfigurationtoolStripButton
            // 
            ConfigurationtoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ConfigurationtoolStripButton.Image = Properties.Resources.ConfigurationEditor;
            ConfigurationtoolStripButton.ImageTransparentColor = Color.Magenta;
            ConfigurationtoolStripButton.Name = "ConfigurationtoolStripButton";
            ConfigurationtoolStripButton.Size = new Size(23, 22);
            ConfigurationtoolStripButton.Text = "Configuration";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // AligntoolStripButton
            // 
            AligntoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            AligntoolStripButton.Image = Properties.Resources.AlignRight;
            AligntoolStripButton.ImageTransparentColor = Color.Magenta;
            AligntoolStripButton.Name = "AligntoolStripButton";
            AligntoolStripButton.Size = new Size(23, 22);
            AligntoolStripButton.Text = "Align Ouput Panel";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // RuntoolStripButton
            // 
            RuntoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            RuntoolStripButton.Image = Properties.Resources.Run;
            RuntoolStripButton.ImageTransparentColor = Color.Magenta;
            RuntoolStripButton.Name = "RuntoolStripButton";
            RuntoolStripButton.Size = new Size(23, 22);
            RuntoolStripButton.Text = "Run";
            // 
            // PausetoolStripButton
            // 
            PausetoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            PausetoolStripButton.Image = Properties.Resources.Pause;
            PausetoolStripButton.ImageTransparentColor = Color.Magenta;
            PausetoolStripButton.Name = "PausetoolStripButton";
            PausetoolStripButton.Size = new Size(23, 22);
            PausetoolStripButton.Text = "Pause";
            // 
            // StoptoolStripButton
            // 
            StoptoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            StoptoolStripButton.Image = Properties.Resources.Stop;
            StoptoolStripButton.ImageTransparentColor = Color.Magenta;
            StoptoolStripButton.Name = "StoptoolStripButton";
            StoptoolStripButton.Size = new Size(23, 22);
            StoptoolStripButton.Text = "Stop";
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 49);
            splitContainer1.Margin = new Padding(4, 3, 4, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(OutputtextBox);
            splitContainer1.Panel2.Controls.Add(toolStrip2);
            splitContainer1.Panel2.Controls.Add(OutputstatusStrip);
            splitContainer1.Size = new Size(1027, 736);
            splitContainer1.SplitterDistance = 625;
            splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Margin = new Padding(4, 3, 4, 3);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(beepTabControl1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(CodeStatusStrip);
            splitContainer2.Size = new Size(623, 734);
            splitContainer2.SplitterDistance = 700;
            splitContainer2.SplitterWidth = 1;
            splitContainer2.TabIndex = 1;
            // 
            // beepTabControl1
            // 
            beepTabControl1.closeButton = (RectangleF)resources.GetObject("beepTabControl1.closeButton");
            beepTabControl1.Controls.Add(tabPage2);
            beepTabControl1.Dock = DockStyle.Fill;
            beepTabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            beepTabControl1.Location = new Point(0, 0);
            beepTabControl1.Margin = new Padding(4, 3, 4, 3);
            beepTabControl1.Name = "beepTabControl1";
            beepTabControl1.nextButton = (RectangleF)resources.GetObject("beepTabControl1.nextButton");
            beepTabControl1.Padding = new Point(14, 4);
            beepTabControl1.prevButton = (RectangleF)resources.GetObject("beepTabControl1.prevButton");
            beepTabControl1.SelectedIndex = 0;
            beepTabControl1.Size = new Size(623, 700);
            beepTabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 26);
            tabPage2.Margin = new Padding(4, 3, 4, 3);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(4, 3, 4, 3);
            tabPage2.Size = new Size(615, 670);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // CodeStatusStrip
            // 
            CodeStatusStrip.Dock = DockStyle.Fill;
            CodeStatusStrip.Items.AddRange(new ToolStripItem[] { LinetoolStripStatusLabel, CurrentLineValue, ColumnPositionLabel, TotalLinestoolStripStatusLabel, TotalLinesValue, progressBar1 });
            CodeStatusStrip.Location = new Point(0, 0);
            CodeStatusStrip.Name = "CodeStatusStrip";
            CodeStatusStrip.Size = new Size(623, 33);
            CodeStatusStrip.TabIndex = 0;
            CodeStatusStrip.Text = "statusStrip2";
            // 
            // LinetoolStripStatusLabel
            // 
            LinetoolStripStatusLabel.BackColor = Color.White;
            LinetoolStripStatusLabel.Name = "LinetoolStripStatusLabel";
            LinetoolStripStatusLabel.Size = new Size(26, 28);
            LinetoolStripStatusLabel.Text = "Ln :";
            // 
            // CurrentLineValue
            // 
            CurrentLineValue.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            CurrentLineValue.BorderStyle = Border3DStyle.RaisedInner;
            CurrentLineValue.Name = "CurrentLineValue";
            CurrentLineValue.Size = new Size(14, 28);
            CurrentLineValue.Text = " ";
            CurrentLineValue.ToolTipText = "Line";
            // 
            // ColumnPositionLabel
            // 
            ColumnPositionLabel.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            ColumnPositionLabel.BorderStyle = Border3DStyle.RaisedInner;
            ColumnPositionLabel.Name = "ColumnPositionLabel";
            ColumnPositionLabel.Size = new Size(14, 28);
            ColumnPositionLabel.Text = " ";
            ColumnPositionLabel.ToolTipText = "Column";
            // 
            // TotalLinestoolStripStatusLabel
            // 
            TotalLinestoolStripStatusLabel.BackColor = Color.White;
            TotalLinestoolStripStatusLabel.Name = "TotalLinestoolStripStatusLabel";
            TotalLinestoolStripStatusLabel.Size = new Size(35, 28);
            TotalLinestoolStripStatusLabel.Text = "Total:";
            // 
            // TotalLinesValue
            // 
            TotalLinesValue.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            TotalLinesValue.BorderStyle = Border3DStyle.RaisedInner;
            TotalLinesValue.Name = "TotalLinesValue";
            TotalLinesValue.Size = new Size(14, 28);
            TotalLinesValue.Text = " ";
            TotalLinesValue.ToolTipText = "Total  Lines";
            // 
            // progressBar1
            // 
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(100, 27);
            progressBar1.Style = ProgressBarStyle.Marquee;
            // 
            // OutputtextBox
            // 
            OutputtextBox.BorderStyle = BorderStyle.FixedSingle;
            OutputtextBox.Dock = DockStyle.Fill;
            OutputtextBox.Location = new Point(0, 25);
            OutputtextBox.Margin = new Padding(4, 3, 4, 3);
            OutputtextBox.Multiline = true;
            OutputtextBox.Name = "OutputtextBox";
            OutputtextBox.Size = new Size(396, 685);
            OutputtextBox.TabIndex = 0;
            // 
            // toolStrip2
            // 
            toolStrip2.Items.AddRange(new ToolStripItem[] { ClearOutputtoolStripButton });
            toolStrip2.Location = new Point(0, 0);
            toolStrip2.Name = "toolStrip2";
            toolStrip2.Size = new Size(396, 25);
            toolStrip2.TabIndex = 2;
            toolStrip2.Text = "toolStrip2";
            // 
            // ClearOutputtoolStripButton
            // 
            ClearOutputtoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ClearOutputtoolStripButton.Image = Properties.Resources.ClearWindowContent;
            ClearOutputtoolStripButton.ImageTransparentColor = Color.Magenta;
            ClearOutputtoolStripButton.Name = "ClearOutputtoolStripButton";
            ClearOutputtoolStripButton.Size = new Size(23, 22);
            ClearOutputtoolStripButton.Text = "Clear";
            // 
            // OutputstatusStrip
            // 
            OutputstatusStrip.Items.AddRange(new ToolStripItem[] { toolStripProgressBar1, OutputStatusLabel });
            OutputstatusStrip.Location = new Point(0, 710);
            OutputstatusStrip.Name = "OutputstatusStrip";
            OutputstatusStrip.Size = new Size(396, 24);
            OutputstatusStrip.TabIndex = 1;
            OutputstatusStrip.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new Size(100, 18);
            // 
            // OutputStatusLabel
            // 
            OutputStatusLabel.Name = "OutputStatusLabel";
            OutputStatusLabel.Size = new Size(0, 19);
            // 
            // uc_BeepIDE
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "uc_BeepIDE";
            Size = new Size(1027, 785);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            beepTabControl1.ResumeLayout(false);
            CodeStatusStrip.ResumeLayout(false);
            CodeStatusStrip.PerformLayout();
            toolStrip2.ResumeLayout(false);
            toolStrip2.PerformLayout();
            OutputstatusStrip.ResumeLayout(false);
            OutputstatusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStrip toolStrip1;
        private SplitContainer splitContainer1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem loadFileToolStripMenuItem;
        private ToolStripMenuItem newFileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem configurationToolStripMenuItem;
        private ToolStripButton ConfigurationtoolStripButton;
        private StatusStrip OutputstatusStrip;
        private TextBox OutputtextBox;
        private SplitContainer splitContainer2;
        private StatusStrip CodeStatusStrip;
        private ToolStripButton NewFiletoolStripButton;
        private ToolStripButton OpenFiletoolStripButton;
        private ToolStripButton SaveFiletoolStripButton;
        private ToolStripButton SaveAstoolStripButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton AligntoolStripButton;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton RuntoolStripButton;
        private ToolStripButton PausetoolStripButton;
        private ToolStripButton StoptoolStripButton;
        private ToolStrip toolStrip2;
        private ToolStripButton ClearOutputtoolStripButton;
        private ToolStripProgressBar toolStripProgressBar1;
        private ToolStripStatusLabel OutputStatusLabel;
        private ToolStripStatusLabel LinetoolStripStatusLabel;
        private ToolStripStatusLabel TotalLinestoolStripStatusLabel;
        private ToolStripStatusLabel CurrentLineValue;
        private ToolStripStatusLabel TotalLinesValue;
        private BeepTabControl beepTabControl1;
        private TabPage tabPage2;
        private ToolStripStatusLabel ColumnPositionLabel;
        private ToolStripButton SaveAsProjecttoolStripButton;
        private ToolStripMenuItem SaveToProjecttoolStripMenuItem;
        private ToolStripProgressBar progressBar1;
    }
}
