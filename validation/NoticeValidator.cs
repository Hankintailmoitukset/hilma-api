using System.Collections.Generic;
using System.Linq;
using Hilma.Domain.DataContracts.Notices;
using Hilma.Domain.EForms.Contracts;
using Hilma.Domain.Enums;
using Hilma.Domain.Extensions;

namespace Hilma.Domain.Validators.EForms
{
    /// <summary>
    /// Custom validation rules for EForms.
    /// These should be validated in addition to TED API validation when submitting enotices either via UI or API.
    /// To be kept updated with UI validation rules
    /// </summary>
    public class ENoticeValidator
    {
        public static List<ValidationError> Validate(EFormContract eForm, Dictionary<string, HilmaStatistics> hilmaStatistics, int nationalThreshold)
        {
            var result = new List<ValidationError> {
                ValidateBT501(eForm),
                ValidateBT36(eForm),
                ValidateBT58(eForm),
                ValidateBT113(eForm),
                ValidateBT64(eForm),
                ValidateBT729(eForm),
                ValidateBT75(eForm),
                ValidateBT762(eForm),
                ValidateBT98(eForm),
                ValidateBT512Company(eForm),
                ValidateBT15(eForm),
                ValidateBT51(eForm),
                ValidateBT644(eForm),
                ValidateBT512TouchPoint(eForm),
                ValidateHilmaStatistics(eForm, hilmaStatistics, nationalThreshold),
            };

            result.RemoveAll(e => e == null);

            return result;
        }


        public static ValidationError ValidateHilmaStatistics(EFormContract eForm, Dictionary<string, HilmaStatistics> hilmaStatistics, int nationalThreshold)
        {
            // Prior info or exante shouldn't have statistics
            if (hilmaStatistics != null && (eForm.IsPriorInformation() || eForm.IsExAnte()))
            {
                return new ValidationError("hilmaStatistics",
                    "Hilma statistics is not allowed on prior information and ex-ante notices.");
            }

            if (eForm.IsPriorInformation() || eForm.IsExAnte() || (eForm.IsContractModification() && hilmaStatistics == null))
            {
                return null;
            }

            // Contract awards with total value < national threshold do not need to be validated
            if (eForm.IsContractAward() && eForm.GetTotalValue() < nationalThreshold)
            {
                return null;
            }

            if (hilmaStatistics == null || hilmaStatistics.Count == 0)
            {
                return new ValidationError("hilmaStatistics", "HilmaStatistics is required.");
            }

            foreach (var statistics in hilmaStatistics)
            {
                if (!statistics.Value.Validate(eForm, out var err))
                {
                    return new ValidationError($"hilmaStatistics[{statistics.Key}]", err);
                }
            }

            return null;
        }

        public static ValidationError ValidateBT501(EFormContract eForm)
        {
            const string path = "ublExtensions.ublExtension.extensionContent" +
                                ".eformsExtension.organizations.organization.company.partyLegalEntity.companyID";

            var values = eForm.GetCompanyIds();

            return values.Any(id => !id.IsNationalIdentifier())
                ? new ValidationError(path, $"Company Id ({string.Join(", ", values.Where(x => !x.IsNationalIdentifier()))}) is not a valid National Identifier")
                : null;
        }

        public static ValidationError ValidateBT36(EFormContract eForm)
        {
            const string path = "procurementProjectLot.procurementProject.plannedPeriod.durationMeasure";

            var values = eForm
                .GetAllLots()
                .Select(lot => lot
                    .ProcurementProject?
                    .PlannedPeriod?
                    .DurationMeasure?
                    .Value)
                .Where(v => v != null);

            return values.Any(val => val is < 1 or > 999)
                ? new ValidationError(path, "Planned duration has to be between 1 and 999")
                : null;
        }

        public static ValidationError ValidateBT58(EFormContract eForm)
        {
            const string path = "ProcurementProjectLot.ProcurementProject.ContractExtension.MaximumNumberNumeric";

            var values = eForm
                .GetAllLots()
                .Select(lot => lot
                    .ProcurementProject?
                    .ContractExtension?
                    .MaximumNumberNumeric?
                    .Value)
                .Where(v => v != null);

            return values.Any(val => val is < 1 or > 999)
                ? new ValidationError(path, "Contract extension maximum has to be between 1 and 999")
                : null;
        }

        public static ValidationError ValidateBT113(EFormContract eForm)
        {
            const string path = "ProcurementProjectLot.TenderingProcess.FrameworkAgreement.MaximumOperatorQuantity";

            var values = eForm
                .GetAllLots()
                .Select(lot => lot
                    .TenderingProcess?
                    .FrameworkAgreement?
                    .MaximumOperatorQuantity?
                    .Value)
                .Where(v => v != null);

            return values.Any(val => val is < 1 or > 999)
                ? new ValidationError(path, "Maximum operator quantity has to be between 1 and 999")
                : null;
        }

        public static ValidationError ValidateBT64(EFormContract eForm)
        {
            const string path = "ProcurementProjectLot.tenderingTerms.allowedSubcontractTerms.minimumPercent";

            var values = eForm
                .GetAllAllowedSubcontractTermsContracts()
                .Select(a => a
                    .MinimumPercent?
                    .Value)
                .Where(v => v != null);

            return values.Any(val => val is < 1 or > 999)
                ? new ValidationError(path, "Minimum percent has to be between 1 and 999")
                : null;
        }

