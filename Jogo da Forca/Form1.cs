using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Media;

namespace NOVOFORCA
{
    public partial class Form1 : Form
    {
        string pasta_aplicacao = "";
        string palavra;
        bool achou = false;
        int tam, tentativas = 6, categoria;
        Random naleatorio = new Random();
        string[,] PALAVRAS = new string[5, 12] { {"banana", "laranja", "abacate", "pera", "uva", "mamão", "pitanga", "pêssego", "manga", "jaboticaba", "maçã", "amora"}, 
        /* 0 = FRUTAS */                        {"cachorro", "vaca", "boi", "cavalo", "égua", "elefante", "tigre", "onça", "hipopótamo", "jacaré", "rinoceronte", "girafa"},
        /* 1 = ANIMAIS */                       {"azul", "vermelho", "amarelo", "verde", "branco", "marrom", "violeta", "rosa", "preto", "laranja", "cinza", "bege"},
        /* 2 = CORES */                         {"faca", "garfo", "colher", "fogão", "micro-ondas", "panela", "chaleira", "frigideira", "sabão", "geladeira", "fósforo", "abridor"},
        /* 3 = COISAS DE COZINHA */             {"cruzeiro", "atlético", "américa", "flamengo", "corinthians", "vasco da gama", "fluminense", "chapecoense", "grêmio", "são paulo", "palmeiras", "santos"}};
        /* 4 - CLUBES DE FUTEBOL */

        Label[] labels;
        const int X = 0, Y = 0;
        char[] acentos = new char[6];
        int stage = 0;
        PictureBox picture = new PictureBox();   // IMAGEM DE CARINHA QUE ERROU
        PictureBox picture1 = new PictureBox();  // IMAGEM DE PARABÉNS
        PictureBox picture2 = new PictureBox();  // IMAGEM QUE PENINHA

        public Form1()
        {
            InitializeComponent();
            pasta_aplicacao = Application.StartupPath + @"\";  // CAMINHO DO PROGRAMA 

            Paint += Form1_Paint; // PARA O DESENHO DA FORCA
            ativabotoes(false);  // DESATIVA OS BOTÕES DAS LETRAS
            button27.Enabled = false; // DESATIVA BOTÃO NOVO JOGO
            label3.Text = tentativas.ToString();

            //CRIAÇÃO DO PICTUREBOX DA IMAGEM DE PERDA
            picture.Name = "pictureBox1";
            picture.Size = new Size(160, 155);
            picture.Location = new Point(245, 12);
            picture2.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.Image = Image.FromFile(pasta_aplicacao + @"imagens\errou1.jpg");// local da imagem
            this.Controls.Add(picture); // adiciona ao formulário
            picture.Visible = false;   // fica invisível

            //CRIAÇÃO DO PICTUREBOX DA IMAGEM DE VITORIA
            picture1.Name = "pictureBox2";
            picture1.Size = new Size(401, 284);
            picture1.Location = new Point(9, 12);
            picture2.SizeMode = PictureBoxSizeMode.StretchImage;
            picture1.Image = Image.FromFile(pasta_aplicacao + @"imagens\parabens.jpg");
            this.Controls.Add(picture1);
            picture1.Visible = false;

            //CRIAÇÃO DO PICTUREBOX DA IMAGEM QUE PENINHA
            picture2.Name = "pictureBox3";
            picture2.Size = new Size(398, 91);
            picture2.Location = new Point(9, 184);
            picture2.SizeMode = PictureBoxSizeMode.StretchImage; // a imagem se adapta ao tamanho do picturebox
            picture2.Image = Image.FromFile(pasta_aplicacao + @"imagens\que_peninha.jpg");
            this.Controls.Add(picture2);
            picture2.Visible = false;
        }

