using Entidades;
using System.Collections;
using System.Diagnostics;

namespace Formularios
{
    public partial class MenuPerfil : MenuAbstract
    {
        private Persona yo;
        private Image imagenInicial;
        public string nombreInicial;
        public string direccionImagen;
        public MenuPerfil(Persona yo)
        {
            InitializeComponent();
            this.yo = yo;

            this.txtNombre.Text = yo.Nombre;
            this.pbPerfil.Image = Image.FromFile(yo.ImagenDireccion);
            this.nombreInicial = yo.Nombre;
            this.imagenInicial = Image.FromFile(yo.ImagenDireccion);
            this.direccionImagen = yo.ImagenDireccion;

            this.txtNombre.MaxLength = 13;
        }
        [DebuggerStepThrough]
        private void MenuPerfil_MouseLeave(object sender, EventArgs e) { base.Menu_MouseLeave(sender, e); }
        [DebuggerStepThrough]
        private void MenuPerfil_MouseEnter(object sender, EventArgs e) { base.Menu_MouseEnter(sender, e); }
        private void MenuPerfil_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.SonImagenesIguales(this.pbPerfil.Image, Image.FromFile(this.yo.ImagenDireccion))
                || this.txtNombre.Text != this.yo.Nombre)
            {
                if (this.txtNombre.Text.Length < 4)
                {
                    MessageBox.Show("El nombre debe ser de mínimo 4 caracteres", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
                else
                {
                    CerrarMenu c = new CerrarMenu("Desea guardar los cambios hechos en el perfil? Piensalo bien...", "SI", "NO");
                    c.ShowDialog();
                    this.nombreInicial = this.txtNombre.Text;
                    this.DialogResult = c.DialogResult;
                }
            }
            else this.DialogResult = DialogResult.Cancel;
        }
        // metodo de chat gpt la verdad
        private bool SonImagenesIguales(Image imagen1, Image imagen2)
        {
            // Convertir ambas imágenes a matrices de bytes
            byte[] bytesImagen1;
            byte[] bytesImagen2;

            using (MemoryStream ms1 = new MemoryStream())
            {
                imagen1.Save(ms1, imagen1.RawFormat);
                bytesImagen1 = ms1.ToArray();
            }

            using (MemoryStream ms2 = new MemoryStream())
            {
                imagen2.Save(ms2, imagen2.RawFormat);
                bytesImagen2 = ms2.ToArray();
            }

            // Comparar las matrices de bytes
            return StructuralComparisons.StructuralEqualityComparer.Equals(bytesImagen1, bytesImagen2);
        }

        private void lblCambioImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Selecciona un archivo JPG";
            ofd.Filter = "Archivos de imagen JPG|*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.direccionImagen = ofd.FileName;
                this.pbPerfil.Image = Image.FromFile(ofd.FileName);
            }
        }
        private void lblVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblHistorial_Click(object sender, EventArgs e)
        {
            string deserializado = Serializadora<string>.DeserializarStr("historial.txt");
            Historial h = new Historial(deserializado);
            h.ShowDialog();
        }
    }
}