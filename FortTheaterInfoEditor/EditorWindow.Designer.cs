using System.Windows.Forms;

namespace FortTheaterInfoEditor
{
    partial class EditorWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            loadJsonToolStripMenuItem = new ToolStripMenuItem();
            saveJsonToolStripMenuItem = new ToolStripMenuItem();
            newJsonToolStripMenuItem = new ToolStripMenuItem();
            utilitiesToolStripMenuItem = new ToolStripMenuItem();
            treeToolStripMenuItem = new ToolStripMenuItem();
            expandAllToolStripMenuItem = new ToolStripMenuItem();
            collapseAllToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            darkModeToolStripMenuItem = new ToolStripMenuItem();
            paksFolderToolStripMenuItem = new ToolStripMenuItem();
            uEVersionToolStripMenuItem = new ToolStripMenuItem();
            loadLastLoadedJsonOnLoadToolStripMenuItem = new ToolStripMenuItem();
            aESKeyToolStripMenuItem = new ToolStripMenuItem();
            ContentPanel = new Panel();
            splitContainer2 = new SplitContainer();
            treeView = new TreeView();
            propertyGrid1 = new PropertyGrid();
            logTextBox = new RichTextBox();
            LogPanel = new Panel();
            splitContainer1 = new SplitContainer();
            ImageViewerPanel = new Panel();
            menuStrip1.SuspendLayout();
            ContentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            LogPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, utilitiesToolStripMenuItem, settingsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 2, 0, 2);
            menuStrip1.Size = new Size(1696, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadJsonToolStripMenuItem, saveJsonToolStripMenuItem, newJsonToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // loadJsonToolStripMenuItem
            // 
            loadJsonToolStripMenuItem.Name = "loadJsonToolStripMenuItem";
            loadJsonToolStripMenuItem.Size = new Size(126, 22);
            loadJsonToolStripMenuItem.Text = "Load Json";
            loadJsonToolStripMenuItem.Click += loadJsonToolStripMenuItem_Click;
            // 
            // saveJsonToolStripMenuItem
            // 
            saveJsonToolStripMenuItem.Name = "saveJsonToolStripMenuItem";
            saveJsonToolStripMenuItem.Size = new Size(126, 22);
            saveJsonToolStripMenuItem.Text = "Save Json";
            saveJsonToolStripMenuItem.Click += saveJsonToolStripMenuItem_Click;
            // 
            // newJsonToolStripMenuItem
            // 
            newJsonToolStripMenuItem.Name = "newJsonToolStripMenuItem";
            newJsonToolStripMenuItem.Size = new Size(126, 22);
            newJsonToolStripMenuItem.Text = "New Json";
            // 
            // utilitiesToolStripMenuItem
            // 
            utilitiesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { treeToolStripMenuItem });
            utilitiesToolStripMenuItem.Name = "utilitiesToolStripMenuItem";
            utilitiesToolStripMenuItem.Size = new Size(58, 20);
            utilitiesToolStripMenuItem.Text = "Utilities";
            // 
            // treeToolStripMenuItem
            // 
            treeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { expandAllToolStripMenuItem, collapseAllToolStripMenuItem });
            treeToolStripMenuItem.Name = "treeToolStripMenuItem";
            treeToolStripMenuItem.Size = new Size(95, 22);
            treeToolStripMenuItem.Text = "Tree";
            // 
            // expandAllToolStripMenuItem
            // 
            expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            expandAllToolStripMenuItem.Size = new Size(136, 22);
            expandAllToolStripMenuItem.Text = "Expand All";
            expandAllToolStripMenuItem.Click += expandAllToolStripMenuItem_Click;
            // 
            // collapseAllToolStripMenuItem
            // 
            collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            collapseAllToolStripMenuItem.Size = new Size(136, 22);
            collapseAllToolStripMenuItem.Text = "Collapse All";
            collapseAllToolStripMenuItem.Click += collapseAllToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { darkModeToolStripMenuItem, paksFolderToolStripMenuItem, uEVersionToolStripMenuItem, loadLastLoadedJsonOnLoadToolStripMenuItem, aESKeyToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // darkModeToolStripMenuItem
            // 
            darkModeToolStripMenuItem.CheckOnClick = true;
            darkModeToolStripMenuItem.Name = "darkModeToolStripMenuItem";
            darkModeToolStripMenuItem.Size = new Size(240, 22);
            darkModeToolStripMenuItem.Text = "Dark Mode";
            darkModeToolStripMenuItem.CheckedChanged += darkModeToolStripMenuItem_CheckedChanged;
            // 
            // paksFolderToolStripMenuItem
            // 
            paksFolderToolStripMenuItem.Name = "paksFolderToolStripMenuItem";
            paksFolderToolStripMenuItem.Size = new Size(240, 22);
            paksFolderToolStripMenuItem.Text = "Paks Folder";
            paksFolderToolStripMenuItem.Click += paksFolderToolStripMenuItem_Click;
            // 
            // uEVersionToolStripMenuItem
            // 
            uEVersionToolStripMenuItem.Name = "uEVersionToolStripMenuItem";
            uEVersionToolStripMenuItem.Size = new Size(240, 22);
            uEVersionToolStripMenuItem.Text = "UE Version";
            uEVersionToolStripMenuItem.Click += uEVersionToolStripMenuItem_Click;
            // 
            // loadLastLoadedJsonOnLoadToolStripMenuItem
            // 
            loadLastLoadedJsonOnLoadToolStripMenuItem.CheckOnClick = true;
            loadLastLoadedJsonOnLoadToolStripMenuItem.Name = "loadLastLoadedJsonOnLoadToolStripMenuItem";
            loadLastLoadedJsonOnLoadToolStripMenuItem.Size = new Size(240, 22);
            loadLastLoadedJsonOnLoadToolStripMenuItem.Text = "Load Last Loaded Json On Load";
            loadLastLoadedJsonOnLoadToolStripMenuItem.Click += loadLastLoadedJsonOnLoadToolStripMenuItem_Click;
            // 
            // aESKeyToolStripMenuItem
            // 
            aESKeyToolStripMenuItem.Name = "aESKeyToolStripMenuItem";
            aESKeyToolStripMenuItem.Size = new Size(240, 22);
            aESKeyToolStripMenuItem.Text = "AESKey";
            aESKeyToolStripMenuItem.Click += aESKeyToolStripMenuItem_Click;
            // 
            // ContentPanel
            // 
            ContentPanel.Controls.Add(ImageViewerPanel);
            ContentPanel.Controls.Add(splitContainer2);
            ContentPanel.Dock = DockStyle.Fill;
            ContentPanel.Location = new Point(0, 0);
            ContentPanel.Margin = new Padding(4, 3, 4, 3);
            ContentPanel.Name = "ContentPanel";
            ContentPanel.Size = new Size(1696, 560);
            ContentPanel.TabIndex = 1;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Left;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Margin = new Padding(4, 3, 4, 3);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(treeView);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(propertyGrid1);
            splitContainer2.Size = new Size(1332, 560);
            splitContainer2.SplitterDistance = 627;
            splitContainer2.SplitterWidth = 5;
            splitContainer2.TabIndex = 2;
            // 
            // treeView
            // 
            treeView.Dock = DockStyle.Fill;
            treeView.Location = new Point(0, 0);
            treeView.Margin = new Padding(4, 3, 4, 3);
            treeView.Name = "treeView";
            treeView.Size = new Size(627, 560);
            treeView.TabIndex = 0;
            treeView.AfterSelect += treeView_AfterSelect;
            // 
            // propertyGrid1
            // 
            propertyGrid1.Dock = DockStyle.Fill;
            propertyGrid1.Location = new Point(0, 0);
            propertyGrid1.Margin = new Padding(4, 3, 4, 3);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.Size = new Size(700, 560);
            propertyGrid1.TabIndex = 1;
            // 
            // logTextBox
            // 
            logTextBox.BackColor = SystemColors.AppWorkspace;
            logTextBox.Dock = DockStyle.Fill;
            logTextBox.Font = new Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            logTextBox.Location = new Point(0, 0);
            logTextBox.Margin = new Padding(4, 3, 4, 3);
            logTextBox.Name = "logTextBox";
            logTextBox.ReadOnly = true;
            logTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            logTextBox.Size = new Size(1696, 208);
            logTextBox.TabIndex = 2;
            logTextBox.Text = "";
            // 
            // LogPanel
            // 
            LogPanel.Controls.Add(logTextBox);
            LogPanel.Dock = DockStyle.Fill;
            LogPanel.Location = new Point(0, 0);
            LogPanel.Margin = new Padding(4, 3, 4, 3);
            LogPanel.Name = "LogPanel";
            LogPanel.Size = new Size(1696, 208);
            LogPanel.TabIndex = 4;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Margin = new Padding(4, 3, 4, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(ContentPanel);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(LogPanel);
            splitContainer1.Size = new Size(1696, 773);
            splitContainer1.SplitterDistance = 560;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 1;
            // 
            // ImageViewerPanel
            // 
            ImageViewerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ImageViewerPanel.BackColor = SystemColors.ControlDark;
            ImageViewerPanel.Location = new Point(1394, 154);
            ImageViewerPanel.Name = "ImageViewerPanel";
            ImageViewerPanel.Size = new Size(256, 256);
            ImageViewerPanel.TabIndex = 3;
            // 
            // EditorWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1696, 797);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 3, 4, 3);
            Name = "EditorWindow";
            Text = "Fortnite Theater Info Editor";
            FormClosing += EditorWindow_FormClosing;
            Load += EditorWindow_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ContentPanel.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            LogPanel.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadJsonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveJsonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newJsonToolStripMenuItem;
        private System.Windows.Forms.Panel ContentPanel;
        private System.Windows.Forms.RichTextBox logTextBox;
        private Panel LogPanel;
        private SplitContainer splitContainer1;
        private TreeView treeView;
        private PropertyGrid propertyGrid1;
        private ToolStripMenuItem utilitiesToolStripMenuItem;
        private ToolStripMenuItem treeToolStripMenuItem;
        private ToolStripMenuItem expandAllToolStripMenuItem;
        private ToolStripMenuItem collapseAllToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem darkModeToolStripMenuItem;
        private SplitContainer splitContainer2;
        private ToolStripMenuItem paksFolderToolStripMenuItem;
        private ToolStripMenuItem uEVersionToolStripMenuItem;
        private ToolStripMenuItem loadLastLoadedJsonOnLoadToolStripMenuItem;
        private ToolStripMenuItem aESKeyToolStripMenuItem;
        private Panel ImageViewerPanel;
    }
}

