using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}