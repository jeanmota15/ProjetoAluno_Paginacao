using ProjetoAluno.Aplicacao;
using ProjetoAluno.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ProjetoAluno.UI.Web.Controllers
{
    public class AlunoController : Controller
    {

        private AlunoAplicacao aplicacao;

        public AlunoController()
        {
            aplicacao = new AlunoAplicacao();
        }

        public ActionResult Index(string ordem, string pesquisa, string pagina, int? numeroPagina)
        {
            var lista = aplicacao.ListarTodos();
            ViewBag.Sobrenome = string.IsNullOrEmpty(ordem) ? "Sobrenome_desc" : "";
            ViewBag.Data = ordem == "Date" ? "Data_desc" : "Date";

            ViewBag.Ordem = ordem;

            if (!string.IsNullOrEmpty(pesquisa))
            {
                lista = lista.Where(x => x.Nome.ToUpper().Contains(pesquisa.ToUpper()) || 
                    x.Sobrenome.ToUpper().Contains(pesquisa.ToUpper())).ToList();
            }

            if (pesquisa != null)
            {
                numeroPagina = 1;
            }
            else
            {
                pesquisa = pagina;
            }

            ViewBag.Pesquisa = pesquisa;

            switch (ordem)
            {
                case "Sobrenome_desc":
                    lista = lista.OrderByDescending(x => x.Sobrenome).ToList();
                    break;
                case "Date"://tava data
                    lista = lista.OrderBy(x => x.DataInscricao).ToList();
                    break;
                case "Data_desc":
                    lista = lista.OrderByDescending(x => x.DataInscricao).ToList();
                    break;
                default:
                    lista = lista.OrderBy(x => x.DataInscricao).ToList();
                    break;
            }

            int PageSize = 3;//numero por pagina
            int PageNumber = (numeroPagina ?? 1);//começa p.1
            return View(lista.ToPagedList(PageNumber, PageSize));
        }
        public ActionResult Detalhes(int id)
        {
            var lista = aplicacao.ListarPorId(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(lista);
            }
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                aplicacao.Salvar(aluno);
                return RedirectToAction("Index");
            }
            else
            {
                return View(aluno);
            }
        }

        public ActionResult Editar(int id)
        {
            var lista = aplicacao.ListarPorId(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(lista);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                aplicacao.Salvar(aluno);
                return RedirectToAction("Index");
            }
            else
            {
                return View(aluno);
            }
        }

        public ActionResult Excluir(int id)
        {
            var lista = aplicacao.ListarPorId(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(lista);
            }
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirComando(int id)
        {
            aplicacao.Excluir(id);
            return RedirectToAction("Index");
        }
    }
}
