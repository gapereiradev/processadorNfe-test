using CORE.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.MessagesCQRS.Mediator
{
    public interface IMediatrHandler
    {
        Task PublicarEvento<T>(T evento) where T : EventCQRS;
        Task<bool> EnviarComando<T>(T comando) where T : Command;
        Task<ResponseModel<string>> EnviarComandoGenerico<T>(T comando) where T : GenericoCommand;
        //Task<Guid> EnviarComandoGuid<T>(T comando) where T : GenericoCommandGuid;
    }
}
