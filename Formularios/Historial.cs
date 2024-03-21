using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formularios
{
    public partial class Historial : MenuAbstract
    {
        public Historial(string deserializado)
        {
            InitializeComponent();
            this.rbHistorial.ReadOnly = true;
            this.rbHistorial.Text = deserializado;
            this.RecorridoTexto();
        }
        private void RecorridoTexto()
        {
            string palabra = string.Empty;
            int indiceInicioPalabra=0;

            for (int i=0;  i < this.rbHistorial.Text.Length; i++)
            {
                if (this.rbHistorial.Text[i] == ' ' || this.rbHistorial.Text[i] == '\n')
                {
                    indiceInicioPalabra = 0;
                    palabra = string.Empty;
                }
                else
                {
                    if (this.rbHistorial.Text[i] == 'I' || this.rbHistorial.Text[i] == 'G' || this.rbHistorial.Text[i] == 'P') indiceInicioPalabra = i;
                    palabra += this.rbHistorial.Text[i];
                }
                

                switch (palabra)
                {
                    case "INCONCLUSO":
                        this.colorearRb(indiceInicioPalabra, i + 100, Color.Yellow);
                        break;
                    case "PERDEDOR":
                        this.colorearRb(indiceInicioPalabra, i + 100, Color.Red);
                        break;
                    case "GANADOR":
                        this.colorearRb(indiceInicioPalabra, i + 100, Color.Green);
                        break;
                }
            }
        }

        private void colorearRb(int indiceInicio, int indiceFin, Color colorcito)
        {
            rbHistorial.Select(indiceInicio, indiceFin);
            rbHistorial.SelectionColor = colorcito;
            rbHistorial.SelectionLength = 0;
        }
    }
}
