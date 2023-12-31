﻿using System.Text.RegularExpressions;

namespace TrucoJuego
{
    public class Carta
    {
        static List<string> listaImagenes;
        private string cartaActual;

        #region Propiedades
        public string CartaActual { get { return this.cartaActual; } }
        #endregion

        static Carta()
        {
            Carta.listaImagenes = new List<string>();
            //listaImagenes.Add("../../../../media/cartas/REVERSO.png");
            listaImagenes.Add("../../../../media/cartas/1 BASTO.png");
            listaImagenes.Add("../../../../media/cartas/1 ESPADA.png");
            listaImagenes.Add("../../../../media/cartas/1 COPA.png");
            listaImagenes.Add("../../../../media/cartas/1 ORO.png");
            listaImagenes.Add("../../../../media/cartas/2 ORO.png");
            listaImagenes.Add("../../../../media/cartas/2 BASTO.png");
            listaImagenes.Add("../../../../media/cartas/2 COPA.png");
            listaImagenes.Add("../../../../media/cartas/2 ESPADA.png");
            listaImagenes.Add("../../../../media/cartas/3 ORO.png");
            listaImagenes.Add("../../../../media/cartas/3 BASTO.png");
            listaImagenes.Add("../../../../media/cartas/3 COPA.png");
            listaImagenes.Add("../../../../media/cartas/3 ESPADA.png");
            listaImagenes.Add("../../../../media/cartas/4 ORO.png");
            listaImagenes.Add("../../../../media/cartas/4 BASTO.png");
            listaImagenes.Add("../../../../media/cartas/4 COPA.png");
            listaImagenes.Add("../../../../media/cartas/4 ESPADA.png");
            listaImagenes.Add("../../../../media/cartas/5 ORO.png");
            listaImagenes.Add("../../../../media/cartas/5 BASTO.png");
            listaImagenes.Add("../../../../media/cartas/5 COPA.png");
            listaImagenes.Add("../../../../media/cartas/5 ESPADA.png");
            listaImagenes.Add("../../../../media/cartas/6 ORO.png");
            listaImagenes.Add("../../../../media/cartas/6 BASTO.png");
            listaImagenes.Add("../../../../media/cartas/6 COPA.png");
            listaImagenes.Add("../../../../media/cartas/6 ESPADA.png");
            listaImagenes.Add("../../../../media/cartas/7 ORO.png");
            listaImagenes.Add("../../../../media/cartas/7 BASTO.png");
            listaImagenes.Add("../../../../media/cartas/7 COPA.png");
            listaImagenes.Add("../../../../media/cartas/7 ESPADA.png");
            listaImagenes.Add("../../../../media/cartas/10 ORO.png");
            listaImagenes.Add("../../../../media/cartas/10 BASTO.png");
            listaImagenes.Add("../../../../media/cartas/10 COPA.png");
            listaImagenes.Add("../../../../media/cartas/10 ESPADA.png");
            listaImagenes.Add("../../../../media/cartas/11 ORO.png");
            listaImagenes.Add("../../../../media/cartas/11 BASTO.png");
            listaImagenes.Add("../../../../media/cartas/11 COPA.png");
            listaImagenes.Add("../../../../media/cartas/11 ESPADA.png");
            listaImagenes.Add("../../../../media/cartas/12 ORO.png");
            listaImagenes.Add("../../../../media/cartas/12 BASTO.png");
            listaImagenes.Add("../../../../media/cartas/12 COPA.png");
            listaImagenes.Add("../../../../media/cartas/12 ESPADA.png");
        }
        public Carta() { }
        public void DefinirCarta() { this.cartaActual = this.CartaRandom(); }
        private string CartaRandom()
        {
            Random rnd = new Random();
            int indice = rnd.Next(0, Carta.listaImagenes.Count);
            return Carta.listaImagenes[indice];
        }

        #region Sobrecargas equals y tostring
        public override bool Equals(object? obj)
        {
            bool retorno = false;
            if (obj is Carta)
            {
                retorno = ((Carta)obj).cartaActual == this.cartaActual;
            }
            return retorno;
        }

        public override string ToString()
        {
            try { return this.cartaActual; }
            catch (Exception ex) { return null; }
        }
        #endregion
    }
}