using System.Text.RegularExpressions;

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
        public Carta()
        {
            this.cartaActual = this.CartaRandom(); // me da null si llamo a DefinirCarta :/
        }

        public void DefinirCarta()
        {
            this.cartaActual = this.CartaRandom();
        }
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
            return this.cartaActual;
        }
        #endregion

        //#region Duelo de cartas; logica ganador por valor 
        //public string Ganador(Carta cartaPropia, Carta cartaRival)
        //{
        //    string estadoRonda;

        //    int puntajePropio = this.AsignarPuntajeCarta(cartaPropia);
        //    int puntajeRival = this.AsignarPuntajeCarta(cartaRival);

        //    if (puntajePropio > puntajeRival) estadoRonda = "gano";
        //    else if (puntajePropio < puntajeRival) estadoRonda = "perdio";
        //    else estadoRonda = "empato";
        //    return estadoRonda;
        //}

        //private int AsignarPuntajeCarta(Carta cartaPropia)
        //{
        //    string carta = cartaPropia.ToString();
        //    int puntaje;

        //    if (Regex.IsMatch(carta, "1 ESPADA")) puntaje = 14;
        //    else if(Regex.IsMatch(carta, "1 BASTO")) puntaje = 13;
        //    else if(Regex.IsMatch(carta, "7 ESPADA")) puntaje = 12;
        //    else if(Regex.IsMatch(carta, "7 ORO")) puntaje = 11;
        //    else if (Regex.IsMatch(carta, "3 [A-Z]")) puntaje = 10;
        //    else if (Regex.IsMatch(carta, "2 [A-Z]")) puntaje = 9;
        //    else if (Regex.IsMatch(carta, "1 COPA") || Regex.IsMatch(carta, "1 ORO")) puntaje = 8;
        //    else if (Regex.IsMatch(carta, "12 [A-Z]")) puntaje = 7;
        //    else if (Regex.IsMatch(carta, "11 [A-Z]")) puntaje = 6;
        //    else if (Regex.IsMatch(carta, "10 [A-Z]")) puntaje = 5;
        //    else if (Regex.IsMatch(carta, "7 BASTO") || Regex.IsMatch(carta, "7 COPA")) puntaje = 4;
        //    else if (Regex.IsMatch(carta, "6 [A-Z]")) puntaje = 3;
        //    else if (Regex.IsMatch(carta, "5 [A-Z]")) puntaje = 2;
        //    else puntaje = 1;

        //    return puntaje;
        //}
        //#endregion
    }
}