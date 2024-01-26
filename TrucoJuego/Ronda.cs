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
        public bool envido;
        public bool envidoEnvido;
        public bool realEnvido;
        public bool faltaEnvido;
        private int sumaPuntaje;
        private string estadoTruco; // no-truco-retruco-valeCuatro
        private string estadoEnvido; // no-envido-envidoEnvido-realEnvido-faltaEnvido

        public int SumaPuntaje { get { return this.sumaPuntaje; } }
        public string EstadoTruco { get { return this.estadoTruco; } }
        public string EstadoEnvido { get { return this.estadoEnvido; } }
        public Ronda(Jugador yo, JugadorIA rival)
        {
            this.yo = yo;
            this.rival = rival;

            this.ResetRonda();
        }
        public void ResetRonda()
        {
            this.envido = false;
            this.envidoEnvido = false;
            this.realEnvido = false;
            this.faltaEnvido = false;

            this.truco = false;
            this.retruco = false;
            this.valeCuatro = false;

            this.yo.cantoTruco = false;

            this.sumaPuntaje = 1;
            this.estadoTruco = "no";
            this.estadoEnvido = "no";
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
        #region Truco
        public bool TrucoBoton(bool cantoYo, bool rival=false, Carta carta = null, Carta cartaYo = null)
        {
            bool retorno = false;
            switch (this.estadoTruco)
            {
                case "no":
                    if (this.rival.AceptaTruco("truco", this.yo, this.rival, cantoYo, carta))
                    {
                        retorno = true;
                        this.sumaPuntaje = 2;
                        this.estadoTruco = "truco";
 
                        if (rival == false) this.yo.cantoTruco = true;
                        else this.rival.cantoTruco = true;
                    }
                    break;
                case "truco":
                    if (this.rival.AceptaTruco("retruco", this.yo, this.rival, cantoYo, carta))
                    {
                        retorno = true;
                        this.sumaPuntaje = 3;
                        this.estadoTruco = "retruco";
                    }
                    break;
                case "retruco":
                    if (this.rival.AceptaTruco("valeCuatro", this.yo, this.rival, cantoYo, carta))
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
                retorno = this.TrucoFinal(retorno, this.rival, this.yo, carta, cartaYo);
            }

            return retorno;
        }

        public bool TrucoFinal(bool estado, JugadorIA rival, Jugador yo, Carta carta, Carta cartaYo)
        {
            bool retorno = estado;

            if (rival.CartasJugadas == 2 && yo.CartasJugadas == 3)
            {
                int puntajeYo = Jugador.AsignarPuntajeCarta(cartaYo);
                int puntajeRival = Jugador.AsignarPuntajeCarta(carta);

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
        #endregion

        #region Envido
        public bool EnvidoBoton() // -si la "IA" quiere jugar o no y cambia el estado de los atributos
        {
            bool quiereJugar = false;
            switch (this.estadoEnvido)
            {
                case "no":
                    if (this.AceptaEnvido)
                    break;
                case "envido":
                    break;
                case "envidoEnvido":
                    break;
                case "realEnvido":
                    break;
                case "faltaEnvido":
                    break;
            }
            return quiereJugar;
        }

        private string AceptaEnvido()
        {
            string retorno;
            int tanto = this.rival.PuntajeEnvidoNumerico();

            if (tanto <= 25) retorno = "no";
            else if (tanto <= 27) retorno = "envido";
            else if (tanto <= 29) retorno = "realEnvido";
            else retorno = "faltaEnvido";

            return retorno;
        }

        #endregion
    } // fin clase
} // fin namespace