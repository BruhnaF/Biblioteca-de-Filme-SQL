using ProjetoBibliotecaDeFilme.BLL;
using ProjetoBibliotecaDeFilme.Enumerador;
using ProjetoBibliotecaDeFilme.Model;
using ProjetoBibliotecaDeFilme.Utils;
using ProjetoWebBibliotecaDeFilme.Helper;
using ProjetoWebBibliotecaDeFilme.ViewModel.Generos;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ProjetoWebBibliotecaDeFilme.Controllers
{
    public class GeneroController : Controller
    {
        /// <summary>
        /// Armazena Instancia de GeneroBLO.
        /// </summary>
        private readonly ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL.GeneroBLO _generoBLONovo;

        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        public GeneroController()
        {
            _generoBLONovo = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL.GeneroBLO();
        }

        /// <summary>
        /// Mostra lista de Generos Cadastrados.
        /// </summary>
        /// <returns>Tela de Generos Cadastrados.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var view = new GeneroIndexViewModel();
            return View(view);
        }

        /// <summary>
        /// Busca Itens de Generos Cadastrados.
        /// </summary>
        /// <param name="nome">Valor a ser Comparado</param>
        /// <returns>Itens da Lista de Generos</returns>
        [HttpPost]
        public ActionResult BuscarItensGeneros(string nome)
        {
            var listaGeneros = _generoBLONovo.Listar();

            if (!string.IsNullOrEmpty(nome))
                listaGeneros
                    = listaGeneros.Where(x =>
                    x.Descricao.ToUpper().Contains(nome.ToUpper()));                    ;

            var listaView
                = listaGeneros
                .Select(x =>
                new Genero_Item_TabelaViewModel
                {
                    GeneroId = x.GeneroId,
                    Descricao = x.Descricao
                }
                ).OrderBy(x => x.GeneroId).ToList();

            return PartialView("_genero_Tabela", listaView);
        }

        /// <summary>
        /// Mostra a Pagina Para Cadastrar o Genero
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        /// <summary>
        /// Cadastra Genero
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Cadastrar(GeneroViewModel view)
        {
            var retorno = new RetornoMensagem();           

            try
            {
                var genero = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.Genero
                {
                    GeneroId = view.GeneroId,
                    Descricao = view.Descricao
                };

                _generoBLONovo.Salvar(genero);

                retorno.Mensagem
                    = string.Format("Genero {0} Cadastrado com Sucesso. <br />", view.Descricao);
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }
            catch(ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils.ProjetoException ex)
            {
                retorno.Mensagem = ex.Message;
                retorno.TipoMensagem = TipoMensagem.Alerta;
                retorno.Resultado = false;
            }
            catch(Exception ex)
            {
                retorno.Mensagem = "Erro ao Cadastrar.<br />";
                retorno.TipoMensagem = TipoMensagem.Erro;
                retorno.Resultado = false;
            }
            return Json(retorno);
        }

        /// <summary>
        /// Mostra a pagina para Editar o Genero.
        /// </summary>
        /// <param name="id">Valor a ser Comparado.</param>
        /// <returns>Retorna View com a Id Encontrada.</returns>
        [HttpGet]
        public ActionResult Editar(int id)
        {
            var genero = _generoBLONovo.BuscarPorId(id);
            var view = new GeneroViewModel(genero);
            return View(view);
        }

        /// <summary>
        /// Recebe os dados da View e envia para o Context
        /// </summary>
        /// <param name="view">Valor a ser Editado</param>
        /// <returns></returns>
        [HttpPost]
        public  ActionResult Editar(GeneroViewModel view)
        {
            var retorno = new RetornoMensagem();
            try
            {
                var genero = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.Genero
                {
                    GeneroId = view.GeneroId,
                    Descricao = view.Descricao
                };

                _generoBLONovo.Editar(genero);

                retorno.Mensagem
                    = string.Format("Genero {0} - {1} Editado com Sucesso. <br />", view.GeneroId, view.Descricao);
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }
            catch(ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils.ProjetoException ex)
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
        /// Recebe os dados da View e envia para o Context
        /// </summary>
        /// <param name="id">Valor a ser Excluido</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            var retorno = new RetornoMensagem();
            try
            {
                var generoMensagem = _generoBLONovo.BuscarPorId(id);

                _generoBLONovo.Excluir(id);

                retorno.Mensagem
                   = string.Format("Genero {0} - {1} Excluido com Sucesso. <br />", generoMensagem.GeneroId, generoMensagem.Descricao);
                retorno.TipoMensagem = TipoMensagem.Sucesso;
                retorno.Resultado = true;
            }
            catch(ProjetoException ex)
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