using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Entidades
{
    public class Persona
    {
        private string nombre;
        private string imagenDireccion;
        private static List<string> listaNombres;
        private static List<string> listaDirecciones;
        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }
        public string ImagenDireccion
        {
            get { return this.imagenDireccion; }
            set { this.imagenDireccion = value; }
        }
        static Persona()
        {
            Persona.listaNombres = new List<string>
            {
                "stark", "fushiguro", "kotaro", "naruto", "tanaka", "tanjiro"
            };

        }
        public Persona(string nombre, string imagenDireccion)
        {
            Nombre = nombre;
            ImagenDireccion = imagenDireccion;
        }
        public void PersonaRandom()
        {
            //int direccionRandom;

            Random a = new Random();
            int random = a.Next(0, 6); // 0 a 6

            Nombre = Persona.listaNombres[random];
            ImagenDireccion = $"media/perfiles/{Nombre}.jpg";
        }
    }
}