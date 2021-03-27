
namespace WinFormsGallery
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
            this.galleryLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.imageButton = new System.Windows.Forms.Button();
            this.colorButton = new System.Windows.Forms.Button();
            this.emptyLabel01 = new System.Windows.Forms.Label();
            this.emptyLabel00 = new System.Windows.Forms.Label();
            this.galleryLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // galleryLayoutPanel
            // 
            this.galleryLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.galleryLayoutPanel.ColumnCount = 2;
            this.galleryLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.96296F));
            this.galleryLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.03704F));
            this.galleryLayoutPanel.Controls.Add(this.imageButton, 1, 0);
            this.galleryLayoutPanel.Controls.Add(this.colorButton, 1, 1);
            this.galleryLayoutPanel.Controls.Add(this.emptyLabel01, 0, 1);
            this.galleryLayoutPanel.Controls.Add(this.emptyLabel00, 0, 0);
            this.galleryLayoutPanel.Location = new System.Drawing.Point(12, 12);
            this.galleryLayoutPanel.Name = "galleryLayoutPanel";
            this.galleryLayoutPanel.RowCount = 2;
            this.galleryLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.galleryLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.galleryLayoutPanel.Size = new System.Drawing.Size(760, 437);
            this.galleryLayoutPanel.TabIndex = 0;
            // 
            // imageButton
            // 
            this.imageButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageButton.Location = new System.Drawing.Point(481, 3);
            this.imageButton.Name = "imageButton";
            this.imageButton.Size = new System.Drawing.Size(276, 212);
            this.imageButton.TabIndex = 0;
            this.imageButton.Text = "Choose Image";
            this.imageButton.UseVisualStyleBackColor = true;
            this.imageButton.Click += new System.EventHandler(this.imageButton_Click);
            // 
            // colorButton
            // 
            this.colorButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorButton.Location = new System.Drawing.Point(481, 221);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(276, 213);
            this.colorButton.TabIndex = 1;
            this.colorButton.Text = "Choose Color";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // emptyLabel01
            // 
            this.emptyLabel01.AutoSize = true;
            this.emptyLabel01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emptyLabel01.Location = new System.Drawing.Point(3, 218);
            this.emptyLabel01.Name = "emptyLabel01";
            this.emptyLabel01.Size = new System.Drawing.Size(472, 219);
            this.emptyLabel01.TabIndex = 2;
            // 
            // emptyLabel00
            // 
            this.emptyLabel00.AutoSize = true;
            this.emptyLabel00.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emptyLabel00.Location = new System.Drawing.Point(3, 0);
            this.emptyLabel00.Name = "emptyLabel00";
            this.emptyLabel00.Size = new System.Drawing.Size(472, 218);
            this.emptyLabel00.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.galleryLayoutPanel);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Awesome Gallery";
            this.galleryLayoutPanel.ResumeLayout(false);
            this.galleryLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel galleryLayoutPanel;
        private System.Windows.Forms.Button imageButton;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.Label emptyLabel01;
        private System.Windows.Forms.Label emptyLabel00;
    }
}

