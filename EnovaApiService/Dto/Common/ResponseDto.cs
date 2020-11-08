using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soneta.Business;

namespace EnovaApiService.Dto.Common
{
    public class ResponseDto
    {
        public ResponseStatusDto Status { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public ResponseResultDto[] Results { get; set; }

        public string SupportCode { get; set; }
        public ResponseDto()
        {
            ErrorCode = string.Empty;
            ErrorMessage = string.Empty;
            Status = ResponseStatusDto.SUCCESS;
            SupportCode = string.Empty;
        }

        public ResponseDto(ResponseResultDto[] results)
            : this()
        {
            Results = results;
        }

        public ResponseDto(ResponseResultDto result)
            : this()
        {
            Results = new[] { result };
        }

        public ResponseDto(GuidedRow guidedRow)
            : this(new GuidResponseResultDto(guidedRow))
        {

        }
    }
}
