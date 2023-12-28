using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrucoJuego;

namespace Entidades
{
    public class JugadorIA : Jugador
    {
        public JugadorIA(Jugador pj) :base(pj) { }
        public JugadorIA() : base() { }

        #region JugarInteligenteIA

        public int IndicePanio(Jugador rival)
        {
            int retorno = -1;
            if (rival.CartasJugadas == 1 && base.cartasJugadas == 0) retorno = 1;
            else if (rival.CartasJugadas == 2 && base.cartasJugadas == 1) retorno = 2;
            else if (rival.CartasJugadas == 3 && base.cartasJugadas == 2) retorno = 3;
            return retorno;
        }
        public int JugarInteligente(Carta cartaRival)
        {
            int puntajeRival = Jugador.AsignarPuntajeCarta(cartaRival);

            int carta1 = Jugador.AsignarPuntajeCarta(base.cartas[0]);
            int carta2 = Jugador.AsignarPuntajeCarta(base.cartas[1]);
            int carta3 = Jugador.AsignarPuntajeCarta(base.cartas[2]);

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

            foreach (int i in listaPuntajes)
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
            if (puntaje1 < puntaje2) retorno = puntaje1;
            else retorno = puntaje2;
            return retorno;
        }

        private int NumeroMenor(List<int> listaPuntajes)
        {
            bool bandera = false;
            int menor = -1;
            int indiceMenor = -1;

            for (int i = 0; i < listaPuntajes.Count; i++)
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
