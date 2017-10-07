namespace ProjetoBibliotecaDeFilme.Utils
{
    public class Validacao
    {
        /// <summary>
        /// Recebe valor e valida se valor está vazio ou não.
        /// </summary>
        /// <param name="valor">Valor a ser validado.</param>
        /// <returns>True = Verdadeiro, False = Falso.</returns>
        public static bool EhVazio(string valor)
        {
            return string.IsNullOrEmpty(valor);
        }

        /// <summary>
        /// Recebe valor e valida se este é menor que tamanho informado.
        /// </summary>
        /// <param name="valor">Valor a ser validado.</param>
        /// <param name="tamanho">Valor a ser Comparado.</param>
        /// <returns></returns>
        public static bool TamanhoEhMenor(string valor, int tamanho)
        {
            return !string.IsNullOrEmpty(valor) && valor.Length < tamanho;
        }

        /// <summary>
        /// Recebe valor e valida se este é maioor que tamanho informado.
        /// </summary>
        /// <param name="valor">Valor a ser validado.</param>
        /// <param name="tamanho">Valor a ser Comparado.</param>
        /// <returns></returns>
        public static bool TamanhoEhMaior(string valor, int tamanho)
        {
            return !string.IsNullOrEmpty(valor) && valor.Length > tamanho;
        }
    }
}
