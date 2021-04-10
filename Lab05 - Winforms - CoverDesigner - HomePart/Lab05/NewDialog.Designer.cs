
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewDialog));
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
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.cancelButtonNewDialog, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.widthLabelNewDialog, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.heightLabelNewDialog, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.spineWidthLabelNewDialog, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.newWidth, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.newHeight, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.newSpineWidth, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.okButtonNewDialog, 0, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // cancelButtonNewDialog
            // 
            resources.ApplyResources(this.cancelButtonNewDialog, "cancelButtonNewDialog");
            this.cancelButtonNewDialog.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButtonNewDialog.Name = "cancelButtonNewDialog";
            this.cancelButtonNewDialog.UseVisualStyleBackColor = true;
            // 
            // widthLabelNewDialog
            // 
            resources.ApplyResources(this.widthLabelNewDialog, "widthLabelNewDialog");
            this.widthLabelNewDialog.Name = "widthLabelNewDialog";
            // 
            // heightLabelNewDialog
            // 
            resources.ApplyResources(this.heightLabelNewDialog, "heightLabelNewDialog");
            this.heightLabelNewDialog.Name = "heightLabelNewDialog";
            // 
            // spineWidthLabelNewDialog
            // 
            resources.ApplyResources(this.spineWidthLabelNewDialog, "spineWidthLabelNewDialog");
            this.spineWidthLabelNewDialog.Name = "spineWidthLabelNewDialog";
            // 
            // newWidth
            // 
            resources.ApplyResources(this.newWidth, "newWidth");
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
            this.newWidth.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // newHeight
            // 
            resources.ApplyResources(this.newHeight, "newHeight");
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
            this.newHeight.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // newSpineWidth
            // 
            resources.ApplyResources(this.newSpineWidth, "newSpineWidth");
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
            this.newSpineWidth.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // okButtonNewDialog
            // 
            resources.ApplyResources(this.okButtonNewDialog, "okButtonNewDialog");
            this.okButtonNewDialog.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButtonNewDialog.Name = "okButtonNewDialog";
            this.okButtonNewDialog.UseVisualStyleBackColor = true;
            this.okButtonNewDialog.Click += new System.EventHandler(this.OkButtonNewDialog_Click);
            // 
            // NewDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewDialog";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
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
        private System.Windows.Forms.NumericUpDown newSpineWidth;        
        private System.Windows.Forms.Button cancelButtonNewDialog;
        private System.Windows.Forms.Button okButtonNewDialog;
    }
}