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
        private Jugador rival;

        private int sumaPuntaje;

        private bool truco;
        private bool retruco;
        private bool valeCuatro;
        public Ronda(Jugador yo, Jugador rival)
        {
            this.yo = yo;
            this.rival = rival;

            this.ResetRonda();
        }
        public void ResetRonda()
        {
            this.sumaPuntaje = 1;

            this.truco = false;
            this.retruco = false;
            this.valeCuatro = false;
        }

        public void SumarPuntaje(Jugador player)
        {
            player.Puntaje += this.sumaPuntaje;
        }

        public string ValorPuntosTruco(string str, string player)
        {
            switch (str)
            {
                case "TRUCO":
                    if (player == "yo")
                    {
                        str = "VALE CUATRO";
                    }
                    else str = "RETRUCO";
                    this.sumaPuntaje = 2;
                    break;
                case "RETRUCO":
                    str = "VALE CUATRO";
                    this.sumaPuntaje = 3;
                    break;
                case "VALE CUATRO":
                    this.sumaPuntaje = 4;
                    break;

            }
            return str;
        }
        public void AnalizarPuntaje()
        {
            if (yo.PuntosRondaActual == 2)
            {
                this.SumarPuntaje(this.yo);
            }
            else if (rival.PuntosRondaActual == 2)
            {
                this.SumarPuntaje(this.rival);
            }
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
    }
}