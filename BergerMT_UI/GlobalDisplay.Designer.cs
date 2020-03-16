namespace BergerMT_UI
{
    partial class GlobalDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GlobalDisplay));
            this.display1 = new BergerMT_UI.Display();
            this.SuspendLayout();
            // 
            // display1
            // 
            this.display1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.display1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("display1.BackgroundImage")));
            this.display1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.display1.Location = new System.Drawing.Point(1, 2);
            this.display1.Name = "display1";
            this.display1.Size = new System.Drawing.Size(1005, 621);
            this.display1.TabIndex = 1;
            // 
            // GlobalDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1005, 620);
            this.Controls.Add(this.display1);
            this.Name = "GlobalDisplay";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Display display1;
    }
}

