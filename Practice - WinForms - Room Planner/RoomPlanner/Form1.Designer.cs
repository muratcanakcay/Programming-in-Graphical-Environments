
namespace RoomPlanner
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.createdFurnitureBox = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.addFurnitureBox = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.coffeeTableButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tableButton = new System.Windows.Forms.Button();
            this.sofaButton = new System.Windows.Forms.Button();
            this.bedButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.createdFurnitureBox.SuspendLayout();
            this.addFurnitureBox.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 426);
            this.splitContainer1.SplitterDistance = 550;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(544, 444);
            this.tableLayoutPanel2.TabIndex = 0;
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
            this.createdFurnitureBox.Controls.Add(this.listBox1);
            this.createdFurnitureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.createdFurnitureBox.Location = new System.Drawing.Point(3, 216);
            this.createdFurnitureBox.Name = "createdFurnitureBox";
            this.createdFurnitureBox.Size = new System.Drawing.Size(240, 207);
            this.createdFurnitureBox.TabIndex = 0;
            this.createdFurnitureBox.TabStop = false;
            this.createdFurnitureBox.Text = "Created Furniture";
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(3, 19);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(234, 185);
            this.listBox1.TabIndex = 0;
            // 
            // addFurnitureBox
            // 
            this.addFurnitureBox.Controls.Add(this.flowLayoutPanel1);
            this.addFurnitureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addFurnitureBox.Location = new System.Drawing.Point(3, 3);
            this.addFurnitureBox.Name = "addFurnitureBox";
            this.addFurnitureBox.Size = new System.Drawing.Size(240, 207);
            this.addFurnitureBox.TabIndex = 1;
            this.addFurnitureBox.TabStop = false;
            this.addFurnitureBox.Text = "Add Furniture";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.coffeeTableButton);
            this.flowLayoutPanel1.Controls.Add(this.tableButton);
            this.flowLayoutPanel1.Controls.Add(this.sofaButton);
            this.flowLayoutPanel1.Controls.Add(this.bedButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(234, 185);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // coffeeTableButton
            // 
            this.coffeeTableButton.BackColor = System.Drawing.Color.White;
            this.coffeeTableButton.BackgroundImage = global::RoomPlanner.Properties.Resources.coffee_table;
            this.coffeeTableButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.coffeeTableButton.Location = new System.Drawing.Point(3, 3);
            this.coffeeTableButton.Name = "coffeeTableButton";
            this.coffeeTableButton.Size = new System.Drawing.Size(75, 75);
            this.coffeeTableButton.TabIndex = 0;
            this.coffeeTableButton.UseVisualStyleBackColor = false;
            this.coffeeTableButton.Click += new System.EventHandler(this.coffeeTableButton_Click);
            this.coffeeTableButton.MouseEnter += new System.EventHandler(this.coffeeTableButton_MouseEnter);
            this.coffeeTableButton.MouseLeave += new System.EventHandler(this.coffeeTableButton_MouseLeave);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.ShortcutKeyDisplayString = "F2";
            this.toolStripMenuItem2.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.toolStripMenuItem2.Size = new System.Drawing.Size(168, 22);
            this.toolStripMenuItem2.Text = "New Blueprint";
            // 
            // tableButton
            // 
            this.tableButton.BackColor = System.Drawing.Color.White;
            this.tableButton.BackgroundImage = global::RoomPlanner.Properties.Resources.table;
            this.tableButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableButton.Location = new System.Drawing.Point(84, 3);
            this.tableButton.Name = "tableButton";
            this.tableButton.Size = new System.Drawing.Size(75, 75);
            this.tableButton.TabIndex = 1;
            this.tableButton.UseVisualStyleBackColor = false;
            this.tableButton.Click += new System.EventHandler(this.tableButton_Click);
            // 
            // sofaButton
            // 
            this.sofaButton.BackColor = System.Drawing.Color.White;
            this.sofaButton.BackgroundImage = global::RoomPlanner.Properties.Resources.sofa;
            this.sofaButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sofaButton.Location = new System.Drawing.Point(3, 84);
            this.sofaButton.Name = "sofaButton";
            this.sofaButton.Size = new System.Drawing.Size(75, 75);
            this.sofaButton.TabIndex = 2;
            this.sofaButton.UseVisualStyleBackColor = false;
            this.sofaButton.Click += new System.EventHandler(this.sofaButton_Click);
            // 
            // bedButton
            // 
            this.bedButton.BackColor = System.Drawing.Color.White;
            this.bedButton.BackgroundImage = global::RoomPlanner.Properties.Resources.double_bed;
            this.bedButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bedButton.Location = new System.Drawing.Point(84, 84);
            this.bedButton.Name = "bedButton";
            this.bedButton.Size = new System.Drawing.Size(75, 75);
            this.bedButton.TabIndex = 3;
            this.bedButton.UseVisualStyleBackColor = false;
            this.bedButton.Click += new System.EventHandler(this.bedButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RoomPlanner";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.createdFurnitureBox.ResumeLayout(false);
            this.addFurnitureBox.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox createdFurnitureBox;
        private System.Windows.Forms.GroupBox addFurnitureBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Button coffeeTableButton;
        private System.Windows.Forms.Button tableButton;
        private System.Windows.Forms.Button sofaButton;
        private System.Windows.Forms.Button bedButton;
    }
}

