using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.MessagesCQRS
{
    public abstract class MessageCQRS
    {
        protected MessageCQRS()
        {
            TipoMensagem = GetType().Name;
        }

        public string TipoMensagem { get; protected set; }
        public Guid AggregateId { get; protected set; }

        public void SetAggregateId(Guid id) => AggregateId = id;
    }
}