using CORE.DTOS;
using MediatR;

namespace CORE.MessagesCQRS.Mediator
{
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediator _mediator;

        public MediatrHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> EnviarComando<T>(T comando) where T : Command
        {
            return await _mediator.Send(comando);
        }

        //public async Task<Guid> EnviarComandoGuid<T>(T comando) where T : GenericoCommandGuid
        //{
        //    return await _mediator.Send(comando);
        //}

        public async Task<ResponseModel<string>> EnviarComandoGenerico<T>(T comando) where T : GenericoCommand
        {
            return await _mediator.Send(comando);
        }

        public async Task PublicarEvento<T>(T evento) where T : EventCQRS
        {
            await _mediator.Publish(evento);
        }
    }
}
