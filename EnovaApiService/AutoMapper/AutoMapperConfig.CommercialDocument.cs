﻿using AutoMapper;
using EnovaApi.Models.CommercialDocument;
using EnovaApi.Models.Common;
using EnovaApiService.Extensions;
using Soneta.Business;
using Soneta.Handel;
using Soneta.Kasa;

namespace EnovaApiService.AutoMapper
{
    internal static partial class AutoMapperConfig
    {
        private static class CommercialDocument
        {
            public static void ConfigureMappings(IMapperConfigurationExpression cfg)
            {
                cfg.CreateMap<DefDokHandlowego, DocumentDefinition>()
                    .IncludeBase<GuidedRow, GuidedEntity>()
                    .ForMember(d => d.Category, o => o.MapFrom(s => s.Kategoria.ToDto()))
                    .ForMember(d => d.Symbol, o => o.MapFrom(s => s.Symbol))
                    .ForMember(d => d.PrintTitle, o => o.MapFrom(s => s.TytulWydruku))
                    .ForMember(d => d.DateName, o => o.MapFrom(s => s.NazwaDaty))
                    .ForMember(d => d.OperationDateName, o => o.MapFrom(s => s.NazwaDatyOperacji));

                cfg.CreateMap<PozycjaDokHandlowego, CommercialDocumentRow>()
                    .IncludeBase<Row, Entity>()
                    .ForMember(d => d.OrdinalNumber, o => o.MapFrom(s => s.Lp))
                    .ForMember(d => d.ProductGuid, o => o.MapFrom(s => s.Towar.Guid))
                    .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Towar.Nazwa))
                    .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Ilosc.Value))
                    .ForMember(d => d.QuantityUnit, o => o.MapFrom(s => s.Ilosc.Symbol))
                    .ForMember(d => d.QuantityTurnover, o => o.MapFrom(s => s.IloscNaObrotach.Value))
                    .ForMember(d => d.QuantityTurnoverUnit, o => o.MapFrom(s => s.IloscNaObrotach.Symbol))
                    .ForMember(d => d.PriceWithoutTaxAfterDiscount, o => o.MapFrom(s => s.CenaNettoPoRabacie.Value))
                    .ForMember(d => d.TaxPercent, o => o.MapFrom(s => (decimal)s.Stawka.Procent))
                    .ForMember(d => d.TaxName, o => o.MapFrom(s => s.Stawka.ToString()))
                    .ForMember(d => d.TotalValueWithoutTax, o => o.MapFrom(s => s.WartoscCy.Value))
                    .ForMember(d => d.TotalCorrectionValueWithoutTax,
                        o => o.MapFrom(s => s.PozycjaKorygowana != null ? s.PozycjaKorygowana.WartoscCy.Value : 0M))
                    .ForMember(d => d.CorrectionType, o => o.MapFrom(s => s.RodzajKorekty));

                cfg.CreateMap<RodzajKorektyPozycji, PositionCorrectionType>().ConvertUsing<PositionCorrectionTypeConventer>();

                cfg.CreateMap<SumaVAT, DocumentTaxRow>()
                    .ForMember(d => d.TaxName, o => o.MapFrom(s => s.DefinicjaStawki.Kod))
                    .ForMember(d => d.TaxPercent, o => o.MapFrom(s => s.DefinicjaStawki.Stawka.Procent))
                    .ForMember(d => d.TotalValueWithoutTax, o => o.MapFrom(s => s.Suma.Netto))
                    .ForMember(d => d.TotalValueTax, o => o.MapFrom(s => s.Suma.VAT))
                    .ForMember(d => d.TotalValueWithTax, o => o.MapFrom(s => s.Suma.Brutto));

                cfg.CreateMap<Platnosc, DocumentPaymentRow>()
                    .ForMember(d => d.PaymentMethod, o => o.MapFrom(s => s.SposobZaplaty.Nazwa))
                    .ForMember(d => d.DateOfPayment, o => o.MapFrom(s => s.Termin))
                    .ForMember(d => d.PaymentAmount, o => o.MapFrom(s => s.Kwota.Value));

                cfg.CreateMap<DokumentHandlowy, EnovaApi.Models.CommercialDocument.CommercialDocument>()
                    .IncludeBase<GuidedRow, GuidedEntity>()
                    .ForMember(d => d.Definition, o => o.MapFrom(s => s.Definicja))
                    .ForMember(d => d.State, o => o.MapFrom(s => s.GetDocumentSate()))
                    .ForMember(d => d.Date, o => o.MapFrom(s => s.Data))
                    .ForMember(s => s.OperationDate, o => o.MapFrom(s => s.DataOperacji))
                    .ForMember(d => d.DocumentNumber, o => o.MapFrom(s => s.Numer.NumerPelny))
                    //.ForMember(d => d.CorrectedDocument, o => o.MapFrom(s => s.DokumentKorygowanyPierwszy))
                    .ForMember(d => d.CorrectedDocument, o => o.MapFrom(s => GetKorygowanyPierwszy(s.DokumentKorygowany)))
                    .ForMember(d => d.Customer, o => o.MapFrom(s => s.Kontrahent))
                    .ForMember(d => d.TotalValueWithoutTax, o => o.MapFrom(s => s.Suma.Netto))
                    .ForMember(d => d.TotalValueTax, o => o.MapFrom(s => s.Suma.VAT))
                    .ForMember(d => d.TotalValueWithTax, o => o.MapFrom(s => s.Suma.Brutto))
                    .ForMember(d => d.TotalValueWithTaxInWords, o => o.MapFrom(s => s.Suma.BruttoCy.Słownie))
                    .ForMember(d => d.Rows, o => o.MapFrom(s => s.Pozycje))
                    .ForMember(d => d.TaxesSummary, o => o.MapFrom(s => s.SumyVAT))
                    .ForMember(d => d.PaymentSummary, o => o.MapFrom(s => s.Platnosci))
                    .ForMember(s => s.Description, o => o.MapFrom(s => s.Opis));
            }

            class PositionCorrectionTypeConventer : ITypeConverter<RodzajKorektyPozycji, PositionCorrectionType>
            {
                public PositionCorrectionType Convert(RodzajKorektyPozycji source, PositionCorrectionType destination, ResolutionContext context)
                    => source switch
                    {
                        RodzajKorektyPozycji.Brak => PositionCorrectionType.None,
                        RodzajKorektyPozycji.Ceny => PositionCorrectionType.Price,
                        RodzajKorektyPozycji.Ilości => PositionCorrectionType.Quantity,
                        RodzajKorektyPozycji.Zwrot => PositionCorrectionType.Return,
                        RodzajKorektyPozycji.StawkiVAT => PositionCorrectionType.Tax,
                        _ => PositionCorrectionType.Other
                    };
            }

            private static DokumentHandlowy GetKorygowanyPierwszy(DokumentHandlowy dokumentHandlowy)
            {
                if (dokumentHandlowy != null && dokumentHandlowy.DokumentKorygowany != null)
                {
                    return GetKorygowanyPierwszy(dokumentHandlowy.DokumentKorygowany);
                }

                return dokumentHandlowy;
            }
        }
    }
}
