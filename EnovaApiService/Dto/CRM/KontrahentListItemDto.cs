using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soneta.CRM;
using EnovaApiService.Dto.Common;

namespace EnovaApiService.Dto.CRM
{
    public class KontrahentListItemDto : GuidedRowDto
    {
        public string Kod { get; set; }
        public string Nazwa { get; set; }
        public string NIP { get; set; }
        public bool Blokada { get; set; }
        public bool BlokadaSprzedazy { get; set; }

        public static KontrahentDto FromEnova(Kontrahent kontrahent)
        {
            return new KontrahentDto
            {
                Id = kontrahent.ID,
                Guid = kontrahent.Guid,
                Kod = kontrahent.Kod,
                Nazwa = kontrahent.Nazwa,
                NIP = kontrahent.NIP,
                Blokada = kontrahent.Blokada,
                BlokadaSprzedazy = kontrahent.BlokadaSprzedazy
            };
        }
    }
}
