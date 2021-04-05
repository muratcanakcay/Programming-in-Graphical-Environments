
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
            this.newWidth = new System.Windows.Forms.NumericUpDown();
            this.newHeight = new System.Windows.Forms.NumericUpDown();
            this.newSpineWidth = new System.Windows.Forms.NumericUpDown();
            this.okButtonNewDialog = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newSpineWidth)).BeginInit();
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
            this.tableLayoutPanel1.Controls.Add(this.newWidth, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.newHeight, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.newSpineWidth, 1, 2);
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
            // spineWidthLabelNewDialog
            // 
            this.spineWidthLabelNewDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spineWidthLabelNewDialog.AutoSize = true;
            this.spineWidthLabelNewDialog.Location = new System.Drawing.Point(3, 104);
            this.spineWidthLabelNewDialog.Name = "spineWidthLabelNewDialog";
            this.spineWidthLabelNewDialog.Size = new System.Drawing.Size(111, 52);
            this.spineWidthLabelNewDialog.TabIndex = 2;
            this.spineWidthLabelNewDialog.Text = "Spine Width";
            this.spineWidthLabelNewDialog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // newWidth
            // 
            this.newWidth.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.newWidth.Location = new System.Drawing.Point(129, 14);
            this.newWidth.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.newWidth.Minimum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.newWidth.Name = "newWidth";
            this.newWidth.Size = new System.Drawing.Size(92, 23);
            this.newWidth.TabIndex = 3;
            this.newWidth.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // newHeight
            // 
            this.newHeight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.newHeight.Location = new System.Drawing.Point(129, 66);
            this.newHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.newHeight.Minimum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.newHeight.Name = "newHeight";
            this.newHeight.Size = new System.Drawing.Size(92, 23);
            this.newHeight.TabIndex = 4;
            this.newHeight.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // newSpineWidth
            // 
            this.newSpineWidth.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.newSpineWidth.Location = new System.Drawing.Point(129, 118);
            this.newSpineWidth.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.newSpineWidth.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.newSpineWidth.Name = "newSpineWidth";
            this.newSpineWidth.Size = new System.Drawing.Size(92, 23);
            this.newSpineWidth.TabIndex = 5;
            this.newSpineWidth.Value = new decimal(new int[] {
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
            ((System.ComponentModel.ISupportInitialize)(this.newWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newSpineWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label widthLabelNewDialog;
        private System.Windows.Forms.Label heightLabelNewDialog;
        private System.Windows.Forms.Label spineWidthLabelNewDialog;
        private System.Windows.Forms.NumericUpDown newWidth;
        private System.Windows.Forms.NumericUpDown newHeight;
        private System.Windows.Forms.NumericUpDown SpineWidth;
        private System.Windows.Forms.Button cancelButtonNewDialog;
        private System.Windows.Forms.Button okButtonNewDialog;
        private System.Windows.Forms.NumericUpDown newSpineWidth;
    }
}