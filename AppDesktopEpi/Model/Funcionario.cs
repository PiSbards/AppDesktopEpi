using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AppDesktopEpi.Model
{
    public class Funcionario
    {
        public int matricula { get; set; }
        public string nome { get; set; }        
        public string epi { get; set; }       
        public DateTime data_entrega { get; set; }
        public DateTime data_vencimento { get; set; }

    }
}
