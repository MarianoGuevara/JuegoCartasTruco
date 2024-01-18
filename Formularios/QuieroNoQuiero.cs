using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formularios
{
    public partial class QuieroNoQuiero : Form
    {
        private bool apretoBoton;
        public QuieroNoQuiero()
        {
            InitializeComponent();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowIcon = false;
            this.Text = "Decide...";

            this.apretoBoton = false;
        }

        #region Mismas animaciones botones
        [DebuggerStepThrough]
        private void AnimacionCartas(PictureBox pb, bool hover = true)
        {
            int x = pb.Location.X;
            int y;

            if (hover) y = pb.Location.Y - 15;
            else y = pb.Location.Y + 15;

            pb.Location = new Point(x, y);
        }

        [DebuggerStepThrough]
        private void Partida_MouseEnter(object sender, EventArgs e)
        {
            if (sender is PictureBox pb) this.AnimacionCartas(pb, true);
            else if (sender is System.Windows.Forms.Label lbl) AsignarHover(lbl, true);
        }

        [DebuggerStepThrough]
        private void Partida_MouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox pb) this.AnimacionCartas(pb, false);
            else if (sender is System.Windows.Forms.Label lbl) AsignarHover(lbl);
        }

        [DebuggerStepThrough]
        private void AsignarHover(System.Windows.Forms.Label label, bool hover = false)
        {
            if (hover)
            {
                FontFamily f = new FontFamily("Century Gothic");
                label.Font = new Font(f, 14F, FontStyle.Bold);
            }
            else
            {
                FontFamily f = new FontFamily("Century Gothic");
                label.Font = new Font(f, 12F, FontStyle.Regular);
            }
        }
        #endregion

        private void lblQuiero_Click(object sender, EventArgs e)
        {
            this.apretoBoton = true;
            this.DialogResult = DialogResult.OK;
        }

        private void lblNoQuiero_Click(object sender, EventArgs e)
        {
            this.apretoBoton = true;
            this.DialogResult = DialogResult.Cancel;
        }

        private void QuieroNoQuiero_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.apretoBoton == false) e.Cancel = true;
        }
    }
}
