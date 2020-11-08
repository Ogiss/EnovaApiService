using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnovaApiService.Dto.Kasa
{
    public class PlatnoscDto
    {
        public decimal? Kwota { get; set; }
        public SposobZaplatyDto? SposobZaplaty { get; set; }
        public int? TerminDni { get; set; }
    }
}
