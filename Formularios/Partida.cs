namespace Formularios
{
    public partial class Partida : Form
    {
        public Partida()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowIcon = false;
            this.Text = "Partida Truco";

            this.pbCartaRival1.Image = Image.FromFile("../../../../media/cartas/REVERSO.png");
            this.pbCartaRival2.Image = Image.FromFile("../../../../media/cartas/REVERSO.png");
            this.pbCartaRival3.Image = Image.FromFile("../../../../media/cartas/REVERSO.png");

            this.pbCartaPropia1.Image = Image.FromFile("../../../../media/cartas/1 DE BASTO.png");
            this.pbCartaPropia2.Image = Image.FromFile("../../../../media/cartas/7 BASTO.png");
            this.pbCartaPropia3.Image = Image.FromFile("../../../../media/cartas/4 ORO.png");
        }
    }
}