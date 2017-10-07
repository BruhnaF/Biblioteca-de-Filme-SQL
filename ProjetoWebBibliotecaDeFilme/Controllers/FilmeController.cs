using ProjetoBibliotecaDeFilme.BLL;
using ProjetoBibliotecaDeFilme.Enumerador;
using ProjetoBibliotecaDeFilme.Model;
using ProjetoBibliotecaDeFilme.Utils;
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
        private readonly FilmeBLO _filmeBLO;

        private readonly GeneroBLO _generoBLO;
        private readonly IdiomaBLO _idiomaBLO;
        private readonly NomedoFilmeBLO _nomedoFilmeBLO;

        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        public FilmeController()
        {
            _filmeBLO = new FilmeBLO();
            _generoBLO = new GeneroBLO();
            _idiomaBLO = new IdiomaBLO();
            _nomedoFilmeBLO = new NomedoFilmeBLO();
        }

        private static FilmeViewModel filmeTemp { get; set; }

        /// <summary>
        /// Adicionar o Nome do Filme ao FilmeTemp.
        /// </summary>
        /// <param name="codNomeFilme">Nome a ser adicionado.</param>
        /// <returns>Objeto com dados de sucesso ou falha.</returns>
        [HttpPost]
        public ActionResult AdicionarNomeFilme(int idFilme, string nomeFilme, string idioma)
        {
            var retorno = new RetornoMensagem();
            try
            {
                var nomedoFilmeView = new NomedoFilmeViewModel { Nome = nomeFilme, IdiomaId = idioma };
                filmeTemp.ListaNomedoFilme.Add(nomedoFilmeView);

                if (idFilme > 0)
                {
                    var filme = _filmeBLO.BuscarPorId(idFilme);
                    filme.Nomes.Add(new NomedoFilme { Nome = nomeFilme, IdiomaId = idioma });
                    _filmeBLO.Editar(filme);
                }
                
                retorno.Mensagem = ("Nome do Filme e Idioma Adicionado com Sucesso ao Filme. <br />");
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }
            catch (ProjetoException ex)
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
                var idioma = _idiomaBLO.BuscarPorId(codIdioma);
                var view = new IdiomaViewModel(idioma);

                if (filmeTemp.ListaIdiomas.Count(x => x.IdiomaId.Equals(codIdioma)) > 0)
                    throw new ProjetoException(string.Format("{0} Já Adicionado", view.Descricao));

                filmeTemp.ListaIdiomas.Add(view);

                retorno.Mensagem
                   = string.Format("Idioma {0} - {1} Adicionado com Sucesso ao Filme. <br />",
                       idioma.IdiomaId, idioma.Descricao);
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }
            catch (ProjetoException ex)
            {
                retorno.Mensagem = ex.Message;
                retorno.TipoMensagem = TipoMensagem.Alerta;
                retorno.Resultado = false;
            }
            catch (Exception)
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
                var genero = _generoBLO.BuscarPorId(codGenero);

                var view = new GeneroViewModel(genero);

                if (filmeTemp.ListaGeneros.Count(x => x.GeneroId.Equals(codGenero)) > 0)
                    throw new ProjetoException(string.Format("{0} Já Adicionado", view.Descricao));

                filmeTemp.ListaGeneros.Add(view);

                retorno.Mensagem
                  = string.Format("Genero {0} - {1} Adicionado com Sucesso ao Filme. <br />",
                      genero.GeneroId, genero.Descricao);
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }
            catch (ProjetoException ex)
            {
                retorno.Mensagem = ex.Message;
                retorno.TipoMensagem = TipoMensagem.Alerta;
                retorno.Resultado = false;
            }
            catch (Exception)
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

                        var filme = _filmeBLO.Listar().Where(x => x.FilmeId == filmeTemp.FilmeId).FirstOrDefault();

                        if (filme.FilmeId > 0)
                        {
                            var idiomaRemove = filme.Idiomas.Where(x => x.IdiomaId == codIdioma).FirstOrDefault();
                            filme.Idiomas.Remove(idiomaRemove);

                            _filmeBLO.RemoverItensFilme(filme);
                        }
                    }
                }
                else
                    throw new ProjetoException("Idioma não encontrado na Lista.");

                retorno.Mensagem = string.Format("Idioma {0} - {1} Removido com Sucesso do Filme. <br />",
                        idiomaView.IdiomaId, idiomaView.Descricao);
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }
            catch (ProjetoException ex)
            {
                retorno.Mensagem = ex.Message;
                retorno.TipoMensagem = TipoMensagem.Alerta;
                retorno.Resultado = false;
            }
            catch (Exception)
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

                    var filme = _filmeBLO.Listar().Where(x => x.FilmeId == filmeTemp.FilmeId).FirstOrDefault();
                    if (filme.FilmeId > 0)
                    {
                        var generoRemove = filme.Generos.Where(x => x.GeneroId == codGenero).FirstOrDefault();
                        filme.Generos.Remove(generoRemove);

                        _filmeBLO.RemoverItensFilme(filme);
                    }
                }
                else
                    throw new ProjetoException("Genero não encontrado na Lista.");

                retorno.Mensagem
                     = string.Format("Genero {0} - {1} Removido com Sucesso do Filme. <br />",
                        generoView.GeneroId, generoView.Descricao);
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }

            catch (ProjetoException ex)
            {
                retorno.Mensagem = ex.Message;
                retorno.TipoMensagem = TipoMensagem.Alerta;
                retorno.Resultado = false;
            }
            catch (Exception)
            {
                retorno.Mensagem = "Erro ao Remover Genero ao Filme.<br />";
                retorno.TipoMensagem = TipoMensagem.Erro;
                retorno.Resultado = false;
            }

            return Json(retorno);
        }

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
                        var filme = _filmeBLO.Listar().Where(x => x.FilmeId == filmeTemp.FilmeId).FirstOrDefault();
                        var nomeFilmeRemover = filme.Nomes.Where(x => x.Id == idNomeFilme).FirstOrDefault();
                        filme.Nomes.Remove(nomeFilmeRemover);
                        _filmeBLO.RemoverItensFilme(filme);
                    }

                }
                else
                    throw new ProjetoException("NomeFilme não encontrado na Lista.");

                retorno.Mensagem
                     = string.Format("NomeFilme {0} - {1} Removido com Sucesso do Filme. <br />",
                       nomeFilmeView.Nome, nomeFilmeView.IdiomaId);
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }
            catch (ProjetoException ex)
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
            var listaFilmes = _filmeBLO.Listar();
            if (!string.IsNullOrEmpty(nome))
                listaFilmes = listaFilmes.Where
                    (x => x.Nomes.Where(xs=>xs.Nome.Contains(nome)).Count()>0);
            var listaView
                = listaFilmes.Select(x => new Filme_Item_TabelaViewModel
                {
                    FilmeId = x.FilmeId,
                    listaNomeFilme = x.Nomes.Select(xn => new NomedoFilmeViewModel(xn)).ToList()
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
                var filme = new Filme()
                {
                    FilmeId = view.FilmeId,
                    Descricao = view.Descricao,
                    Generos = new List<Genero>(),
                    Idiomas = new List<Idioma>(),
                    Nomes = new List<NomedoFilme>()
                };

                if (filmeTemp.ListaNomedoFilme.Any())
                {
                    foreach (var item in filmeTemp.ListaNomedoFilme)
                    {
                        var nome = new NomedoFilme { IdiomaId = item.IdiomaId, Nome = item.IdiomaId};
                        filme.Nomes.Add(nome);
                    }
                }

                if (filmeTemp.ListaGeneros.Any())
                {
                    foreach (var item in filmeTemp.ListaGeneros)
                    {
                        var genero = new Genero { GeneroId = item.GeneroId, Descricao = item.Descricao };
                        filme.Generos.Add(genero);
                    }
                }

                if (filmeTemp.ListaIdiomas.Any())
                {
                    foreach (var item in filmeTemp.ListaIdiomas)
                    {
                        var idioma = new Idioma { IdiomaId = item.IdiomaId, Descricao = item.Descricao };
                        filme.Idiomas.Add(idioma);
                    }
                }

                _filmeBLO.Salvar(filme);
                retorno.Mensagem
                    = "Filme Cadastrado com Sucesso. <br />";
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }
            catch (ProjetoException ex)
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
            var filme = _filmeBLO.BuscarPorId(id);

            var generos = PegarSelectListaGenero();
            var idiomas = PegarSelectListaIdioma();
            var nomedoFilme = PegarSelectListaNomedoFilme();

            var view = new FilmeViewModel(filme);
            view.Generos.AddRange(generos);
            view.Idiomas.AddRange(idiomas);
            view.NomesdoFilme.AddRange(nomedoFilme);

            filmeTemp = view;

            if (filme.Nomes != null)
            {
                var listaNomesdeFilmes = filme.Nomes.Select(x => new NomedoFilmeViewModel(x));
                view.ListaNomedoFilme.AddRange(listaNomesdeFilmes);
            }

            if (filme.Generos != null)
            {
                var listaGeneros = filme.Generos.Select(x => new GeneroViewModel(x));

                view.ListaGeneros.AddRange(listaGeneros);
            }

            if (filme.Idiomas != null)
            {
                var listaIdiomas = filme.Idiomas.Select(x => new IdiomaViewModel(x));

                view.ListaIdiomas.AddRange(listaIdiomas);
            }

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
                var filme = new Filme()
                {
                    FilmeId = view.FilmeId,
                    Descricao = view.Descricao,
                    Generos = new List<Genero>(),
                    Idiomas = new List<Idioma>(),
                    Nomes = new List<NomedoFilme>()
                };

                if (filmeTemp.ListaGeneros.Any())
                {
                    foreach (var item in filmeTemp.ListaGeneros)
                    {
                        var genero = new Genero { GeneroId = item.GeneroId, Descricao = item.Descricao };
                        filme.Generos.Add(genero);
                    }
                }

                if (filmeTemp.ListaIdiomas.Any())
                {
                    foreach (var item in filmeTemp.ListaIdiomas)
                    {
                        var idioma = new Idioma { IdiomaId = item.IdiomaId, Descricao = item.Descricao };
                        filme.Idiomas.Add(idioma);
                    }
                }

                _filmeBLO.Editar(filme);

                retorno.Mensagem
                    = "Filme Editado com Sucesso. <br />";
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }
            catch (ProjetoException ex)
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
                var filme = _filmeBLO.BuscarPorId(id);

                if (filme.Generos.Count > 0 || (filme.Idiomas.Count > 0))
                {
                    retorno.Mensagem
                   = string.Format("Filme {0} - {1} Possui Generos e/ou Idiomas Adicionados. <br />",
                       filme.FilmeId, filme.Descricao);
                    retorno.TipoMensagem = TipoMensagem.Sucesso;
                    retorno.Resultado = false;
                }
                else
                {
                    _filmeBLO.Excluir(id);

                    retorno.Mensagem
                        = string.Format("Filme {0} - {1} Excluido com Sucesso. <br />",
                            filme.FilmeId, filme.Descricao);
                    retorno.TipoMensagem = TipoMensagem.Sucesso;
                    retorno.Resultado = true;
                }


            }
            catch (ProjetoException ex)
            {
                retorno.Mensagem = ex.Message;
                retorno.TipoMensagem = TipoMensagem.Alerta;
                retorno.Resultado = false;
            }
            catch (Exception)
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
            var generos = _generoBLO.Listar();

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
            var idiomas = _idiomaBLO.Listar();

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
            var nomedoFilme = _nomedoFilmeBLO.Listar();
            if (nomedoFilme != null)
            {
                var selectList = nomedoFilme.Select(x => new SelectListItem
                { Text = x.Nome, Value = x.Id.ToString() }).ToList();
                itens.AddRange(selectList);
            }
            return itens;
        }
    }
}