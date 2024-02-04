using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public static class Puntaje
    {
        private static string ObtenerPuntaje(int puntaje)
        {
            string enteroString = puntaje.ToString();
            return enteroString + ".jpg";
        }
        public static string ImagenPuntaje(int puntaje)
        {
            string stringFinal = Puntaje.ObtenerPuntaje(puntaje);
            return $"../../../../media/puntaje/{stringFinal}";
        }
        public static void SumarPuntaje(Jugador player, int sumaPuntaje)
        {
            player.Puntaje += sumaPuntaje;
        }

        public static void AnalizarPuntaje(Jugador yo, Jugador rival, int sumaPuntaje)
        {
            if (yo.PuntosRondaActual == 2)
            {
                Puntaje.SumarPuntaje(yo, sumaPuntaje);
            }
            else if (rival.PuntosRondaActual == 2)
            {
                Puntaje.SumarPuntaje(rival, sumaPuntaje);
            }
        }

        public static string TrucoTexto(string situacion)
        {
            string retorno = "";
            switch(situacion)
            {
                case "no":
                    retorno = "TRUCO";
                    break;
                case "truco":
                    retorno = "RETRUCO";
                    break;
                case "retruco":
                    retorno = "VALE CUATRO";
                    break;
            }
            return retorno;
        }

        public static string EnvidoTexto(string situacion)
        {
            string retorno = "";
            switch (situacion)
            {
                case "no":
                    retorno = "ENVIDO";
                    break;
                case "envido":
                    retorno = "REAL ENVIDO";
                    break;
                case "envidoEnvido":
                    retorno = "REAL ENVIDO";
                    break;
                case "realEnvido":
                    retorno = "FALTA ENVIDO";
                    break;
            }
            return retorno;
        }

        public static bool GaneYoEnvido(int puntajeYo, int puntajeEl, bool manoYo)
        {
            bool gane = false;
            if (puntajeYo == puntajeEl && manoYo) gane = true;
            else if (puntajeYo > puntajeEl) gane = true;
            return gane;
        }

        public static void FaltaEnvido(Ronda ronda, Jugador yo, JugadorIA rival, string ganador)
        {
            int suma;
            if (ganador == "yo") suma = 30 - rival.Puntaje;
            else suma = 30 - yo.Puntaje;
            ronda.SumaPuntajeTanto = suma;
            SumarPuntajesTanto(ganador, yo, rival, ronda);
        }
        public static void SumarPuntajesTanto(string ganador, Jugador yo, Jugador rival, Ronda ronda)
        {
            if (ganador == "yo") yo.Puntaje +=  ronda.SumaPuntajeTanto;
            else rival.Puntaje += ronda.SumaPuntajeTanto;
        }
        public static void CalcularPuntajeNoQuiero(Ronda ronda, Jugador player)
        {
            switch (ronda.SumaPuntajeTanto)
            {
                case 2:
                    ronda.SumaPuntajeTanto = 1;
                    break;
                case 4:
                case 5:
                case 6:
                    ronda.SumaPuntajeTanto = 2;
                    break;
                case 7:
                    ronda.SumaPuntajeTanto = 4;
                    break;
                case 9:
                    ronda.SumaPuntajeTanto = 7;
                    break;
            }
            player.Puntaje += ronda.SumaPuntajeTanto;
        }
    }
}