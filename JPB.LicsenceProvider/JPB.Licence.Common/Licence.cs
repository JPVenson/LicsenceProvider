using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPB.Licence.Common
{
    [Serializable]
    public class Licence
    {
        public string For { get; set; }
        public DateTime ValidUntil { get; set; }
        public string ProjectIntegrity { get; set; }
    }
}
