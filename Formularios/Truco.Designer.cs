namespace Formularios
{
    partial class Truco
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
            lblTruco = new Label();
            lblRetruco = new Label();
            lblValeCuatro = new Label();
            SuspendLayout();
            // 
            // lblQuiero
            // 
            lblQuiero.Location = new Point(168, 39);
            // 
            // lblNoQuiero
            // 
            lblNoQuiero.Location = new Point(306, 39);
            // 
            // lblTruco
            // 
            lblTruco.AutoSize = true;
            lblTruco.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblTruco.ForeColor = Color.LightGray;
            lblTruco.Location = new Point(43, 115);
            lblTruco.Name = "lblTruco";
            lblTruco.Size = new Size(96, 30);
            lblTruco.TabIndex = 31;
            lblTruco.Text = "TRUCO";
            lblTruco.Click += lblTruco_Click;
            lblTruco.MouseEnter += Partida_MouseEnter;
            lblTruco.MouseLeave += Partida_MouseLeave;
            // 
            // lblRetruco
            // 
            lblRetruco.AutoSize = true;
            lblRetruco.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblRetruco.ForeColor = Color.LightGray;
            lblRetruco.Location = new Point(220, 115);
            lblRetruco.Name = "lblRetruco";
            lblRetruco.Size = new Size(124, 30);
            lblRetruco.TabIndex = 32;
            lblRetruco.Text = "RETRUCO";
            lblRetruco.Click += lblRetruco_Click;
            lblRetruco.MouseEnter += Partida_MouseEnter;
            lblRetruco.MouseLeave += Partida_MouseLeave;
            // 
            // lblValeCuatro
            // 
            lblValeCuatro.AutoSize = true;
            lblValeCuatro.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblValeCuatro.ForeColor = Color.LightGray;
            lblValeCuatro.Location = new Point(402, 115);
            lblValeCuatro.Name = "lblValeCuatro";
            lblValeCuatro.Size = new Size(178, 30);
            lblValeCuatro.TabIndex = 33;
            lblValeCuatro.Text = "VALE CUATRO";
            lblValeCuatro.Click += lblValeCuatro_Click;
            lblValeCuatro.MouseEnter += Partida_MouseEnter;
            lblValeCuatro.MouseLeave += Partida_MouseLeave;
            // 
            // Truco
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(622, 181);
            Controls.Add(lblValeCuatro);
            Controls.Add(lblRetruco);
            Controls.Add(lblTruco);
            MaximizeBox = true;
            MinimizeBox = true;
            Name = "Truco";
            Text = "Truco";
            Controls.SetChildIndex(lblQuiero, 0);
            Controls.SetChildIndex(lblNoQuiero, 0);
            Controls.SetChildIndex(lblTruco, 0);
            Controls.SetChildIndex(lblRetruco, 0);
            Controls.SetChildIndex(lblValeCuatro, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTruco;
        private Label lblRetruco;
        private Label lblValeCuatro;
    }
}