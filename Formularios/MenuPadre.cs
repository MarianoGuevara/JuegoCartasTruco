using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formularios
{
    public partial class MenuPadre : Form
    {
        protected SoundPlayer MusicaFondo;
        protected bool MusicaActivada;
        public MenuPadre()
        {
            InitializeComponent();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            
            this.ShowIcon = false;
            this.Text = "Menu";

            this.MusicaActivada = true;
            this.MusicaFondo = new SoundPlayer("../../../../media/sounds/maharanjan_partida.wav");
        }

        #region Animaciones
        [DebuggerStepThrough]
        private void AnimacionCartas(Label lbl, bool hover = true)
        {
            int x = lbl.Location.X;
            int y;

            if (hover) y = lbl.Location.Y - 5;
            else y = lbl.Location.Y + 5;

            lbl.Location = new Point(x, y);
        }

        [DebuggerStepThrough]
        private void AsignarHover(System.Windows.Forms.Label label, bool hover = false)
        {
            if (hover)
            {
                this.AnimacionCartas(label);
                FontFamily f = new FontFamily("Century Gothic");
                label.Font = new Font(f, 21F, FontStyle.Bold);
            }
            else
            {
                this.AnimacionCartas(label, false);
                FontFamily f = new FontFamily("Century Gothic");
                label.Font = new Font(f, 20F, FontStyle.Regular);
            }
        }
        protected void Menu_MouseEnter(object sender, EventArgs e) { if (sender is System.Windows.Forms.Label lbl) AsignarHover(lbl, true); }
        protected void Menu_MouseLeave(object sender, EventArgs e) { if (sender is System.Windows.Forms.Label lbl) AsignarHover(lbl, false); }
        #endregion
        private void pbVolumen_Click(object sender, EventArgs e)
        {
            this.MusicaActivada = !this.MusicaActivada;
            if (this.MusicaActivada)
            {
                this.MusicaFondo.PlayLooping();
                this.pbVolumen.Image = Image.FromFile("../../../../media/soundON.png");
            }
            else
            {
                this.MusicaFondo.Stop();
                this.pbVolumen.Image = Image.FromFile("../../../../media/soundOFF.png");
            }
        }
    }
}
