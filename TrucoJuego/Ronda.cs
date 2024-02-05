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

        private int sumaPuntaje;
        private int sumaPuntajeTanto;

        private string estadoTruco; // no-truco-retruco-valeCuatro
        private string estadoEnvido; // no-envido-envidoEnvido-realEnvido-faltaEnvido

        public bool truco;
        public bool retruco;
        public bool valeCuatro;

        public bool envido;
        public bool envidoEnvido;
        public bool realEnvido;
        public bool faltaEnvido;
        public bool faltaAceptada;
        public int SumaPuntaje
        { 
            get { return this.sumaPuntaje; } 
            set { this.sumaPuntaje = value; }
        }
        public int SumaPuntajeTanto
        {
            get { return this.sumaPuntajeTanto; }
            set { this.sumaPuntajeTanto = value; }
        }
        public string EstadoTruco 
        {
            get { return this.estadoTruco; }
            set { this.estadoTruco = value; }
        }
        public string EstadoEnvido { get { return this.estadoEnvido; } }
        public Ronda(Jugador yo, JugadorIA rival)
        {
            this.envido = false;
            this.envidoEnvido = false;
            this.realEnvido = false;
            this.faltaEnvido = false;

            this.truco = false;
            this.retruco = false;
            this. valeCuatro = false;

            this.yo = yo;
            this.rival = rival;

            this.ResetRonda();
        }
        public void ResetRonda()
        {
            this.truco = false;
            this.retruco = false;
            this.valeCuatro = false;

            this.envido = false;
            this.envidoEnvido = false;
            this.realEnvido = false;
            this.faltaEnvido = false;

            this.faltaAceptada = false;
            this.yo.cantoFalta = false;

            this.yo.cantoTruco = false;
            this.rival.cantoTruco = false;
            this.yo.cantoEnvido = false;
            this.rival.cantoEnvido = false;
            this.sumaPuntaje = 1;
            this.sumaPuntajeTanto = 0;
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
        public string QueCantaTanto(Jugador rivalScreenshot)
        {
            string retorno = "noQuiero";
            string casoRival = this.RivalAceptaEnvido(rivalScreenshot);

            switch (casoRival)
            {
                case "envido":
                    if (this.envido == false && this.realEnvido == false && this.faltaEnvido == false)
                    {
                        this.sumaPuntajeTanto += 2;
                        retorno = "envido";
                        this.envido = true;
                        this.rival.cantoEnvido = true;
                    }
                    else
                    {
                        if (this.realEnvido == true || this.faltaEnvido == true) retorno = "noQuiero";
                        else retorno = "quiero";
                    }
                    break;
                case "realEnvido":
                    if (this.realEnvido == false && this.faltaEnvido == false)
                    {
                        this.sumaPuntajeTanto += 3;
                        retorno = "realEnvido";
                        this.realEnvido = true;
                    }
                    else
                    {
                        if (this.faltaEnvido == true) retorno = "noQuiero";
                        else retorno = "quiero";
                    }
                    break;
                case "faltaEnvido":
                    if (this.faltaEnvido == false)
                    {
                        this.sumaPuntajeTanto += 3;
                        retorno = "faltaEnvido";
                        this.faltaEnvido = true;
                    }
                    else retorno = "quiero";
                    break;
            }
            return retorno;
        }
        private string RivalAceptaEnvido(Jugador rival)
        {
            string retorno;
            int tanto = rival.PuntajeEnvidoNumerico();

            if (tanto < 27) retorno = "noQuiero";
            else if (tanto == 28 || tanto == 27) retorno = "envido";
            else if (tanto == 29 || tanto == 30) retorno = "realEnvido";
            else retorno = "faltaEnvido";

            string mentira = this.MentiraTanto(retorno);
            if (mentira != string.Empty) retorno = mentira;

            return retorno;
        }
        private string MentiraTanto(string rivalSituacion) // cambia el retorno en algunos casos para mentir
        {
            string retorno = string.Empty;
            Random r = new Random();

            int randomNum = r.Next(1,11); // del 1 al 10
            if (randomNum <= 2) // si el random es 1, multiplica mucho la apuesta. Si es 2 o 3,la sube 1 nivel
            {
                if (rivalSituacion == "noQuiero")
                {
                    if (randomNum == 1) retorno = "realEnvido";
                    else retorno = "envido";
                }
                else if (rivalSituacion == "envido")
                {
                    if (randomNum == 1) retorno = "faltaEnvido";
                    else retorno = "realEnvido"; 
                }
                else if (rivalSituacion == "realEnvido") retorno = "faltaEnvido";
            }
            return retorno;
        }
        #endregion
    } // fin clase
} // fin namespace