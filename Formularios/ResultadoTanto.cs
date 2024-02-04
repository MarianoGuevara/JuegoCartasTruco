using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formularios
{
    public partial class ResultadoTanto : Form
    {
        private string ganador;
        public ResultadoTanto(string ganeYo, int resYo, int resRival)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ganador = ganeYo;
            this.TextoGanador();
            this.lblYo.Text = resYo.ToString();
            this.lblRival.Text = resRival.ToString();
        }
        private void TextoGanador()
        {
            if (this.ganador == "yo")
            {
                this.lblResultado.ForeColor = Color.LimeGreen;
                this.lblResultado.Text = "Ganaste";
            }
            else
            {
                this.lblResultado.ForeColor = Color.Firebrick;
                this.lblResultado.Text = "Perdiste";
            }
        }
    }
}
