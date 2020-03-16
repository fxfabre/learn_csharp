namespace BergerMT_UI
{
    partial class Display
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ImgBerger = new System.Windows.Forms.Button();
            this.ImgGoat = new System.Windows.Forms.Button();
            this.ImgCabbage = new System.Windows.Forms.Button();
            this.ImgWolf = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ImgBerger
            // 
            this.ImgBerger.BackgroundImage = global::BergerMT_UI.Properties.Resources.berger;
            this.ImgBerger.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ImgBerger.Location = new System.Drawing.Point(20, 25);
            this.ImgBerger.Name = "ImgBerger";
            this.ImgBerger.Size = new System.Drawing.Size(139, 107);
            this.ImgBerger.TabIndex = 3;
            this.ImgBerger.UseVisualStyleBackColor = true;
            // 
            // ImgGoat
            // 
            this.ImgGoat.BackgroundImage = global::BergerMT_UI.Properties.Resources.chèvre;
            this.ImgGoat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ImgGoat.Location = new System.Drawing.Point(20, 251);
            this.ImgGoat.Name = "ImgGoat";
            this.ImgGoat.Size = new System.Drawing.Size(139, 107);
            this.ImgGoat.TabIndex = 2;
            this.ImgGoat.UseVisualStyleBackColor = true;
            // 
            // ImgCabbage
            // 
            this.ImgCabbage.BackgroundImage = global::BergerMT_UI.Properties.Resources.choux;
            this.ImgCabbage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ImgCabbage.Location = new System.Drawing.Point(20, 364);
            this.ImgCabbage.Name = "ImgCabbage";
            this.ImgCabbage.Size = new System.Drawing.Size(139, 107);
            this.ImgCabbage.TabIndex = 1;
            this.ImgCabbage.UseVisualStyleBackColor = true;
            // 
            // ImgWolf
            // 
            this.ImgWolf.BackgroundImage = global::BergerMT_UI.Properties.Resources.loup;
            this.ImgWolf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ImgWolf.Location = new System.Drawing.Point(20, 138);
            this.ImgWolf.Name = "ImgWolf";
            this.ImgWolf.Size = new System.Drawing.Size(139, 107);
            this.ImgWolf.TabIndex = 0;
            this.ImgWolf.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(20, 494);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(790, 118);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = string.Empty;
            this.richTextBox1.Visible = false;
            // 
            // display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BergerMT_UI.Properties.Resources.fleuve;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.ImgBerger);
            this.Controls.Add(this.ImgGoat);
            this.Controls.Add(this.ImgCabbage);
            this.Controls.Add(this.ImgWolf);
            this.Name = "display";
            this.Size = new System.Drawing.Size(828, 636);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button ImgWolf;
        private System.Windows.Forms.Button ImgCabbage;
        private System.Windows.Forms.Button ImgGoat;
        private System.Windows.Forms.Button ImgBerger;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}
