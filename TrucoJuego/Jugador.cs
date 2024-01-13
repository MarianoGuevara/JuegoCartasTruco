using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TrucoJuego;
using static System.Net.Mime.MediaTypeNames;

namespace Entidades
{
    public class Jugador
    {
        protected List<Carta> cartas;
        protected int cartasJugadas;
        protected int puntaje;
        protected int puntosRondaActual;
        public bool cantoTruco;

        #region Propiedades
        public List<Carta> Cartas { get { return this.cartas; } }
        public int CartasJugadas 
        {
            get { return this.cartasJugadas; } 
            set { this.cartasJugadas = value; }
        }
        public int Puntaje
        {
            get { return this.puntaje; }
            set { this.puntaje = value; }
        }
        public int PuntosRondaActual
        {
            get { return this.puntosRondaActual; }
            set { this.puntosRondaActual = value; }
        }
        #endregion
        public Jugador(Jugador? jugadorBis = null)
        {
            this.cantoTruco = false;
            this.puntaje = 0;
            if (jugadorBis is null) this.ComenzarJugador();
            else this.ComenzarJugador(jugadorBis);
        }

        public void ComenzarJugador()
        {
            this.cartas = new List<Carta>();
            this.cartasJugadas = 0;
            this.CartasJugador();
        }
        public void ComenzarJugador(Jugador jugadorBis)
        {
            this.cartas = new List<Carta>();
            this.cartasJugadas = 0;
            this.CartasJugador(jugadorBis);
        }

        #region Añadir y asignar cartas
        private void CartasJugador()
        {
            this.cartas.Add(this.AsignarCarta());
            this.cartas.Add(this.AsignarCarta());
            this.cartas.Add(this.AsignarCarta());
        }

        private void CartasJugador(Jugador yo)
        {
            this.cartas.Add(this.AsignarCarta(yo));
            this.cartas.Add(this.AsignarCarta(yo));
            this.cartas.Add(this.AsignarCarta(yo));
        }

        private Carta AsignarCarta()
        {
            Carta cartaObjeto = new Carta();
            do
            {
                cartaObjeto.DefinirCarta();
            } while (this.CartaUnica(cartaObjeto) == false);

            return cartaObjeto;
        }

        private Carta AsignarCarta(Jugador player)
        {
            Carta cartaObjeto = new Carta();
            do
            {
                cartaObjeto.DefinirCarta();

            } while ((this.CartaUnica(player, cartaObjeto) == false) || (this.CartaUnica(cartaObjeto) == false));

            return cartaObjeto;
        }
        #endregion

        #region Verificar carta unica
        private bool CartaUnica(Jugador j1, Carta c1)
        {
            bool retorno = true;
            foreach(Carta c in j1.Cartas) 
            {
                if (c.Equals(c1))
                {
                    retorno = false;
                    break;
                }
            }
            return retorno;
        }
        private bool CartaUnica(Carta c1)
        {
            bool retorno = true;
            foreach (Carta c in this.Cartas)
            {
                if (c.Equals(c1))
                {
                    retorno = false;
                    break;
                }
            }
            return retorno;
        }

        public int CoincidirCartaConJugador(string carta)
        {
            int indice = -1;
            for (int i=0; i<this.cartas.Count; i++)
            {
                if (this.cartas[i].CartaActual == carta)
                {
                    indice = i;
                    break;
                }
            }
            return indice;
        }

        #endregion

        #region Duelo de cartas; logica ganador por valor 
        public static string CartaVsCarta(Carta cartaPropia, Carta cartaRival)
        {
            string estadoRonda;

            int puntajePropio = Jugador.AsignarPuntajeCarta(cartaPropia);
            int puntajeRival = Jugador.AsignarPuntajeCarta(cartaRival);

            if (puntajePropio > puntajeRival) estadoRonda = "gano";
            else if (puntajePropio < puntajeRival) estadoRonda = "perdio";
            else estadoRonda = "empato";
            return estadoRonda;
        }

        public static int AsignarPuntajeCarta(Carta cartaPropia)
        {
            string carta;
            if (cartaPropia is null) carta = "null";
            else carta = cartaPropia.ToString();
            int puntaje;

            if (carta == "null") puntaje = -1;
            else if (Regex.IsMatch(carta, @"\b1 ESPADA")) puntaje = 14;
            else if (Regex.IsMatch(carta, @"\b1 BASTO")) puntaje = 13;
            else if (Regex.IsMatch(carta, @"\b7 ESPADA")) puntaje = 12;
            else if (Regex.IsMatch(carta, @"\b7 ORO")) puntaje = 11;
            else if (Regex.IsMatch(carta, @"\b3 [A-Z]")) puntaje = 10;
            else if (Regex.IsMatch(carta, @"\b2 [A-Z]")) puntaje = 9;
            else if (Regex.IsMatch(carta, @"\b1 COPA") || Regex.IsMatch(carta, @"\b1 ORO")) puntaje = 8;
            else if (Regex.IsMatch(carta, @"\b12 [A-Z]")) puntaje = 7;
            else if (Regex.IsMatch(carta, @"\b11 [A-Z]")) puntaje = 6;
            else if (Regex.IsMatch(carta, @"\b10 [A-Z]")) puntaje = 5;
            else if (Regex.IsMatch(carta, @"\b7 BASTO") || Regex.IsMatch(carta, @"\b7 COPA")) puntaje = 4;
            else if (Regex.IsMatch(carta, @"\b6 [A-Z]")) puntaje = 3;
            else if (Regex.IsMatch(carta, @"\b5 [A-Z]")) puntaje = 2;
            else puntaje = 1;

            return puntaje;
        }
        #endregion

        //public bool AceptaTruco(string acepta)
        //{
        //    if (acepta == "quiero")
        //}
    }
}
