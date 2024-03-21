using Entidades;
using System;
using System.CodeDom;
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
    public partial class MenuMain : MenuAbstract
    {
        Persona yoPersona;
        public MenuMain()
        {
            InitializeComponent();
            this.Text = "Menu";
            this.ShowIcon = false;
        }
        [DebuggerStepThrough]
        private void MenuMain_MouseEnter(object sender, EventArgs e) { base.Menu_MouseEnter(sender, e); }
        [DebuggerStepThrough]
        private void MenuMain_MouseLeave(object sender, EventArgs e) { base.Menu_MouseLeave(sender, e); }

        private void lblJugar_Click(object sender, EventArgs e)
        {
            Partida p = new Partida(this, this.yoPersona);
            p.ShowDialog();
        }
        private void lblPerfil_Click(object sender, EventArgs e)
        {
            MenuPerfil perfil = new MenuPerfil(this.yoPersona);
            perfil.ShowDialog();
            if (perfil.DialogResult == DialogResult.OK)
            {
                this.yoPersona.Nombre = perfil.nombreInicial;
                this.yoPersona.ImagenDireccion = perfil.direccionImagen;
            }
        }
        private void lblTutorial_Click(object sender, EventArgs e) { MessageBox.Show("Tutorial aún no disponible en la app, vea las otras opciones.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        private void lblSalir_Click(object sender, EventArgs e) { this.Close(); }
        private void MenuMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            CerrarMenu c = new CerrarMenu("¿Realmente desea cerrar la aplicación? ¡Hay mucho por jugar! ", "SI", "NO");
            c.ShowDialog();
            if (c.DialogResult == DialogResult.No) { e.Cancel = true; }
            else
            {
                //this.yoPersona = new Persona("mariano", "../../../../media/perfiles/default.jpg");

                bool serializacion = Serializadora<Persona>.SerializarJson(this.yoPersona, "../../../../persona.json");
                if (!serializacion) { MessageBox.Show("Ha ocurrido un error con la serializacion de su perfil. Comuniquese con el desarrollador. Mail: marianoguevara2005@gmail.com"); }
            }
        }
        private void MenuMain_Load(object sender, EventArgs e)
        {
            try
            {
                this.yoPersona = Serializadora<Persona>.DeserializarJson("../../../../persona.json");
            }
            catch
            { 
                MessageBox.Show("Ha ocurrido un error con la deserializacion de su perfil. Comuniquese con el desarrollador. Mail: marianoguevara2005@gmail.com"); 
                this.Close();
            }
        }
    }
}