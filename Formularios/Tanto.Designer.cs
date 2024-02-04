namespace Formularios
{
    partial class Tanto
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
            lblEnvido = new Label();
            lblReal = new Label();
            lblFalta = new Label();
            SuspendLayout();
            // 
            // lblQuiero
            // 
            lblQuiero.Location = new Point(224, 49);
            lblQuiero.Click += lblQuiero_Click;
            // 
            // lblNoQuiero
            // 
            lblNoQuiero.Location = new Point(395, 49);
            // 
            // lblEnvido
            // 
            lblEnvido.AutoSize = true;
            lblEnvido.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblEnvido.ForeColor = Color.LightGray;
            lblEnvido.Location = new Point(104, 119);
            lblEnvido.Name = "lblEnvido";
            lblEnvido.Size = new Size(106, 30);
            lblEnvido.TabIndex = 30;
            lblEnvido.Text = "ENVIDO";
            lblEnvido.Click += lblEnvido_Click;
            lblEnvido.MouseEnter += Partida_MouseEnter;
            lblEnvido.MouseLeave += Partida_MouseLeave;
            // 
            // lblReal
            // 
            lblReal.AutoSize = true;
            lblReal.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblReal.ForeColor = Color.LightGray;
            lblReal.Location = new Point(278, 119);
            lblReal.Name = "lblReal";
            lblReal.Size = new Size(169, 30);
            lblReal.TabIndex = 31;
            lblReal.Text = "REAL ENVIDO";
            lblReal.Click += lblReal_Click;
            lblReal.MouseEnter += Partida_MouseEnter;
            lblReal.MouseLeave += Partida_MouseLeave;
            // 
            // lblFalta
            // 
            lblFalta.AutoSize = true;
            lblFalta.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblFalta.ForeColor = Color.LightGray;
            lblFalta.Location = new Point(484, 119);
            lblFalta.Name = "lblFalta";
            lblFalta.Size = new Size(181, 30);
            lblFalta.TabIndex = 32;
            lblFalta.Text = "FALTA ENVIDO";
            lblFalta.Click += lblFalta_Click;
            lblFalta.MouseEnter += Partida_MouseEnter;
            lblFalta.MouseLeave += Partida_MouseLeave;
            // 
            // Tanto
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(765, 208);
            Controls.Add(lblFalta);
            Controls.Add(lblReal);
            Controls.Add(lblEnvido);
            Name = "Tanto";
            Text = "Tanto";
            Controls.SetChildIndex(lblQuiero, 0);
            Controls.SetChildIndex(lblNoQuiero, 0);
            Controls.SetChildIndex(lblEnvido, 0);
            Controls.SetChildIndex(lblReal, 0);
            Controls.SetChildIndex(lblFalta, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblEnvido;
        private Label lblReal;
        private Label lblFalta;
    }
}