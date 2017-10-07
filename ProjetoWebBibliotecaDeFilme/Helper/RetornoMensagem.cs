using ProjetoBibliotecaDeFilme.Enumerador;

namespace ProjetoWebBibliotecaDeFilme.Helper
{
    /// <summary>
    /// Representa mensagem de retorno para Tela.
    /// </summary>
    public class RetornoMensagem
    {
        /// <summary>
        /// Mensagem para Tela
        /// </summary>
        public string Mensagem { get; set; }

        /// <summary>
        /// Tipo da Mensagem.
        /// </summary>
        public TipoMensagem TipoMensagem { get; set; }

        /// <summary>
        /// Indica o resultado da Ação.
        /// </summary>
        public bool Resultado { get; set; }
    }
}