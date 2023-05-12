namespace Trabalho02_JogoDasPalavrasWinApp
{
    public partial class FormJogoDasPalavras : Form
    {
        private JogoDasPalavras jogoDasPalavras;
        private int contadorPainel;

        public FormJogoDasPalavras()
        {
            InitializeComponent();

            InicializarJogo();
        }

        private void InicializarJogo()
        {
            jogoDasPalavras = new();

            btnJogarNovamente.Visible = false;
            btnSair.Visible = false;
            lbAviso.Visible = false;

            contadorPainel = plPainelDeLetras.ColumnCount * plPainelDeLetras.RowCount - 1;

            foreach (Control c in plPainelDeLetras.Controls)
            {
                c.Text = "";
                c.BackColor = Color.FromArgb(100, 100, 150);
            }

            for (int i = contadorPainel; i > contadorPainel - 5; i--)
            {
                Control c = plPainelDeLetras.Controls[i];

                if (plPainelDeLetras.GetRow(c) == jogoDasPalavras.rodada)
                {
                    c.BackColor = Color.FromArgb(125, 125, 175);
                }
            }

            plTeclado.Controls.OfType<Button>().ToList().ForEach(btn =>
            {
                btn.BackColor = Color.FromArgb(75, 75, 110);
            });

            plTeclado.Enabled = true;
        }

        private void Teclado_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            for (int i = contadorPainel; i > contadorPainel - 5; i--)
            {
                Control c = plPainelDeLetras.Controls[i];

                if (plPainelDeLetras.GetRow(c) == jogoDasPalavras.rodada && c.Text == "")
                {
                    c.Text = btn.Text;
                    break;
                }
            }
        }

        private void Enter_Click(object sender, EventArgs e)
        {
            ObterPalavraEscolhida();

            if (jogoDasPalavras.VerificaPalavraCompleta())
                FinalizarRodada();
            else
                MostrarAvisoPalavraIncompleta(jogoDasPalavras.AvisoPalavraIncompleta(), Color.FromArgb(255, 128, 128));
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            for (int i = contadorPainel - 5; i <= contadorPainel; i++)
            {
                Control c = plPainelDeLetras.Controls[i];

                if (plPainelDeLetras.GetRow(c) == jogoDasPalavras.rodada && c.Text != "")
                {
                    c.Text = "";
                    break;
                }
            }
        }

        private void JogarNovamente_Click(object sender, EventArgs e)
        {
            InicializarJogo();
        }

        private void Sair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ObterPalavraEscolhida()
        {
            jogoDasPalavras.palavraEscolhida = "";

            for (int i = contadorPainel; i > contadorPainel - 5; i--)
            {
                Control c = plPainelDeLetras.Controls[i];

                if (plPainelDeLetras.GetRow(c) == jogoDasPalavras.rodada)
                {
                    jogoDasPalavras.palavraEscolhida += c.Text;
                }
            }
        }

        private void FinalizarRodada()
        {
            for (int i = contadorPainel; i > contadorPainel - 5; i--)
            {
                Control c = plPainelDeLetras.Controls[i];

                if (plPainelDeLetras.GetRow(c) == jogoDasPalavras.rodada)
                {
                    c.BackColor = Color.FromArgb(75, 75, 125);

                    plTeclado.Controls.OfType<Button>().ToList().ForEach(btn =>
                    {
                        if (btn.Text != "Del" && btn.Text != "Enter")
                            if (jogoDasPalavras.CompararLetras(Convert.ToChar(btn.Text), Convert.ToChar(c.Text)))
                                btn.BackColor = Color.FromArgb(25, 25, 75);
                    });
                }
            }

            VerificarLetraExistente();
            VerificarPosicaoLetra();

            if (jogoDasPalavras.VerificaSeJogadorGanhou())
                JogadorGanhou();
            else if (jogoDasPalavras.VerificaSeJogadorPerdeu())
                JogadorPerdeu();

            jogoDasPalavras.rodada++;
            contadorPainel -= 5;

            if (contadorPainel > -1)
            {
                for (int i = contadorPainel; i > contadorPainel - 5; i--)
                {
                    Control c = plPainelDeLetras.Controls[i];

                    if (plPainelDeLetras.GetRow(c) == jogoDasPalavras.rodada)
                    {
                        c.BackColor = Color.FromArgb(125, 125, 175);
                    }
                }
            }
        }

        private void JogadorGanhou()
        {
            MostrarMensagemFinal(jogoDasPalavras.AvisoVitoria(), Color.FromArgb(0, 64, 0));
            btnJogarNovamente.Visible = true;
            btnSair.Visible = true;
        }

        private void JogadorPerdeu()
        {
            MostrarMensagemFinal(jogoDasPalavras.AvisoDerrota() + jogoDasPalavras.PalavraSecreta, Color.FromArgb(64, 0, 0));
            btnJogarNovamente.Visible = true;
            btnSair.Visible = true;
        }

        private void JogarNovamente(object sender, EventArgs e)
        {

        }

        private void MostrarMensagemFinal(string mensagem, Color cor)
        {
            lbAviso.Text = mensagem;
            lbAviso.ForeColor = cor;
            lbAviso.Visible = true;
            plTeclado.Enabled = false;
        }

        private void VerificarLetraExistente()
        {
            foreach (char letra2 in jogoDasPalavras.PalavraSecreta)
            {
                for (int i = contadorPainel; i > contadorPainel - 5; i--)
                {
                    Control letra1 = plPainelDeLetras.Controls[i];

                    if (plPainelDeLetras.GetRow(letra1) == jogoDasPalavras.rodada)
                    {
                        if (jogoDasPalavras.CompararLetras(Convert.ToChar(letra1.Text), letra2))
                        {
                            letra1.BackColor = Color.DarkOrange;

                            plTeclado.Controls.OfType<Button>().ToList().ForEach(btn =>
                            {
                                if (btn.Text == letra1.Text)
                                    btn.BackColor = Color.DarkOrange;
                            });
                        }
                    }
                }
            }
        }

        private void VerificarPosicaoLetra()
        {
            int j = 0;

            for (int i = contadorPainel; i > contadorPainel - 5; i--)
            {
                Control letra1 = plPainelDeLetras.Controls[i];

                if (plPainelDeLetras.GetRow(letra1) == jogoDasPalavras.rodada)
                {
                    Char letra2 = jogoDasPalavras.PalavraSecreta[j];

                    if (jogoDasPalavras.CompararLetras(Convert.ToChar(letra1.Text), letra2))
                    {
                        letra1.BackColor = Color.Green;
                        letra1.Text = letra2.ToString();

                        plTeclado.Controls.OfType<Button>().ToList().ForEach(btn =>
                        {
                            if (btn.Text == letra1.Text)
                                btn.BackColor = Color.Green;
                        });
                    }

                    j++;

                    if (j == 4)
                        return;
                }
            }
        }

        private void CentralizarElementos(object sender, EventArgs e)
        {
            plPrincipal.Location = new Point((ClientSize.Width - plPrincipal.Width) / 2, (ClientSize.Height - plPrincipal.Height) / 2);
        }

        private async void MostrarAvisoPalavraIncompleta(string mensagem, Color cor)
        {
            lbAviso.Text = mensagem;
            lbAviso.ForeColor = cor;
            lbAviso.Visible = true;

            await Task.Delay(3000);

            lbAviso.Visible = false;
        }
    }
}