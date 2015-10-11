using ProjetoAluno.Dominio;
using ProjetoAluno.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAluno.Aplicacao
{
    public class AlunoAplicacao
    {
        private Contexto contexto;

        private void Inserir(Aluno aluno)
        {
            string strQuery = " INSERT INTO ALUNO (Nome, Sobrenome, DataInscricao) ";
            strQuery += string.Format(" VALUES ('{0}', '{1}', '{2}') ", aluno.Nome, aluno.Sobrenome, aluno.DataInscricao);

            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Aluno aluno)
        {
            string strQuery = " UPDATE ALUNO SET ";
            strQuery += string.Format(" Nome = '{0}', ", aluno.Nome);
            strQuery += string.Format(" Sobrenome = '{0}', ", aluno.Sobrenome);
            strQuery += string.Format(" DataInscricao = '{0}' ", aluno.DataInscricao);
            strQuery += string.Format(" WHERE Id = {0} ", aluno.Id);

            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        public void Salvar(Aluno aluno)
        {
            if (aluno.Id > 0)
            {
                Alterar(aluno);
            }
            else
            {
                Inserir(aluno);
            }
        }

        public void Excluir(int id)
        {
            string strQuery = string.Format(" DELETE FROM ALUNO WHERE Id = {0} ", id);

            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        public List<Aluno> ListarTodos()
        {
            string strQuery = " SELECT * FROM ALUNO ";

            using (contexto = new Contexto())
            {
               var retorno = contexto.ExecutaComandoComRetorno(strQuery);
               return TransformaDataReaderEmLista(retorno);
            }
        }

        public Aluno ListarPorId(int id)
        {
            string strQuery = string.Format(" SELECT * FROM ALUNO WHERE Id = {0} ", id);

            using (contexto = new Contexto())
            {
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaDataReaderEmLista(retorno).FirstOrDefault();
            }
        }

        private List<Aluno> TransformaDataReaderEmLista(SqlDataReader reader)
        {
            var alunos = new List<Aluno>();

            while (reader.Read())
            {
                var temObjetos = new Aluno()
                {
                    Id = int.Parse(reader["Id"].ToString()),//fica td int
                    Nome = reader["Nome"].ToString(),
                    Sobrenome = reader["Sobrenome"].ToString(),
                    DataInscricao = DateTime.Parse(reader["DataInscricao"].ToString())
                };
                alunos.Add(temObjetos);//antes de sair do while add
            }
            reader.Close();//depois do while fecha o data reader
            return alunos;
        }
    }
}
