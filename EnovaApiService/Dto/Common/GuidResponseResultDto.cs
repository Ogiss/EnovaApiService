using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soneta.Business;

namespace EnovaApiService.Dto.Common
{
    public class GuidResponseResultDto : ResponseResultDto
    {
        public Guid Guid { get; private set; }

        public GuidResponseResultDto(Guid guid)
        {
            this.Guid = guid;
        }

        public GuidResponseResultDto(GuidedRow guidedRow)
            : this(guidedRow.Guid) { }
    }
}
