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
        private List<int> PuntajesCartas()
        {
            int puntajeCarta1 = Jugador.AsignarPuntajeCarta(base.cartas[0]);
            int puntajeCarta2 = Jugador.AsignarPuntajeCarta(base.cartas[1]);
            int puntajeCarta3 = Jugador.AsignarPuntajeCarta(base.cartas[2]);

            List<int> l = new List<int> { puntajeCarta1, puntajeCarta2, puntajeCarta3 };

            return l;
        }

        public int PuntajeCartas(Carta carta3=null)
        {
            int puntajeFinal = 0;

            List<int> l = this.PuntajesCartas();

            foreach (int i in l)
            {
                if (i != -1) puntajeFinal += i;
            }

            if (puntajeFinal == 0)
            {
                puntajeFinal = Jugador.AsignarPuntajeCarta(carta3);
            }

            return puntajeFinal;
        }
        private string ValorMinimo(int puntaje, int param1, int param2, int param3, Jugador yo, JugadorIA rival)
        {
            string situacion;

            if (rival.PuntosRondaActual == 1 && yo.PuntosRondaActual == 0)
            {
                param1 -= 2;
                param2 -= 2;
                param3 -= 2;
            }

            if (puntaje < param1) situacion = "noQuiero";
            else if (puntaje <= param2) situacion = "truco";
            else if (puntaje <= param3) situacion = "retruco";
            else situacion = "valeCuatro";
            return situacion;
        }
        private string MentiraTruco(string rivalSituacion) // cambia el retorno en algunos casos para mentir
        {
            string retorno = rivalSituacion;
            Random r = new Random();

            int randomNum = r.Next(1, 11); // del 1 al 10
            if (randomNum <= 3) // si el random es 1, multiplica mucho la apuesta. Si es 2 o 3,la sube 1 nivel
            {
                if (rivalSituacion == "noQuiero") retorno = "truco";
                else if (rivalSituacion == "truco") retorno = "retruco";
                else if (rivalSituacion == "retruco") retorno = "valeCuatro";
            }
            return retorno;
        }
        private string situacionAJugar(int puntaje, Jugador yo, JugadorIA rival)
        {
            string situacion = "noQuiero";

            switch (this.cartasJugadas)
            {
                case 0:
                    situacion = this.ValorMinimo(puntaje, 23, 26, 29, yo, rival);
                    break;
                case 1:
                    situacion = this.ValorMinimo(puntaje, 18, 21, 24, yo, rival);
                    break;
                case 2:
                    situacion = this.ValorMinimo(puntaje, 9, 10, 11, yo, rival);
                    break;
                case 3:
                    situacion = this.ValorMinimo(puntaje, 7, 8, 9, yo, rival);
                    break;
            }
            return situacion;
        }
        public string QueCantaTruco(Ronda ronda, Jugador yo, JugadorIA rival, Carta cartaRival=null, Carta cartaYo = null)
        {
            string aCantar;
            int puntajeFinal = this.PuntajeCartas(cartaRival);

            aCantar = this.situacionAJugar(puntajeFinal, yo, rival);
            aCantar = this.MentiraTruco(aCantar);

            if (yo.CartasJugadas == 3 && rival.cartasJugadas == 2)
            {
                int indiceNoNulo = this.IndiceNoNulo();
                cartaRival = rival.cartas[indiceNoNulo];
                aCantar = this.SituacionUltimaCarta(cartaRival, cartaYo, ronda);
                aCantar = this.MentiraTruco(aCantar);
            }
            else
            {
                switch (aCantar)
                {
                    case "truco":
                        if (ronda.truco) aCantar = "quiero";
                        else
                        {
                            ronda.truco = true;
                            rival.cantoTruco = true;
                            aCantar = "truco";
                            ronda.EstadoTruco = "truco";
                            ronda.SumaPuntaje = 3;
                        }
                        break;
                    case "retruco":
                        if (ronda.retruco) aCantar = "quiero";
                        else
                        {
                            if (ronda.truco == false)
                            {
                                ronda.truco = true;
                                rival.cantoTruco = true;
                                aCantar = "truco";
                                ronda.EstadoTruco = "truco";
                                ronda.SumaPuntaje = 2;
                            }
                            else
                            {
                                aCantar = "retruco";
                                ronda.retruco = true;
                                ronda.EstadoTruco = "retruco";
                                ronda.SumaPuntaje = 3;
                            }

                        }
                        break;
                    case "valeCuatro":
                        if (ronda.valeCuatro) aCantar = "quiero";
                        else
                        {
                            if (ronda.truco == false)
                            {
                                ronda.truco = true;
                                rival.cantoTruco = true;
                                aCantar = "truco";
                                ronda.EstadoTruco = "truco";
                                ronda.SumaPuntaje = 2;
                            }
                            else if (ronda.retruco == false && ronda.PuedeCantar(rival))
                            {
                                aCantar = "retruco";
                                ronda.retruco = true;
                                ronda.EstadoTruco = "retruco";
                                ronda.SumaPuntaje = 3;
                            }
                            else
                            {
                                ronda.valeCuatro = true;
                                aCantar = "valeCuatro";
                                ronda.EstadoTruco = "valeCuatro";
                                ronda.SumaPuntaje = 4;
                            }
                        }
                        break;
                }
            }
            return aCantar;
        }
        private string SituacionUltimaCarta(Carta carta, Carta cartaYo, Ronda ronda)
        {
            string retorno = "noQuiero";

            int puntajeYo = Jugador.AsignarPuntajeCarta(cartaYo);
            int puntajeRival = Jugador.AsignarPuntajeCarta(carta);

            if (puntajeRival > puntajeYo)
            {
                if (ronda.truco == false)
                {
                    this.cantoTruco = true;
                    ronda.truco = true;
                    retorno = "truco";
                }
                    
                else if(ronda.truco && this.cantoTruco==false && ronda.retruco==false)
                {
                    ronda.retruco = true;
                    retorno = "retruco";
                }
                else if (ronda.truco && this.cantoTruco && ronda.retruco)
                {
                    ronda.valeCuatro = true;
                    retorno = "valeCuatro";
                }
            }
            return retorno;
        }

        private int IndiceNoNulo()
        {
            int carta1 = Jugador.AsignarPuntajeCarta(base.cartas[0]);
            int carta2 = Jugador.AsignarPuntajeCarta(base.cartas[1]);
            int carta3 = Jugador.AsignarPuntajeCarta(base.cartas[2]);

            List<int> list = new List<int> { carta1, carta2, carta3 };

            for (int i=0; i!=list.Count ;i++)
            {
                if (list[i] != -1) return i;
            }
            return -1;
        }

        #endregion
    }
}