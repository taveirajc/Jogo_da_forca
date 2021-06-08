using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace NOVOFORCA
{
    public partial class TELA_INICIAL : Form
    {
        string pasta_aplicacao = string.Empty;

        public TELA_INICIAL()
        {
            InitializeComponent();
            pasta_aplicacao = Application.StartupPath + @"\";  // CAMINHO DO PROGRAMA 
            SoundPlayer simpleSound = new SoundPlayer(pasta_aplicacao + @"sons\Trance.wav");
            button2.Select();


            InitializeComponent();
            simpleSound.Play(); // TOCA A MÚSICA ATÉ CLICAR NO BOTÃO JOGAR
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 newForm2 = new Form1();
            this.Hide();
            SoundPlayer simpleSound = new SoundPlayer(pasta_aplicacao + @"sons\Trance.wav");
            simpleSound.Stop(); // PARA A MÚSICA INICIAL
            newForm2.ShowDialog(); // CHAMA O FORMULÁRIO DO JOGO
        }
    }
}
