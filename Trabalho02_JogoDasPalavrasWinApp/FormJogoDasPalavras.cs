using System.Collections.Generic;
using System.Linq;

namespace Trabalho02_JogoDasPalavrasWinApp
{
    public partial class FormJogoDasPalavras : Form
    {
        private JogoDasPalavras jogoDasPalavras;

        public FormJogoDasPalavras()
        {
            InitializeComponent();

            InicializarJogo();
        }

        private void InicializarJogo()
        {
            jogoDasPalavras = new();

            btnEnter.Select();

            btnJogarNovamente.Visible = false;
            btnSair.Visible = false;
            lbAviso.Visible = false;

            plTeclado.Enabled = true;

            ResetarPainelDeLetras();

            ResetarTeclado();

            InicializarRodada();
        }

        private void Teclado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) && plTeclado.Enabled)
                InserirLetra(Char.ToUpper(e.KeyChar));
        }

        private void Teclado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back && plTeclado.Enabled)
                ApagarUltimaLetra();
        }

        private void Teclado_Click(object sender, EventArgs e)
        {
            Button btnTeclado = (Button)sender;

            InserirLetra(Convert.ToChar(btnTeclado.Text));
        }

        private void Enter_Click(object sender, EventArgs e)
        {
            ConfirmaPalavraEscolhida();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            ApagarUltimaLetra();
        }

        private void JogarNovamente_Click(object sender, EventArgs e)
        {
            InicializarJogo();
        }

        private void Sair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConfirmaPalavraEscolhida()
        {
            ObterPalavraEscolhida();

            if (jogoDasPalavras.VerificaPalavraCompleta())
                FinalizarRodada();

            else
                MostrarAvisoPalavraIncompleta(jogoDasPalavras.AvisoPalavraIncompleta(), Color.FromArgb(255, 128, 128));
        }

        private void ObterPalavraEscolhida()
        {
            jogoDasPalavras.palavraEscolhida = "";

            foreach (Control btnLetra in plPainelDeLetras.Controls.Cast<Control>().Reverse().Where(btnLetra => plPainelDeLetras.GetRow(btnLetra) == jogoDasPalavras.rodada))
            {
                jogoDasPalavras.palavraEscolhida += btnLetra.Text;
            }
        }

        private void FinalizarRodada()
        {
            VerificarLetrasNaoExistentes();

            VerificarLetraExistente();

            VerificarPosicaoLetra();

            if (jogoDasPalavras.VerificaSeJogadorGanhou())
                JogadorGanhou();

            else if (jogoDasPalavras.VerificaSeJogadorPerdeu())
                JogadorPerdeu();

            jogoDasPalavras.rodada++;

            InicializarRodada();
        }

        private void JogadorGanhou()
        {
            MostrarMensagemFinal(jogoDasPalavras.AvisoVitoria(), Color.FromArgb(0, 64, 0));
            btnJogarNovamente.Visible = true;
            btnSair.Visible = true;
            plTeclado.Enabled = false;
        }

        private void JogadorPerdeu()
        {
            MostrarMensagemFinal(jogoDasPalavras.AvisoDerrota() + jogoDasPalavras.PalavraSecreta, Color.FromArgb(64, 0, 0));
            btnJogarNovamente.Visible = true;
            btnSair.Visible = true;
            plTeclado.Enabled = false;
        }

        private void VerificarLetrasNaoExistentes()
        {
            foreach (Control btnLetra in plPainelDeLetras.Controls.Cast<Control>().Where(btnLetra => plPainelDeLetras.GetRow(btnLetra) == jogoDasPalavras.rodada))
            {
                btnLetra.BackColor = Color.FromArgb(75, 75, 125);

                foreach (Control btnTeclado in plTeclado.Controls.Cast<Control>().Where(btnTeclado => btnTeclado.Text != "Del" && btnTeclado.Text != "Enter"))
                {
                    if (jogoDasPalavras.CompararLetras(Convert.ToChar(btnTeclado.Text), Convert.ToChar(btnLetra.Text)))
                        btnTeclado.BackColor = Color.FromArgb(25, 25, 75);
                }
            }
        }

        private void VerificarLetraExistente()
        {
            foreach (char letraPalavraSecreta in jogoDasPalavras.PalavraSecreta)
            {
                foreach (Control btnLetra in plPainelDeLetras.Controls.Cast<Control>().Reverse().Where(btnLetra => plPainelDeLetras.GetRow(btnLetra) == jogoDasPalavras.rodada))
                {
                    if (jogoDasPalavras.CompararLetras(Convert.ToChar(btnLetra.Text), letraPalavraSecreta))
                    {
                        btnLetra.BackColor = Color.DarkOrange;

                        foreach (Control btnTeclado in plTeclado.Controls.Cast<Control>().Where(btnTeclado => btnTeclado.Text == btnLetra.Text && btnTeclado.BackColor != Color.Green))
                        {
                            btnTeclado.BackColor = Color.DarkOrange;
                        }
                    }
                }
            }
        }

        private void VerificarPosicaoLetra()
        {
            for (int letra = 0; letra < jogoDasPalavras.PalavraSecreta.Length; letra++)
            {
                List<Control> btnLetras = plPainelDeLetras.Controls.Cast<Control>().Reverse().Where(btnLetras => plPainelDeLetras.GetRow(btnLetras) == jogoDasPalavras.rodada).ToList();

                if (jogoDasPalavras.CompararLetras(Convert.ToChar(btnLetras[letra].Text), jogoDasPalavras.PalavraSecreta[letra]))
                {
                    btnLetras[letra].BackColor = Color.Green;
                    btnLetras[letra].Text = jogoDasPalavras.PalavraSecreta[letra].ToString();

                    foreach (Control btnTeclado in plTeclado.Controls.Cast<Control>().Where(btnTeclado => btnTeclado.Text == btnLetras[letra].Text))
                    {
                        btnTeclado.BackColor = Color.Green;
                    }
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

        private void MostrarMensagemFinal(string mensagem, Color cor)
        {
            lbAviso.Text = mensagem;
            lbAviso.ForeColor = cor;
            lbAviso.Visible = true;
        }

        private void ResetarPainelDeLetras()
        {
            foreach (Control btnLetra in plPainelDeLetras.Controls)
            {
                btnLetra.Text = "";
                btnLetra.BackColor = Color.FromArgb(100, 100, 150);
            }
        }

        private void ResetarTeclado()
        {
            foreach (Control btnTeclado in plTeclado.Controls)
            {
                btnTeclado.BackColor = Color.FromArgb(75, 75, 110);
            }
        }

        private void InicializarRodada()
        {
            foreach (Control btnLetra in plPainelDeLetras.Controls.Cast<Control>().Where(btnLetra => plPainelDeLetras.GetRow(btnLetra) == jogoDasPalavras.rodada))
            {
                btnLetra.BackColor = Color.FromArgb(125, 125, 175);
            }
        }

        private void InserirLetra(char letraTeclado)
        {
            foreach (Control btnLetra in plPainelDeLetras.Controls.Cast<Control>().Reverse().Where(btnLetra => plPainelDeLetras.GetRow(btnLetra) == jogoDasPalavras.rodada && btnLetra.Text == ""))
            {
                btnLetra.Text = letraTeclado.ToString();
                break;
            }
        }

        private void ApagarUltimaLetra()
        {
            foreach (Control btnLetra in plPainelDeLetras.Controls.Cast<Control>().Where(btnLetra => plPainelDeLetras.GetRow(btnLetra) == jogoDasPalavras.rodada && btnLetra.Text != ""))
            {
                btnLetra.Text = "";
                break;
            }
        }

        private void ManterFocoEnter(object sender, EventArgs e)
        {
            btnEnter.Select();
        }
    }
}