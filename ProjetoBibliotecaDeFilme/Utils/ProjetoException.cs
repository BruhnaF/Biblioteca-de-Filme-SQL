using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBibliotecaDeFilme.Utils
{
    /// <summary>
    /// Exceções do Projeto.
    /// </summary>
    public class ProjetoException : Exception
    {
        public ProjetoException(string mensagem) : base(mensagem) { }
    }
}
