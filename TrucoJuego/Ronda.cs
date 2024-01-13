using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using TrucoJuego;

namespace Entidades
{
    public delegate void DelegadoComenzarJuego();
    public delegate void DelegadoComenzarJuegoDos();

    public class Ronda
    {
        private Jugador yo;
        private JugadorIA rival;
        public bool truco;
        public bool retruco;
        public bool valeCuatro;

        private int sumaPuntaje;

        private string estadoTruco; // no-truco-retruco-valeCuatro

        public int SumaPuntaje { get { return this.sumaPuntaje; } }
        public string EstadoTruco { get { return this.estadoTruco; } }
        public Ronda(Jugador yo, JugadorIA rival)
        {
            this.yo = yo;
            this.rival = rival;

            this.ResetRonda();
        }
        public void ResetRonda()
        {
            this.yo.cantoTruco = false;

            this.sumaPuntaje = 1;
            this.estadoTruco = "no";
        }
        public void DarPuntoPorMano(string ganadorActual)
        {
            switch (ganadorActual)
            {
                case "gano":
                    this.yo.PuntosRondaActual += 1;
                    break;
                case "perdio":
                    this.rival.PuntosRondaActual += 1;
                    break;
            }
        }
        public bool TrucoBoton()
        {
            bool retorno = false;
            switch (this.estadoTruco)
            {
                case "no":
                    if (this.rival.AceptaTruco("truco"))
                    {
                        retorno = true;
                        this.sumaPuntaje = 2;
                        this.estadoTruco = "truco";
 
                        this.yo.cantoTruco = true;
                    }
                    break;
                case "truco":
                    if (this.rival.AceptaTruco("retruco"))
                    {
                        retorno = true;
                        this.sumaPuntaje = 3;
                        this.estadoTruco = "retruco";
                    }
                    break;
                case "retruco":
                    if (this.rival.AceptaTruco("valeCuatro"))
                    {
                        retorno = true;
                        this.sumaPuntaje = 4;
                        this.estadoTruco = "valeCuatro";
                    }
                    break;
                case "valeCuatro":
                    break;
            }
            return retorno;
        }

        public bool TrucoBoton(string estadoJuego, JugadorIA rival, Carta carta)
        {
            bool retorno = false;
            if (this.truco == false)
            {
                int minimo = 0;
                if (rival.CartasJugadas == 1)
                {
                    this.truco = true;

                    int puntajeRival = rival.PuntajeCartas();

                    if (estadoJuego == "no")
                    {
                        estadoJuego = "truco";
                        minimo = 12;
                    }
                    else if (estadoJuego == "truco")
                    {
                        estadoJuego = "retruco";
                        minimo = 15;
                    }
                    else if (estadoJuego == "retruco")
                    {
                        estadoJuego = "valeCuatro";
                        minimo = 18;
                    }
                    else if (estadoJuego == "valeCuatro")
                    {
                        minimo = 21;
                    }

                    if (puntajeRival > minimo) retorno = true;
                }
                else if (rival.CartasJugadas == 2)
                {
                    int puntajeYo = Jugador.AsignarPuntajeCarta(carta);
                    int puntajeRival = rival.PuntajeCartas();

                    if (puntajeRival > puntajeYo) retorno = true;
                }

            }
            return retorno;
        }
    }
}