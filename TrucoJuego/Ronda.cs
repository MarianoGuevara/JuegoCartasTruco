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
            this.truco = false;
            this.retruco = false;
            this.valeCuatro = false;

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
        public bool TrucoBoton(bool rival=false, Carta carta=null)
        {
            bool retorno = false;
            switch (this.estadoTruco)
            {
                case "no":
                    if (this.rival.AceptaTruco("truco", this.yo, this.rival))
                    {
                        retorno = true;
                        this.sumaPuntaje = 2;
                        this.estadoTruco = "truco";
 
                        if (rival == false) this.yo.cantoTruco = true;
                        else this.rival.cantoTruco = true;
                    }
                    break;
                case "truco":
                    if (this.rival.AceptaTruco("retruco", this.yo, this.rival))
                    {
                        retorno = true;
                        this.sumaPuntaje = 3;
                        this.estadoTruco = "retruco";
                    }
                    break;
                case "retruco":
                    if (this.rival.AceptaTruco("valeCuatro", this.yo, this.rival))
                    {
                        retorno = true;
                        this.sumaPuntaje = 4;
                        this.estadoTruco = "valeCuatro";
                    }
                    break;
                case "valeCuatro":
                    break;
            }

            if (rival == true)
            {
                retorno = this.TrucoFinal(retorno, this.rival, this.yo, carta);
            }

            return retorno;
        }

        public bool TrucoFinal(bool estado, JugadorIA rival, Jugador yo, Carta carta)
        {
            bool retorno = estado;

            if (rival.CartasJugadas == 2 && yo.CartasJugadas == 3 && this.estadoTruco != "no")
            {
                int puntajeYo = Jugador.AsignarPuntajeCarta(carta);
                int puntajeRival = rival.PuntajeCartas();

                if (puntajeRival > puntajeYo) retorno = true;
            }
            return retorno;
        }

        public bool PuedeCantar(Jugador player)
        {
            bool retorno = false;
            switch (this.estadoTruco)
            {
                case "no":
                    retorno = true;
                    break;
                case "truco":
                    if (player.cantoTruco == false) retorno = true;
                    break;
                case "retruco":
                    if (player.cantoTruco == true) retorno = true;
                    break;
                case "valeCuatro":
                    retorno = false;
                    break;
            }
            return retorno;
        }
    }
}