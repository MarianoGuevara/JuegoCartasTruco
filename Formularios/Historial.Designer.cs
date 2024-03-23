namespace Formularios
{
    partial class Historial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Historial));
            rbHistorial = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            SuspendLayout();
            // 
            // pbLogo
            // 
            pbLogo.Image = (Image)resources.GetObject("pbLogo.Image");
            pbLogo.Location = new Point(95, 49);
            // 
            // rbHistorial
            // 
            rbHistorial.BackColor = Color.FromArgb(45, 50, 59);
            rbHistorial.BorderStyle = BorderStyle.FixedSingle;
            rbHistorial.Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rbHistorial.ForeColor = Color.LightGray;
            rbHistorial.Location = new Point(21, 193);
            rbHistorial.Name = "rbHistorial";
            rbHistorial.Size = new Size(490, 621);
            rbHistorial.TabIndex = 1;
            rbHistorial.Text = "";
            // 
            // Historial
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(533, 826);
            Controls.Add(rbHistorial);
            Name = "Historial";
            Text = "Historial";
            Controls.SetChildIndex(pbLogo, 0);
            Controls.SetChildIndex(rbHistorial, 0);
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox rbHistorial;
    }
}