        #region SELEÇÃO DAS CATEGORIAS
        private void subradiobotons()
        {
            panel1.Controls.Clear();    // LIMPA O PANEL1 (PALAVRA A SER ACERTADA)
            label3.Text = string.Empty; // NUMERO DE TENTATIVAS
            ativabotoes(false);         // ATIVA OS BOTÕES DAS LETRAS
            tentativas = 6;             // REINICIALIZA NÚMERO DE TENTATIVAS
            stage = 0;                  // REINICIALIZA O VALOR DE ERRO PARA O DESENHA DA FORCA
            label3.Text = tentativas.ToString();
            label2.Text = String.Empty;
            // desmarca_buttons(false);
            //categoria = -1; // REINICIA O VALOR DO RADIOBUTTON
            this.Invalidate(); // LIMPA A FORCA
            picture.Visible = false;
            picture1.Visible = false;
            picture2.Visible = false;
            obtempalavra();
            ativabotoes(true);
            button27.Enabled = true;
        }


        private void rdb1(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            subradiobotons();
        }

        private void rdb2(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            subradiobotons();
        }

        private void rdb3(object sender, EventArgs e)
        {
            radioButton3.Checked = true;
            subradiobotons();
        }

        private void rdb4(object sender, EventArgs e)
        {
            radioButton4.Checked = true;
            subradiobotons();
        }

        private void rdb5(object sender, EventArgs e)
        {
            radioButton5.Checked = true;
            subradiobotons();
        }
        #endregion

        #region SORTEIA A PALAVRA E PEGA NO ARRAY DE ACORDO COM A CATEGORIA ESCOLHIDA
        public void obtempalavra()
        {
            foreach (Control ctrl in groupBox1.Controls)
            {
                if (ctrl is RadioButton)
                {
                    RadioButton radioB = (RadioButton)ctrl;
                    if (radioB.Checked)
                    {
                        categoria = radioB.TabIndex; // PEGA O ÍNDICE DA CATEGORIA ESCOLHIDO PARA COLOCAR NA PRIMEIRA COLUNA DA MATRIZ
                    }
                }
            }
            int x = naleatorio.Next(0, 11);
            palavra = PALAVRAS[categoria, x].ToUpper();  // palavra a ser advinhada em maiuscula
            tam = PALAVRAS[categoria, x].Length;
            IniciarLabels();

        }
        #endregion

        #region DESENHA OS LABELS DA PALAVRA
        private void IniciarLabels()
        {
            tam = palavra.Length;
            labels = new Label[tam];

            for (int i = 0; i < tam; i++)
            {
                labels[i] = new Label();
                labels[i].Font = new Font("Arial", 26, FontStyle.Bold);
                labels[i].ForeColor = System.Drawing.Color.Black;
                labels[i].TextAlign = ContentAlignment.MiddleLeft;
                labels[i].AutoSize = false;
                labels[i].Size = new Size(40, 40);
                labels[i].Text = palavra[i] == ' ' ? " " : "_";
                labels[i].Location = i == 0 ? new Point(X, Y + 5) : new Point((labels[i - 1].Location.X + 43), Y + 5);
                panel1.Controls.Add(labels[i]);
            }

        }
        #endregion

        #region VERIFICA AS CONSOANTES

        private void btn_consoantes(object sender, EventArgs e)
        {
            var btn = (Button)sender;

            if (palavra.Contains(Convert.ToChar(btn.Text)))
                for (int i = 0; i < palavra.Length; i++)
                {
                    if (palavra[i].ToString() == btn.Text)
                    {
                        labels[i].Text = btn.Text;
                        btn.BackColor = Color.FromArgb(0, 210, 105);  // VERDE CLARO NO BOTÃO DA LETRA
                        btn.ForeColor = Color.White;
                    }

                }
            else
            {
                stage++;
                this.Invalidate();
                tentativas--;
                SoundPlayer simpleSound = new SoundPlayer(pasta_aplicacao + @"sons\errou1.wav");
                simpleSound.Play();
                btn.BackColor = Color.FromArgb(255, 128, 128); // VERMELHO CLARO NO BOTÃO DA LETRA
                btn.ForeColor = Color.White;
            }

            label3.Text = (tentativas).ToString();
            btn.Enabled = false;
            ChecarFinal();
        }
        #endregion

