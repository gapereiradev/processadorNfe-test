using CORE.DTOS;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.MessagesCQRS
{
    public abstract class GenericoCommand : MessageCQRS, IRequest<ResponseModel<string>>
    {
        public DateTime DataHora { get; private set; }
        public ValidationResult ValidationResult { get; private set; }

        protected GenericoCommand()
        {
            DataHora = DateTime.Now;
        }
        public virtual ValidationResult EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
