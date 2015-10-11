using ProjetoAluno.Aplicacao;
using ProjetoAluno.Dominio.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoAluno.UI.Web.Controllers
{
    public class HomeController : Controller
    {

        private AlunoAplicacao aplicacao;

        public HomeController()
        {
            aplicacao = new AlunoAplicacao();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Página de Descrição do Aplicativo";

            //return View();

            var data = from aluno in aplicacao.ListarTodos()
                       group aluno by aluno.DataInscricao into dataGrupo

         
                       select new InscricaoDataGrupo()
                       {
                           InscricaoData = dataGrupo.Key,
                           contadorAlunos = dataGrupo.Count()
                       };

            return View(data);

        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Página de Contato";

            return View();
        }
    }
}