        #region VERIFICA AS VOGAIS E VOGAIS COM ACENTOS
        private void btnVogais(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var caractere = ' ';
            for (int i = 0; i < palavra.Length; i++)
            {
                acentos = btn.Text.VerificarVetor();
                caractere = palavra[i].VerificarLetra(acentos);
                if (caractere == palavra[i])
                {
                    if (caractere == 'I')
                        labels[i].TextAlign = ContentAlignment.MiddleCenter; // centraliza a letra no label. Os outros caracteres são alinhados à esquerda.
                    labels[i].Text = caractere.ToString();
                    btn.ForeColor = Color.White;
                    btn.BackColor = Color.FromArgb(0, 210, 105);  // VERDE CLARO NO BOTÃO DA LETRA
                    achou = true;
                }
            }
            if (achou == false)
            {
                tentativas--;
                btn.ForeColor = Color.White;
                btn.BackColor = Color.FromArgb(255, 128, 128); // VERMELHO CLARO NO BOTÃO DA LETRA
                SoundPlayer simpleSound = new SoundPlayer(pasta_aplicacao + @"sons\errou1.wav");
                simpleSound.Play();
                label3.Text = tentativas.ToString();
            }
            achou = false;
            acentos = btn.Text.VerificarVetor();
            VerificarErros(acentos);
            btn.Enabled = false;
            ChecarFinal();
        }
        #endregion

        #region VERIFICA OS ERROS
        private void VerificarErros(char[] acento)
        {
            var verificar = acento.Count(v => palavra.Contains(v));
            if (verificar == 0)
            {

                stage++;
                this.Invalidate();
                label3.Text = (tentativas).ToString(); // TENTATIVAS
            }
        }
        #endregion

        #region VERIFICA SE GANHOU OU PERDEU
        private void ChecarFinal()
        {
            if (tentativas == 0)  // NÃO GANHOU.
            {
                for (int i = 0; i < palavra.Length; i++)
                {
                    labels[i].Text = palavra[i].ToString();
                }
                picture.Visible = true; // mostra a imagem
                picture.Update();
                picture2.Visible = true; // mostra a imagem
                picture2.Update();
                label2.Text = "A palavra é:";
                desmarca_buttons(true);
                ativabotoes(false);
                SoundPlayer simpleSound = new SoundPlayer(pasta_aplicacao + @"sons\rindo.wav");
                simpleSound.Play();

            }
            else
            {
                // ACERTOU A PALAVRA
                var acertos = labels.Count(m => m.Text != "_");
                if (acertos == palavra.Length)
                {
                    picture1.Visible = true; // mostra imagem de parabéns
                    picture1.Update();
                    desmarca_buttons(true);
                    ativabotoes(false);
                    SoundPlayer simpleSound = new SoundPlayer(pasta_aplicacao + @"sons\palmas.wav");
                    simpleSound.Play();
                }
            }
        }
        #endregion

        #region ATIVA OS BOTÕES DAS LETRAS
        private void ativabotoes(bool liga)
        {

            for (int x = 0; x <= flowLayoutPanel1.Controls.Count - 1; x++)
            {
                if (flowLayoutPanel1.Controls[x] is Button)
                    if (liga)
                        flowLayoutPanel1.Controls[x].Enabled = true;
                    else
                        flowLayoutPanel1.Controls[x].Enabled = false;
                flowLayoutPanel1.Controls[x].BackColor = Color.FromArgb(255, 255, 255);  // VERDE CLARO NO BOTÃO DA LETRA
                flowLayoutPanel1.Controls[x].ForeColor = Color.Black;
            }

        }
        #endregion

