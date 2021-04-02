
namespace Lab05
{
    partial class NewDialog
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelButtonNewDialog = new System.Windows.Forms.Button();
            this.widthLabelNewDialog = new System.Windows.Forms.Label();
            this.heightLabelNewDialog = new System.Windows.Forms.Label();
            this.spineWidthLabelNewDialog = new System.Windows.Forms.Label();
            this.widthUpDownNewDialog = new System.Windows.Forms.NumericUpDown();
            this.heightUpDownNewDialog = new System.Windows.Forms.NumericUpDown();
            this.spineWidthUpDownNewDialog = new System.Windows.Forms.NumericUpDown();
            this.okButtonNewDialog = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDownNewDialog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDownNewDialog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spineWidthUpDownNewDialog)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.cancelButtonNewDialog, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.widthLabelNewDialog, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.heightLabelNewDialog, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.spineWidthLabelNewDialog, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.widthUpDownNewDialog, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.heightUpDownNewDialog, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.spineWidthUpDownNewDialog, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.okButtonNewDialog, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(234, 211);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cancelButtonNewDialog
            // 
            this.cancelButtonNewDialog.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cancelButtonNewDialog.Location = new System.Drawing.Point(138, 172);
            this.cancelButtonNewDialog.Name = "cancelButtonNewDialog";
            this.cancelButtonNewDialog.Size = new System.Drawing.Size(75, 23);
            this.cancelButtonNewDialog.TabIndex = 7;
            this.cancelButtonNewDialog.Text = "Cancel";
            this.cancelButtonNewDialog.UseVisualStyleBackColor = true;
            this.cancelButtonNewDialog.Click += new System.EventHandler(this.cancelButtonNewDialog_Click);
            // 
            // widthLabelNewDialog
            // 
            this.widthLabelNewDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.widthLabelNewDialog.AutoSize = true;
            this.widthLabelNewDialog.Location = new System.Drawing.Point(3, 0);
            this.widthLabelNewDialog.Name = "widthLabelNewDialog";
            this.widthLabelNewDialog.Size = new System.Drawing.Size(111, 52);
            this.widthLabelNewDialog.TabIndex = 0;
            this.widthLabelNewDialog.Text = "Width";
            this.widthLabelNewDialog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // heightLabelNewDialog
            // 
            this.heightLabelNewDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.heightLabelNewDialog.AutoSize = true;
            this.heightLabelNewDialog.Location = new System.Drawing.Point(3, 52);
            this.heightLabelNewDialog.Name = "heightLabelNewDialog";
            this.heightLabelNewDialog.Size = new System.Drawing.Size(111, 52);
            this.heightLabelNewDialog.TabIndex = 1;
            this.heightLabelNewDialog.Text = "Height";
            this.heightLabelNewDialog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Worker.spineWidthLabelNewDialog
            // 
            this.spineWidthLabelNewDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spineWidthLabelNewDialog.AutoSize = true;
            this.spineWidthLabelNewDialog.Location = new System.Drawing.Point(3, 104);
            this.spineWidthLabelNewDialog.Name = "Worker.spineWidthLabelNewDialog";
            this.spineWidthLabelNewDialog.Size = new System.Drawing.Size(111, 52);
            this.spineWidthLabelNewDialog.TabIndex = 2;
            this.spineWidthLabelNewDialog.Text = "Spine Width";
            this.spineWidthLabelNewDialog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // widthUpDownNewDialog
            // 
            this.widthUpDownNewDialog.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.widthUpDownNewDialog.Location = new System.Drawing.Point(129, 14);
            this.widthUpDownNewDialog.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.widthUpDownNewDialog.Minimum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.widthUpDownNewDialog.Name = "widthUpDownNewDialog";
            this.widthUpDownNewDialog.Size = new System.Drawing.Size(92, 23);
            this.widthUpDownNewDialog.TabIndex = 3;
            this.widthUpDownNewDialog.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // heightUpDownNewDialog
            // 
            this.heightUpDownNewDialog.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.heightUpDownNewDialog.Location = new System.Drawing.Point(129, 66);
            this.heightUpDownNewDialog.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.heightUpDownNewDialog.Minimum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.heightUpDownNewDialog.Name = "heightUpDownNewDialog";
            this.heightUpDownNewDialog.Size = new System.Drawing.Size(92, 23);
            this.heightUpDownNewDialog.TabIndex = 4;
            this.heightUpDownNewDialog.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // Worker.spineWidthUpDownNewDialog
            // 
            this.spineWidthUpDownNewDialog.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.spineWidthUpDownNewDialog.Location = new System.Drawing.Point(129, 118);
            this.spineWidthUpDownNewDialog.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.spineWidthUpDownNewDialog.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.spineWidthUpDownNewDialog.Name = "Worker.spineWidthUpDownNewDialog";
            this.spineWidthUpDownNewDialog.Size = new System.Drawing.Size(92, 23);
            this.spineWidthUpDownNewDialog.TabIndex = 5;
            this.spineWidthUpDownNewDialog.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // okButtonNewDialog
            // 
            this.okButtonNewDialog.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.okButtonNewDialog.Location = new System.Drawing.Point(21, 172);
            this.okButtonNewDialog.Name = "okButtonNewDialog";
            this.okButtonNewDialog.Size = new System.Drawing.Size(75, 23);
            this.okButtonNewDialog.TabIndex = 6;
            this.okButtonNewDialog.Text = "OK";
            this.okButtonNewDialog.UseVisualStyleBackColor = true;
            this.okButtonNewDialog.Click += new System.EventHandler(this.okButtonNewDialog_Click);
            // 
            // NewDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 211);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(250, 250);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(250, 250);
            this.Name = "NewDialog";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Cover...";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDownNewDialog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDownNewDialog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spineWidthUpDownNewDialog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label widthLabelNewDialog;
        private System.Windows.Forms.Label heightLabelNewDialog;
        private System.Windows.Forms.Label spineWidthLabelNewDialog;
        private System.Windows.Forms.NumericUpDown widthUpDownNewDialog;
        private System.Windows.Forms.NumericUpDown heightUpDownNewDialog;
        private System.Windows.Forms.NumericUpDown spineWidthUpDownNewDialog;
        private System.Windows.Forms.Button cancelButtonNewDialog;
        private System.Windows.Forms.Button okButtonNewDialog;
    }
}