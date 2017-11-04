using ProjetoBibliotecaDeFilme.BLL;
using ProjetoBibliotecaDeFilme.Enumerador;
using ProjetoBibliotecaDeFilme.Model;
using ProjetoBibliotecaDeFilme.Utils;
using ProjetoWebBibliotecaDeFilme.Helper;
using ProjetoWebBibliotecaDeFilme.ViewModel.Idiomas;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ProjetoWebBibliotecaDeFilme.Controllers
{
    public class IdiomaController : Controller
    {
        /// <summary>
        /// Armazena Instancia de IdiomaBLO.
        /// </summary>
        
        private readonly ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL.IdiomaBLO _idiomaBLONovo;

        /// <summary>
        /// Contrutor Padrao.
        /// </summary>
        public IdiomaController()
        {
            _idiomaBLONovo = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL.IdiomaBLO();
        }

        /// <summary>
        /// Mostra Lista de Idiomas Cadastrados.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            var view = new IdiomaIndexViewModel();
            return View(view);
        }

        /// <summary>
        /// Metodo que busca a lista de idiomas cadastrados.
        /// </summary>
        /// <param name="nome">Parâmetro para filtrar a lista.</param>
        /// <returns>Partial View de Tabela de Idiomas.</returns>
        [HttpPost]
        public ActionResult BuscarItensIdiomas(string nome)
        {
            var listaIdiomas = _idiomaBLONovo.Listar();

            if (!string.IsNullOrEmpty(nome))
                listaIdiomas
                    = listaIdiomas.Where(x =>
                    x.Descricao.ToUpper().Contains(nome.ToUpper())
                    || x.IdiomaId.ToUpper().Contains(nome.ToUpper()));

            var listaView
                = listaIdiomas
                .Select(x =>
                new Idioma_Item_TabelaViewModel
                {
                    IdiomaId = x.IdiomaId,
                    Descricao = x.Descricao
                }
                ).OrderBy(x => x.IdiomaId).ToList();

            return PartialView("_idioma_Tabela", listaView);
        }

        /// <summary>
        /// Mostra a Pagina Para Cadastrar o Idioma
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        /// <summary>
        /// Recebe os dados da View e envia para o Context
        /// </summary>
        /// <param name="idioma"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Cadastrar(IdiomaViewModel view)
        {
            var retorno = new RetornoMensagem();

            try
            {
                var idioma = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.Idioma
                {
                    IdiomaId = view.IdiomaId,
                    Descricao = view.Descricao
                };

                _idiomaBLONovo.Salvar(idioma);

                retorno.Mensagem
                    = string.Format("Idioma {0} - {1} Cadastrado com Sucesso. <br />", view.IdiomaId, view.Descricao);
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
        /// Mostra a Pagina Para Editar o Idioma.
        /// </summary>
        /// <param name="id">Valor a ser Comparado.</param>
        /// <returns>Retorna View com a Id Encontrada.</returns>
        [HttpGet]
        public ActionResult Editar(string id)
        {
            var idioma = _idiomaBLONovo.BuscarPorId(id);
            var view = new IdiomaViewModel(idioma);
            return View(view);
        }

        /// <summary>
        /// Recebe os dados da View e envia para o Context
        /// </summary>
        /// <param name="view">Valor a ser Editado</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Editar(IdiomaViewModel view)
        {
            var retorno = new RetornoMensagem();
            try
            {
                var idioma = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.Idioma
                {
                    IdiomaId = view.IdiomaId,
                    Descricao = view.Descricao
                };

                _idiomaBLONovo.Editar(idioma);

                retorno.Mensagem
                    = string.Format("Idioma {0} - {1} Editado com Sucesso. <br />", view.IdiomaId, view.Descricao);
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }
            catch (ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils.ProjetoException ex)
            {
                retorno.Mensagem = ex.Message;
                retorno.TipoMensagem = TipoMensagem.Alerta;
                retorno.Resultado = false;
            }
            catch (Exception)
            {
                retorno.Mensagem = "Erro ao Editar.<br />";
                retorno.TipoMensagem = TipoMensagem.Erro;
                retorno.Resultado = false;
            }

            return Json(retorno);
        }

        /// <summary>
        /// Recebe os dados da View e envia para o Context
        /// </summary>
        /// <param name="id">Valor a ser Excluido</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Excluir(string id)
        {
            var retorno = new RetornoMensagem();
            try
            {
                var idiomaMensagem = _idiomaBLONovo.BuscarPorId(id);

                _idiomaBLONovo.Excluir(id);

                retorno.Mensagem
                   = string.Format("Idioma {0} - {1} Excluido com Sucesso. <br />", idiomaMensagem.IdiomaId, idiomaMensagem.Descricao);
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
                retorno.Mensagem = "Erro ao Excluir.<br />";
                retorno.TipoMensagem = TipoMensagem.Erro;
                retorno.Resultado = false;
            }
            return Json(retorno);
        }
    }
}