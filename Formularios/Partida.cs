using Entidades;
using System.Diagnostics;
using System.Windows.Forms;
using TrucoJuego;

namespace Formularios
{
    public delegate void DelegadoComenzarJuego();
    public delegate void DelegadoComenzarJuegoDos();
    public delegate void DelegadoJuegoRival(PictureBox pb, PictureBox pbPanio);
    public partial class Partida : Form
    {
        //private CancellationToken cancelarFlujo;
        //private CancellationTokenSource fuenteDeCancelacion;
        private Jugador yo;
        private Jugador rival;
        private event DelegadoComenzarJuego EventoComenzarJuego;
        private string ganadorActual;
        private bool manoYo;
        private bool parda;
        private Carta cartaYo;
        private Carta cartaRival;
        private bool habilitado = true;

        public Partida()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowIcon = false;
            this.Text = "Partida Truco";
            this.parda = false;

            this.yo = new Jugador();
            this.rival = new Jugador(this.yo);

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
            this.rival.ComenzarJugador(this.yo);

            this.pbCartaRival1.Image = Image.FromFile("../../../../media/cartas/REVERSO.png");
            this.pbCartaRival1.Tag = rival.Cartas[0].ToString();

            this.pbCartaRival2.Image = Image.FromFile("../../../../media/cartas/REVERSO.png");
            this.pbCartaRival2.Tag = rival.Cartas[1].ToString();

            this.pbCartaRival3.Image = Image.FromFile("../../../../media/cartas/REVERSO.png");
            this.pbCartaRival3.Tag = rival.Cartas[2].ToString();
        }

        private void RepartirCartasYo()
        {
            this.yo.ComenzarJugador();

            this.pbCartaPropia1.Image = Image.FromFile(yo.Cartas[0].ToString());
            this.pbCartaPropia1.Tag = yo.Cartas[0].ToString();

            this.pbCartaPropia2.Image = Image.FromFile(yo.Cartas[1].ToString());
            this.pbCartaPropia2.Tag = yo.Cartas[1].ToString();

            this.pbCartaPropia3.Image = Image.FromFile(yo.Cartas[2].ToString());
            this.pbCartaPropia3.Tag = yo.Cartas[2].ToString();
        }
        private void IniciarRonda()
        {
            this.yo.PuntosRondaActual = 0;
            this.rival.PuntosRondaActual = 0;
            this.yo.CartasJugadas = 0;
            this.rival.CartasJugadas = 0;

            pbCartaPropia1.Enabled = true;
            pbCartaPropia2.Enabled = true;
            pbCartaPropia3.Enabled = true;

            this.pbCartaPropiaPanio1.Image = null;
            this.pbCartaPropiaPanio2.Image = null;
            this.pbCartaPropiaPanio3.Image = null;

            this.pbCartaRivalPanio1.Image = null;
            this.pbCartaRivalPanio2.Image = null;
            this.pbCartaRivalPanio3.Image = null;

            this.ganadorActual = string.Empty;
            this.manoYo = true;

            this.EventoComenzarJuego.Invoke();
        }

        #endregion

        #region clicks cartas
        private void pbCartaPropia1_Click(object sender, EventArgs e)
        {
            if (this.habilitado) this.JugarCartaPropia(this.pbCartaPropia1);
        }
        private void pbCartaPropia2_Click(object sender, EventArgs e)
        {
            if (this.habilitado) this.JugarCartaPropia(this.pbCartaPropia2);
        }
        private void pbCartaPropia3_Click(object sender, EventArgs e)
        {
            if (this.habilitado) this.JugarCartaPropia(this.pbCartaPropia3);
        }
        #endregion