        #region DESMARCA OS RADIOBUTTONS
        private void desmarca_buttons(bool inibe)
        {

            foreach (Control ctrl in groupBox1.Controls)  // DESMARCA OS RADIOBUTTON
            {
                if (ctrl is RadioButton)
                {
                    RadioButton radioB = (RadioButton)ctrl;
                        if (inibe)
                        {
                            radioB.Enabled = false;
                        }
                        else
                        {
                            radioB.Enabled = true;
                        }
                        radioB.Checked = false;
                }
            }
        }
        #endregion

        #region DESENHO DA FORCA
        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

            SolidBrush solidBrush = new SolidBrush(Color.FromArgb(178, 114, 20));  // define a cor do preenchimento do retângulo
            e.Graphics.DrawLine(new Pen(Color.Black, 2), 90, 190, 190, 190);  // LINHA DA BASE

            e.Graphics.DrawRectangle(new Pen(Color.Brown, 2), 110, 35, 12, 153);   // RETANGULO DA COLUNA
            e.Graphics.FillRectangle(solidBrush, 110, 35, 12, 153);                // PREENCHE O RETÂNGULO

            e.Graphics.DrawRectangle(new Pen(Color.Brown, 2), 100, 45, 80, 12);   // RETANGULO DA TRAVESSA
            e.Graphics.FillRectangle(solidBrush, 100, 45, 80, 12);

            e.Graphics.DrawLine(new Pen(Color.FromArgb(178, 114, 20), 3), 122, 76, 143, 57);  // TRAVESSA INCLINADA
            e.Graphics.DrawLine(new Pen(Color.FromArgb(178, 114, 20), 3), 122, 78, 146, 57);  // TRAVESSA INCLINADA
            e.Graphics.DrawLine(new Pen(Color.FromArgb(178, 114, 20), 3), 122, 80, 149, 57);  // TRAVESSA INCLINADA


            e.Graphics.DrawLine(new Pen(Color.Orange, 4), 163, 58, 163, 70); // DESENHO DA CORDA EM PRETO

            if (stage >= 1)
            {
                e.Graphics.DrawEllipse(new Pen(Color.Black, 2), new Rectangle(153, 70, 20, 20)); // CABEÇA
                e.Graphics.DrawEllipse(new Pen(Color.Black, 2), new Rectangle(157, 77, 2, 2)); // OLHO DIREITO 
                e.Graphics.DrawEllipse(new Pen(Color.Black, 2), new Rectangle(166, 77, 2, 2)); // OLHO ESQUERDO
                e.Graphics.DrawEllipse(new Pen(Color.Black, 1), new Rectangle(159, 83, 9, 3)); // BOCA (X, Y, LARGURA, ALTURA)

            }
            if (stage >= 2)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 163, 90, 163, 130);
            }
            if (stage >= 3)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 163, 100, 144, 115);
            }
            if (stage >= 4)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 163, 100, 180, 115);
            }
            if (stage >= 5)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 162, 130, 144, 156);
            }
            if (stage >= 6)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 163, 130, 180, 156);
                e.Graphics.DrawLine(new Pen(Color.Red, 2), 175, 90, 150, 102);
            }

        }
        #endregion

        #region SAÍDA DO PROGRAMA
        private void button28_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region NOVO JOGO
        private void button27_Click_1(object sender, EventArgs e)
        {
            panel1.Controls.Clear();    // LIMPA O PANEL1 (PALAVRA A SER ACERTADA)
            label3.Text = string.Empty; // NUMERO DE TENTATIVAS
            ativabotoes(false);         // ATIVA OS BOTÕES DAS LETRAS
            tentativas = 6;             // REINICIALIZA NÚMERO DE TENTATIVAS
            stage = 0;                  // REINICIALIZA O VALOR DE ERRO PARA O DESENHA DA FORCA
            label3.Text = tentativas.ToString();
            label2.Text = String.Empty;
            desmarca_buttons(false);
            categoria = -1; // REINICIA O VALOR DO RADIOBUTTON
            this.Invalidate(); // LIMPA A FORCA
            picture.Visible = false;
            picture1.Visible = false;
            picture2.Visible = false;
        }
        #endregion
    }
}
