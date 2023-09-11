using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTOS
{
    public class NfeParaDownload
    {
        public string IdNfe { get; set; }
        public string LinkBlobDownload { get; set; }
        public bool? NfeJaBaixada { get; set; }
    }
}
