namespace ReflexionLibsUI
{
    partial class DisplayType
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
            this.LblClassName = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblDllName = new System.Windows.Forms.Label();
            this.loadingImg = new System.Windows.Forms.PictureBox();
            this.listBoxClassName = new System.Windows.Forms.ListBox();
            this.listBoxMethods = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.loadingImg)).BeginInit();
            this.SuspendLayout();
            // 
            // LblClassName
            // 
            this.LblClassName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblClassName.AutoSize = true;
            this.LblClassName.Location = new System.Drawing.Point(13, 12);
            this.LblClassName.MinimumSize = new System.Drawing.Size(150, 15);
            this.LblClassName.Name = "LblClassName";
            this.LblClassName.Size = new System.Drawing.Size(150, 15);
            this.LblClassName.TabIndex = 2;
            this.LblClassName.Text = "Class name";
            this.LblClassName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(788, 30);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(100, 43);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "Load DLL";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.BtnLoadClick);
            // 
            // lblDllName
            // 
            this.lblDllName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDllName.AutoSize = true;
            this.lblDllName.Location = new System.Drawing.Point(13, 661);
            this.lblDllName.MinimumSize = new System.Drawing.Size(100, 18);
            this.lblDllName.Name = "lblDllName";
            this.lblDllName.Size = new System.Drawing.Size(100, 18);
            this.lblDllName.TabIndex = 5;
            this.lblDllName.Text = "                       ";
            this.lblDllName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loadingImg
            // 
            this.loadingImg.Image = global::ReflexionLibsUI.Properties.Resources.loading;
            this.loadingImg.Location = new System.Drawing.Point(788, 94);
            this.loadingImg.Name = "loadingImg";
            this.loadingImg.Size = new System.Drawing.Size(128, 128);
            this.loadingImg.TabIndex = 6;
            this.loadingImg.TabStop = false;
            this.loadingImg.Visible = false;
            // 
            // listBoxClassName
            // 
            this.listBoxClassName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxClassName.FormattingEnabled = true;
            this.listBoxClassName.Location = new System.Drawing.Point(16, 30);
            this.listBoxClassName.Name = "listBoxClassName";
            this.listBoxClassName.Size = new System.Drawing.Size(380, 602);
            this.listBoxClassName.TabIndex = 7;
            this.listBoxClassName.Click += new System.EventHandler(this.ListBoxClassNameClick);
            // 
            // listBoxMethods
            // 
            this.listBoxMethods.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxMethods.FormattingEnabled = true;
            this.listBoxMethods.Location = new System.Drawing.Point(402, 30);
            this.listBoxMethods.Name = "listBoxMethods";
            this.listBoxMethods.Size = new System.Drawing.Size(380, 602);
            this.listBoxMethods.TabIndex = 8;
            // 
            // DisplayType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBoxMethods);
            this.Controls.Add(this.listBoxClassName);
            this.Controls.Add(this.loadingImg);
            this.Controls.Add(this.lblDllName);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.LblClassName);
            this.Name = "DisplayType";
            this.Size = new System.Drawing.Size(950, 700);
            ((System.ComponentModel.ISupportInitialize)(this.loadingImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblClassName;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblDllName;
        private System.Windows.Forms.PictureBox loadingImg;
        private System.Windows.Forms.ListBox listBoxClassName;
        private System.Windows.Forms.ListBox listBoxMethods;
    }
}
