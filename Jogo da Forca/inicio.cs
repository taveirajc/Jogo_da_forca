using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NOVOFORCA

{
       
    public partial class inicio : Form
    {
        string pasta_aplicacao = ""; 

        public inicio()
        {         
            
            pasta_aplicacao = Application.StartupPath + @"\";  // CAMINHO DO PROGRAMA 
            SoundPlayer simpleSound = new SoundPlayer(pasta_aplicacao + @"sons\Trance.wav");

            InitializeComponent();
            simpleSound.Play(); // TOCA A MÚSICA ATÉ CLICAR NO BOTÃO JOGAR
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 newForm2 = new Form1();
            this.Hide(); 
            SoundPlayer simpleSound = new SoundPlayer(pasta_aplicacao + @"sons\Trance.wav");
            simpleSound.Stop(); // PARA A MÚSICA INICIAL
            newForm2.ShowDialog(); // CHAMA O FORMULÁRIO DO JOGO
        }
    }
}