        #region Jugar cartas
        private async void JugarCartaPropia(PictureBox cartaAJugar)
        {
            this.habilitado = false;

            if (this.manoYo)
            {
                if (this.ganadorActual == "gano" || yo.CartasJugadas == 0)
                {
                    this.cartaYo = this.JuegoYo(cartaAJugar);
                    await Task.Delay(2000);
                    this.cartaRival = this.JuegaRival();
                }
                else this.cartaYo = this.JuegoYo(cartaAJugar);
            }
            else
            {
                if (this.ganadorActual == "perdio" || rival.CartasJugadas == 0)
                {
                    await Task.Delay(2000);
                    cartaRival = this.JuegaRival();
                    cartaYo = this.JuegoYo(cartaAJugar);
                }
                else
                {
                    cartaYo = this.JuegoYo(cartaAJugar);
                    await Task.Delay(2000);
                    cartaRival = this.JuegaRival();
                }
            }

            // Hasta aca tengo 2 cartas en mesa, haya jugado yo primero o el rival.
            this.ganadorActual = Jugador.CartaVsCarta(cartaYo, cartaRival);
            this.DarPuntoPorMano(this.ganadorActual);

            if (this.ganadorActual == "empato")
            {
                if (this.yo.PuntosRondaActual > 0 || this.rival.PuntosRondaActual > 0)
                {
                    if (this.rival.PuntosRondaActual > 0) this.rival.Puntaje += 1;
                    else this.yo.Puntaje += 1;
                    
                    await Task.Delay(2000);
                    this.IniciarRonda();
                    
                }
                else this.parda = true;
            }

            if (this.parda)
            {
                //this.parda = false;

                //cartaYo = this.JuegoYo(cartaAJugar);
                //await Task.Delay(2000);
                //cartaRival = this.JuegaRival();

                if (this.ganadorActual == "gano") this.yo.Puntaje += 1;
                else if (this.ganadorActual == "perdio") this.rival.Puntaje += 1;

                if (this.ganadorActual != "empato")
                {
                    this.parda = false;
                    await Task.Delay(2000);
                    this.IniciarRonda();
                }
            }
            else
            {
                if (yo.PuntosRondaActual == 2 || rival.PuntosRondaActual == 2)
                {
                    this.AnalizarPuntaje();
                    await Task.Delay(2000);
                    this.IniciarRonda();
                }
                else
                {
                    if (this.ganadorActual == "perdio" && this.rival.CartasJugadas != 3)
                    {
                        await Task.Delay(2000);
                        this.cartaRival = this.JuegaRival();
                    }

                    if (this.yo.CartasJugadas == 3 && this.rival.CartasJugadas == 3)
                    {
                        await Task.Delay(2000);
                        this.IniciarRonda();
                        //taskRival = Task.Run(() => this.RepartirNuevamente());
                    }
                }
            }

            this.ActualizarPuntajes();
            this.habilitado = true;
        }

        private void AnalizarPuntaje()
        {
            if (yo.PuntosRondaActual == 2)
            {
                yo.Puntaje += 1;
            }
            else if (rival.PuntosRondaActual == 2)
            {
                rival.Puntaje += 1;
            }
        }
        private void ActualizarPuntajes()
        {
            this.lblPuntajePropio.Text = this.yo.Puntaje.ToString();
            this.lblPuntajeRival.Text = this.rival.Puntaje.ToString();
        }
        private void DarPuntoPorMano(string ganadorActual)
        {
            switch (ganadorActual)
            {
                case "gano":
                    yo.PuntosRondaActual += 1;
                    break;
                case "perdio":
                    rival.PuntosRondaActual += 1;
                    break;
                //case "empato":
                //    yo.PuntosRondaActual = -1;
                //    break;
            }

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
            Carta cartaRival = null;

            switch (rival.CartasJugadas)
            {
                case 0:
                
                    this.ModificarEstadoCartaJugada(this.pbCartaRival1, this.pbCartaRivalPanio1);
                    cartaRival = this.rival.Cartas[0];
                    break;
                case 1:
         
                    this.ModificarEstadoCartaJugada(this.pbCartaRival2, this.pbCartaRivalPanio2);
                    cartaRival = this.rival.Cartas[1];
                    break;
                case 2:

                    this.ModificarEstadoCartaJugada(this.pbCartaRival3, this.pbCartaRivalPanio3);
                    cartaRival = this.rival.Cartas[2];
                    break;
            }
            this.rival.CartasJugadas += 1;
            return cartaRival;
        }

        private void ModificarEstadoCartaJugada(PictureBox pb, PictureBox pbPanio)
        {
            pbPanio.Image = Image.FromFile(pb.Tag.ToString());
            pb.Tag = null;
            pb.Image = null;
            pb.Enabled = false;
        }
        #endregion
    }
}