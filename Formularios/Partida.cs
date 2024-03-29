using Entidades;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Media;
using System.Reflection.Emit;
using System.Windows.Forms;
using TrucoJuego;

namespace Formularios
{
    public delegate void DelegadoMusicaFondo();
    public delegate void DelegadoComenzarJuego();
    public delegate void DelegadoComenzarJuegoDos();
    public delegate void DelegadoJuegoRival(PictureBox pb, PictureBox pbPanio);
    public partial class Partida : Form
    {
        private bool estaCerrandose;
        private Persona perfilYo;
        private Persona perfilRival;

        MenuMain menuMain;
        private SoundPlayer efectoCarta;
        private SoundPlayer efectoCambioRonda;
        private bool banderaMusicaActivada;

        private Jugador yo;
        private JugadorIA rival;
        private Jugador rivalScreenshot;

        private event DelegadoComenzarJuego EventoComenzarJuego;

        private string ganadorActual;
        private bool manoYo;
        private bool parda;

        private string turno;
        private bool mazo;

        private Ronda rondaActual;

        private Carta cartaYo;
        private Carta cartaRival;
        private Carta cartaRivalActual;
        private Carta cartaYoActual;
        public Partida(MenuMain menu, Persona perfilYo)
        {
            InitializeComponent();
            this.estaCerrandose = false;
            this.perfilYo = perfilYo;
            this.perfilRival = new Persona("", "");
            this.perfilRival.PersonaRandom();

            this.AsignarNombreYFotoPerfil();

            this.menuMain = menu;
            this.menuMain.Hide();

            this.mazo = false;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowIcon = false;
            this.Text = "Partida Truco";

            this.efectoCarta = new SoundPlayer("media/sounds/tirarCarta.wav");
            this.efectoCambioRonda = new SoundPlayer("media/sounds/coin_roundReset.wav");
            this.banderaMusicaActivada = true;

            this.yo = new Jugador();
            this.rival = new JugadorIA(this.yo);
            this.rivalScreenshot = new Jugador(); // lo uso para el envido 

            this.rondaActual = new Ronda(this.yo, this.rival);

            this.parda = false;
            this.ganadorActual = string.Empty;
            this.manoYo = true;

            if (this.manoYo) this.turno = "yo";
            else this.turno = "rival";

            this.EventoComenzarJuego += new DelegadoComenzarJuego(RepartirCartasYo);
            this.EventoComenzarJuego += new DelegadoComenzarJuego(RepartirCartasRival);
            this.EventoComenzarJuego.Invoke();
            this.rivalScreenshot.Cartas = new List<Carta>(this.rival.Cartas); // copia aca pq se reemplaza la lista d cartas en el invoke
        }

        #region Animaciones
        private void AsignarNombreYFotoPerfil()
        {
            this.lblInfoYo.Text = this.perfilYo.Nombre;
            this.pbFotoYo.Image = Image.FromFile(this.perfilYo.ImagenDireccion);

            this.lblInfoRival.Text = this.perfilRival.Nombre;
            this.pbFotoRival.Image = Image.FromFile(this.perfilRival.ImagenDireccion);
        }

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

            // hardcodear cartas que me tocan
            //Carta carta = new Carta();
            //carta.CartaActual = "media/cartas/6 BASTO.png";
            //Carta carta2 = new Carta();
            //carta2.CartaActual = "media/cartas/1 ORO.png";
            //Carta carta3 = new Carta();
            //carta3.CartaActual = "media/cartas/2 COPA.png";
            //this.rival.Cartas[0] = carta;
            //this.rival.Cartas[1] = carta2;
            //this.rival.Cartas[2] = carta3;

