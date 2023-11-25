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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToProjecttoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.NewFiletoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.OpenFiletoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SaveFiletoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SaveAstoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SaveAsProjecttoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ConfigurationtoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.AligntoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.RuntoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.PausetoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.StoptoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.beepTabControl1 = new Beep.IDE.Winform.Controls.BeepTabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CodeStatusStrip = new System.Windows.Forms.StatusStrip();
            this.LinetoolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CurrentLineValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.ColumnPositionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TotalLinestoolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TotalLinesValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.OutputtextBox = new System.Windows.Forms.TextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.ClearOutputtoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.OutputstatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.OutputStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.beepTabControl1.SuspendLayout();
            this.CodeStatusStrip.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.OutputstatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(880, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileToolStripMenuItem,
            this.loadFileToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.SaveToProjecttoolStripMenuItem,
            this.configurationToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Image = global::Beep.IDE.Properties.Resources.FolderClosed;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newFileToolStripMenuItem
            // 
            this.newFileToolStripMenuItem.Image = global::Beep.IDE.Properties.Resources.NewDocument;
            this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            this.newFileToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.newFileToolStripMenuItem.Text = "New";
            // 
            // loadFileToolStripMenuItem
            // 
            this.loadFileToolStripMenuItem.Image = global::Beep.IDE.Properties.Resources.OpenFile;
            this.loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
            this.loadFileToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.loadFileToolStripMenuItem.Text = "Load";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::Beep.IDE.Properties.Resources.SaveAs;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = global::Beep.IDE.Properties.Resources.SaveFileDialog;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            // 
            // SaveToProjecttoolStripMenuItem
            // 
            this.SaveToProjecttoolStripMenuItem.Image = global::Beep.IDE.Properties.Resources.SaveTable;
            this.SaveToProjecttoolStripMenuItem.Name = "SaveToProjecttoolStripMenuItem";
            this.SaveToProjecttoolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.SaveToProjecttoolStripMenuItem.Text = "Save To Project";
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Image = global::Beep.IDE.Properties.Resources.ConfigurationEditor;
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.configurationToolStripMenuItem.Text = "Configuration";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::Beep.IDE.Properties.Resources.Exit;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewFiletoolStripButton,
            this.OpenFiletoolStripButton,
            this.SaveFiletoolStripButton,
            this.SaveAstoolStripButton,
            this.SaveAsProjecttoolStripButton,
            this.ConfigurationtoolStripButton,
            this.toolStripSeparator1,
            this.AligntoolStripButton,
            this.toolStripSeparator2,
            this.RuntoolStripButton,
            this.PausetoolStripButton,
            this.StoptoolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(880, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // NewFiletoolStripButton
            // 
            this.NewFiletoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewFiletoolStripButton.Image = global::Beep.IDE.Properties.Resources.NewDocument;
            this.NewFiletoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewFiletoolStripButton.Name = "NewFiletoolStripButton";
            this.NewFiletoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.NewFiletoolStripButton.Text = "New File";
            // 
            // OpenFiletoolStripButton
            // 
            this.OpenFiletoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenFiletoolStripButton.Image = global::Beep.IDE.Properties.Resources.OpenFile;
            this.OpenFiletoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenFiletoolStripButton.Name = "OpenFiletoolStripButton";
            this.OpenFiletoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.OpenFiletoolStripButton.Text = "Open File";
            // 
            // SaveFiletoolStripButton
            // 
            this.SaveFiletoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveFiletoolStripButton.Image = global::Beep.IDE.Properties.Resources.SaveFileDialog;
            this.SaveFiletoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveFiletoolStripButton.Name = "SaveFiletoolStripButton";
            this.SaveFiletoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.SaveFiletoolStripButton.Text = "Save";
            // 
            // SaveAstoolStripButton
            // 
            this.SaveAstoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveAstoolStripButton.Image = global::Beep.IDE.Properties.Resources.SaveAs;
            this.SaveAstoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveAstoolStripButton.Name = "SaveAstoolStripButton";
            this.SaveAstoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.SaveAstoolStripButton.Text = "Save As";
            // 
            // SaveAsProjecttoolStripButton
            // 
            this.SaveAsProjecttoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveAsProjecttoolStripButton.Image = global::Beep.IDE.Properties.Resources.SaveTable;
            this.SaveAsProjecttoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveAsProjecttoolStripButton.Name = "SaveAsProjecttoolStripButton";
            this.SaveAsProjecttoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.SaveAsProjecttoolStripButton.Text = "Save As in Project";
            // 
            // ConfigurationtoolStripButton
            // 
            this.ConfigurationtoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ConfigurationtoolStripButton.Image = global::Beep.IDE.Properties.Resources.ConfigurationEditor;
            this.ConfigurationtoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ConfigurationtoolStripButton.Name = "ConfigurationtoolStripButton";
            this.ConfigurationtoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.ConfigurationtoolStripButton.Text = "Configuration";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // AligntoolStripButton
            // 
            this.AligntoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AligntoolStripButton.Image = global::Beep.IDE.Properties.Resources.AlignRight;
            this.AligntoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AligntoolStripButton.Name = "AligntoolStripButton";
            this.AligntoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.AligntoolStripButton.Text = "Align Ouput Panel";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // RuntoolStripButton
            // 
            this.RuntoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RuntoolStripButton.Image = global::Beep.IDE.Properties.Resources.Run;
            this.RuntoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RuntoolStripButton.Name = "RuntoolStripButton";
            this.RuntoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.RuntoolStripButton.Text = "Run";
            // 
            // PausetoolStripButton
            // 
            this.PausetoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PausetoolStripButton.Image = global::Beep.IDE.Properties.Resources.Pause;
            this.PausetoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PausetoolStripButton.Name = "PausetoolStripButton";
            this.PausetoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.PausetoolStripButton.Text = "Pause";
            // 
            // StoptoolStripButton
            // 
            this.StoptoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StoptoolStripButton.Image = global::Beep.IDE.Properties.Resources.Stop;
            this.StoptoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StoptoolStripButton.Name = "StoptoolStripButton";
            this.StoptoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.StoptoolStripButton.Text = "Stop";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.OutputtextBox);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer1.Panel2.Controls.Add(this.OutputstatusStrip);
            this.splitContainer1.Size = new System.Drawing.Size(880, 631);
            this.splitContainer1.SplitterDistance = 536;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.beepTabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.CodeStatusStrip);
            this.splitContainer2.Size = new System.Drawing.Size(534, 629);
            this.splitContainer2.SplitterDistance = 600;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 1;
            // 
            // beepTabControl1
            // 
            this.beepTabControl1.Controls.Add(this.tabPage2);
            this.beepTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.beepTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.beepTabControl1.Location = new System.Drawing.Point(0, 0);
            this.beepTabControl1.Name = "beepTabControl1";
            this.beepTabControl1.Padding = new System.Drawing.Point(14, 4);
            this.beepTabControl1.SelectedIndex = 0;
            this.beepTabControl1.Size = new System.Drawing.Size(534, 600);
            this.beepTabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(526, 572);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // CodeStatusStrip
            // 
            this.CodeStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LinetoolStripStatusLabel,
            this.CurrentLineValue,
            this.ColumnPositionLabel,
            this.TotalLinestoolStripStatusLabel,
            this.TotalLinesValue});
            this.CodeStatusStrip.Location = new System.Drawing.Point(0, 4);
            this.CodeStatusStrip.Name = "CodeStatusStrip";
            this.CodeStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.CodeStatusStrip.Size = new System.Drawing.Size(534, 24);
            this.CodeStatusStrip.TabIndex = 0;
            this.CodeStatusStrip.Text = "statusStrip2";
            // 
            // LinetoolStripStatusLabel
            // 
            this.LinetoolStripStatusLabel.BackColor = System.Drawing.Color.White;
            this.LinetoolStripStatusLabel.Name = "LinetoolStripStatusLabel";
            this.LinetoolStripStatusLabel.Size = new System.Drawing.Size(26, 19);
            this.LinetoolStripStatusLabel.Text = "Ln :";
            // 
            // CurrentLineValue
            // 
            this.CurrentLineValue.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.CurrentLineValue.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedInner;
            this.CurrentLineValue.Name = "CurrentLineValue";
            this.CurrentLineValue.Size = new System.Drawing.Size(14, 19);
            this.CurrentLineValue.Text = " ";
            this.CurrentLineValue.ToolTipText = "Line";
            // 
            // ColumnPositionLabel
            // 
            this.ColumnPositionLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.ColumnPositionLabel.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedInner;
            this.ColumnPositionLabel.Name = "ColumnPositionLabel";
            this.ColumnPositionLabel.Size = new System.Drawing.Size(14, 19);
            this.ColumnPositionLabel.Text = " ";
            this.ColumnPositionLabel.ToolTipText = "Column";
            // 
            // TotalLinestoolStripStatusLabel
            // 
            this.TotalLinestoolStripStatusLabel.BackColor = System.Drawing.Color.White;
            this.TotalLinestoolStripStatusLabel.Name = "TotalLinestoolStripStatusLabel";
            this.TotalLinestoolStripStatusLabel.Size = new System.Drawing.Size(35, 19);
            this.TotalLinestoolStripStatusLabel.Text = "Total:";
            // 
            // TotalLinesValue
            // 
            this.TotalLinesValue.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.TotalLinesValue.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedInner;
            this.TotalLinesValue.Name = "TotalLinesValue";
            this.TotalLinesValue.Size = new System.Drawing.Size(14, 19);
            this.TotalLinesValue.Text = " ";
            this.TotalLinesValue.ToolTipText = "Total  Lines";
            // 
            // OutputtextBox
            // 
            this.OutputtextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OutputtextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputtextBox.Location = new System.Drawing.Point(0, 25);
            this.OutputtextBox.Multiline = true;
            this.OutputtextBox.Name = "OutputtextBox";
            this.OutputtextBox.Size = new System.Drawing.Size(339, 582);
            this.OutputtextBox.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearOutputtoolStripButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(339, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // ClearOutputtoolStripButton
            // 
            this.ClearOutputtoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearOutputtoolStripButton.Image = global::Beep.IDE.Properties.Resources.ClearWindowContent;
            this.ClearOutputtoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearOutputtoolStripButton.Name = "ClearOutputtoolStripButton";
            this.ClearOutputtoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.ClearOutputtoolStripButton.Text = "Clear";
            // 
            // OutputstatusStrip
            // 
            this.OutputstatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.OutputStatusLabel});
            this.OutputstatusStrip.Location = new System.Drawing.Point(0, 607);
            this.OutputstatusStrip.Name = "OutputstatusStrip";
            this.OutputstatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.OutputstatusStrip.Size = new System.Drawing.Size(339, 22);
            this.OutputstatusStrip.TabIndex = 1;
            this.OutputstatusStrip.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(86, 16);
            // 
            // OutputStatusLabel
            // 
            this.OutputStatusLabel.Name = "OutputStatusLabel";
            this.OutputStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // uc_BeepIDE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "uc_BeepIDE";
            this.Size = new System.Drawing.Size(880, 680);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.beepTabControl1.ResumeLayout(false);
            this.CodeStatusStrip.ResumeLayout(false);
            this.CodeStatusStrip.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.OutputstatusStrip.ResumeLayout(false);
            this.OutputstatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}
