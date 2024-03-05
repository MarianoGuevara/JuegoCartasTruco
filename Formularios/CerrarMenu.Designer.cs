namespace Formularios
{
    partial class CerrarMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CerrarMenu));
            lblTxt = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblQuiero
            // 
            lblQuiero.Location = new Point(291, 197);
            lblQuiero.Size = new Size(31, 30);
            lblQuiero.Text = "SI";
            lblQuiero.MouseEnter += CerrarMenu_MouseEnter;
            lblQuiero.MouseLeave += CerrarMenu_MouseLeave;
            // 
            // lblNoQuiero
            // 
            lblNoQuiero.Location = new Point(473, 197);
            lblNoQuiero.Size = new Size(59, 30);
            lblNoQuiero.Text = "NO ";
            lblNoQuiero.MouseEnter += CerrarMenu_MouseEnter;
            lblNoQuiero.MouseLeave += CerrarMenu_MouseLeave;
            // 
            // lblTxt
            // 
            lblTxt.AutoSize = true;
            lblTxt.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTxt.ForeColor = Color.LightGray;
            lblTxt.Location = new Point(11, 127);
            lblTxt.Name = "lblTxt";
            lblTxt.Size = new Size(775, 30);
            lblTxt.TabIndex = 30;
            lblTxt.Text = "¿Realmente desea cerrar la aplicación? ¡Hay mucho por jugar! ";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(323, 27);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(150, 75);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 31;
            pictureBox1.TabStop = false;
            // 
            // CerrarMenu
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 50, 59);
            ClientSize = new Size(796, 260);
            Controls.Add(pictureBox1);
            Controls.Add(lblTxt);
            Name = "CerrarMenu";
            Text = "CerrarMenu";
            FormClosing += CerrarMenu_FormClosing;
            Controls.SetChildIndex(lblQuiero, 0);
            Controls.SetChildIndex(lblNoQuiero, 0);
            Controls.SetChildIndex(lblTxt, 0);
            Controls.SetChildIndex(pictureBox1, 0);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTxt;
        private PictureBox pictureBox1;
    }
}