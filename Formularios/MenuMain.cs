using System;
using System.CodeDom;
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
    public partial class MenuMain : MenuPadre
    {
        public MenuMain()
        {
            InitializeComponent();
            this.Text = "Menu";
            this.ShowIcon = false;

            base.MusicaFondo.PlayLooping();
        }
        private void MenuMain_MouseEnter(object sender, EventArgs e) { base.Menu_MouseEnter(sender, e); }
        private void MenuMain_MouseLeave(object sender, EventArgs e) { base.Menu_MouseLeave(sender, e); }

        private void lblJugar_Click(object sender, EventArgs e)
        {
            base.MusicaFondo.Stop();
            Partida p = new Partida(this);
            p.ShowDialog();
            base.MusicaFondo.PlayLooping();
        }
        private void lblPerfil_Click(object sender, EventArgs e)
        {

        }
        private void lblTutorial_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tutorial aún no disponible en la app, vea las otras opciones.");
        }
        private void lblSalir_Click(object sender, EventArgs e) { this.Close(); }
        private void MenuMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            CerrarMenu c = new CerrarMenu("¿Realmente desea cerrar la aplicación? ¡Hay mucho por jugar! ", "SI", "NO");
            c.ShowDialog();
            if (c.DialogResult == DialogResult.No) { e.Cancel = true; }
        }
    }
}
