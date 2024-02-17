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
        private Jugador yo;
        private JugadorIA rival;
        private Jugador rivalScreenshot;

        private event DelegadoComenzarJuego EventoComenzarJuego;

        private string ganadorActual;
        private bool manoYo;
        private bool parda;
        //private bool habilitado = true;

        private string turno;

        private Ronda rondaActual;

        private Carta cartaYo;
        private Carta cartaRival;
        private Carta cartaRivalActual;
        private Carta cartaYoActual;
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
            this.rival = new JugadorIA(this.yo);
            this.rivalScreenshot = new Jugador(); // lo uso para el envido 

            this.rondaActual = new Ronda(this.yo, this.rival);

            this.ganadorActual = string.Empty;
            this.manoYo = true;
            
            if (this.manoYo) this.turno = "yo";
            else this.turno= "rival";

            this.EventoComenzarJuego += new DelegadoComenzarJuego(RepartirCartasYo);
            this.EventoComenzarJuego += new DelegadoComenzarJuego(RepartirCartasRival);
            this.EventoComenzarJuego.Invoke();
            this.rivalScreenshot.Cartas = new List<Carta>(this.rival.Cartas); // copia aca pq se reemplaza la lista d cartas en el invoke
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
        private async Task IniciarRonda()
        {
            await Task.Delay(2000);
            this.ActualizarPuntajes();

            this.yo.cantoTruco = false;
            this.rondaActual.ResetRonda();

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
            this.rivalScreenshot.Cartas = new List<Carta>(this.rival.Cartas); // copia 

            this.manoYo = !this.manoYo;
            if (this.manoYo) this.turno = "yo";
            else this.turno = "rival";
            this.MiTurno();

            if (this.manoYo == false && rival.CartasJugadas == 0) this.cartaRival = await this.JuegaRival(this.cartaYo);
        }
        #endregion

        #region clicks cartas
        private void MiTurno()
        {
            if (this.turno == "yo") this.ModificarEstadoBotones();
            else this.ModificarEstadoBotones(true);
        }
        private void pbCartaPropia1_Click(object sender, EventArgs e)
        {
            if (this.pbCartaPropia1.Tag != null)
            {
                this.turno = "rival";
                this.LogicaJuego(this.pbCartaPropia1);
            }
        }
        private void pbCartaPropia2_Click(object sender, EventArgs e)
        {
            if (this.pbCartaPropia2.Tag != null)
            {
                this.turno = "rival";
                this.LogicaJuego(this.pbCartaPropia2);
            }
        }
        private void pbCartaPropia3_Click(object sender, EventArgs e)
        {
            if (this.pbCartaPropia3.Tag != null)
            {
                this.turno = "rival";
                this.LogicaJuego(this.pbCartaPropia3);
            }
        }
        #endregion

        #region Jugar cartas
        public async void LogicaJuego(PictureBox cartaAJugar)
        {
            this.MiTurno();

            if (this.manoYo)
            {
                if (this.ganadorActual == "gano" || yo.CartasJugadas == 0)
                {
                    this.cartaYo = await this.JuegoYo(cartaAJugar);
                    this.cartaRival = await this.JuegaRival(this.cartaYo);
                }
                else this.cartaYo = await this.JuegoYo(cartaAJugar);
            }
            else
            {
                if (this.ganadorActual == "perdio" || this.yo.CartasJugadas == 0) this.cartaYo = await this.JuegoYo(cartaAJugar);
                else
                {
                    this.cartaYo = await this.JuegoYo(cartaAJugar);
                    if (this.parda == false) this.cartaRival = await this.JuegaRival(this.cartaYo);
                }
            }

            this.ganadorActual = Jugador.CartaVsCarta(this.cartaYo, this.cartaRival);
            if (this.ganadorActual == "empato")
            {
                if (this.manoYo) this.turno = "yo";
                else this.turno = "rival";
            }
            else if (this.ganadorActual == "gano") this.turno = "yo";
            else this.turno = "rival";
            this.MiTurno();

            this.rondaActual.DarPuntoPorMano(this.ganadorActual);
            
            if (this.ganadorActual == "empato")
            {
                if (this.yo.PuntosRondaActual > 0 || this.rival.PuntosRondaActual > 0)
                {
                    if (this.rival.PuntosRondaActual > 0) Puntaje.SumarPuntaje(this.rival, this.rondaActual.SumaPuntaje);
                    else Puntaje.SumarPuntaje(this.yo, this.rondaActual.SumaPuntaje); ;

                    await this.IniciarRonda();
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
                    await this.IniciarRonda();
                }

                if (this.manoYo == false)
                {
                    this.cartaRival = await this.JuegaRival(this.cartaYo);
                }
            }
            else
            {
                if (this.yo.PuntosRondaActual == 2 || this.rival.PuntosRondaActual == 2)
                {
                    this.ModificarEstadoBotones(true);
                    Puntaje.AnalizarPuntaje(this.yo, this.rival, this.rondaActual.SumaPuntaje);
                    await this.IniciarRonda();
                }
                else
                {
                    if (this.ganadorActual == "perdio" && this.rival.CartasJugadas != 3)
                    {
                        this.cartaRival = await this.JuegaRival(this.cartaYo);
                    }

                    if (this.yo.CartasJugadas == 3 && this.rival.CartasJugadas == 3)
                    {
                        this.ModificarEstadoBotones(true);
                        await this.IniciarRonda();
                    }
                }
            }

            this.ActualizarPuntajes();
            this.MiTurno();
        }
        private void ActualizarPuntajes()
        {
            if (this.yo.Puntaje <= 15)
            {
                this.pbPuntajeYo2.Image = Image.FromFile(Puntaje.ImagenPuntaje(this.yo.Puntaje));
            }
            else
            {
                this.pbPuntajeYo2.Image = Image.FromFile(Puntaje.ImagenPuntaje(15));
                this.pbPuntajeYo1.Image = Image.FromFile(Puntaje.ImagenPuntaje(this.yo.Puntaje));
            }
            if (this.rival.Puntaje <= 15)
            {
                this.pbPuntajeRival2.Image = Image.FromFile(Puntaje.ImagenPuntaje(this.rival.Puntaje));
            }
            else
            {
                this.pbPuntajeRival2.Image = Image.FromFile(Puntaje.ImagenPuntaje(15));
                this.pbPuntajeRival1.Image = Image.FromFile(Puntaje.ImagenPuntaje(this.rival.Puntaje));
            }
        }
        private async Task<Carta> JuegoYo(PictureBox cartaAJugar)
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
            this.cartaYoActual = cartaYo;
            this.turno = "rival";

            return cartaYo;
        }
        private async Task<Carta> JuegaRival(Carta cartaYo)
        {
            Carta cartaRival;
            bool mazo = false;

            if (((this.rival.CartasJugadas == 1 && this.yo.CartasJugadas == 0) || 
                (this.rival.CartasJugadas == 0 && (this.yo.CartasJugadas == 1 || this.yo.CartasJugadas == 0))) &&
                (this.rondaActual.envido == false && this.rondaActual.realEnvido == false) && this.rondaActual.faltaEnvido == false) await this.RivalCantaTanto(this.rivalScreenshot);

            if (rival.CartasJugadas != 0 && this.rondaActual.PuedeCantar(this.rival))
            {
                mazo = await this.RivalTruco(this.cartaRivalActual, this.cartaYoActual);
            }
            await Task.Delay(2000);
            if (mazo == false)
            {
                int indice;
                indice = this.rival.IndicePanio(this.yo);

                if (indice != -1) indice = this.rival.JugarInteligente(cartaYo);
                else
                {
                    indice = this.rival.JugarInteligente(null);
                }

                cartaRival = this.rivalScreenshot.Cartas[indice]; // pq si le pongo solo rival, se pone null tmb pq es lo mismo en memoria
                //cartaRival = this.rival.Cartas[indice];
                this.rival.Cartas[indice] = null; // lo hago nulo porque tiene que jugar inteligente
                                                  // y eso lo hace viendo su lista de cartas, entonces 
                                                  // la tengo que poner nula una vez que la usé
                this.cartaRivalActual = this.rivalScreenshot.Cartas[indice];

                if (rival.CartasJugadas == 0) this.ModificarEstadoCartaJugada(this.pbCartaRival1, this.pbCartaRivalPanio1, cartaRival);
                else if (rival.CartasJugadas == 1) this.ModificarEstadoCartaJugada(this.pbCartaRival2, this.pbCartaRivalPanio2, cartaRival);
                else if (rival.CartasJugadas == 2) this.ModificarEstadoCartaJugada(this.pbCartaRival3, this.pbCartaRivalPanio3, cartaRival);

                this.rival.CartasJugadas += 1;

                this.turno = "yo";
                if (this.rival.CartasJugadas == 3 && this.yo.CartasJugadas == 3) this.ModificarEstadoBotones(true);
                else this.MiTurno();
                return cartaRival;
            }
            else
            {
                if (this.rival.CartasJugadas == 3 && this.yo.CartasJugadas == 3) this.ModificarEstadoBotones(true);
                else this.MiTurno();
                return null;
            }
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
        private async Task DialogoRival(string imagenDialogo)
        {
            await Task.Delay(2000);
            this.pbDialogoRival.Image = Image.FromFile(imagenDialogo);
            await Task.Delay(2000);
            this.pbDialogoRival.Image = null;
        }
        private void ModificarEstadoBotones(bool desactivar = false)
        {
            this.lblEnvido.Enabled = true;
            this.lblTruco.Enabled = true;
            this.lblMazo.Enabled = true;
            this.pbCartaPropia1.Enabled = true;
            this.pbCartaPropia2.Enabled = true;
            this.pbCartaPropia3.Enabled = true;

            if (desactivar)
            {
                this.lblEnvido.Enabled = false;
                this.lblTruco.Enabled = false;
                this.lblMazo.Enabled = false;
                this.pbCartaPropia1.Enabled = false;
                this.pbCartaPropia2.Enabled = false;
                this.pbCartaPropia3.Enabled = false;
            }
        }
        #endregion

        #region Truco
        private async void lblTruco_Click(object sender, EventArgs e)
        {
            this.ModificarEstadoBotones(true);
            Truco t = new Truco(this.rondaActual, this.yo, true);
            t.ShowDialog();

            #region DialogResult significados
            /* 
            OK -> Quiero
            No-> No Quiero
            Abort -> Truco
            Retry -> Retruco
            Yes -> Vale Cuatro
            Cancel -> Volver
            */
            #endregion

            if (t.DialogResult != DialogResult.Cancel) await this.RivalTruco(this.cartaRivalActual, this.cartaYoActual, true);
            this.MiTurno();
        }
        private async Task<bool> RivalTruco(Carta carta, Carta cartaYo, bool cantoYo=false, bool iniciarRonda=false)
        {
            bool mazo = false;
            string retorno = this.rival.QueCantaTruco(this.rondaActual, this.yo, this.rival, carta,cartaYo);

            if ((retorno == "noQuiero" || retorno == "quiero") && cantoYo == false) { }
            else
            {
                this.ModificarEstadoBotones(true);
                await this.DialogoRival($"../../../../media/dialogos/{retorno}.jpg");

                if (retorno == "noQuiero")
                {
                    mazo = true;
                    if (this.rondaActual.SumaPuntaje > 1) this.rondaActual.SumaPuntaje -= 1;
                    this.yo.Puntaje += this.rondaActual.SumaPuntaje;
                    await this.IniciarRonda();
                }
                else if (retorno != "quiero" && retorno != "")
                {
                    this.yo.miTurnoTruco = true;
                    await Task.Delay(1500);

                    Truco t = new Truco(this.rondaActual, this.yo);
                    t.ShowDialog();

                    if (t.DialogResult == DialogResult.No)
                    {
                        mazo = true;

                        if(this.rondaActual.SumaPuntaje>1) this.rondaActual.SumaPuntaje -= 1;
                        this.rival.Puntaje += this.rondaActual.SumaPuntaje;
                        await this.IniciarRonda();
                    }
                    else if (t.DialogResult != DialogResult.OK)
                    {
                        this.MiTurno();
                        this.yo.miTurnoTruco = false;
                        await this.RivalTruco(this.cartaRivalActual, this.cartaYoActual, true);
                    }
                    else this.yo.miTurnoTruco = true;
                }
                else this.yo.miTurnoTruco = false;
            }
            return mazo;
        }
        #endregion

        #region Envido
        private string DeDialogResultATanto(DialogResult result)
        {
            string tanto = string.Empty;
            if (result == DialogResult.Abort) tanto = "envido";
            else if (result == DialogResult.Retry) tanto = "realEnvido";
            else if (result == DialogResult.Yes) tanto = "faltaEnvido";

            return tanto;
        }
        private async void lblEnvido_Click(object sender, EventArgs e)
        {
            this.ModificarEstadoBotones(true);
            Tanto t = new Tanto(this.rondaActual, this.yo, this.rival, true);
            t.ShowDialog();

            #region DialogResult significados
            /* 
            OK -> Quiero
            No-> No Quiero
            Abort -> Envido
            Retry -> Real envido
            Yes -> Falta envido
            Cancel -> Volver
            */
            #endregion

            if (t.DialogResult != DialogResult.Cancel)
            {
                this.yo.miTurnoTanto = false;
                string aCantar = this.DeDialogResultATanto(t.DialogResult);

                await this.RivalCantaTanto(this.rivalScreenshot, true);
            }
            else if (t.DialogResult == DialogResult.No) Puntaje.CalcularPuntajeNoQuiero(this.rondaActual, this.rival);

            this.MiTurno();
        }
        private async Task RivalCantaTanto(Jugador rivalScreenshot, bool cantoYo = false, bool delay=false)
        {
            string retorno = this.rondaActual.QueCantaTanto(rivalScreenshot);

            if ((retorno == "noQuiero" || retorno == "quiero") && cantoYo == false) { }
            else
            {
                await this.DialogoRival($"../../../../media/dialogos/{retorno}.jpg");

                if (retorno == "noQuiero")
                {
                    if (cantoYo)
                    {
                        Puntaje.CalcularPuntajeNoQuiero(this.rondaActual, this.yo);
                        this.ActualizarPuntajes();
                    }
                }
                else if (retorno == "quiero")
                {
                    if (this.rondaActual.faltaEnvido == true) this.rondaActual.SumaPuntajeTanto = 10;
                    await this.LuchaTanto();
                }
                else
                {
                    this.yo.miTurnoTanto = true;
                    Tanto t = new Tanto(this.rondaActual, this.yo, this.rival); ;
                    t.ShowDialog();

                    if (t.DialogResult == DialogResult.No)
                    {
                        this.yo.miTurnoTanto = false;
                        Puntaje.CalcularPuntajeNoQuiero(this.rondaActual, this.rival);
                        this.ActualizarPuntajes();
                    }
                    else if (t.DialogResult == DialogResult.OK)
                    {
                        this.yo.miTurnoTanto = false;
                        await this.LuchaTanto();
                    }
                    else await this.RivalCantaTanto(this.rivalScreenshot, true);
                }
            }
        }
        private async Task LuchaTanto()
        {
            int tantoYo = this.yo.PuntajeEnvidoNumerico();
            int tantoRival = this.rival.PuntajeEnvidoNumerico();

            string ganador;

            if (tantoYo == tantoRival)
            {
                if (this.manoYo) ganador = "yo";
                else ganador = "rival";
            }
            else if (tantoYo > tantoRival) ganador = "yo";
            else ganador = "rival";

            if (this.rondaActual.SumaPuntajeTanto == 10) Puntaje.FaltaEnvido(this.rondaActual, this.yo, this.rival, ganador);
            else Puntaje.SumarPuntajesTanto(ganador, this.yo, this.rival, this.rondaActual);

            await Task.Delay(1500);
            ResultadoTanto r = new ResultadoTanto(ganador, tantoYo, tantoRival);
            r.ShowDialog();
            await Task.Delay(1000);
            this.ActualizarPuntajes();
        }
        #endregion
        private async void lblMazo_Click(object sender, EventArgs e)
        {
            if (!this.rondaActual.envido && !this.rondaActual.realEnvido && !this.rondaActual.faltaEnvido) this.rival.Puntaje += 2;
            else this.rival.Puntaje += 1;

            this.ModificarEstadoBotones(true);
            await this.IniciarRonda();
        }
    }
}