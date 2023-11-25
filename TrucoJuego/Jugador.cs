using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrucoJuego;
using static System.Net.Mime.MediaTypeNames;

namespace Entidades
{
    public class Jugador
    {
        private List<Carta> cartas;

        public List<Carta> Cartas { get { return this.cartas; } }

        public Jugador(Jugador jugadorBis = null)
        {
            this.cartas = new List<Carta>();

            if (jugadorBis is not null) this.CartasJugador(jugadorBis);
            else this.CartasJugador();
        }
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

            } while (this.CartaUnica(player, cartaObjeto) == false && this.CartaUnica(cartaObjeto) == false);

            return cartaObjeto;
        }

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
    }
}
