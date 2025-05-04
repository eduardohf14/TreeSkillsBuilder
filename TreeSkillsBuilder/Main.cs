using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeSkillsBuilder
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnCaptureMousePos_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveSkill_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(skillNameTxt.Text))
            {
                MessageBox.Show("Escolha um nome pro skill.");
                return;
            }

            if (pointsTxt.Value == 0)
            {
                MessageBox.Show("Informe a qtd de pontos para esse skill.");
                return;
            }

            if (string.IsNullOrEmpty(positionTxt.Text))
            {
                MessageBox.Show("Informe a posição X Y da skill");
                return;
            }
        }
    }
}
