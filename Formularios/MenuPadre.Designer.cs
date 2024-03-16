namespace Formularios
{
    partial class MenuPadre
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPadre));
            pbLogo = new PictureBox();
            pbVolumen = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbVolumen).BeginInit();
            SuspendLayout();
            // 
            // pbLogo
            // 
            pbLogo.Location = new Point(68, 65);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(342, 125);
            pbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pbLogo.TabIndex = 19;
            pbLogo.TabStop = false;
            // 
            // pbVolumen
            // 
            pbVolumen.Image = (Image)resources.GetObject("pbVolumen.Image");
            pbVolumen.Location = new Point(32, 714);
            pbVolumen.Name = "pbVolumen";
            pbVolumen.Size = new Size(68, 60);
            pbVolumen.SizeMode = PictureBoxSizeMode.Zoom;
            pbVolumen.TabIndex = 20;
            pbVolumen.TabStop = false;
            pbVolumen.Click += pbVolumen_Click;
            // 
            // MenuPadre
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 50, 59);
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(485, 802);
            Controls.Add(pbVolumen);
            Controls.Add(pbLogo);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MenuPadre";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu";
            MouseEnter += Menu_MouseEnter;
            MouseLeave += Menu_MouseLeave;
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbVolumen).EndInit();
            ResumeLayout(false);
        }

        #endregion
        protected PictureBox pbLogo;
        protected PictureBox pbVolumen;
    }
}