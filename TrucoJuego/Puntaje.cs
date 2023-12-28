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



    }
}