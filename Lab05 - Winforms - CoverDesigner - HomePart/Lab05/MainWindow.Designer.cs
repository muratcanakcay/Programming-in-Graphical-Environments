
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.addButton = new System.Windows.Forms.Button();
            this.addTextBox = new System.Windows.Forms.TextBox();
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
            this.cmdPolish = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.menuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BackColor = System.Drawing.Color.LightSteelBlue;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
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
            this.splitContainer.Panel2.Controls.Add(this.addButton);
            this.splitContainer.Panel2.Controls.Add(this.addTextBox);
            this.splitContainer.Panel2.Controls.Add(this.titleTextBox);
            this.splitContainer.Panel2.Controls.Add(this.addTextLabel);
            this.splitContainer.Panel2.Controls.Add(this.titleLabel);
            this.splitContainer.Panel2.Controls.Add(this.authorLabel);
            this.splitContainer.Panel2.Controls.Add(this.authorTextBox);
            this.splitContainer.Panel2MinSize = 200;
            this.splitContainer.Size = new System.Drawing.Size(1384, 837);
            this.splitContainer.SplitterDistance = 1170;
            this.splitContainer.TabIndex = 0;
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.Color.White;
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(1170, 837);
            this.Canvas.TabIndex = 0;
            this.Canvas.TabStop = false;
            this.Canvas.SizeChanged += new System.EventHandler(this.Canvas_SizeChanged);
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.Canvas.MouseEnter += new System.EventHandler(this.Canvas_MouseEnter);
            this.Canvas.MouseLeave += new System.EventHandler(this.Canvas_MouseLeave);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(3, 378);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(200, 23);
            this.addButton.TabIndex = 6;
            this.addButton.Text = "Add Text";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // addTextBox
            // 
            this.addTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addTextBox.Location = new System.Drawing.Point(3, 196);
            this.addTextBox.MinimumSize = new System.Drawing.Size(200, 4);
            this.addTextBox.Multiline = true;
            this.addTextBox.Name = "addTextBox";
            this.addTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.addTextBox.Size = new System.Drawing.Size(200, 175);
            this.addTextBox.TabIndex = 5;
            this.addTextBox.TextChanged += new System.EventHandler(this.addTextBox_TextChanged);
            // 
            // titleTextBox
            // 
            this.titleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleTextBox.Location = new System.Drawing.Point(3, 31);
            this.titleTextBox.MinimumSize = new System.Drawing.Size(200, 4);
            this.titleTextBox.Multiline = true;
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(200, 92);
            this.titleTextBox.TabIndex = 4;
            this.titleTextBox.Tag = "title";
            this.titleTextBox.TextChanged += new System.EventHandler(this.CoverTextChanged);
            // 
            // addTextLabel
            // 
            this.addTextLabel.AutoSize = true;
            this.addTextLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.addTextLabel.Location = new System.Drawing.Point(3, 174);
            this.addTextLabel.Name = "addTextLabel";
            this.addTextLabel.Size = new System.Drawing.Size(111, 19);
            this.addTextLabel.TabIndex = 1;
            this.addTextLabel.Text = "Additional Text";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.titleLabel.Location = new System.Drawing.Point(3, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(38, 19);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Title";
            // 
            // authorLabel
            // 
            this.authorLabel.AutoSize = true;
            this.authorLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.authorLabel.Location = new System.Drawing.Point(3, 126);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(55, 19);
            this.authorLabel.TabIndex = 2;
            this.authorLabel.Text = "Author";
            // 
            // authorTextBox
            // 
            this.authorTextBox.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.authorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.authorTextBox.Location = new System.Drawing.Point(3, 148);
            this.authorTextBox.MinimumSize = new System.Drawing.Size(200, 4);
            this.authorTextBox.Name = "authorTextBox";
            this.authorTextBox.Size = new System.Drawing.Size(200, 23);
            this.authorTextBox.TabIndex = 3;
            this.authorTextBox.Tag = "author";
            this.authorTextBox.TextChanged += new System.EventHandler(this.CoverTextChanged);
            // 
            // menuBar
            // 
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuSettings});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(1384, 24);
            this.menuBar.TabIndex = 1;
            this.menuBar.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdNew,
            this.cmdOpen,
            this.cmdSave});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "File";
            this.menuFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdNew
            // 
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.cmdNew.Size = new System.Drawing.Size(146, 22);
            this.cmdNew.Text = "New";
            this.cmdNew.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // cmdOpen
            // 
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.cmdOpen.Size = new System.Drawing.Size(146, 22);
            this.cmdOpen.Text = "Open";
            this.cmdOpen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdSave
            // 
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.cmdSave.Size = new System.Drawing.Size(146, 22);
            this.cmdSave.Text = "Save";
            this.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuSettings
            // 
            this.menuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLanguage});
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(61, 20);
            this.menuSettings.Text = "Settings";
            this.menuSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuLanguage
            // 
            this.menuLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdEnglish,
            this.cmdPolish});
            this.menuLanguage.Name = "menuLanguage";
            this.menuLanguage.Size = new System.Drawing.Size(126, 22);
            this.menuLanguage.Text = "Language";
            // 
            // cmdEnglish
            // 
            this.cmdEnglish.Checked = true;
            this.cmdEnglish.CheckOnClick = true;
            this.cmdEnglish.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmdEnglish.Name = "cmdEnglish";
            this.cmdEnglish.Size = new System.Drawing.Size(112, 22);
            this.cmdEnglish.Text = "English";
            this.cmdEnglish.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdEnglish.Click += new System.EventHandler(this.cmdEnglish_Click);
            // 
            // cmdPolish
            // 
            this.cmdPolish.CheckOnClick = true;
            this.cmdPolish.Name = "cmdPolish";
            this.cmdPolish.Size = new System.Drawing.Size(112, 22);
            this.cmdPolish.Text = "Polish";
            this.cmdPolish.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdPolish.Click += new System.EventHandler(this.cmdPolish_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 861);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuBar);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Book Cover Designer";
            this.Load += new System.EventHandler(this.MainWindowLoad);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.PictureBox Canvas;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.TextBox addTextBox;
        private System.Windows.Forms.Label addTextLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label authorLabel;
        private System.Windows.Forms.TextBox authorTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem cmdNew;
        private System.Windows.Forms.ToolStripMenuItem cmdOpen;
        private System.Windows.Forms.ToolStripMenuItem cmdSave;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem menuLanguage;
        private System.Windows.Forms.ToolStripMenuItem cmdEnglish;
        private System.Windows.Forms.ToolStripMenuItem cmdPolish;
    }
}

