
namespace Lab05
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.colorSettingsLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textLabel = new System.Windows.Forms.Label();
            this.backgroundLabel = new System.Windows.Forms.Label();
            this.backgroundColorButton = new System.Windows.Forms.Button();
            this.textColorButton = new System.Windows.Forms.Button();
            this.addTextButton = new System.Windows.Forms.Button();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.addTextLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.authorLabel = new System.Windows.Forms.Label();
            this.authorTextBox = new System.Windows.Forms.TextBox();
            this.menuBar = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdNew = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdEnglish = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdTurkish = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer.Panel1.Controls.Add(this.Canvas);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer.Panel2.Controls.Add(this.colorSettingsLabel);
            this.splitContainer.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer.Panel2.Controls.Add(this.addTextButton);
            this.splitContainer.Panel2.Controls.Add(this.titleTextBox);
            this.splitContainer.Panel2.Controls.Add(this.addTextLabel);
            this.splitContainer.Panel2.Controls.Add(this.titleLabel);
            this.splitContainer.Panel2.Controls.Add(this.authorLabel);
            this.splitContainer.Panel2.Controls.Add(this.authorTextBox);
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.Canvas, "Canvas");
            this.Canvas.Name = "Canvas";
            this.Canvas.TabStop = false;
            this.Canvas.SizeChanged += new System.EventHandler(this.Canvas_SizeChanged);
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.Canvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Canvas_Click);
            this.Canvas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Canvas_DoubleClick);
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.Canvas.MouseEnter += new System.EventHandler(this.Canvas_MouseEnter);
            this.Canvas.MouseLeave += new System.EventHandler(this.Canvas_MouseLeave);
            this.Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            // 
            // colorSettingsLabel
            // 
            resources.ApplyResources(this.colorSettingsLabel, "colorSettingsLabel");
            this.colorSettingsLabel.Name = "colorSettingsLabel";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.textLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.backgroundLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.backgroundColorButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textColorButton, 1, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // textLabel
            // 
            resources.ApplyResources(this.textLabel, "textLabel");
            this.textLabel.Name = "textLabel";
            // 
            // backgroundLabel
            // 
            resources.ApplyResources(this.backgroundLabel, "backgroundLabel");
            this.backgroundLabel.Name = "backgroundLabel";
            // 
            // backgroundColorButton
            // 
            resources.ApplyResources(this.backgroundColorButton, "backgroundColorButton");
            this.backgroundColorButton.Name = "backgroundColorButton";
            this.backgroundColorButton.Tag = "background";
            this.backgroundColorButton.UseVisualStyleBackColor = true;
            this.backgroundColorButton.Click += new System.EventHandler(this.ColorsChanged);
            // 
            // textColorButton
            // 
            resources.ApplyResources(this.textColorButton, "textColorButton");
            this.textColorButton.Name = "textColorButton";
            this.textColorButton.Tag = "text";
            this.textColorButton.UseVisualStyleBackColor = true;
            this.textColorButton.Click += new System.EventHandler(this.ColorsChanged);
            // 
            // addTextButton
            // 
            resources.ApplyResources(this.addTextButton, "addTextButton");
            this.addTextButton.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.addTextButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.addTextButton.Name = "addTextButton";
            this.addTextButton.UseVisualStyleBackColor = false;
            this.addTextButton.Click += new System.EventHandler(this.AddTextButton_Click);
            // 
            // titleTextBox
            // 
            resources.ApplyResources(this.titleTextBox, "titleTextBox");
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Tag = "title";
            this.titleTextBox.TextChanged += new System.EventHandler(this.CoverTextChanged);
            // 
            // addTextLabel
            // 
            resources.ApplyResources(this.addTextLabel, "addTextLabel");
            this.addTextLabel.Name = "addTextLabel";
            // 
            // titleLabel
            // 
            resources.ApplyResources(this.titleLabel, "titleLabel");
            this.titleLabel.Name = "titleLabel";
            // 
            // authorLabel
            // 
            resources.ApplyResources(this.authorLabel, "authorLabel");
            this.authorLabel.Name = "authorLabel";
            // 
            // authorTextBox
            // 
            this.authorTextBox.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            resources.ApplyResources(this.authorTextBox, "authorTextBox");
            this.authorTextBox.Name = "authorTextBox";
            this.authorTextBox.Tag = "author";
            this.authorTextBox.TextChanged += new System.EventHandler(this.CoverTextChanged);
            // 
            // menuBar
            // 
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuSettings});
            resources.ApplyResources(this.menuBar, "menuBar");
            this.menuBar.Name = "menuBar";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdNew,
            this.cmdOpen,
            this.cmdSave});
            this.menuFile.Name = "menuFile";
            resources.ApplyResources(this.menuFile, "menuFile");
            // 
            // cmdNew
            // 
            this.cmdNew.Name = "cmdNew";
            resources.ApplyResources(this.cmdNew, "cmdNew");
            this.cmdNew.Click += new System.EventHandler(this.CmdNew_Click);
            // 
            // cmdOpen
            // 
            this.cmdOpen.Name = "cmdOpen";
            resources.ApplyResources(this.cmdOpen, "cmdOpen");
            this.cmdOpen.Click += new System.EventHandler(this.CmdOpen_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Name = "cmdSave";
            resources.ApplyResources(this.cmdSave, "cmdSave");
            this.cmdSave.Click += new System.EventHandler(this.CmdSave_Click);
            // 
            // menuSettings
            // 
            this.menuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLanguage});
            this.menuSettings.Name = "menuSettings";
            resources.ApplyResources(this.menuSettings, "menuSettings");
            // 
            // menuLanguage
            // 
            this.menuLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdEnglish,
            this.cmdTurkish});
            this.menuLanguage.Name = "menuLanguage";
            resources.ApplyResources(this.menuLanguage, "menuLanguage");
            // 
            // cmdEnglish
            // 
            this.cmdEnglish.Checked = true;
            this.cmdEnglish.CheckOnClick = true;
            this.cmdEnglish.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmdEnglish.Name = "cmdEnglish";
            resources.ApplyResources(this.cmdEnglish, "cmdEnglish");
            this.cmdEnglish.Click += new System.EventHandler(this.CmdEnglish_Click);
            // 
            // cmdTurkish
            // 
            this.cmdTurkish.CheckOnClick = true;
            this.cmdTurkish.Name = "cmdTurkish";
            resources.ApplyResources(this.cmdTurkish, "cmdTurkish");
            this.cmdTurkish.Click += new System.EventHandler(this.CmdTurkish_Click);
            // 
            // MainWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuBar);
            this.KeyPreview = true;
            this.Name = "MainWindow";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.PictureBox Canvas;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Label addTextLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label authorLabel;
        private System.Windows.Forms.TextBox authorTextBox;
        private System.Windows.Forms.Button addTextButton;
        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem cmdNew;
        private System.Windows.Forms.ToolStripMenuItem cmdOpen;
        private System.Windows.Forms.ToolStripMenuItem cmdSave;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem menuLanguage;
        private System.Windows.Forms.ToolStripMenuItem cmdEnglish;
        private System.Windows.Forms.ToolStripMenuItem cmdTurkish;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label backgroundLabel;
        private System.Windows.Forms.Label textLabel;
        private System.Windows.Forms.Button backgroundColorButton;
        private System.Windows.Forms.Button textColorButton;
        private System.Windows.Forms.Label colorSettingsLabel;
    }
}

