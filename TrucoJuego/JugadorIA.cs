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

        #region JugarInteligenteIA cartas

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

        #region JugarInteligenteIA pensar si acepta truco
        private int valorMinimoEsperado(string situacionAJugar)
        {
            int valorMinimo = -1;

            switch (this.cartasJugadas)
            {
                case 0:
                    if (situacionAJugar == "truco") valorMinimo = 18;
                    else if (situacionAJugar == "retruco") valorMinimo = 21;
                    else if (situacionAJugar == "valeCuatro") valorMinimo = 24;
                    break;
                case 1:
                    if (situacionAJugar == "truco") valorMinimo = 14;
                    else if (situacionAJugar == "retruco") valorMinimo = 17;
                    else if (situacionAJugar == "valeCuatro") valorMinimo = 20;
                    break;
                case 2:
                    if (situacionAJugar == "truco") valorMinimo = 9;
                    else if (situacionAJugar == "retruco" || situacionAJugar == "valeCuatro") valorMinimo = 11;
                    break;
            }
            return valorMinimo;
        }

        private List<int> PuntajesCartas()
        {
            int puntajeCarta1 = Jugador.AsignarPuntajeCarta(base.cartas[0]);
            int puntajeCarta2 = Jugador.AsignarPuntajeCarta(base.cartas[1]);
            int puntajeCarta3 = Jugador.AsignarPuntajeCarta(base.cartas[2]);

            List<int> l = new List<int> { puntajeCarta1, puntajeCarta2, puntajeCarta3 };

            return l;
        }

        public int PuntajeCartas()
        {
            int puntajeFinal = 0;

            List<int> l = this.PuntajesCartas();

            foreach (int i in l)
            {
                if (i != -1) puntajeFinal += i;
            }
            return puntajeFinal;
        }
        public bool AceptaTruco(string situacionAJugar)
        {
            bool retorno = false;
            int puntajeFinal = this.PuntajeCartas();

            if (puntajeFinal >= this.valorMinimoEsperado(situacionAJugar)) retorno = true;
            return retorno;
        }
        #endregion

        #region  JugarInteligenteIA canta truco

        public string CantarTruco(string situacionAJugar)
        {
            string retorno = "";
            if (this.AceptaTruco(situacionAJugar))
            {
                if (situacionAJugar == "no")
                {
                    situacionAJugar = "truco";
                    retorno = "truco";
                }
                    
                else if (situacionAJugar == "truco")
                {
                    situacionAJugar = "retruco";
                    retorno = "retruco";
                }

                else if (situacionAJugar == "retruco")
                {
                    //situacionAJugar = "truco";
                    retorno = "valeCuatro";
                } 
            }
            return retorno;
        }

        #endregion
    }
}