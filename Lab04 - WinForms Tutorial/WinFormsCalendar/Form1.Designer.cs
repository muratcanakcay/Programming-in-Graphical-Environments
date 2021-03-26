
namespace WinFormsCalendar
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.noteTextBox = new System.Windows.Forms.TextBox();
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.removeButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.calendarListView = new System.Windows.Forms.ListView();
            this.dateColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.noteColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BackColor = System.Drawing.Color.LightSteelBlue;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer.Panel1.Controls.Add(this.noteTextBox);
            this.splitContainer.Panel1.Controls.Add(this.monthCalendar);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer.Panel2.Controls.Add(this.removeButton);
            this.splitContainer.Panel2.Controls.Add(this.addButton);
            this.splitContainer.Panel2.Controls.Add(this.calendarListView);
            this.splitContainer.Size = new System.Drawing.Size(784, 461);
            this.splitContainer.SplitterDistance = 200;
            this.splitContainer.TabIndex = 0;
            // 
            // noteTextBox
            // 
            this.noteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.noteTextBox.Location = new System.Drawing.Point(483, 12);
            this.noteTextBox.Multiline = true;
            this.noteTextBox.Name = "noteTextBox";
            this.noteTextBox.Size = new System.Drawing.Size(289, 174);
            this.noteTextBox.TabIndex = 1;
            // 
            // monthCalendar
            // 
            this.monthCalendar.CalendarDimensions = new System.Drawing.Size(2, 1);
            this.monthCalendar.Location = new System.Drawing.Point(9, 12);
            this.monthCalendar.MaxSelectionCount = 1;
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.TabIndex = 0;
            // 
            // removeButton
            // 
            this.removeButton.Enabled = false;
            this.removeButton.Location = new System.Drawing.Point(93, 12);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 2;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(12, 12);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // calendarListView
            // 
            this.calendarListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.calendarListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.dateColumnHeader,
            this.noteColumnHeader});
            this.calendarListView.FullRowSelect = true;
            this.calendarListView.HideSelection = false;
            this.calendarListView.Location = new System.Drawing.Point(12, 41);
            this.calendarListView.Name = "calendarListView";
            this.calendarListView.Size = new System.Drawing.Size(760, 204);
            this.calendarListView.TabIndex = 0;
            this.calendarListView.UseCompatibleStateImageBehavior = false;
            this.calendarListView.View = System.Windows.Forms.View.Details;
            this.calendarListView.SelectedIndexChanged += new System.EventHandler(this.calendarListView_SelectedIndexChanged);
            // 
            // dateColumnHeader
            // 
            this.dateColumnHeader.Text = "Date";
            this.dateColumnHeader.Width = 120;
            // 
            // noteColumnHeader
            // 
            this.noteColumnHeader.Text = "Note";
            this.noteColumnHeader.Width = 120;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifyIconContextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Windows Forms Calendar";
            this.notifyIcon.Visible = true;
            // 
            // notifyIconContextMenu
            // 
            this.notifyIconContextMenu.Name = "notifyIconContextMenu";
            this.notifyIconContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.splitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Awesome Calendar";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TextBox noteTextBox;
        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ListView calendarListView;
        private System.Windows.Forms.ColumnHeader dateColumnHeader;
        private System.Windows.Forms.ColumnHeader noteColumnHeader;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyIconContextMenu;
    }
}

