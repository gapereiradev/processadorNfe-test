using CORE.MessagesCQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfe.CQRS.Command
{
    public class AtualizarNfeIntegracaoCommand : GenericoCommand
    {
        public string NfId { get; set; }
    }
}
