﻿namespace Formularios
{
    partial class MenuMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuMain));
            lblJugar = new Label();
            lblPerfil = new Label();
            lblTutorial = new Label();
            lblSalir = new Label();
            ((System.ComponentModel.ISupportInitialize)pbVolumen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            SuspendLayout();
            // 
            // pbVolumen
            // 
            pbVolumen.Location = new Point(204, 709);
            // 
            // pbLogo
            // 
            pbLogo.Image = (Image)resources.GetObject("pbLogo.Image");
            pbLogo.Location = new Point(46, 47);
            pbLogo.Size = new Size(385, 221);
            // 
            // lblJugar
            // 
            lblJugar.AutoSize = true;
            lblJugar.Font = new Font("Century Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblJugar.ForeColor = Color.LightGray;
            lblJugar.Location = new Point(161, 386);
            lblJugar.Name = "lblJugar";
            lblJugar.Size = new Size(154, 49);
            lblJugar.TabIndex = 21;
            lblJugar.Text = "JUGAR";
            lblJugar.Click += lblJugar_Click;
            lblJugar.MouseEnter += MenuMain_MouseEnter;
            lblJugar.MouseLeave += MenuMain_MouseLeave;
            // 
            // lblPerfil
            // 
            lblPerfil.AutoSize = true;
            lblPerfil.Font = new Font("Century Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPerfil.ForeColor = Color.LightGray;
            lblPerfil.Location = new Point(169, 454);
            lblPerfil.Name = "lblPerfil";
            lblPerfil.Size = new Size(139, 49);
            lblPerfil.TabIndex = 22;
            lblPerfil.Text = "PERFIL";
            lblPerfil.Click += lblPerfil_Click;
            lblPerfil.MouseEnter += MenuMain_MouseEnter;
            lblPerfil.MouseLeave += MenuMain_MouseLeave;
            // 
            // lblTutorial
            // 
            lblTutorial.AutoSize = true;
            lblTutorial.Font = new Font("Century Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTutorial.ForeColor = Color.LightGray;
            lblTutorial.Location = new Point(62, 518);
            lblTutorial.Name = "lblTutorial";
            lblTutorial.Size = new Size(353, 49);
            lblTutorial.TabIndex = 23;
            lblTutorial.Text = "¿CÓMO JUGAR?";
            lblTutorial.Click += lblTutorial_Click;
            lblTutorial.MouseEnter += MenuMain_MouseEnter;
            lblTutorial.MouseLeave += MenuMain_MouseLeave;
            // 
            // lblSalir
            // 
            lblSalir.AutoSize = true;
            lblSalir.Font = new Font("Century Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSalir.ForeColor = Color.LightGray;
            lblSalir.Location = new Point(177, 580);
            lblSalir.Name = "lblSalir";
            lblSalir.Size = new Size(122, 49);
            lblSalir.TabIndex = 24;
            lblSalir.Text = "SALIR";
            lblSalir.Click += lblSalir_Click;
            lblSalir.MouseEnter += MenuMain_MouseEnter;
            lblSalir.MouseLeave += MenuMain_MouseLeave;
            // 
            // MenuMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(476, 794);
            Controls.Add(lblSalir);
            Controls.Add(lblTutorial);
            Controls.Add(lblPerfil);
            Controls.Add(lblJugar);
            Name = "MenuMain";
            Text = "MenuMain";
            FormClosing += MenuMain_FormClosing;
            Load += MenuMain_Load;
            Controls.SetChildIndex(pbLogo, 0);
            Controls.SetChildIndex(pbVolumen, 0);
            Controls.SetChildIndex(lblJugar, 0);
            Controls.SetChildIndex(lblPerfil, 0);
            Controls.SetChildIndex(lblTutorial, 0);
            Controls.SetChildIndex(lblSalir, 0);
            ((System.ComponentModel.ISupportInitialize)pbVolumen).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblJugar;
        private Label lblPerfil;
        private Label lblTutorial;
        private Label lblSalir;
    }
}