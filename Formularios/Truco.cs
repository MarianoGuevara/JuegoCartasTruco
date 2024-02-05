using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formularios
{
    public partial class Truco : QuieroNoQuiero
    {
        private Ronda rondaActual;
        private Jugador yo;
        private bool abriYo;
        public Truco(Ronda rondaActual, Jugador yo)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.abriYo = false;
            base.apretoBoton = false;
            this.rondaActual = rondaActual;
            this.yo = yo;
            this.EnableBotonesTruco();
        }
        public Truco(Ronda rondaActual, Jugador yo, bool abroYo) : this(rondaActual, yo)
        {
            this.abriYo = true;
            base.lblQuiero.Enabled = false;
            base.lblNoQuiero.Enabled = false;
            base.apretoBoton = true;
        }
        private void EnableBotonesTruco()
        {
            if (this.rondaActual.truco) this.lblTruco.Enabled = false;
            if (this.rondaActual.retruco || this.rondaActual.truco == false) this.lblRetruco.Enabled = false;
            if (this.rondaActual.valeCuatro || (this.rondaActual.truco == false || this.rondaActual.retruco == false)) this.lblValeCuatro.Enabled = false;

            if (this.yo.cantoTruco)
            {
                this.lblRetruco.Enabled = false;
                if (this.rondaActual.valeCuatro == false) this.lblValeCuatro.Enabled = true;
            }

        }
        private void lblTruco_Click(object sender, EventArgs e)
        {
            this.yo.cantoTruco = true;
            this.rondaActual.SumaPuntaje = 2;
            this.rondaActual.truco = true;
            this.rondaActual.EstadoTruco = "truco";
            base.apretoBoton = true;
            this.DialogResult = DialogResult.Abort;
        }
        private void lblRetruco_Click(object sender, EventArgs e)
        {
            this.rondaActual.SumaPuntaje = 3;
            this.rondaActual.retruco = true;
            this.rondaActual.EstadoTruco = "retruco";
            base.apretoBoton = true;
            this.DialogResult = DialogResult.Retry;
        }
        private void lblValeCuatro_Click(object sender, EventArgs e)
        {
            this.rondaActual.SumaPuntaje = 4;
            this.rondaActual.valeCuatro = true;
            this.rondaActual.EstadoTruco = "valeCuatro";
            base.apretoBoton = true;
            this.DialogResult = DialogResult.Yes;
        }
    }
}