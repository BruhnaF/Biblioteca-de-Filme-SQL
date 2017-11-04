using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL
{
    /// <summary>
    /// Classe de Negocios do Filme
    /// </summary>
    public class FilmeBLO
    {
        private readonly FilmeDAO _filmeDAO = new FilmeDAO();
        private readonly IdiomaDAO _idiomaDAO = new IdiomaDAO();
        private readonly GeneroDAO _generoDAO = new GeneroDAO();
        private readonly NomedoFilmeDAO _nomedoFilmeDAO = new NomedoFilmeDAO();
        private readonly FilmeGeneroDAO _filmeGeneroDAO = new FilmeGeneroDAO();
        private readonly FilmeIdiomaDAO _filmeIdiomaDAO = new FilmeIdiomaDAO();

        /// <summary>
        /// Busca por Filmes.
        /// </summary>
        /// <returns>Lista de Filmes.</returns>
        public IEnumerable<Filme> Listar()
        {
            var listaFilme = new List<Filme>();
            var listaNomedoFilme = new List<NomedoFilme>();
            using (SqlConnection objConexao = new SqlConnection(ContextFilme.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    listaFilme = _filmeDAO.Listar(objConexao, objCommand).ToList();

                    foreach (var itemFilme in listaFilme)
                    {
                        itemFilme.Nomes = _nomedoFilmeDAO.BuscarporIdFilme(objCommand, objConexao, itemFilme.FilmeId);
                    }

                    objConexao.Close();
                }
            }
            return listaFilme;
        }

        /// <summary>
        /// Buscar por Id.
        /// </summary>
        /// <param name="id">Id a ser Comparado.</param>
        /// <returns>Valor Encontrado.</returns>
        public Filme BuscarPorId(int id)
        {
            var filme = new Filme();
            using (SqlConnection objConexao = new SqlConnection(ContextFilme.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    filme = _filmeDAO.BuscarPorId(objCommand, objConexao, id);

                    objConexao.Close();
                }
            }
            return filme;
        }

        /// <summary>
        /// Salvar Filme.
        /// </summary>
        /// <param name="filme">Filme a ser Salvo.</param>
        public void Salvar(Filme filme)
        {

            int idFilme = 0;
            using (SqlConnection objConexao = new SqlConnection(ContextFilme.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    idFilme = _filmeDAO.Salvar(objCommand, objConexao, filme);

                    if (idFilme > 0)
                    {
                        foreach (var item in filme.Nomes)
                        {
                            item.FilmeId = idFilme;
                            _nomedoFilmeDAO.Salvar(objCommand, objConexao, item);
                        }

                        foreach (var item in filme.Generos)
                        {
                            _filmeGeneroDAO.Salvar(objCommand, objConexao, idFilme, item.GeneroId);
                        }

                        foreach (var item in filme.Idiomas)
                        {
                            _filmeIdiomaDAO.Salvar(objCommand, objConexao, idFilme, item.IdiomaId);
                        }
                    }

                    ValidaFilme(filme);

                    objConexao.Close();
                }
            }
        }

        /// <summary>
        /// Editar Filme.
        /// </summary>
        /// <param name="filme">Filme a ser Editado.</param>
        public void Editar(Filme filme)
        {
            using (SqlConnection objConexao = new SqlConnection(ContextFilme.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    ValidaFilme(filme);

                    objConexao.Open();

                    var filmedoBanco = BuscarPorId(filme.FilmeId);
                    filmedoBanco.Descricao = filme.Descricao;

                    var listaNomedosFilmesCadastrados = _nomedoFilmeDAO.BuscarporIdFilme(objCommand, objConexao, filme.FilmeId);
                    var listaGenerosFilmesCadastrados = _filmeGeneroDAO.BuscarporIdFilme(objCommand, objConexao, filme.FilmeId);
                    var listaIdiomasFilmesCadastrados = _filmeIdiomaDAO.BuscarporIdFilme(objCommand, objConexao, filme.FilmeId);

                    foreach (var item in filme.Nomes)
                    {
                        if (item.NomedoFilmeId == 0)
                            _nomedoFilmeDAO.Salvar(objCommand, objConexao, item);
                    }

                    foreach (var item in filme.Generos)
                    {
                        if (listaGenerosFilmesCadastrados.Count(x => x == item.GeneroId) == 0)
                            _filmeGeneroDAO.Salvar(objCommand, objConexao, filme.FilmeId, item.GeneroId);
                    }

                    foreach (var item in filme.Idiomas)
                    {
                        if (listaIdiomasFilmesCadastrados.Count(x => x == item.IdiomaId) == 0)
                            _filmeIdiomaDAO.Salvar(objCommand, objConexao, filme.FilmeId, item.IdiomaId);
                    }

                    ValidaFilme(filme);

                    _filmeDAO.Editar(objCommand, objConexao, filmedoBanco);

                    objConexao.Close();
                }
            }
        }

        public void Excluir(int idFilme)
        {
            using (SqlConnection objConexao = new SqlConnection(ContextFilme.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();
                    _filmeDAO.Excluir(objCommand, objConexao, idFilme);
                    objConexao.Close();
                }
            }
        }

        public void RemoverIdiomaFilme(string idiomaId, int filmeId)
        {
            using (SqlConnection objConexao = new SqlConnection(ContextFilme.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();
                    _filmeIdiomaDAO.RemoverIdiomaFilme(objCommand, objConexao, idiomaId, filmeId);
                    objConexao.Close();
                }
            }
        }

        public void RemoverGeneroFilme(int generoId, int filmeId)
        {
            using (SqlConnection objConexao = new SqlConnection(ContextFilme.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();
                    _filmeGeneroDAO.RemoverGeneroFilme(objCommand, objConexao, generoId, filmeId);
                    objConexao.Close();
                }
            }
        }

        public void RemoverNomesFilme(int nomeFilmeId)
        {
            using (SqlConnection objConexao = new SqlConnection(ContextFilme.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();
                    _nomedoFilmeDAO.RemoverNomesFilme(objCommand, objConexao, nomeFilmeId);
                    objConexao.Close();
                }
            }
        }

        /// <summary>
        /// Verifica se Filme já existe no Context.
        /// </summary>
        /// <param name="filme">Filme a ser Comparado.</param>
        public void JaExiste(Filme filme)
        {
            
        }

        /// <summary>
        /// Valida Filme.
        /// </summary>
        /// <param name="filme">Filme a ser Validado.</param>
        public void ValidaFilme(Filme filme)
        {
            var mensagem = new StringBuilder();
            var codigoEhNulo = Validacao.EhVazio(filme.FilmeId.ToString());
            var descricaoEhNulo = Validacao.EhVazio(filme.Descricao);
            var tamanhoDescricaoEhMAior = Validacao.TamanhoEhMaior(filme.Descricao, 50);

            if (codigoEhNulo)
                mensagem.AppendLine("Codigo não pode ser Vazio.<br />");

            if (descricaoEhNulo)
                mensagem.Append("Descrição não pode ser Vazio. <br />");
            if (tamanhoDescricaoEhMAior)
                mensagem.Append("Descrição não pode ser maior que 50 caracteres. <br />");

            var EhOk = !codigoEhNulo && !descricaoEhNulo && !tamanhoDescricaoEhMAior;

            if (!EhOk)
            {
                throw new ProjetoException(mensagem.ToString());
            }
        }
    }
}
