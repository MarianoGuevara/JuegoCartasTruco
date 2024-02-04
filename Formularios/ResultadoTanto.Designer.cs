namespace Formularios
{
    partial class ResultadoTanto
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
            lblYo = new Label();
            lblRival = new Label();
            label1 = new Label();
            label2 = new Label();
            lblResultado = new Label();
            SuspendLayout();
            // 
            // lblYo
            // 
            lblYo.AutoSize = true;
            lblYo.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblYo.ForeColor = Color.LightGray;
            lblYo.Location = new Point(100, 176);
            lblYo.Name = "lblYo";
            lblYo.Size = new Size(21, 30);
            lblYo.TabIndex = 31;
            lblYo.Text = "-";
            // 
            // lblRival
            // 
            lblRival.AutoSize = true;
            lblRival.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblRival.ForeColor = Color.LightGray;
            lblRival.Location = new Point(339, 176);
            lblRival.Name = "lblRival";
            lblRival.Size = new Size(21, 30);
            lblRival.TabIndex = 32;
            lblRival.Text = "-";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.LightGray;
            label1.Location = new Point(75, 68);
            label1.Name = "label1";
            label1.Size = new Size(66, 30);
            label1.TabIndex = 33;
            label1.Text = "[YO]";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.LightGray;
            label2.Location = new Point(298, 68);
            label2.Name = "label2";
            label2.Size = new Size(95, 30);
            label2.TabIndex = 34;
            label2.Text = "[RIVAL]";
            // 
            // lblResultado
            // 
            lblResultado.AutoSize = true;
            lblResultado.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblResultado.ForeColor = Color.LimeGreen;
            lblResultado.Location = new Point(184, 275);
            lblResultado.Name = "lblResultado";
            lblResultado.Size = new Size(107, 30);
            lblResultado.TabIndex = 35;
            lblResultado.Text = "[RESULT]";
            // 
            // ResultadoTanto
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 50, 59);
            ClientSize = new Size(480, 387);
            Controls.Add(lblResultado);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblRival);
            Controls.Add(lblYo);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ResultadoTanto";
            Text = "ResultadoTanto";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblYo;
        private Label lblRival;
        private Label label1;
        private Label label2;
        private Label lblResultado;
    }
}