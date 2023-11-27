using Entidades;
using System.Diagnostics;
using System.Windows.Forms;
using TrucoJuego;

namespace Formularios
{
    public delegate void DelegadoComenzarJuego();
    public delegate void DelegadoJuegoRival(PictureBox pb, PictureBox pbPanio);
    public partial class Partida : Form
    {
        private Jugador yo;
        private Jugador rival;
        private event DelegadoComenzarJuego EventoComenzarJuego;
        private CancellationToken cancelarFlujo;
        private CancellationTokenSource fuenteDeCancelacion;
        private string ganadorActual;
        private bool manoYo;
        private Carta cartaYo;
        private Carta cartaRival;


        public Partida()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowIcon = false;
            this.Text = "Partida Truco";

            this.ganadorActual = string.Empty;
            this.manoYo = true;

            this.EventoComenzarJuego += new DelegadoComenzarJuego(RepartirCartasYo);
            this.EventoComenzarJuego += new DelegadoComenzarJuego(RepartirCartasRival);
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
        private void IniciarRonda()
        {
            this.yo.CartasJugadas = 0;
            this.rival.CartasJugadas = 0;

            this.pbCartaPropiaPanio1.Image = null;
            this.pbCartaPropiaPanio2.Image = null;
            this.pbCartaPropiaPanio3.Image = null;

            this.pbCartaRivalPanio1.Image = null;
            this.pbCartaRivalPanio2.Image = null;
            this.pbCartaRivalPanio3.Image = null;

            this.RepartirCartasYo();
            this.RepartirCartasRival();
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
        private void JugarCartaPropia(PictureBox cartaAJugar)
        {
            Task taskRival;

            if (this.manoYo)
            {
                if (this.ganadorActual == "gano" || yo.CartasJugadas == 0)
                {
                    this.cartaYo = this.JuegoYo(cartaAJugar);
                    this.cartaRival = this.JuegaRival();
                }
                else //(this.ganadorActual == "perdio")
                {
                    this.cartaYo = this.JuegoYo(cartaAJugar);
                }
            }
            else
            {
                if (this.ganadorActual == "perdio" || rival.CartasJugadas == 0)
                {
                    cartaRival = this.JuegaRival();
                    cartaYo = this.JuegoYo(cartaAJugar);
                }
                else
                {
                    cartaYo = this.JuegoYo(cartaAJugar);
                    cartaRival = this.JuegaRival();
                }

            }
            this.ganadorActual = Jugador.CartaVsCarta(cartaYo, cartaRival);
            if (this.ganadorActual == "perdio") this.cartaRival = this.JuegaRival();
            if (this.yo.CartasJugadas == 3 && this.rival.CartasJugadas == 3) this.IniciarRonda();
        }

        private Carta JuegoYo(PictureBox cartaAJugar)
        {
            Carta cartaYo;
            int indiceCoincidencia = -1;

            switch (yo.CartasJugadas)
            {
                case 0:
                    indiceCoincidencia = this.yo.CoincidirCartaConJugador(cartaAJugar.Tag.ToString());

                    this.ModificarEstadoCartaJugada(cartaAJugar, this.pbCartaPropiaPanio1);

                    break;
                case 1:
                    indiceCoincidencia = this.yo.CoincidirCartaConJugador(cartaAJugar.Tag.ToString());

                    this.ModificarEstadoCartaJugada(cartaAJugar, this.pbCartaPropiaPanio2);

                    break;
                case 2:
                    indiceCoincidencia = this.yo.CoincidirCartaConJugador(cartaAJugar.Tag.ToString());

                    this.ModificarEstadoCartaJugada(cartaAJugar, this.pbCartaPropiaPanio3);

                    this.manoYo = !this.manoYo;
                    break;
            }
            this.yo.CartasJugadas += 1;
            cartaYo = this.yo.Cartas[indiceCoincidencia];
            return cartaYo;
        } 

        private Carta JuegaRival()
        {
            Task taskRival;
            Carta cartaRival = null;

            switch (rival.CartasJugadas)
            {
                case 0:
                    taskRival = Task.Run(() => this.ModificarEstadoCartaJugadaRival(this.pbCartaRival1, this.pbCartaRivalPanio1));
                    
                    cartaRival = this.rival.Cartas[0];
                    break;
                case 1:
                    taskRival = Task.Run(() => this.ModificarEstadoCartaJugadaRival(this.pbCartaRival2, this.pbCartaRivalPanio2));
                    
                    cartaRival = this.rival.Cartas[1];
                    break;
                case 2:
                    taskRival = Task.Run(() => this.ModificarEstadoCartaJugadaRival(this.pbCartaRival3, this.pbCartaRivalPanio3));

                    cartaRival = this.rival.Cartas[2];
                    break;
            }
            this.rival.CartasJugadas += 1;
            return cartaRival;
        }

        private void ModificarEstadoCartaJugada(PictureBox pb, PictureBox pbPanio)
        {
            if (pbPanio.InvokeRequired)
            {
                DelegadoJuegoRival d = new DelegadoJuegoRival(ModificarEstadoCartaJugada);
                object[] arrayParametros = { pb, pbPanio };

                pb.Invoke(d, arrayParametros);
            }
            else
            {
                pbPanio.Image = Image.FromFile(pb.Tag.ToString());
                pb.Tag = null;
                pb.Image = null;
                pb.Enabled = false;
            }
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