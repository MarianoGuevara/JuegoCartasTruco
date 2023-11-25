using Entidades;
using System.Windows.Forms;
using TrucoJuego;

namespace Formularios
{
    public partial class Partida : Form
    {
        public Partida()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowIcon = false;
            this.Text = "Partida Truco";

            Jugador yo = new Jugador();
            Jugador rival = new Jugador(yo);

            this.pbCartaRival1.Image = Image.FromFile("../../../../media/cartas/REVERSO.png");
            this.pbCartaRival2.Image = Image.FromFile("../../../../media/cartas/REVERSO.png");
            this.pbCartaRival3.Image = Image.FromFile("../../../../media/cartas/REVERSO.png");

            this.pbCartaPropia1.Image = Image.FromFile(yo.Cartas[0].ToString());
            this.pbCartaPropia2.Image = Image.FromFile(yo.Cartas[1].ToString());
            this.pbCartaPropia3.Image = Image.FromFile(yo.Cartas[2].ToString());
        }

       
        private void AnimacionCartas(PictureBox pb, bool hover = true)
        {
            int x = pb.Location.X;
            int y;

            if (hover) y = pb.Location.Y - 15;
            else y = pb.Location.Y + 15;

            pb.Location = new Point(x, y);
        }
        private void Partida_MouseEnter(object sender, EventArgs e)
        {
            if (sender is PictureBox pb) this.AnimacionCartas(pb, true);
        }

        private void Partida_MouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox pb) this.AnimacionCartas(pb, false);
        }
    }
}