            this.pbCartaRival1.Image = Image.FromFile("media/cartas/REVERSO.png");
            this.pbCartaRival2.Image = Image.FromFile("media/cartas/REVERSO.png");
            this.pbCartaRival3.Image = Image.FromFile("media/cartas/REVERSO.png");
        }
        private void RepartirCartasYo()
        {
            this.yo.ComenzarJugador();

            // hardcodear cartas que me tocan
            //Carta carta = new Carta();
            //carta.CartaActual = "media/cartas/6 BASTO.png";
            //Carta carta2 = new Carta();
            //carta2.CartaActual = "media/cartas/1 ORO.png";
            //Carta carta3 = new Carta();
            //carta3.CartaActual = "media/cartas/2 COPA.png";
            //this.yo.Cartas[0] = carta;
            //this.yo.Cartas[1] = carta2;
            //this.yo.Cartas[2] = carta3;

            this.pbCartaPropia1.Image = Image.FromFile(yo.Cartas[0].ToString());
            this.pbCartaPropia1.Tag = yo.Cartas[0].ToString();

            this.pbCartaPropia2.Image = Image.FromFile(yo.Cartas[1].ToString());
            this.pbCartaPropia2.Tag = yo.Cartas[1].ToString();

            this.pbCartaPropia3.Image = Image.FromFile(yo.Cartas[2].ToString());
            this.pbCartaPropia3.Tag = yo.Cartas[2].ToString();
        }
        private async Task IniciarRonda()
        {
            this.SonarEfecto(this.efectoCambioRonda);

            await Task.Delay(2000);
            this.ActualizarPuntajes();
            if (this.estaCerrandose == false)
            {
                this.parda = false;

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

                this.mazo = false;
                if (this.manoYo == false && rival.CartasJugadas == 0) this.cartaRival = await this.JuegaRival(this.cartaYo);
            }
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

                if (this.parda) this.cartaRival = await this.JuegaRival(this.cartaYo);
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

            if (!this.mazo || this.parda)
            {
                this.mazo = !this.mazo;

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
                        this.ModificarEstadoBotones(true);

                        if (this.rival.PuntosRondaActual > 0 && this.yo.PuntosRondaActual == 0) Puntaje.SumarPuntaje(this.rival, this.rondaActual.SumaPuntaje);
                        else if (this.yo.PuntosRondaActual > 0 && this.rival.PuntosRondaActual == 0) Puntaje.SumarPuntaje(this.yo, this.rondaActual.SumaPuntaje);
                        else if (this.rondaActual.ganePrimeraYo) Puntaje.SumarPuntaje(this.yo, this.rondaActual.SumaPuntaje);

                        await this.IniciarRonda();
                    }
                    else if (this.yo.CartasJugadas == 3 && this.rival.CartasJugadas == 3)
                    {
                        this.ModificarEstadoBotones(true);
                        this.PardaTotal();
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
                        this.ModificarEstadoBotones(true);
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
            }

            this.ActualizarPuntajes();
            if (this.estaCerrandose == false)
            {
                this.MiTurno();
            }
        }
        private void PardaTotal()
        {
            if (this.manoYo) Puntaje.SumarPuntaje(this.yo, this.rondaActual.SumaPuntaje);
            else Puntaje.SumarPuntaje(this.rival, this.rondaActual.SumaPuntaje);
        }
        private void ActualizarPuntajes()
        {
            if (this.yo.Puntaje >= 30)
            {
                ResultadoTanto r = new ResultadoTanto("yo", 30, this.rival.Puntaje, false);
                r.ShowDialog();
                this.estaCerrandose = true;
                this.DialogResult = DialogResult.OK;
            }
            else if (this.rival.Puntaje >= 30)
            {
                ResultadoTanto r = new ResultadoTanto("rival", this.yo.Puntaje, this.rival.Puntaje, false);
                r.ShowDialog();
                this.estaCerrandose = true;
                this.DialogResult = DialogResult.OK;
            }
            else
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

            if (this.mazo && this.yo.CartasJugadas == 1 && !this.manoYo) this.turno = "yo";
            else this.turno = "rival";

            return cartaYo;
        }
        private async Task<Carta> JuegaRival(Carta cartaYo)
        {
            if (this.estaCerrandose == false)
            {
                Carta cartaRival;
                bool mazo = false;

                if (((this.rival.CartasJugadas == 1 && this.yo.CartasJugadas == 0) ||
                    (this.rival.CartasJugadas == 0 && (this.yo.CartasJugadas == 1 || this.yo.CartasJugadas == 0))) &&
                    (this.rondaActual.envido == false && this.rondaActual.realEnvido == false) && this.rondaActual.faltaEnvido == false &&
                    (!this.rondaActual.truco && !this.rondaActual.retruco && !this.rondaActual.valeCuatro)) await this.RivalCantaTanto(this.rivalScreenshot);

                if (rival.CartasJugadas != 0 && this.rondaActual.PuedeCantar(this.rival))
                {
                    mazo = await this.RivalTruco(this.cartaRivalActual, this.cartaYoActual);
                }

                await Task.Delay(2000);
                this.mazo = mazo;

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
                                                      // la tengo que poner nula una vez que la us�
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
                    return this.cartaRival;
                }
            }
            return this.cartaRival;
        }
        private void ModificarEstadoCartaJugada(PictureBox pb, PictureBox pbPanio, Carta carta = null)
        {
            this.SonarEfecto(this.efectoCarta);

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
            this.lblSalir.Enabled = true;
            this.lblEnvido.Enabled = true;
            this.lblTruco.Enabled = true;
            this.lblMazo.Enabled = true;
            this.pbCartaPropia1.Enabled = true;
            this.pbCartaPropia2.Enabled = true;
            this.pbCartaPropia3.Enabled = true;

            if (desactivar)
            {
                this.lblSalir.Enabled = false;
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
        private async Task<bool> RivalTruco(Carta carta, Carta cartaYo, bool cantoYo = false, bool iniciarRonda = false)
        {
            bool mazo = false;
            string retorno = this.rival.QueCantaTruco(this.rondaActual, this.yo, this.rival, carta, cartaYo);

            if (this.yo.Puntaje == 29 && retorno == "noQuiero") retorno = "quiero";
            else if (this.yo.Puntaje == 28 && this.rondaActual.truco && retorno == "noQuiero") retorno = "quiero";
            else if (this.yo.Puntaje == 27 && (this.rondaActual.truco && this.rondaActual.retruco) && retorno == "noQuiero") retorno = "quiero";

            if ((retorno == "noQuiero" || retorno == "quiero") && cantoYo == false) { }
            else
            {
                this.ModificarEstadoBotones(true);
                await this.DialogoRival($"media/dialogos/{retorno}.jpg");

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

                        if (this.rondaActual.SumaPuntaje > 1) this.rondaActual.SumaPuntaje -= 1;
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
        private async void lblEnvido_Click(object sender, EventArgs e)
        {
            this.ModificarEstadoBotones(true);
            Tanto t = new Tanto(this.rondaActual, this.yo, this.rival, true, this.manoYo);
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
                await this.RivalCantaTanto(this.rivalScreenshot, true);
            }
            else if (t.DialogResult == DialogResult.No) Puntaje.CalcularPuntajeNoQuiero(this.rondaActual, this.rival);

            this.MiTurno();
        }
        private async Task RivalCantaTanto(Jugador rivalScreenshot, bool cantoYo = false)
        {
            string retorno = this.rondaActual.QueCantaTanto(rivalScreenshot);

            if ((retorno == "noQuiero" || retorno == "quiero") && cantoYo == false) { }
            else
            {
                await this.DialogoRival($"media/dialogos/{retorno}.jpg");

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
                    await this.LuchaTanto(true);
                }
                else
                {
                    this.yo.miTurnoTanto = true;
                    Tanto t = new Tanto(this.rondaActual, this.yo, this.rival, this.manoYo);
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
        private async Task LuchaTanto(bool cantoYo = false)
        {
            int tantoYo = this.yo.PuntajeEnvidoNumerico();
            int tantoRival = this.rivalScreenshot.PuntajeEnvidoNumerico();

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


            if (cantoYo && ganador == "yo")
            {
                await this.DialogoRival("media/dialogos/sonBuenas.jpg");
            }
            else
            {
                await Task.Delay(1500);
                ResultadoTanto r = new ResultadoTanto(ganador, tantoYo, tantoRival);
                r.ShowDialog();
                await Task.Delay(1000);
            }
            this.ActualizarPuntajes();
        }
        #endregion

        #region LOAD, CLOSING y mazo; sonido efecto
        private void SonarEfecto(SoundPlayer efecto) { if (this.banderaMusicaActivada) efecto.Play(); }
        private void lblSalir_Click(object sender, EventArgs e) { this.DialogResult = DialogResult.No; }
        private string MensajeSerializacionPartida()
        {
            string resultado = "INCONCLUSO";
            if (this.DialogResult != DialogResult.No)
            {
                if (this.yo.Puntaje >= 30)
                {
                    resultado = "GANADOR";
                    this.yo.Puntaje = 30;
                }
                else if (this.rival.Puntaje >= 30) resultado = "PERDEDOR";
            }
            return $"{resultado} ---> YO:   {this.yo.Puntaje}-{this.rival.Puntaje}  :RIVAL [{this.perfilRival.Nombre}]";
        }
        private void Partida_FormClosing(object sender, FormClosingEventArgs e)
        {
            string serializacion = string.Empty;
            serializacion = this.MensajeSerializacionPartida();
            if (this.DialogResult == DialogResult.No || this.DialogResult == DialogResult.Cancel)
            { 
                CerrarMenu c = new CerrarMenu("Salir de la partida mancha tu historial �Quiere salir igualmente?", "SI", "NO");
                c.ShowDialog();
                if (c.DialogResult == DialogResult.No) { e.Cancel = true; }
                else 
                {
                    Serializadora<string>.SerializarStr(serializacion, "historial.txt");
                    this.estaCerrandose = true;
                    this.menuMain.Show();
                }
            }
            else
            {
                Serializadora<string>.SerializarStr(serializacion, "historial.txt");
                this.estaCerrandose = true;
                this.menuMain.Show();
            }
        }
        private void Partida_Load(object sender, EventArgs e)
        {
        }
        private async void lblMazo_Click(object sender, EventArgs e)
        {
            this.rival.Puntaje += this.rondaActual.SumaPuntosMazo();
            this.ModificarEstadoBotones(true);
            await this.IniciarRonda();
        }
        private void pbVolumen_Click(object sender, EventArgs e)
        {
            this.banderaMusicaActivada = !this.banderaMusicaActivada;
            if (this.banderaMusicaActivada) this.pbVolumen.Image = Image.FromFile("media/soundON.png");
            else this.pbVolumen.Image = Image.FromFile("media/soundOFF.png");
        }
        #endregion
    }
}