namespace Formularios
{
    partial class MenuPerfil
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPerfil));
            lblVolver = new Label();
            lblHistorial = new Label();
            pbPerfil = new PictureBox();
            txtNombre = new TextBox();
            lblCambioImagen = new Label();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPerfil).BeginInit();
            SuspendLayout();
            // 
            // pbLogo
            // 
            pbLogo.Image = (Image)resources.GetObject("pbLogo.Image");
            pbLogo.Location = new Point(20, 33);
            pbLogo.Size = new Size(418, 229);
            // 
            // lblVolver
            // 
            lblVolver.AutoSize = true;
            lblVolver.Font = new Font("Century Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblVolver.ForeColor = Color.LightGray;
            lblVolver.Location = new Point(142, 548);
            lblVolver.Name = "lblVolver";
            lblVolver.Size = new Size(174, 49);
            lblVolver.TabIndex = 22;
            lblVolver.Text = "VOLVER";
            lblVolver.Click += lblVolver_Click;
            lblVolver.MouseEnter += MenuPerfil_MouseEnter;
            lblVolver.MouseLeave += MenuPerfil_MouseLeave;
            // 
            // lblHistorial
            // 
            lblHistorial.AutoSize = true;
            lblHistorial.Font = new Font("Century Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHistorial.ForeColor = Color.LightGray;
            lblHistorial.Location = new Point(82, 483);
            lblHistorial.Name = "lblHistorial";
            lblHistorial.Size = new Size(294, 49);
            lblHistorial.TabIndex = 23;
            lblHistorial.Text = "VER HISTORIAL";
            lblHistorial.Click += lblHistorial_Click;
            lblHistorial.MouseEnter += MenuPerfil_MouseEnter;
            lblHistorial.MouseLeave += MenuPerfil_MouseLeave;
            // 
            // pbPerfil
            // 
            pbPerfil.Location = new Point(30, 285);
            pbPerfil.Name = "pbPerfil";
            pbPerfil.Size = new Size(138, 110);
            pbPerfil.SizeMode = PictureBoxSizeMode.Zoom;
            pbPerfil.TabIndex = 24;
            pbPerfil.TabStop = false;
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(45, 50, 59);
            txtNombre.Font = new Font("Century Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNombre.ForeColor = Color.LightGray;
            txtNombre.Location = new Point(185, 317);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(251, 57);
            txtNombre.TabIndex = 20;
            // 
            // lblCambioImagen
            // 
            lblCambioImagen.AutoSize = true;
            lblCambioImagen.Font = new Font("Century Gothic", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCambioImagen.ForeColor = Color.LightGray;
            lblCambioImagen.Location = new Point(40, 398);
            lblCambioImagen.Name = "lblCambioImagen";
            lblCambioImagen.Size = new Size(119, 25);
            lblCambioImagen.TabIndex = 25;
            lblCambioImagen.Text = "CAMBIAR";
            lblCambioImagen.Click += lblCambioImagen_Click;
            lblCambioImagen.MouseEnter += MenuPerfil_MouseEnter;
            lblCambioImagen.MouseLeave += MenuPerfil_MouseLeave;
            // 
            // MenuPerfil
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(459, 641);
            Controls.Add(lblCambioImagen);
            Controls.Add(txtNombre);
            Controls.Add(pbPerfil);
            Controls.Add(lblHistorial);
            Controls.Add(lblVolver);
            Name = "MenuPerfil";
            Text = "MenuPerfil";
            FormClosing += MenuPerfil_FormClosing;
            MouseEnter += MenuPerfil_MouseEnter;
            MouseLeave += MenuPerfil_MouseLeave;
            Controls.SetChildIndex(pbLogo, 0);
            Controls.SetChildIndex(lblVolver, 0);
            Controls.SetChildIndex(lblHistorial, 0);
            Controls.SetChildIndex(pbPerfil, 0);
            Controls.SetChildIndex(txtNombre, 0);
            Controls.SetChildIndex(lblCambioImagen, 0);
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPerfil).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblVolver;
        private Label lblHistorial;
        private PictureBox pbPerfil;
        private TextBox txtNombre;
        private Label lblCambioImagen;
    }
}