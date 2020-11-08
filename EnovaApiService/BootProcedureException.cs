using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnovaApiService
{
    class BootProcedureException : Exception
    {
        public bool IsCritical { get; set; }

        public BootProcedureException(bool isCritical, string message)
            : base(message)
        {
            IsCritical = isCritical;
        }

        public BootProcedureException(bool isCritical, string message, Exception innerException)
            : base(message, innerException)
        {
            IsCritical = isCritical;
        }
    }
}
