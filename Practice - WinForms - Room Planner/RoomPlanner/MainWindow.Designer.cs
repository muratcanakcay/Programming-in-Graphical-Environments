
namespace RoomPlanner
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.createdFurnitureBox = new System.Windows.Forms.GroupBox();
            this.listBox = new System.Windows.Forms.ListBox();
            this.addFurnitureBox = new System.Windows.Forms.GroupBox();
            this.buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.coffeeTableButton = new RoomPlanner.ToggleButton();
            this.tableButton = new RoomPlanner.ToggleButton();
            this.sofaButton = new RoomPlanner.ToggleButton();
            this.bedButton = new RoomPlanner.ToggleButton();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.fileItemNewBlueprint = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.createdFurnitureBox.SuspendLayout();
            this.addFurnitureBox.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.Canvas);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer.Size = new System.Drawing.Size(800, 426);
            this.splitContainer.SplitterDistance = 550;
            this.splitContainer.TabIndex = 0;
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(554, 426);
            this.Canvas.TabIndex = 0;
            this.Canvas.TabStop = false;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.CanvasPaint);
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CanvasMouseDown);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.createdFurnitureBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.addFurnitureBox, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(246, 426);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // createdFurnitureBox
            // 
            this.createdFurnitureBox.Controls.Add(this.listBox);
            this.createdFurnitureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.createdFurnitureBox.Location = new System.Drawing.Point(3, 216);
            this.createdFurnitureBox.Name = "createdFurnitureBox";
            this.createdFurnitureBox.Size = new System.Drawing.Size(240, 207);
            this.createdFurnitureBox.TabIndex = 0;
            this.createdFurnitureBox.TabStop = false;
            this.createdFurnitureBox.Text = "Created Furniture";
            // 
            // listBox
            // 
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 15;
            this.listBox.Location = new System.Drawing.Point(3, 19);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(234, 185);
            this.listBox.TabIndex = 0;
            this.listBox.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.listBoxFormat);
            // 
            // addFurnitureBox
            // 
            this.addFurnitureBox.Controls.Add(this.buttonsPanel);
            this.addFurnitureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addFurnitureBox.Location = new System.Drawing.Point(3, 3);
            this.addFurnitureBox.Name = "addFurnitureBox";
            this.addFurnitureBox.Size = new System.Drawing.Size(240, 207);
            this.addFurnitureBox.TabIndex = 1;
            this.addFurnitureBox.TabStop = false;
            this.addFurnitureBox.Text = "Add Furniture";
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.AutoScroll = true;
            this.buttonsPanel.Controls.Add(this.coffeeTableButton);
            this.buttonsPanel.Controls.Add(this.tableButton);
            this.buttonsPanel.Controls.Add(this.sofaButton);
            this.buttonsPanel.Controls.Add(this.bedButton);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsPanel.Location = new System.Drawing.Point(3, 19);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(234, 185);
            this.buttonsPanel.TabIndex = 0;
            // 
            // coffeeTableButton
            // 
            this.coffeeTableButton.BackColor = System.Drawing.Color.White;
            this.coffeeTableButton.BackgroundImage = global::RoomPlanner.Properties.Resources.coffee_table;
            this.coffeeTableButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.coffeeTableButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.coffeeTableButton.Location = new System.Drawing.Point(3, 3);
            this.coffeeTableButton.Name = "coffeeTableButton";
            this.coffeeTableButton.Size = new System.Drawing.Size(75, 75);
            this.coffeeTableButton.State = RoomPlanner.ButtonState.off;
            this.coffeeTableButton.TabIndex = 0;
            this.coffeeTableButton.Tag = "Coffee Table";
            this.coffeeTableButton.UseVisualStyleBackColor = false;
            this.coffeeTableButton.Click += new System.EventHandler(this.SelectButton);
            // 
            // tableButton
            // 
            this.tableButton.BackColor = System.Drawing.Color.White;
            this.tableButton.BackgroundImage = global::RoomPlanner.Properties.Resources.table;
            this.tableButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tableButton.Location = new System.Drawing.Point(84, 3);
            this.tableButton.Name = "tableButton";
            this.tableButton.Size = new System.Drawing.Size(75, 75);
            this.tableButton.State = RoomPlanner.ButtonState.off;
            this.tableButton.TabIndex = 1;
            this.tableButton.Tag = "Table";
            this.tableButton.UseVisualStyleBackColor = false;
            this.tableButton.Click += new System.EventHandler(this.SelectButton);
            // 
            // sofaButton
            // 
            this.sofaButton.BackColor = System.Drawing.Color.White;
            this.sofaButton.BackgroundImage = global::RoomPlanner.Properties.Resources.sofa;
            this.sofaButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sofaButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sofaButton.Location = new System.Drawing.Point(3, 84);
            this.sofaButton.Name = "sofaButton";
            this.sofaButton.Size = new System.Drawing.Size(75, 75);
            this.sofaButton.State = RoomPlanner.ButtonState.off;
            this.sofaButton.TabIndex = 2;
            this.sofaButton.Tag = "Sofa";
            this.sofaButton.UseVisualStyleBackColor = false;
            this.sofaButton.Click += new System.EventHandler(this.SelectButton);
            // 
            // bedButton
            // 
            this.bedButton.BackColor = System.Drawing.Color.White;
            this.bedButton.BackgroundImage = global::RoomPlanner.Properties.Resources.double_bed;
            this.bedButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bedButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bedButton.Location = new System.Drawing.Point(84, 84);
            this.bedButton.Name = "bedButton";
            this.bedButton.Size = new System.Drawing.Size(75, 75);
            this.bedButton.State = RoomPlanner.ButtonState.off;
            this.bedButton.TabIndex = 3;
            this.bedButton.Tag = "Bed";
            this.bedButton.UseVisualStyleBackColor = false;
            this.bedButton.Click += new System.EventHandler(this.SelectButton);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip";
            // 
            // menuItemFile
            // 
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileItemNewBlueprint});
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(37, 20);
            this.menuItemFile.Text = "File";
            // 
            // fileItemNewBlueprint
            // 
            this.fileItemNewBlueprint.Name = "fileItemNewBlueprint";
            this.fileItemNewBlueprint.ShortcutKeyDisplayString = "F2";
            this.fileItemNewBlueprint.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.fileItemNewBlueprint.Size = new System.Drawing.Size(168, 22);
            this.fileItemNewBlueprint.Text = "New Blueprint";
            this.fileItemNewBlueprint.Click += new System.EventHandler(this.NewBluePrint);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RoomPlanner";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.createdFurnitureBox.ResumeLayout(false);
            this.addFurnitureBox.ResumeLayout(false);
            this.buttonsPanel.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox createdFurnitureBox;
        private System.Windows.Forms.GroupBox addFurnitureBox;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.FlowLayoutPanel buttonsPanel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItemFile;
        private System.Windows.Forms.ToolStripMenuItem fileItemNewBlueprint;
        private ToggleButton coffeeTableButton;
        private ToggleButton tableButton;
        private ToggleButton sofaButton;
        private ToggleButton bedButton;
        private System.Windows.Forms.PictureBox Canvas;
    }
}

