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
        private List<Carta> cartas;
        private int cartasJugadas;
        private int puntaje;
        private int puntosRondaActual;

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

        private static int AsignarPuntajeCarta(Carta cartaPropia)
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

        #region JugarInteligenteIA

        public int IndicePanio(Jugador rival)
        {
            int retorno = -1;
            if (rival.cartasJugadas == 1 && this.cartasJugadas == 0) retorno = 1;
            else if (rival.cartasJugadas == 2 && this.cartasJugadas == 1) retorno = 2;
            else if (rival.cartasJugadas == 3 && this.cartasJugadas == 2) retorno = 3;
            return retorno;
        }

        public int JugarInteligente(Carta cartaRival)
        {
            int puntajeRival = Jugador.AsignarPuntajeCarta(cartaRival);

            int carta1 = Jugador.AsignarPuntajeCarta(this.cartas[0]);
            int carta2 = Jugador.AsignarPuntajeCarta(this.cartas[1]);
            int carta3 = Jugador.AsignarPuntajeCarta(this.cartas[2]);

            List<int> list = new List<int> { carta1, carta2, carta3 };

            int cantidadSuperaciones = this.HayCartaMasAlta(list, puntajeRival);

            int indiceFinal = -1;
            switch (cantidadSuperaciones)
            {
                case 0:
                case 3:
                    indiceFinal = this.NumeroMenor(list);
                    break;
                case 1:
                    indiceFinal = this.IndiceSuperacionUnica(list, puntajeRival); 
                    break;
                case 2:
                    indiceFinal = this.IndiceSuperacionDos(list, puntajeRival);
                    break;
            }

            return indiceFinal;
        }

        private int HayCartaMasAlta(List<int> listaPuntajes, int puntajeASuperar)
        {
            int cantidadSuperaciones = 0;

            foreach(int i in listaPuntajes) 
            {
                if (i > puntajeASuperar) cantidadSuperaciones++;
            }

            return cantidadSuperaciones;
        }
        private int IndiceSuperacionUnica(List<int> listaPuntajes, int puntajeASuperar) // 1 SUPERADO
        {
            int mayorIndice = -1;

            for (int i = 0; i < listaPuntajes.Count; i++)
            {
                if (listaPuntajes[i] > puntajeASuperar)
                {
                    mayorIndice = i;
                    break;
                }
            }

            return mayorIndice;
        }
        private int IndiceSuperacionDos(List<int> listaPuntajes, int puntajeASuperar)
        {
            int superacion1 = -1;
            int indiceSuperacion1 = 0;
            int superacion2 = -1;
            int indiceSuperacion2 = 0;

            for (int i = 0; i < listaPuntajes.Count; i++)
            {
                if (listaPuntajes[i] > puntajeASuperar && superacion1 == -1)
                {
                    superacion1 = listaPuntajes[i];
                    indiceSuperacion1 = i;
                }
                else if (listaPuntajes[i] > puntajeASuperar)
                {
                    superacion2 = listaPuntajes[i];
                    indiceSuperacion2 = i;
                }
            }

            int indiceRetorno;
            int menorDeDos = this.DevolverSuperacionMenorDos(superacion1, superacion2);
            if (superacion1 == menorDeDos) indiceRetorno = indiceSuperacion1;
            else indiceRetorno = indiceSuperacion2;

            return indiceRetorno;
        }
        private int DevolverSuperacionMenorDos(int puntaje1, int puntaje2) // !! NO ANDA -> devuelve numero en vez de indice
        {
            int retorno;
            if (puntaje1 < puntaje2 ) retorno = puntaje1;
            else retorno = puntaje2;
            return retorno; 
        }

        private int NumeroMenor(List<int> listaPuntajes)
        {
            bool bandera = false;
            int menor = -1;
            int indiceMenor = -1;

            for (int i=0; i<listaPuntajes.Count; i++)
            {
                if (listaPuntajes[i] != -1 && bandera == false)
                {
                    bandera = true;

                    menor = listaPuntajes[i];
                    indiceMenor = i;
                }
                else if (listaPuntajes[i] != -1 && listaPuntajes[i] < menor)
                {
                    menor = listaPuntajes[i];
                    indiceMenor = i;
                }
            }
            return indiceMenor;
        }

        #endregion
    }
}
