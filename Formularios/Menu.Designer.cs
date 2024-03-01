namespace Formularios
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            lblJugar = new Label();
            lblPerfil = new Label();
            lblHistorial = new Label();
            lblSalir = new Label();
            pbLogo = new PictureBox();
            pbVolumen = new PictureBox();
            lblInfo = new Label();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbVolumen).BeginInit();
            SuspendLayout();
            // 
            // lblJugar
            // 
            lblJugar.AutoSize = true;
            lblJugar.Font = new Font("Century Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblJugar.ForeColor = Color.LightGray;
            lblJugar.Location = new Point(165, 279);
            lblJugar.Name = "lblJugar";
            lblJugar.Size = new Size(154, 49);
            lblJugar.TabIndex = 15;
            lblJugar.Text = "JUGAR";
            lblJugar.Click += lblJugar_Click;
            lblJugar.MouseEnter += Menu_MouseEnter;
            lblJugar.MouseLeave += Menu_MouseLeave;
            // 
            // lblPerfil
            // 
            lblPerfil.AutoSize = true;
            lblPerfil.Font = new Font("Century Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPerfil.ForeColor = Color.LightGray;
            lblPerfil.Location = new Point(173, 379);
            lblPerfil.Name = "lblPerfil";
            lblPerfil.Size = new Size(139, 49);
            lblPerfil.TabIndex = 16;
            lblPerfil.Text = "PERFIL";
            lblPerfil.Click += lblPerfil_Click;
            lblPerfil.MouseEnter += Menu_MouseEnter;
            lblPerfil.MouseLeave += Menu_MouseLeave;
            // 
            // lblHistorial
            // 
            lblHistorial.AutoSize = true;
            lblHistorial.Font = new Font("Century Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHistorial.ForeColor = Color.LightGray;
            lblHistorial.Location = new Point(137, 481);
            lblHistorial.Name = "lblHistorial";
            lblHistorial.Size = new Size(211, 49);
            lblHistorial.TabIndex = 17;
            lblHistorial.Text = "HISTORIAL";
            lblHistorial.Click += lblHistorial_Click;
            lblHistorial.MouseEnter += Menu_MouseEnter;
            lblHistorial.MouseLeave += Menu_MouseLeave;
            // 
            // lblSalir
            // 
            lblSalir.AutoSize = true;
            lblSalir.Font = new Font("Century Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSalir.ForeColor = Color.LightGray;
            lblSalir.Location = new Point(181, 580);
            lblSalir.Name = "lblSalir";
            lblSalir.Size = new Size(122, 49);
            lblSalir.TabIndex = 18;
            lblSalir.Text = "SALIR";
            lblSalir.Click += lblSalir_Click;
            lblSalir.MouseEnter += Menu_MouseEnter;
            lblSalir.MouseLeave += Menu_MouseLeave;
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
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInfo.ForeColor = Color.LightGray;
            lblInfo.Location = new Point(133, 729);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(219, 30);
            lblInfo.TabIndex = 21;
            lblInfo.Text = "Juego Truco 2024";
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 50, 59);
            ClientSize = new Size(485, 802);
            Controls.Add(lblInfo);
            Controls.Add(pbVolumen);
            Controls.Add(pbLogo);
            Controls.Add(lblSalir);
            Controls.Add(lblHistorial);
            Controls.Add(lblPerfil);
            Controls.Add(lblJugar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Menu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu";
            MouseEnter += Menu_MouseEnter;
            MouseLeave += Menu_MouseLeave;
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbVolumen).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblJugar;
        private Label lblPerfil;
        private Label lblHistorial;
        private Label lblSalir;
        private PictureBox pbLogo;
        private PictureBox pbVolumen;
        private Label lblInfo;
    }
}