using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Unity;
using EnovaApiService.Framework.Helpers;
using EnovaApiService.Dto.Common;
using EnovaApiService.Dto.Handel;
using EnovaApiService.Enova;
using Soneta.Types;
using Soneta.Business;
using Soneta.CRM;
using Soneta.Handel;
using Soneta.Magazyny;
using Soneta.Towary;
using Soneta.Kasa;
using EnovaApiService.Dto.Kasa;

namespace EnovaApiService.Controllers
{
    public class DokumentyHandloweController : ApiController
    {
        public IHttpActionResult Post(DokumentHandlowySaveDataRequestDto request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DokumentHandlowy dokumentHandlowy = null;

                    using (Session session = EnovaClient.Login.CreateSession(false, false))
                    {
                        HandelModule hm = HandelModule.GetInstance(session);
                        CRMModule cm = CRMModule.GetInstance(session);
                        MagazynyModule mm = MagazynyModule.GetInstance(session);
                        TowaryModule tm = TowaryModule.GetInstance(session);
                        KasaModule km = KasaModule.GetInstance(session);

                        using (ITransaction t = session.Logout(true))
                        {
                            dokumentHandlowy = new DokumentHandlowy();
                            var definicja = hm.DefDokHandlowych[request.DefinicjaGuid];
                            dokumentHandlowy.Definicja = definicja;

                            var magazyn = request.MagazynGuid.HasValue ? mm.Magazyny[request.MagazynGuid.Value] : mm.Magazyny.Firma;
                            dokumentHandlowy.Magazyn = magazyn;

                            hm.DokHandlowe.AddRow(dokumentHandlowy);

                            request.Features?.Foreach((feature) =>
                            {
                                dokumentHandlowy.Features[feature.Name] = feature.Value;
                            });

                            Kontrahent kontrahent = cm.Kontrahenci[request.KontrahentGuid];
                            dokumentHandlowy.Kontrahent = kontrahent;

                            foreach (var pozDto in request.Pozycje)
                            {
                                Towar towar = tm.Towary[pozDto.TowarGuid];
                                PozycjaDokHandlowego pozycja = new PozycjaDokHandlowego(dokumentHandlowy);
                                hm.PozycjeDokHan.AddRow(pozycja);

                                pozycja.Towar = towar;
                                pozycja.Ilosc = new Quantity(pozDto.Ilosc);
                                if (pozDto.CenaNetto.HasValue)
                                {
                                    pozycja.Cena = new Soneta.Types.DoubleCy(pozDto.CenaNetto.Value);
                                }
                                if (pozDto.Rabat.HasValue)
                                {
                                    pozycja.Rabat = new Percent(pozDto.Rabat.Value / 100M);
                                }
                            }

                            session.Events.Invoke();

                            if (request.Platnosci?.Any() ?? false)
                            {
                                var naleznosc = (Naleznosc)dokumentHandlowy.Platnosci.GetNext();
                                var pozostalaKwota = naleznosc.Kwota;

                                request.Platnosci?.Foreach((platnoscDto) =>
                                {
                                    if(naleznosc == null)
                                    {
                                        naleznosc = new Naleznosc(dokumentHandlowy);
                                        km.Platnosci.AddRow(naleznosc);
                                    }

                                    if (platnoscDto.SposobZaplaty.HasValue)
                                    {
                                        switch (platnoscDto.SposobZaplaty.Value)
                                        {
                                            case SposobZaplatyDto.Gotowka:
                                                naleznosc.SposobZaplaty = km.SposobyZaplaty.Gotówka;
                                                break;
                                            case SposobZaplatyDto.Przelew:
                                                naleznosc.SposobZaplaty = km.SposobyZaplaty.Przelew;
                                                break;
                                        }
                                    }

                                    if (platnoscDto.Kwota.HasValue)
                                    {
                                        naleznosc.Kwota = platnoscDto.Kwota.Value;
                                        pozostalaKwota -= platnoscDto.Kwota.Value;
                                    }
                                    else
                                    {
                                        naleznosc.Kwota = pozostalaKwota;
                                        pozostalaKwota = 0;
                                    }

                                    if (platnoscDto.TerminDni.HasValue)
                                    {
                                        naleznosc.TerminDni = platnoscDto.TerminDni.Value;
                                    }

                                    if(naleznosc.SposobZaplaty == km.SposobyZaplaty.Gotówka && naleznosc.TerminDni == 0)
                                    {
                                        RaportESP raport = ((Kasa)naleznosc.EwidencjaSP).NowyRaport(dokumentHandlowy, dokumentHandlowy.Data);
                                        Wplata wplata = new Wplata(dokumentHandlowy, raport);
                                        km.Zaplaty.AddRow(wplata);
                                        wplata.Podmiot = dokumentHandlowy.Kontrahent;
                                        wplata.SposobZaplaty = naleznosc.SposobZaplaty;
                                        wplata.Opis = "?";
                                        wplata.Kwota = naleznosc.Kwota;
                                    }

                                    naleznosc = null;
                                });
                            }

                            session.Events.Invoke();

                            dokumentHandlowy.Stan = StanDokumentuHandlowego.Zatwierdzony;
                            t.Commit();
                        }

                        session.Save();
                    }

                    return Ok(new ResponseDto(dokumentHandlowy));
                }
                catch (FeatureRequiredVerifier ex)
                {
                    return BadRequest(ex.Description);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }
            return BadRequest(ModelState);
        }
    }
}
