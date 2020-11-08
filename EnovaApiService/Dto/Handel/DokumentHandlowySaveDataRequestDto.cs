using System;
using System.ComponentModel.DataAnnotations;
using EnovaApiService.Dto.Common;
using EnovaApiService.Dto.Kasa;
using Soneta.Kasa;

namespace EnovaApiService.Dto.Handel
{
    public class DokumentHandlowySaveDataRequestDto : BusinessObjectRequestDto
    {
        [Required]
        public Guid DefinicjaGuid { get; set; }

        [Required]
        public Guid KontrahentGuid { get; set; }

        public Guid? MagazynGuid { get; set; }

        [Required]
        public PozycjaDokHanSaveDataDto[] Pozycje { get; set; }

        public PlatnoscDto[] Platnosci { get; set; }
    }
}
