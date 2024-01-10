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

        private string estadoTruco; // no-truco-retruco-valeCuatro

        private bool truco;
        private bool retruco;
        private bool valeCuatro;
        public bool Truco { get { return this.truco; } }
        public bool Retruco { get { return this.retruco; } }
        public bool ValeCuatro { get { return this.valeCuatro; } }
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
            this.sumaPuntaje = 1;
            this.estadoTruco = "no";

            this.truco = false;
            this.retruco = false;
            this.valeCuatro = false;
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

    }
}