using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using EnovaApiService.Dto.Common;

namespace EnovaApiService.Dto.Handel
{
    public class PozycjaDokHanSaveDataDto : BusinessObjectRequestDto
    {
        public Guid TowarGuid { get; set; }
        public double Ilosc { get; set; }
        public decimal? CenaNetto { get; set; }
        public decimal? Rabat { get; set; }
    }
}