        public static ValidationError ValidateBT729(EFormContract eForm)
        {
            const string path = "ProcurementProjectLot.tenderingTerms.allowedSubcontractTerms.maximumPercent";

            var values = eForm
                .GetAllAllowedSubcontractTermsContracts()
                .Select(a => a
                    .MaximumPercent?
                    .Value)
                .Where(v => v != null);

            return values.Any(val => val is < 1 or > 999)
                ? new ValidationError(path, "Maximum percent has to be between 1 and 999")
                : null;
        }

        // TODO: This needs to be rethought, currently no visibility info is available in the backend
        public static ValidationError ValidateBT75(EFormContract eForm)
        {
            return null;

            const string path = "ProcurementProjectLot.tenderingTerms.requiredFinancialGuarantee.description";

            var values = eForm
                .GetAllLots()
                .SelectMany(lot => lot
                    .TenderingTerms?
                    .RequiredFinancialGuarantee ?? new List<FinancialGuaranteeContract>())
                .SelectMany(a => a
                    .Description?
                    .Select(d => d.Value))
                .Where(v => v != null);

            return !values.Any()
                ? new ValidationError(path, "Required Financial Guarantee is required")
                : null;
        }

        public static ValidationError ValidateBT762(EFormContract eForm)
        {
            const string path = "ublExtensions.ublExtension.extensionContent.eformsExtension.changes.changeReason.reasonDescription";

            if (!eForm.IsCorrigendum())
            {
                return null;
            }

            var values = eForm
                .GetAllChangeReasons()?
                .SelectMany(c => c.ReasonDescription?
                    .Select(r => r?.Value)
                    .Where(v => !string.IsNullOrWhiteSpace(v)) ?? new List<string>());

            return !values.Any()
                ? new ValidationError(path, "Change reason is required")
                : null;
        }

        public static ValidationError ValidateBT98(EFormContract eForm)
        {
            const string path = "procurementProjectLot.tenderingTerms.tenderValidityPeriod.durationMeasure";

            var values = eForm
                .GetAllLots()
                .Select(lot => lot
                    .TenderingTerms?
                    .TenderValidityPeriod?
                    .DurationMeasure?.Value)
                .Where(v => v != null);

            return values.Any(val => val is < 1 or > 9999)
                ? new ValidationError(path, "Tender validity duration has to be between 1 and 999")
                : null;
        }

        public static ValidationError ValidateBT512Company(EFormContract eForm)
        {
            const string path = "ublExtensions.ublExtension.extensionContent.eformsExtension.organizations.organization.company.postalAddress.postalZone";

            var values = eForm
                .GetAllCompanies()
                .Select(o => o
                    .PostalAddress?
                    .PostalZone?
                    .Value)
                .Where(v => v != null);

            return values.Any(val => val.Length > 20)
                ? new ValidationError(path, "Postal zone can have maximum length of 20 characters")
                : null;
        }

        // TODO: Not possible to use yet as we have no visibility rules in the backend
        public static ValidationError ValidateBT15(EFormContract eForm)
        {
            return null;

            const string path = "procurementProjectLot.tenderingTerms.callForTendersDocumentReference.attachment.externalReference.uri";

            var values = eForm
                .GetAllLots()
                .SelectMany(lot => lot.TenderingTerms.CallForTendersDocumentReference
                    .Select(c => c.Attachment?.ExternalReference?.URI?.Value))
                .Where(v => v != null);

            return values.Any(string.IsNullOrEmpty)
                ? new ValidationError(path, "External reference URI is required")
                : null;
        }

        public static ValidationError ValidateBT51(EFormContract eForm)
        {
            const string path = "procurementProjectLot.tenderingProcess.economicOperatorShortList.maximumQuantity";

            var values = eForm
                .GetAllLots()
                .SelectMany(lot => lot.TenderingProcess.EconomicOperatorShortList?
                    .Select(e => e.MaximumQuantity?.Value) ?? new List<decimal?>())
                .Where(v => v != null);

            return values.Any(val => val is < 1 or > 999)
                ? new ValidationError(path, "Maximum quantity has to be between 1 and 999")
                : null;
        }

        public static ValidationError ValidateBT644(EFormContract eForm)
        {
            const string path = "procurementProjectLot.tenderingTerms.awardingTerms.prize.valueAmount";

            var values = eForm
                .GetAllLots()
                .SelectMany(lot => lot
                    .TenderingTerms?
                    .AwardingTerms?
                    .Prize ?? new List<PrizeContract>())
                .Select(e => e
                    .ValueAmount?
                    .Value)
                .Where(v => v != null);

            return values.Any(val => val is < 1 or > 999999)
                ? new ValidationError(path, "Maximum quantity has to be between 1 and 999")
                : null;
        }

        public static ValidationError ValidateBT512TouchPoint(EFormContract eForm)
        {
            const string path = "ublExtensions.ublExtension.extensionContent.eformsExtension.organizations.organization.touchPoint.postalAddress.postalZone";

            var values = eForm
                    .GetAllOrganizations()
                    .SelectMany(o => o
                        .TouchPoint?
                        .Select(tp => tp
                            .PostalAddress?
                            .PostalZone?
                            .Value)
                        .Where(v => v != null) ?? new List<string>())
                    .Where(v => v != null);

            return values.Any(val => val.Length > 20)
                ? new ValidationError(path, "Postal zone can have maximum length of 20 characters")
                : null;
        }
    }
}
