using Entidades;
using System.Diagnostics;
using System.Windows.Forms;
using TrucoJuego;

namespace Formularios
{
    public delegate void ComenzarJuego();
    public partial class Partida : Form
    {
        private Jugador yo;
        private Jugador rival;
        private event ComenzarJuego EventoComenzarJuego;
        private CancellationToken cancelarFlujo;
        private CancellationTokenSource fuenteDeCancelacion;

        public Partida()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowIcon = false;
            this.Text = "Partida Truco";

            this.EventoComenzarJuego += new ComenzarJuego(RepartirCartasYo);
            this.EventoComenzarJuego += new ComenzarJuego(RepartirCartasRival);
            this.EventoComenzarJuego.Invoke();

        }

        #region Animaciones
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
        }
        [DebuggerStepThrough]
        private void Partida_MouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox pb) this.AnimacionCartas(pb, false);
        }



        #endregion

        #region Repartir cartas; rival y yo
        private void RepartirCartasRival()
        {
            this.rival = new Jugador(yo);

            this.pbCartaRival1.Image = Image.FromFile("../../../../media/cartas/REVERSO.png");
            this.pbCartaRival1.Tag = rival.Cartas[0].ToString();

            this.pbCartaRival2.Image = Image.FromFile("../../../../media/cartas/REVERSO.png");
            this.pbCartaRival2.Tag = rival.Cartas[1].ToString();

            this.pbCartaRival3.Image = Image.FromFile("../../../../media/cartas/REVERSO.png");
            this.pbCartaRival3.Tag = rival.Cartas[2].ToString();
        }

        private void RepartirCartasYo()
        {
            this.yo = new Jugador();

            this.pbCartaPropia1.Image = Image.FromFile(yo.Cartas[0].ToString());
            this.pbCartaPropia1.Tag = yo.Cartas[0].ToString();

            this.pbCartaPropia2.Image = Image.FromFile(yo.Cartas[1].ToString());
            this.pbCartaPropia2.Tag = yo.Cartas[1].ToString();

            this.pbCartaPropia3.Image = Image.FromFile(yo.Cartas[2].ToString());
            this.pbCartaPropia3.Tag = yo.Cartas[2].ToString();
        }
        #endregion

        #region clicks cartas
        private void pbCartaPropia1_Click(object sender, EventArgs e)
        {
            this.JugarCartaPropia(this.pbCartaPropia1);
        }
        private void pbCartaPropia2_Click(object sender, EventArgs e)
        {
            this.JugarCartaPropia(this.pbCartaPropia2);
        }
        private void pbCartaPropia3_Click(object sender, EventArgs e)
        {
            this.JugarCartaPropia(this.pbCartaPropia3);
        }
        #endregion

        #region Jugar cartas
        private void JugarCartaPropia(PictureBox pb)
        {
            Task taskTiempo;
            switch (yo.CartasJugadas)
            {
                case 0:
                    this.ModificarEstadoCartaJugada(pb, this.pbCartaPropiaPanio1);
                    yo.CartasJugadas += 1;

                    taskTiempo = Task.Run(() => this.ModificarEstadoCartaJugadaRival(this.pbCartaRival1, this.pbCartaRivalPanio1));
                    break;
                case 1:
                    this.ModificarEstadoCartaJugada(pb, this.pbCartaPropiaPanio2);
                    yo.CartasJugadas += 1;

                    taskTiempo = Task.Run(() => this.ModificarEstadoCartaJugadaRival(this.pbCartaRival2, this.pbCartaRivalPanio2));
                    break;
                case 2:
                    this.ModificarEstadoCartaJugada(pb, this.pbCartaPropiaPanio3);

                    taskTiempo = Task.Run(() => this.ModificarEstadoCartaJugadaRival(this.pbCartaRival3, this.pbCartaRivalPanio3));
                    break;
            }
        }

        //private void JugarCartaRival(PictureBox pb, PictureBox pbPanio)
        //{
        //    this.ModificarEstadoCartaJugada(p)
        //    pb.Image = ria
        //}

        private void ModificarEstadoCartaJugada(PictureBox pb, PictureBox pbPanio)
        {
            pbPanio.Image = Image.FromFile(pb.Tag.ToString());
            pb.Tag = null;
            pb.Image = null;
            pb.Enabled = false;
        }

        private void ModificarEstadoCartaJugadaRival(PictureBox pb, PictureBox pbPanio)
        {
            Thread.Sleep(2000);
            this.ModificarEstadoCartaJugada(pb, pbPanio);
            this.fuenteDeCancelacion = new CancellationTokenSource();
            this.cancelarFlujo = this.fuenteDeCancelacion.Token;
        }


        #endregion
    }
}