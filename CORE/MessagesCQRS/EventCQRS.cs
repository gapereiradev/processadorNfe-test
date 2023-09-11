using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.MessagesCQRS
{
    public abstract class EventCQRS : MessageCQRS, INotification
    {
        protected EventCQRS()
        {
            DataHora = DateTime.Now;
        }

        public DateTime DataHora { get; private set; }
    }
}