using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho02_JogoDasPalavrasWinApp
{
    public class JogoDasPalavras
    {
        public JogoDasPalavras()
        {
            IniciarJogo();
        }

        private string palavraSecreta;

        public string PalavraSecreta { get { return palavraSecreta; } }

        public int rodada = 0;

        public string palavraEscolhida;

        public void IniciarJogo()
        {
            GerarPalavraAleatoria();
        }

        public void GerarPalavraAleatoria()
        {
            int numeroAleatorio;

            string[] palavrasSecretas = new string[]
            {
                "ácido", "adiar", "ímpar", "algar", "amado", "amigo", "anexo", "anuir", "aonde", "apelo",
                "aquém", "argil", "arroz", "assar", "atrás", "ávido", "azeri", "babar", "bagre", "banho",
                "barco", "bicho", "bolor", "brasa", "brava", "brisa", "bruto", "bulir", "caixa", "cansa",
                "chato", "chave", "chefe", "choro", "chulo", "claro", "cobre", "corte", "curar", "deixo",
                "dizer", "dogma", "dores", "duque", "enfim", "estou", "exame", "falar", "fardo", "farto",
                "fatal", "feliz", "ficar", "fogue", "força", "forno", "fraco", "fugir", "fundo", "fúria",
                "gaita", "gasto", "geada", "gelar", "gosto", "grito", "gueto", "honra", "humor", "idade",
                "ideia", "ídolo", "igual", "imune", "índio", "íngua", "irado", "isola", "janta", "jovem",
                "juizo", "largo", "laser", "leite", "lento", "lerdo", "levar", "lidar", "lindo", "lírio",
                "longe", "luzes", "magro", "maior", "malte", "mamar", "manto", "marca", "matar", "meigo",
                "meios", "melão", "mesmo", "metro", "mimos", "moeda", "moita", "molho", "morno", "morro",
                "motim", "muito", "mural", "naipe", "nasci", "natal", "naval", "ninar", "nível", "nobre",
                "noite", "norte", "nuvem", "oeste", "ombro", "ordem", "órgão", "ósseo", "ossos", "outro",
                "ouvir", "palma", "pardo", "passe", "pátio", "peito", "pêlos", "perdo", "períl", "perto",
                "pezar", "piano", "picar", "pilar", "pingo", "pione", "pista", "poder", "porém", "prado",
                "prato", "prazo", "preço", "prima", "primo", "pular", "quero", "quota", "raiva", "rampa",
                "rango", "reais", "reino", "rezar", "risco", "roçar", "rosto", "roubo", "russo", "saber",
                "sacar", "salto", "samba", "santo", "selar", "selos", "senso", "serão", "serra", "servo",
                "sexta", "sinal", "sobra", "sobre", "sócio", "sorte", "subir", "sujei", "sujos", "talão",
                "talha", "tanga", "tarde", "tempo", "tenho", "terço", "terra", "tesão", "tocar", "lacre",
                "laico", "lamba", "lambo", "largo", "larva", "lasca", "laser", "laura", "lavra", "leigo",
                "leite", "leito", "leiva", "lenho", "lento", "leque", "lerdo", "lesão", "lesma", "levar",
                "libra", "limbo", "lindo", "líneo", "lírio", "lisar", "lista", "livro", "logar", "lombo",
                "lotes", "louca", "louco", "louro", "lugar", "luzes", "macio", "maçom", "maior", "malha",
                "malte", "mamar", "mamãe", "manto", "março", "maria", "marra", "marta", "matar", "medir",
                "meigo", "meios", "melão", "menta", "menti", "mesmo", "metro", "miado", "mídia", "mielo",
                "mielo", "milho", "mimos", "minar", "minha", "miolo", "mirar", "missa", "mitos", "moeda",
                "moído", "moita", "molde", "molho", "monar", "monja", "moral", "morar", "morda", "morfo",
                "morte", "mosca", "mosco", "motim", "motor", "mudar", "muito", "mular", "mulas", "múmia",
                "mural", "extra", "falar", "falta", "fardo", "farol", "farto", "fatal", "feixe", "festa",
                "feudo", "fezes", "fiapo", "fibra", "ficha", "fidel", "filão", "filho", "firma", "fisco",
                "fisga", "fluir", "força", "forma", "forte", "fraco", "frade", "friso", "frito", "fugaz",
                "fugir", "fundo", "furor", "furto", "fuzil", "gabar", "gaita", "galho", "ganho", "garoa",
                "garra", "garro", "garvo", "gasto", "gaupe", "gazua", "geada", "gemer", "germe", "gigas",
                "girar", "gizar", "globo", "gosto", "grãos", "graça", "grava", "grito", "grude", "haver",
                "haver", "hiato", "hidra", "hífen", "hímel", "horas", "hotel", "humor", "ideal", "ídolo",
                "igual", "ileso", "imune", "irado", "isola", "jarra", "jaula", "jegue", "jeito", "jogar",
                "jovem", "juíza", "juizo", "julho", "junho", "jurar", "justa"
            };

            Random GerarNumeroAleatorio = new();

            numeroAleatorio = GerarNumeroAleatorio.Next(0, 377);

            palavraSecreta = palavrasSecretas[numeroAleatorio].ToUpper();
        }

        public bool VerificaPalavraCompleta()
        {
            if (palavraEscolhida.Length == 5)
                return true;

            return false;
        }

        public bool VerificaSeJogadorGanhou()
        {
            if (palavraSecreta == palavraEscolhida)
                return true;

            return false;
        }

        public bool VerificaSeJogadorPerdeu()
        {
            if (rodada == 4)
                return true;

            return false;
        }

        public string AvisoPalavraIncompleta()
        {
            return "Palavra incompleta";
        }

        public string AvisoVitoria()
        {
            return "Parabéns, você GANHOU!";
        }

        public string AvisoDerrota()
        {
            return "Infelizmente você PERDEU, a palavra secreta era ";
        }

        public bool CompararLetras(char letra1, char letra2)
        {
            return String.Compare(letra1.ToString(), letra2.ToString(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0;
        }
    }
}
