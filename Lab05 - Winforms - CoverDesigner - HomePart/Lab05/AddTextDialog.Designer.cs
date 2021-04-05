
namespace Lab05
{
    partial class AddTextDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTextDialog));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.labelFontSize = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.alignmentBox = new System.Windows.Forms.GroupBox();
            this.radioRight = new System.Windows.Forms.RadioButton();
            this.radioCenter = new System.Windows.Forms.RadioButton();
            this.radioLeft = new System.Windows.Forms.RadioButton();
            this.addTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAddTextCancel = new System.Windows.Forms.Button();
            this.buttonAddTextOK = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.alignmentBox.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.addTextBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.alignmentBox, 1, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.labelFontSize, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.numericUpDown1, 1, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // labelFontSize
            // 
            resources.ApplyResources(this.labelFontSize, "labelFontSize");
            this.labelFontSize.Name = "labelFontSize";
            // 
            // numericUpDown1
            // 
            resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
            this.numericUpDown1.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // alignmentBox
            // 
            this.alignmentBox.Controls.Add(this.radioRight);
            this.alignmentBox.Controls.Add(this.radioCenter);
            this.alignmentBox.Controls.Add(this.radioLeft);
            resources.ApplyResources(this.alignmentBox, "alignmentBox");
            this.alignmentBox.Name = "alignmentBox";
            this.alignmentBox.TabStop = false;
            // 
            // radioRight
            // 
            resources.ApplyResources(this.radioRight, "radioRight");
            this.radioRight.Name = "radioRight";
            this.radioRight.TabStop = true;
            this.radioRight.UseVisualStyleBackColor = true;
            // 
            // radioCenter
            // 
            resources.ApplyResources(this.radioCenter, "radioCenter");
            this.radioCenter.Name = "radioCenter";
            this.radioCenter.TabStop = true;
            this.radioCenter.UseVisualStyleBackColor = true;
            // 
            // radioLeft
            // 
            resources.ApplyResources(this.radioLeft, "radioLeft");
            this.radioLeft.Name = "radioLeft";
            this.radioLeft.TabStop = true;
            this.radioLeft.UseVisualStyleBackColor = true;
            // 
            // addTextBox
            // 
            resources.ApplyResources(this.addTextBox, "addTextBox");
            this.addTextBox.Name = "addTextBox";
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.buttonAddTextCancel, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.buttonAddTextOK, 1, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // buttonAddTextCancel
            // 
            resources.ApplyResources(this.buttonAddTextCancel, "buttonAddTextCancel");
            this.buttonAddTextCancel.Name = "buttonAddTextCancel";
            this.buttonAddTextCancel.UseVisualStyleBackColor = true;
            // 
            // buttonAddTextOK
            // 
            resources.ApplyResources(this.buttonAddTextOK, "buttonAddTextOK");
            this.buttonAddTextOK.Name = "buttonAddTextOK";
            this.buttonAddTextOK.UseVisualStyleBackColor = true;
            this.buttonAddTextOK.Click += new System.EventHandler(this.buttonAddTextOK_Click);
            // 
            // AddTextDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTextDialog";
            this.ShowIcon = false;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.alignmentBox.ResumeLayout(false);
            this.alignmentBox.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelFontSize;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.GroupBox alignmentBox;
        private System.Windows.Forms.RadioButton radioRight;
        private System.Windows.Forms.RadioButton radioCenter;
        private System.Windows.Forms.RadioButton radioLeft;
        private System.Windows.Forms.TextBox addTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button buttonAddTextCancel;
        private System.Windows.Forms.Button buttonAddTextOK;
    }
}