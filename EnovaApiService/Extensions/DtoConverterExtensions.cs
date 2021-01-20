using System;
using AutoMapper;
using EnovaApi.Models.CommercialDocument;
using EnovaApi.Models.Customer;
using EnovaApiService.Framework;
using Soneta.CRM;
using Soneta.Handel;

namespace EnovaApiService.Extensions
{
    internal static class DtoConverterExtensions
    {
        private static readonly Lazy<IMapper> Mapper = new Lazy<IMapper>(DependencyProvider.Resolve<IMapper>,true);

        public static CommercialDocument ToDto(this DokumentHandlowy document)
        {
            return Mapper.Value.Map<CommercialDocument>(document);
        }

        public static Customer ToDto(this Kontrahent customer)
        {
            return Mapper.Value.Map<Customer>(customer);
        }

        public static DocumentCategory ToDto(this KategoriaHandlowa category)
        {
            switch (category)
            {
                case KategoriaHandlowa.Brak:
                    return DocumentCategory.NoValue;

                case KategoriaHandlowa.Sprzedaż:
                    return DocumentCategory.Invoice;

                case KategoriaHandlowa.KorektaSprzedaży:
                    return DocumentCategory.InvoiceCorrection;

                case KategoriaHandlowa.Zakup:
                    return DocumentCategory.Purchase;

                case KategoriaHandlowa.KorektaZakupu:
                    return DocumentCategory.PurchaseCorrection;

                default:
                    return DocumentCategory.Other;
            }
        }

        public static DocumentState GetDocumentSate(this DokumentHandlowy document)
        {
            return document.Zatwierdzony 
                ? DocumentState.Approved
                : (document.Anulowany 
                    ? DocumentState.Canceled
                    : (document.Bufor ? DocumentState.Buffer : throw  new NotImplementedException()));
        }
    }
}
