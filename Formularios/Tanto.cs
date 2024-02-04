﻿using Entidades;
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
    public partial class Tanto : QuieroNoQuiero
    {
        private Ronda rondaActual;
        private Jugador yo;
        private bool abriYo;
        public Tanto(Ronda rondaActual, Jugador yo)
        {
            InitializeComponent();
            this.abriYo = false;
            base.apretoBoton = false;
            this.rondaActual = rondaActual;
            this.yo = yo;

            this.EnableBotonesTanto();
        }
        public Tanto(Ronda rondaActual, Jugador yo, bool abroYo) : this(rondaActual, yo)
        {
            this.abriYo = true;
            base.lblQuiero.Enabled = false;
            base.lblNoQuiero.Enabled = false;
            base.apretoBoton = true;
        }
        private void EnableBotonesTanto()
        {
            if (this.yo.cantoEnvido == true) this.lblEnvido.Enabled = false;
            if (this.rondaActual.envidoEnvido == true) this.lblEnvido.Enabled = false;
            if (this.rondaActual.realEnvido == true)
            {
                this.lblEnvido.Enabled = false;
                this.lblReal.Enabled = false;
            }
            if (this.rondaActual.faltaEnvido == true)
            {
                this.lblFalta.Enabled = false;
                this.lblEnvido.Enabled = false;
                this.lblReal.Enabled = false;
            }
        }

        private void lblEnvido_Click(object sender, EventArgs e)
        {
            this.rondaActual.envido = true;
            if (this.abriYo) this.yo.cantoEnvido = true;

            this.rondaActual.SumaPuntajeTanto += 2;
            base.apretoBoton = true;
            this.DialogResult = DialogResult.Abort;
        }

        private void lblReal_Click(object sender, EventArgs e)
        {
            if (this.rondaActual.envido) this.rondaActual.SumaPuntajeTanto += 3;
            else this.rondaActual.SumaPuntajeTanto += 1;

            base.apretoBoton = true;
            this.rondaActual.realEnvido = true;
            this.DialogResult = DialogResult.Retry;
        }

        private void lblFalta_Click(object sender, EventArgs e)
        {
            if (this.rondaActual.envido) this.rondaActual.SumaPuntajeTanto += 3;
            else this.rondaActual.SumaPuntajeTanto += 1;

            this.rondaActual.faltaEnvido = true;
            base.apretoBoton = true;
            this.DialogResult = DialogResult.Yes;
        }

        private void lblQuiero_Click(object sender, EventArgs e)
        {
            if (this.rondaActual.faltaEnvido == true)
            {
                this.rondaActual.SumaPuntajeTanto = 10;
                this.yo.cantoFalta = true;
            }
        }
    }
}