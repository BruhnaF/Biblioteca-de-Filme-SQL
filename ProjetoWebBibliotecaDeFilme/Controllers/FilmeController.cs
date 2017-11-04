using ProjetoBibliotecaDeFilme.Enumerador;
using ProjetoWebBibliotecaDeFilme.Helper;
using ProjetoWebBibliotecaDeFilme.ViewModel.Filmes;
using ProjetoWebBibliotecaDeFilme.ViewModel.Generos;
using ProjetoWebBibliotecaDeFilme.ViewModel.Idiomas;
using ProjetoWebBibliotecaDeFilme.ViewModel.NomesdoFilme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProjetoWebBibliotecaDeFilme.Controllers
{
    public class FilmeController : Controller
    {
        /// <summary>
        /// Armazena Instancia de FilmeBLO.
        /// </summary>
        private readonly ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL.FilmeBLO _filmeBLONovo;
        /// <summary>
        /// Armazena Instancia de GeneroBLO.
        /// </summary>
        private readonly ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL.GeneroBLO _generoBLONovo;
        /// <summary>
        ///  Armazena Instancia de IdiomaBLO.
        /// </summary>
        private readonly ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL.IdiomaBLO _idiomaBLONovo;
        /// <summary>
        ///  Armazena Instancia de NomedoFilmeBLO.
        /// </summary>
        private readonly ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL.NomedoFilmeBLO _nomedoFilmeBLONovo;
        /// <summary>
        /// Armazena Instancia de FilmeGeneroBLO.
        /// </summary>
        private readonly ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL.FilmeGeneroBLO _filmeGeneroBLONovo;
        /// <summary>
        /// Armazena Instancia de FilmeIdiomaBLO.
        /// </summary>
        private readonly ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL.FilmeIdiomaBLO _filmeIdiomaBLONovo;

        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        public FilmeController()
        {
            _generoBLONovo = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL.GeneroBLO();
            _idiomaBLONovo = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL.IdiomaBLO();
            _filmeIdiomaBLONovo = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL.FilmeIdiomaBLO();
            _filmeGeneroBLONovo = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL.FilmeGeneroBLO();
            _filmeBLONovo = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL.FilmeBLO();
            _nomedoFilmeBLONovo = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL.NomedoFilmeBLO();
        }

        private static FilmeViewModel filmeTemp { get; set; }

        /// <summary>
        /// Adicionar o Nome do Filme ao FilmeTemp.
        /// </summary>
        /// <param name="nomeFilme">Nome a ser adicionado.</param>
        /// <returns>Objeto com dados de sucesso ou falha.</returns>
        [HttpPost]
        public ActionResult AdicionarNomeFilme(int idFilme, string nomeFilme, string idioma)
        {
            var retorno = new RetornoMensagem();
            try
            {
                var nomedoFilmeView = new NomedoFilmeViewModel { Nome = nomeFilme, IdiomaId = idioma, FilmeId = idFilme };                

                if (filmeTemp.ListaNomedoFilme.Count(x => x.Nome.Equals(nomeFilme) && x.IdiomaId.Equals(idioma)) > 0)
                    throw new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils.ProjetoException(string.Format("{0} Já Adicionado", nomedoFilmeView.Nome));

                filmeTemp.ListaNomedoFilme.Add(nomedoFilmeView);

                retorno.Mensagem = ("Nome do Filme e Idioma Adicionado com Sucesso ao Filme. <br />");
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }

            catch (ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils.ProjetoException ex)
            {
                retorno.Mensagem = ex.Message;
                retorno.TipoMensagem = TipoMensagem.Alerta;
                retorno.Resultado = false;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao Adicionar Nome do Filme ao Filme";
                retorno.TipoMensagem = TipoMensagem.Erro;
                retorno.Resultado = false;
            }
            return Json(retorno);
        }

        /// <summary>
        /// Adiciona Idioma ao FilmeTemp.
        /// </summary>
        /// <param name="codIdioma">Idioma a ser adicionado ao Filme.</param>
        /// <returns>Objeto com dados de Sucesso ou Falha.</returns>
        [HttpPost]
        public ActionResult AdicionarIdioma(string codIdioma)
        {
            var retorno = new RetornoMensagem();
            try
            {
                var idioma = _idiomaBLONovo.BuscarPorId(codIdioma);
                var view = new IdiomaViewModel(idioma);

                if (filmeTemp.ListaIdiomas.Count(x => x.IdiomaId.Equals(codIdioma)) > 0)
                    throw new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils.ProjetoException(string.Format("{0} Já Adicionado", view.Descricao));

                filmeTemp.ListaIdiomas.Add(view);

                retorno.Mensagem
                   = string.Format("Idioma {0} - {1} Adicionado com Sucesso ao Filme. <br />",
                       idioma.IdiomaId, idioma.Descricao);
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }
            catch (ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils.ProjetoException ex)
            {
                retorno.Mensagem = ex.Message;
                retorno.TipoMensagem = TipoMensagem.Alerta;
                retorno.Resultado = false;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao Adicionar Idioma ao Filme.<br />";
                retorno.TipoMensagem = TipoMensagem.Erro;
                retorno.Resultado = false;
            }
            return Json(retorno);
        }

        /// <summary>
        /// Adiciona Genero ao FilmeTemp.
        /// </summary>
        /// <param name="codGenero">cod do Genero a ser Adicionado.</param>
        /// <returns>Objeto com dados de Sucesso ou Falha.</returns>
        [HttpPost]
        public ActionResult AdicionarGenero(int codGenero)
        {
            var retorno = new RetornoMensagem();
            try
            {
                var genero = _generoBLONovo.BuscarPorId(codGenero);

                var view = new GeneroViewModel(genero);

                if (filmeTemp.ListaGeneros.Count(x => x.GeneroId.Equals(codGenero)) > 0)
                    throw new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils.ProjetoException(string.Format("{0} Já Adicionado", view.Descricao));

                filmeTemp.ListaGeneros.Add(view);

                retorno.Mensagem
                  = string.Format("Genero {0} - {1} Adicionado com Sucesso ao Filme. <br />",
                      genero.GeneroId, genero.Descricao);
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }
            catch (ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils.ProjetoException ex)
            {
                retorno.Mensagem = ex.Message;
                retorno.TipoMensagem = TipoMensagem.Alerta;
                retorno.Resultado = false;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao Adicionar Genero ao Filme.<br />";
                retorno.TipoMensagem = TipoMensagem.Erro;
                retorno.Resultado = false;
            }
            return Json(retorno);
        }

        /// <summary>
        /// Remove item da lista de Idiomas.
        /// </summary>
        /// <param name="codIdioma">Idioma a ser Removido.</param>
        /// <returns>>Objeto com dados de Sucesso ou Falha.</returns>
        [HttpPost]
        public ActionResult RemoverIdiomaDaLista(string codIdioma)
        {
            var retorno = new RetornoMensagem();
            try
            {
                var idiomaView = filmeTemp.ListaIdiomas.Where(x => x.IdiomaId == codIdioma).FirstOrDefault();

                if (idiomaView != null)
                {
                    filmeTemp.ListaIdiomas.Remove(idiomaView);

                    if (idiomaView != null)
                    {
                        filmeTemp.ListaIdiomas.Remove(idiomaView);
                        _filmeIdiomaBLONovo.RemoverIdiomaFilme(filmeTemp.FilmeId, codIdioma);
                    }
                }
                else
                    throw new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils.ProjetoException("Idioma não encontrado na Lista.");

                retorno.Mensagem = string.Format("Idioma {0} - {1} Removido com Sucesso do Filme. <br />",
                        idiomaView.IdiomaId, idiomaView.Descricao);
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }
            catch (ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils.ProjetoException ex)
            {
                retorno.Mensagem = ex.Message;
                retorno.TipoMensagem = TipoMensagem.Alerta;
                retorno.Resultado = false;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao Adicionar Idioma ao Filme.<br />";
                retorno.TipoMensagem = TipoMensagem.Erro;
                retorno.Resultado = false;
            }
            return Json(retorno);
        }

        /// <summary>
        /// Remove item da lista de Generos
        /// </summary>
        /// <param name="codGenero">Genero a ser Removido.</param>
        /// <returns>Objeto com dados de Sucesso ou Falha.</returns>
        [HttpPost]
        public ActionResult RemoverGeneroDaLista(int codGenero)
        {
            var retorno = new RetornoMensagem();
            try
            {
                var generoView = filmeTemp.ListaGeneros.Where(x => x.GeneroId == codGenero).FirstOrDefault();

                if (generoView != null)
                {
                    filmeTemp.ListaGeneros.Remove(generoView);
                    _filmeGeneroBLONovo.RemoverGeneroFilme(codGenero, filmeTemp.FilmeId);
                }
                else
                    throw new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils.ProjetoException("Genero não encontrado na Lista.");

                retorno.Mensagem
                     = string.Format("Genero {0} - {1} Removido com Sucesso do Filme. <br />",
                        generoView.GeneroId, generoView.Descricao);
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }

            catch (ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils.ProjetoException ex)
            {
                retorno.Mensagem = ex.Message;
                retorno.TipoMensagem = TipoMensagem.Alerta;
                retorno.Resultado = false;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao Remover Genero ao Filme.<br />";
                retorno.TipoMensagem = TipoMensagem.Erro;
                retorno.Resultado = false;
            }

            return Json(retorno);
        }

        /// <summary>
        /// Remove item da lista de Nomes do Filme
        /// </summary>
        /// <param name="nomeFilme">Nome a ser Removido.</param>
        /// <returns>Objeto com dados de Sucesso ou Falha.</returns>
        [HttpPost]
        public ActionResult RemoverNomeFilmeDaLista(int idNomeFilme, string nomeFilme, string idioma)
        {
            var retorno = new RetornoMensagem();
            try
            {
                var nomeFilmeView = filmeTemp.ListaNomedoFilme.Where(x => x.Nome == nomeFilme && x.IdiomaId == idioma).FirstOrDefault();
                if (nomeFilmeView != null)
                {
                    filmeTemp.ListaNomedoFilme.Remove(nomeFilmeView);

                    if (idNomeFilme > 0)
                    {
                        var filme = _filmeBLONovo.Listar().Where(x => x.FilmeId == filmeTemp.FilmeId).FirstOrDefault();
                        var nomeFilmeRemover = filme.Nomes.Where(x => x.NomedoFilmeId == idNomeFilme).FirstOrDefault();
                        filme.Nomes.Remove(nomeFilmeRemover);

                        _filmeBLONovo.RemoverNomesFilme(idNomeFilme);
                    }

                }
                else
                    throw new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils.ProjetoException("NomeFilme não encontrado na Lista.");

                retorno.Mensagem
                     = string.Format("NomeFilme {0} - {1} Removido com Sucesso do Filme. <br />",
                       nomeFilmeView.Nome, nomeFilmeView.IdiomaId);
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }
            catch (ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils.ProjetoException ex)
            {
                retorno.Mensagem = ex.Message;
                retorno.TipoMensagem = TipoMensagem.Alerta;
                retorno.Resultado = false;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao Remover Nome do Filme.<br />";
                retorno.TipoMensagem = TipoMensagem.Erro;
                retorno.Resultado = false;
            }
            return Json(retorno);
        }


        /// <summary>
        /// Carrega Tabela de Idiomas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CarregarTabelaIdioma()
        {
            var view = new List<IdiomaViewModel>();

            if (filmeTemp.ListaIdiomas.Any())
                view = filmeTemp.ListaIdiomas;

            return PartialView("_tabelaIdiomas", view);
        }

        /// <summary>
        /// Carrega Tabela de Generos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CarregarTabelaGenero()
        {
            var view = new List<GeneroViewModel>();

            if (filmeTemp.ListaGeneros.Any())
                view = filmeTemp.ListaGeneros;

            return PartialView("_tabelaGeneros", view);
        }

        /// <summary>
        /// Carrega a Tabela de Nome do Filme.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CarregarTabelaNomedoFilme()
        {
            var view = new List<NomedoFilmeViewModel>();
            if (filmeTemp.ListaNomedoFilme.Any())
                view = filmeTemp.ListaNomedoFilme;

            return PartialView("_tabelaNomedoFilme", view);
        }

        /// <summary>
        /// Buscar Itens da Lista de Filmes Cadastrados.
        /// </summary>
        /// <param name="nome">Valor a ser Comparado.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BuscarItensFilmes(string nome)
        {
            var listaFilmes = _filmeBLONovo.Listar();
            if (!string.IsNullOrEmpty(nome))
                listaFilmes = listaFilmes.Where
                    (x => x.Nomes.Where(xs => xs.Nome.Contains(nome)).Count() > 0);
            var listaView
                = listaFilmes.Select(x => new Filme_Item_TabelaViewModel
                {
                    FilmeId = x.FilmeId,
                    listaNomeFilmeNovo = x.Nomes.Select(xn => new NomedoFilmeViewModel(xn)).ToList(),
                }
                ).OrderBy(x => x.FilmeId).ToList();
            return PartialView("_filme_Tabela", listaView);
        }

        /// <summary>
        /// Mostra Tela para Cadastrar Filme
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Cadastrar()
        {
            var generos = PegarSelectListaGenero();
            var idiomas = PegarSelectListaIdioma();
            var nomedoFilme = PegarSelectListaNomedoFilme();

            var view = new FilmeViewModel();
            view.Generos.AddRange(generos);
            view.Idiomas.AddRange(idiomas);
            view.NomesdoFilme.AddRange(nomedoFilme);

            filmeTemp = view;

            return View(view);
        }

        /// <summary>
        /// Cadastra Filme
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Cadastrar(FilmeViewModel view)
        {
            var retorno = new RetornoMensagem();

            try
            {
                var filme = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.Filme
                {
                    FilmeId = view.FilmeId,
                    Descricao = view.Descricao,
                    Generos = new List<ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.Genero>(),
                    Idiomas = new List<ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.Idioma>(),
                    Nomes = new List<ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.NomedoFilme>()
                };

                if (filmeTemp.ListaNomedoFilme.Any())
                {
                    foreach (var item in filmeTemp.ListaNomedoFilme)
                    {
                        var nome = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.NomedoFilme { IdiomaId = item.IdiomaId, Nome = item.Nome };
                        filme.Nomes.Add(nome);
                    }
                }

                if (filmeTemp.ListaGeneros.Any())
                {
                    foreach (var item in filmeTemp.ListaGeneros)
                    {
                        var genero = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.Genero { GeneroId = item.GeneroId, Descricao = item.Descricao };
                        filme.Generos.Add(genero);
                    }
                }

                if (filmeTemp.ListaIdiomas.Any())
                {
                    foreach (var item in filmeTemp.ListaIdiomas)
                    {
                        var idioma = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.Idioma { IdiomaId = item.IdiomaId, Descricao = item.Descricao };
                        filme.Idiomas.Add(idioma);
                    }
                }                

                _filmeBLONovo.Salvar(filme);
                retorno.Mensagem
                    = "Filme Cadastrado com Sucesso. <br />";
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }
            catch (ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils.ProjetoException ex)
            {
                retorno.Mensagem = ex.Message;
                retorno.TipoMensagem = TipoMensagem.Alerta;
                retorno.Resultado = false;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao Cadastrar.<br />";
                retorno.TipoMensagem = TipoMensagem.Erro;
                retorno.Resultado = false;
            }
            return Json(retorno);
        }

        /// <summary>
        /// Mostra pagina para Editar Filme.
        /// </summary>
        /// <param name="id">Filme a ser Editado.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Editar(int id)
        {
            var filme = _filmeBLONovo.BuscarPorId(id);

            var generos = PegarSelectListaGenero();
            var idiomas = PegarSelectListaIdioma();

            var view = new FilmeViewModel(filme);
            view.Generos.AddRange(generos);
            view.Idiomas.AddRange(idiomas);

            filmeTemp = view;

            var listaNomesdeFilmes = _nomedoFilmeBLONovo.BuscarporIdFilme(filme.FilmeId).Select(x => new NomedoFilmeViewModel(x));
            view.ListaNomedoFilme.AddRange(listaNomesdeFilmes);

            var listaGeneros = _filmeGeneroBLONovo.BuscarporFilmeId(filme.FilmeId).Select(x => new GeneroViewModel(x));
            view.ListaGeneros.AddRange(listaGeneros);

            var listaIdiomas = _filmeIdiomaBLONovo.BuscarporIdFilme(filme.FilmeId).Select(x => new IdiomaViewModel(x));
            view.ListaIdiomas.AddRange(listaIdiomas);

            return View(view);
        }

        /// <summary>
        /// Editar Filme.
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Editar(FilmeViewModel view)
        {
            var retorno = new RetornoMensagem();
            try
            {
                var filme = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.Filme()
                {
                    FilmeId = view.FilmeId,
                    Descricao = view.Descricao,
                    Generos = new List<ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.Genero>(),
                    Idiomas = new List<ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.Idioma>(),
                    Nomes = new List<ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.NomedoFilme>()
                };

                if (filmeTemp.ListaNomedoFilme.Any())
                {
                    foreach (var item in filmeTemp.ListaNomedoFilme)
                    {
                        var nomedoFilme = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.NomedoFilme { NomedoFilmeId = item.NomedoFilmeId, FilmeId = item.FilmeId, Nome = item.Nome, IdiomaId = item.IdiomaId };
                        filme.Nomes.Add(nomedoFilme);
                    }
                }

                if (filmeTemp.ListaGeneros.Any())
                {
                    foreach (var item in filmeTemp.ListaGeneros)
                    {
                        var genero = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.Genero { GeneroId = item.GeneroId, Descricao = item.Descricao };
                        filme.Generos.Add(genero);
                    }
                }

                if (filmeTemp.ListaIdiomas.Any())
                {
                    foreach (var item in filmeTemp.ListaIdiomas)
                    {
                        var idioma = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.Idioma { IdiomaId = item.IdiomaId, Descricao = item.Descricao };
                        filme.Idiomas.Add(idioma);
                    }
                }

                _filmeBLONovo.Editar(filme);

                retorno.Mensagem
                    = "Filme Editado com Sucesso. <br />";
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }
            catch (ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils.ProjetoException ex)
            {
                retorno.Mensagem = ex.Message;
                retorno.TipoMensagem = TipoMensagem.Alerta;
                retorno.Resultado = false;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao Editar.<br />";
                retorno.TipoMensagem = TipoMensagem.Erro;
                retorno.Resultado = false;
            }
            return Json(retorno);
        }

        /// <summary>
        /// Excluir Filme
        /// </summary>
        /// <param name="id">Valor a ser Excluido</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            var retorno = new RetornoMensagem();
            try
            {
                var filme = _filmeBLONovo.BuscarPorId(id);
                var nomeFilme = _nomedoFilmeBLONovo.BuscarporIdFilme(id);
                var generofilme = _filmeGeneroBLONovo.BuscarporFilmeId(id);
                var idiomafilme = _filmeIdiomaBLONovo.BuscarporIdFilme(id);

                if (generofilme.Count > 0 || (idiomafilme.Count > 0))
                {
                    retorno.Mensagem
                   = string.Format("Filme {0} - {1} Possui Generos e/ou Idiomas Adicionados. <br />",
                       filme.FilmeId, filme.Descricao);
                    retorno.TipoMensagem = TipoMensagem.Sucesso;
                    retorno.Resultado = false;
                }
                else
                {
                    if (nomeFilme.Count > 0)
                    {
                        foreach (var item in nomeFilme)
                        {
                            if (item.FilmeId == filme.FilmeId)
                            {
                                _nomedoFilmeBLONovo.RemoverNomesFilme(item.NomedoFilmeId);
                            }
                        }
                    }

                    _filmeBLONovo.Excluir(id);

                    retorno.Mensagem
                        = string.Format("Filme {0} - {1} Excluido com Sucesso. <br />",
                            filme.FilmeId, filme.Descricao);
                    retorno.TipoMensagem = TipoMensagem.Sucesso;
                    retorno.Resultado = true;
                }
            }
            catch (ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils.ProjetoException ex)
            {
                retorno.Mensagem = ex.Message;
                retorno.TipoMensagem = TipoMensagem.Alerta;
                retorno.Resultado = false;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao Excluir.<br />";
                retorno.TipoMensagem = TipoMensagem.Erro;
                retorno.Resultado = false;
            }
            return Json(retorno);
        }

        /// <summary>
        /// Mostra Tela de Filmes Cadastrados.
        /// </summary>
        /// <returns>Tela de Filmes Cadastrados.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var view = new FilmeIndexViewModel();

            return View(view);
        }

        /// <summary>
        /// Carrega SelectList de Generos.
        /// </summary>
        /// <returns>Retorna Item Selecionado.</returns>
        private List<SelectListItem> PegarSelectListaGenero()
        {
            var itens = new List<SelectListItem>();
            var generos = _generoBLONovo.Listar();

            if (generos != null)
            {
                var selectList = generos.Select(x => new SelectListItem
                { Text = x.Descricao, Value = x.GeneroId.ToString() }).ToList();

                itens.AddRange(selectList);
            }
            return itens;
        }

        /// <summary>
        /// Carrega SelectList de Idiomas.
        /// </summary>
        /// <returns>Retorna Item Selecionado.</returns>
        private List<SelectListItem> PegarSelectListaIdioma()
        {
            var itens = new List<SelectListItem>();
            var idiomas = _idiomaBLONovo.Listar();

            if (idiomas != null)
            {
                var selectList = idiomas.Select(x => new SelectListItem
                { Text = x.Descricao, Value = x.IdiomaId.ToString() }).ToList();

                itens.AddRange(selectList);
            }
            return itens;
        }
        private List<SelectListItem> PegarSelectListaNomedoFilme()
        {
            var itens = new List<SelectListItem>();
            var nomedoFilme = _nomedoFilmeBLONovo.Listar();
            if (nomedoFilme != null)
            {
                var selectList = nomedoFilme.Select(x => new SelectListItem
                { Text = x.Nome, Value = x.NomedoFilmeId.ToString() }).ToList();
                itens.AddRange(selectList);
            }
            return itens;
        }
    }
}