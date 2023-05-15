using System.Collections.Generic;
using System.Linq;
using Trabalho02_JogoDasPalavrasWinApp.RegrasJogoDasPalavras;

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
            jogoDasPalavras = new JogoDasPalavras();

            ConfigurarInterfaceInicial();

            InicializarRodadaColorindoLinha();
        }

        #region Events

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

        private void CentralizarElementos(object sender, EventArgs e)
        {
            plPrincipal.Location = new Point((ClientSize.Width - plPrincipal.Width) / 2, (ClientSize.Height - plPrincipal.Height) / 2);
        }

        private void ManterFocoEnter(object sender, EventArgs e)
        {
            btnEnter.Select();
        }

        #endregion

        private void InserirLetra(char letraTeclado)
        {
            foreach (Button btnLetra in plPainelDeLetras.Controls.Cast<Button>().Reverse().Where(btnLetra => plPainelDeLetras.GetRow(btnLetra) == jogoDasPalavras.rodada && btnLetra.Text == ""))
            {
                btnLetra.Text = letraTeclado.ToString();
                break;
            }
        }

        private void ApagarUltimaLetra()
        {
            foreach (Button btnLetra in plPainelDeLetras.Controls.Cast<Button>().Where(btnLetra => plPainelDeLetras.GetRow(btnLetra) == jogoDasPalavras.rodada && btnLetra.Text != ""))
            {
                btnLetra.Text = "";
                break;
            }
        }

        private void ConfirmaPalavraEscolhida()
        {
            ObterPalavraEscolhida();

            if (jogoDasPalavras.VerificaPalavraCompleta())
                FinalizarRodada();

            else
                MostrarAvisoPalavraIncompleta(Color.FromArgb(255, 128, 128));
        }

        private void ObterPalavraEscolhida()
        {
            jogoDasPalavras.palavraEscolhida = "";

            foreach (Button btnLetra in plPainelDeLetras.Controls.Cast<Button>().Reverse().Where(btnLetra => plPainelDeLetras.GetRow(btnLetra) == jogoDasPalavras.rodada))
            {
                jogoDasPalavras.palavraEscolhida += btnLetra.Text;
            }
        }

        private void FinalizarRodada()
        {
            ColorirConformeAvaliacaoLetras();

            if (jogoDasPalavras.VerificaSeJogadorGanhou())
                JogadorGanhou();

            else if (jogoDasPalavras.VerificaSeJogadorPerdeu())
                JogadorPerdeu();

            jogoDasPalavras.RodadaFinalizada();

            InicializarRodadaColorindoLinha();
        }

        private void ColorirConformeAvaliacaoLetras()
        {
            EstadoLetras[] estadoLetras = jogoDasPalavras.VerificarLetras();

            for (int i = 0; i < estadoLetras.Length; i++)
            {
                List<Button> btnLetras = plPainelDeLetras.Controls.Cast<Button>().Reverse().Where(btnLetras => plPainelDeLetras.GetRow(btnLetras) == jogoDasPalavras.rodada).ToList();

                switch (estadoLetras[i])
                {
                    case EstadoLetras.ExistePosicaoCorreta: btnLetras[i].BackColor = Color.Green; btnLetras[i].Text = jogoDasPalavras.PalavraSecreta[i].ToString(); break;
                    case EstadoLetras.Existe: btnLetras[i].BackColor = Color.DarkOrange; break;
                    case EstadoLetras.NaoExiste: btnLetras[i].BackColor = Color.FromArgb(25, 25, 75); break;
                }

                foreach (Button btnTeclado in plTeclado.Controls.Cast<Button>().Where(btnTeclado => btnTeclado.Text == jogoDasPalavras.palavraEscolhida[i].ToString()))
                {
                    switch (estadoLetras[i])
                    {
                        case EstadoLetras.ExistePosicaoCorreta: btnTeclado.BackColor = Color.Green; break;
                        case EstadoLetras.Existe:
                            if (btnTeclado.BackColor != Color.Green)
                                btnTeclado.BackColor = Color.DarkOrange;
                            break;
                        case EstadoLetras.NaoExiste: btnTeclado.BackColor = Color.FromArgb(25, 25, 75); break;
                    }
                }
            }
        }

        private void JogadorGanhou()
        {
            MostrarMensagemFinal(Color.FromArgb(0, 64, 0));
            btnJogarNovamente.Visible = true;
            btnSair.Visible = true;
            plTeclado.Enabled = false;
        }

        private void JogadorPerdeu()
        {
            MostrarMensagemFinal(Color.FromArgb(64, 0, 0));
            btnJogarNovamente.Visible = true;
            btnSair.Visible = true;
            plTeclado.Enabled = false;
        }

        private void InicializarRodadaColorindoLinha()
        {
            foreach (Button btnLetra in plPainelDeLetras.Controls.Cast<Button>().Where(btnLetra => plPainelDeLetras.GetRow(btnLetra) == jogoDasPalavras.rodada))
            {
                btnLetra.BackColor = Color.FromArgb(125, 125, 175);
            }
        }

        private async void MostrarAvisoPalavraIncompleta(Color cor)
        {
            lbAviso.Text = jogoDasPalavras.AvisoPalavraIncompleta();
            lbAviso.ForeColor = cor;
            lbAviso.Visible = true;

            await Task.Delay(3000);

            lbAviso.Visible = false;
        }

        private void MostrarMensagemFinal(Color cor)
        {
            lbAviso.Text = jogoDasPalavras.MensagemFinal;
            lbAviso.ForeColor = cor;
            lbAviso.Visible = true;
        }

        private void ConfigurarInterfaceInicial()
        {
            btnEnter.Select();

            btnJogarNovamente.Visible = false;
            btnSair.Visible = false;
            lbAviso.Visible = false;

            plTeclado.Enabled = true;

            ResetarPainelDeLetras();

            ResetarTeclado();
        }

        private void ResetarPainelDeLetras()
        {
            foreach (Button btnLetra in plPainelDeLetras.Controls)
            {
                btnLetra.Text = "";
                btnLetra.BackColor = Color.FromArgb(100, 100, 150);
            }
        }

        private void ResetarTeclado()
        {
            foreach (Button btnTeclado in plTeclado.Controls)
            {
                btnTeclado.BackColor = Color.FromArgb(75, 75, 110);
            }
        }
    }
}