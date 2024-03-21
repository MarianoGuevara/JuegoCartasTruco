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
        public ResultadoTanto(string ganeYo, int resYo, int resRival, bool tanto=true)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ganador = ganeYo;

            if (tanto) { this.lblBtn.Text = "VOLVER A PARTIDA"; }
            else { 
                this.lblBtn.Text = "VOLVER AL MENU";
                this.Text = "Fin partida";
            }
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

        private void lblBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
