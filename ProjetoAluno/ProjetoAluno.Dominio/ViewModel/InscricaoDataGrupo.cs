using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAluno.Dominio.ViewModel
{

    public class InscricaoDataGrupo
    {
        [DataType(DataType.Date)]
        public DateTime? InscricaoData { get; set; }
        public int contadorAlunos { get; set; }
    }
}
