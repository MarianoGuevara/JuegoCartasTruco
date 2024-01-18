using Entidades;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Reflection.Emit;
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
        private JugadorIA rival;

        private event DelegadoComenzarJuego EventoComenzarJuego;

        private string ganadorActual;
        private bool manoYo;
        private bool parda;
        private bool habilitado = true;

        private Ronda rondaActual;

        private Carta cartaYo;
        private Carta cartaRival;

        public Partida()
        {
            InitializeComponent();
            //this.pbDialogoRival.Image = Image.FromFile("../../../../media/dialogos/quiero.jpg");

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowIcon = false;
            this.Text = "Partida Truco";
            this.parda = false;

            this.yo = new Jugador();
            this.rival = new JugadorIA(this.yo);

            this.rondaActual = new Ronda(this.yo, this.rival);

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

        #region Repartir cartas; rival y yo
        private void RepartirCartasRival()
        {
            this.rival.ComenzarJugador(this.yo);

            this.pbCartaRival1.Image = Image.FromFile("../../../../media/cartas/REVERSO.png");
            this.pbCartaRival2.Image = Image.FromFile("../../../../media/cartas/REVERSO.png");
            this.pbCartaRival3.Image = Image.FromFile("../../../../media/cartas/REVERSO.png");
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
        private async void IniciarRonda()
        {
            this.yo.cantoTruco = false;
            this.rondaActual.ResetRonda();

            this.lblMazo.Enabled = true;
            this.lblTruco.Enabled = true;
            this.lblTruco.Text = "TRUCO";

            this.manoYo = !this.manoYo;

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

            this.EventoComenzarJuego.Invoke();

            if (this.manoYo == false && rival.CartasJugadas == 0)
            {
                await Task.Delay(2000);
                this.cartaRival = this.JuegaRival(this.cartaYo);
            }
        }

        #endregion

        #region clicks cartas
        private bool HabilitarClick()
        {
            bool juego = false;

            if (this.manoYo == false)
            {
                if (this.rival.CartasJugadas == 0) juego = false;
                else if (this.habilitado) juego = true;
            }
            else if (this.habilitado) juego = true;

            return juego;
        }
        private void pbCartaPropia1_Click(object sender, EventArgs e)
        {
            bool juego = this.HabilitarClick();
            if (juego) this.LogicaJuego(this.pbCartaPropia1);
        }
        private void pbCartaPropia2_Click(object sender, EventArgs e)
        {
            bool juego = this.HabilitarClick();
            if (juego) this.LogicaJuego(this.pbCartaPropia2);
        }
        private void pbCartaPropia3_Click(object sender, EventArgs e)
        {
            bool juego = this.HabilitarClick();
            if (juego) this.LogicaJuego(this.pbCartaPropia3);
        }
        #endregion

        #region Jugar cartas
        public async void LogicaJuego(PictureBox cartaAJugar)
        {
            this.habilitado = false;

            if (this.manoYo)
            {
                if (this.ganadorActual == "gano" || yo.CartasJugadas == 0)
                {
                    this.cartaYo = this.JuegoYo(cartaAJugar);
                    await Task.Delay(2000);
                    this.cartaRival = this.JuegaRival(this.cartaYo);
                }
                else this.cartaYo = this.JuegoYo(cartaAJugar);
            }
            else
            {
                if (this.ganadorActual == "perdio" || this.yo.CartasJugadas == 0) this.cartaYo = this.JuegoYo(cartaAJugar);
                else
                {
                    this.cartaYo = this.JuegoYo(cartaAJugar);
                    if (this.parda == false)
                    {
                        await Task.Delay(2000);
                        this.cartaRival = this.JuegaRival(this.cartaYo);
                    }

                }
            }

            // Hasta aca tengo 2 cartas en mesa, haya jugado yo primero o el rival.
            this.ganadorActual = Jugador.CartaVsCarta(this.cartaYo, this.cartaRival);
            this.rondaActual.DarPuntoPorMano(this.ganadorActual);

            if (this.ganadorActual == "empato")
            {
                if (this.yo.PuntosRondaActual > 0 || this.rival.PuntosRondaActual > 0)
                {

                    if (this.rival.PuntosRondaActual > 0) Puntaje.SumarPuntaje(this.rival, this.rondaActual.SumaPuntaje);
                    else Puntaje.SumarPuntaje(this.yo, this.rondaActual.SumaPuntaje); ;

                    await Task.Delay(2000);
                    this.IniciarRonda();

                }
                else this.parda = true;
            }

            if (this.parda)
            {
                if (this.ganadorActual == "gano") Puntaje.SumarPuntaje(this.yo, this.rondaActual.SumaPuntaje);
                else if (this.ganadorActual == "perdio") Puntaje.SumarPuntaje(this.rival, this.rondaActual.SumaPuntaje);

                if (this.ganadorActual != "empato")
                {
                    this.parda = false;
                    await Task.Delay(2000);
                    this.IniciarRonda();
                }

                if (this.manoYo == false)
                {
                    await Task.Delay(2000);
                    this.cartaRival = this.JuegaRival(this.cartaYo);
                }
            }
            else
            {
                if (this.yo.PuntosRondaActual == 2 || this.rival.PuntosRondaActual == 2)
                {
                    Puntaje.AnalizarPuntaje(this.yo, this.rival, this.rondaActual.SumaPuntaje);
                    await Task.Delay(2000);
                    this.IniciarRonda();
                }
                else
                {
                    if (this.ganadorActual == "perdio" && this.rival.CartasJugadas != 3)
                    {
                        await Task.Delay(2000);
                        this.cartaRival = this.JuegaRival(this.cartaYo);
                    }

                    if (this.yo.CartasJugadas == 3 && this.rival.CartasJugadas == 3)
                    {
                        await Task.Delay(2000);
                        this.IniciarRonda();
                    }
                }
            }

            this.ActualizarPuntajes();
            this.habilitado = true;
        }
        private void ActualizarPuntajes()
        {

            if (this.yo.Puntaje <= 15)
            {
                this.pbPuntajeYo1.Image = Image.FromFile(Puntaje.ImagenPuntaje(this.yo.Puntaje));
                this.pbPuntajeRival1.Image = Image.FromFile(Puntaje.ImagenPuntaje(this.rival.Puntaje));
            }
            else
            {
                this.pbPuntajeYo2.Image = Image.FromFile(Puntaje.ImagenPuntaje(this.yo.Puntaje));
                this.pbPuntajeRival2.Image = Image.FromFile(Puntaje.ImagenPuntaje(this.rival.Puntaje));
            }
            //this.lblPuntajePropio.Text = this.yo.Puntaje.ToString();
            //this.lblPuntajeRival.Text = this.rival.Puntaje.ToString();
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
                    break;
            }
            this.yo.CartasJugadas += 1;
            cartaYo = this.yo.Cartas[indiceCoincidencia];
            return cartaYo;
        }


        private Carta JuegaRival(Carta cartaYo)
        {
            Carta cartaRival;

            if (rival.CartasJugadas != 0 && this.rondaActual.PuedeCantar(this.rival))
            {
                this.RivalTruco(cartaYo);
            }

            int indice;
            indice = this.rival.IndicePanio(this.yo);

            if (indice != -1) indice = this.rival.JugarInteligente(cartaYo);
            else
            {
                indice = this.rival.JugarInteligente(null);
            }

            cartaRival = this.rival.Cartas[indice];
            this.rival.Cartas[indice] = null;

            if (rival.CartasJugadas == 0) this.ModificarEstadoCartaJugada(this.pbCartaRival1, this.pbCartaRivalPanio1, cartaRival);
            else if (rival.CartasJugadas == 1) this.ModificarEstadoCartaJugada(this.pbCartaRival2, this.pbCartaRivalPanio2, cartaRival);
            else if (rival.CartasJugadas == 2) this.ModificarEstadoCartaJugada(this.pbCartaRival3, this.pbCartaRivalPanio3, cartaRival);

            this.rival.CartasJugadas += 1;

            return cartaRival;
        }

        private void ModificarEstadoCartaJugada(PictureBox pb, PictureBox pbPanio, Carta carta = null)
        {
            if (carta is not null) pbPanio.Image = Image.FromFile(carta.ToString());
            else pbPanio.Image = Image.FromFile(pb.Tag.ToString());

            pb.Tag = null;
            pb.Image = null;
            pb.Enabled = false;
        }
        #endregion

        #region PseudoAnimaciones
        private async void DialogoRival(string imagenDialogo)
        {
            pbDialogoRival.Image = Image.FromFile(imagenDialogo);
            await Task.Delay(3500);
            pbDialogoRival.Image = null;
            await Task.Delay(3500);
        }
        private void ModificarEstadoBotones(bool desactivar = false)
        {
            this.lblEnvido.Enabled = true;
            this.lblTruco.Enabled = true;
            this.lblMazo.Enabled = true;

            if (desactivar)
            {
                this.lblEnvido.Enabled = false;
                this.lblTruco.Enabled = false;
                this.lblMazo.Enabled = false;
            }
        }

        #endregion
        private async void lblMazo_Click(object sender, EventArgs e)
        {
            this.ModificarEstadoBotones(true);
            bool juego = this.HabilitarClick();
            if (juego)
            {
                this.lblMazo.Enabled = false;
                await Task.Delay(2000);
                this.rival.Puntaje += 1;
                this.ActualizarPuntajes();
                this.IniciarRonda();
            }
            this.ModificarEstadoBotones();
        }

        #region Truco
        private async void lblTruco_Click(object sender, EventArgs e)
        {
            this.ModificarEstadoBotones(true);

            bool juego = this.HabilitarClick();
            if (juego)
            {
                await Task.Delay(2000);

                if (this.rondaActual.TrucoBoton())
                {
                    this.DialogoRival($"../../../../media/dialogos/quiero.jpg");
                    this.lblTruco.Text = Puntaje.TrucoTexto(this.rondaActual.EstadoTruco);
                }
                else
                {
                    this.DialogoRival($"../../../../media/dialogos/noQuiero.jpg");
                    this.yo.Puntaje += this.rondaActual.SumaPuntaje;
                    await Task.Delay(2000);
                    this.ActualizarPuntajes();
                    this.IniciarRonda();
                }
            }

            this.ModificarEstadoBotones();
            this.ActualizarBotonTruco();
        }
        private void ActualizarBotonTruco()
        {
            if (rondaActual.EstadoTruco == "no")
            {
                this.lblTruco.Text = "TRUCO";
            }
            else if (rondaActual.EstadoTruco == "truco")
            {
                this.lblTruco.Text = "RETRUCO";
                if (this.yo.cantoTruco) this.lblTruco.Enabled = false;
            }
            else if (rondaActual.EstadoTruco == "retruco")
            {
                this.lblTruco.Text = "VALE CUATRO";
                if (this.yo.cantoTruco == false) this.lblTruco.Enabled = false;
            }
        }
        private async void RivalTruco(Carta carta)
        {
            if (rondaActual.TrucoBoton(true, carta))
            {
                this.DialogoRival($"../../../../media/dialogos/{this.rondaActual.EstadoTruco}.jpg");
                this.lblTruco.Text = Puntaje.TrucoTexto(this.rondaActual.EstadoTruco);

                QuieroNoQuiero quieroNoQuiero = new QuieroNoQuiero();

                quieroNoQuiero.ShowDialog();

                if (quieroNoQuiero.DialogResult == DialogResult.Cancel)
                {
                    if (this.rondaActual.SumaPuntaje > 1)
                    {
                        this.rival.Puntaje += this.rondaActual.SumaPuntaje - 1;
                    }
                    else this.rival.Puntaje += this.rondaActual.SumaPuntaje;

                    await Task.Delay(2000);
                    this.ActualizarPuntajes();
                    this.IniciarRonda();
                }
            }
        }
        #endregion 
    }
}