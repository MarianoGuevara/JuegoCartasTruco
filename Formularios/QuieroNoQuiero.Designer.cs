namespace Formularios
{
    partial class QuieroNoQuiero
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
            lblQuiero = new Label();
            lblNoQuiero = new Label();
            SuspendLayout();
            // 
            // lblQuiero
            // 
            lblQuiero.AutoSize = true;
            lblQuiero.Font = new Font("Century Gothic", 12F);
            lblQuiero.ForeColor = Color.LightGray;
            lblQuiero.Location = new Point(119, 103);
            lblQuiero.Name = "lblQuiero";
            lblQuiero.Size = new Size(105, 30);
            lblQuiero.TabIndex = 28;
            lblQuiero.Text = "QUIERO";
            lblQuiero.Click += lblQuiero_Click;
            lblQuiero.MouseEnter += Partida_MouseEnter;
            lblQuiero.MouseLeave += Partida_MouseLeave;
            // 
            // lblNoQuiero
            // 
            lblNoQuiero.AutoSize = true;
            lblNoQuiero.Font = new Font("Century Gothic", 12F);
            lblNoQuiero.ForeColor = Color.LightGray;
            lblNoQuiero.Location = new Point(326, 103);
            lblNoQuiero.Name = "lblNoQuiero";
            lblNoQuiero.Size = new Size(151, 30);
            lblNoQuiero.TabIndex = 29;
            lblNoQuiero.Text = "NO QUIERO";
            lblNoQuiero.Click += lblNoQuiero_Click;
            lblNoQuiero.MouseEnter += Partida_MouseEnter;
            lblNoQuiero.MouseLeave += Partida_MouseLeave;
            // 
            // QuieroNoQuiero
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 50, 59);
            ClientSize = new Size(594, 239);
            Controls.Add(lblNoQuiero);
            Controls.Add(lblQuiero);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "QuieroNoQuiero";
            Text = "QuieroNoQuiero";
            FormClosing += QuieroNoQuiero_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        protected Label lblQuiero;
        protected Label lblNoQuiero;
    }
}