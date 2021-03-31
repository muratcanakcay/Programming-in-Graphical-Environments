
namespace PracticeWinForms
{
    partial class PracticeWindow
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
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.goButton = new System.Windows.Forms.Button();
            this.nameErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.radioBtnGrpBox = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.colorBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.nameErrorProvider)).BeginInit();
            this.radioBtnGrpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(45, 32);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(67, 15);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Your name:";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(45, 61);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(100, 23);
            this.nameTextBox.TabIndex = 1;
            this.nameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameTextBox_KeyPress);
            this.nameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.nameTextBox_Validating);
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(57, 104);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 23);
            this.goButton.TabIndex = 2;
            this.goButton.Text = "OK";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // nameErrorProvider
            // 
            this.nameErrorProvider.ContainerControl = this;
            // 
            // radioBtnGrpBox
            // 
            this.radioBtnGrpBox.Controls.Add(this.colorBox);
            this.radioBtnGrpBox.Controls.Add(this.radioButton3);
            this.radioBtnGrpBox.Controls.Add(this.radioButton2);
            this.radioBtnGrpBox.Controls.Add(this.radioButton1);
            this.radioBtnGrpBox.Location = new System.Drawing.Point(179, 27);
            this.radioBtnGrpBox.Name = "radioBtnGrpBox";
            this.radioBtnGrpBox.Size = new System.Drawing.Size(276, 166);
            this.radioBtnGrpBox.TabIndex = 3;
            this.radioBtnGrpBox.TabStop = false;
            this.radioBtnGrpBox.Text = "Radio buttons";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(7, 86);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(56, 19);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "Green";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.selectedColor);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(7, 60);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(48, 19);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Blue";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.selectedColor);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 34);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(45, 19);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "Red";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.selectedColor);
            // 
            // colorBox
            // 
            this.colorBox.BackColor = System.Drawing.Color.White;
            this.colorBox.Location = new System.Drawing.Point(140, 23);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(117, 113);
            this.colorBox.TabIndex = 3;
            this.colorBox.TabStop = false;
            // 
            // PracticeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(1093, 707);
            this.Controls.Add(this.radioBtnGrpBox);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.nameLabel);
            this.Name = "PracticeWindow";
            this.Text = "PracticeWindow";
            ((System.ComponentModel.ISupportInitialize)(this.nameErrorProvider)).EndInit();
            this.radioBtnGrpBox.ResumeLayout(false);
            this.radioBtnGrpBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.ErrorProvider nameErrorProvider;
        private System.Windows.Forms.GroupBox radioBtnGrpBox;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.PictureBox colorBox;
    }
}

