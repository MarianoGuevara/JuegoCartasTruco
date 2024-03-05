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
    public partial class CerrarMenu : QuieroNoQuiero
    {

        public CerrarMenu(string lable, string si, string no)
        {
            InitializeComponent();
            this.Text = "Decidir si cerrar"; 
            this.lblTxt.Text = lable;
            base.lblQuiero.Text = si;
            base.lblNoQuiero.Text = no;
        }
        private void CerrarMenu_FormClosing(object sender, FormClosingEventArgs e) { base.QuieroNoQuiero_FormClosing(sender, e); }
        private void CerrarMenu_MouseEnter(object sender, EventArgs e) { base.Partida_MouseEnter(sender, e); }
        private void CerrarMenu_MouseLeave(object sender, EventArgs e) { base.Partida_MouseLeave(sender, e); }

    }
}
