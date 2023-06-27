using System.Collections.Generic;
using System.Linq;
using Hilma.Domain.DataContracts;
using Hilma.Domain.DataContracts.Notices;
using Hilma.Domain.EForms.Contracts;
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
        // Hard coded, should never change
        private const string HanselNationalIdentifier = "0988084-1";

        public static List<ValidationError> Validate(EFormContract eForm, Dictionary<string, HilmaStatistics> hilmaStatistics, int nationalThreshold)
        {
            var result = new List<ValidationError> {
                ValidateBT64(eForm),
                ValidateBT729(eForm),
                ValidateBT75(eForm),
                ValidateBT762(eForm),
                ValidateBT512Company(eForm),
                ValidateBT15(eForm),
                ValidateBT512TouchPoint(eForm),
                ValidateBT738NotSet(eForm),
                ValidateBT150(eForm),
                ValidateHanselESender(eForm),
                ValidateHilmaStatistics(eForm, hilmaStatistics, nationalThreshold),
            };

            result.AddRangeNullSafe(ValidateOrganisationIds(eForm));
            result.AddRangeNullSafe(ValidateBT36(eForm));
            result.AddRangeNullSafe(ValidateBT58(eForm));
            result.AddRangeNullSafe(ValidateBT113(eForm));
            result.AddRangeNullSafe(ValidateBT98(eForm));
            result.AddRangeNullSafe(ValidateBT51(eForm));
            result.AddRangeNullSafe(ValidateBT644(eForm));

            result.RemoveAll(e => e == null);

            return result;
        }


        public static ValidationError ValidateHilmaStatistics(EFormContract eForm, Dictionary<string, HilmaStatistics> hilmaStatistics, int nationalThreshold)
        {
            // Prior info or exante shouldn't have statistics
            if (hilmaStatistics != null && hilmaStatistics.Count > 0 && (eForm.IsPriorInformation() || eForm.IsExAnte()))
            {
                return new ValidationError("hilmaStatistics",
                    "Hilma statistics is not allowed on prior information and ex-ante notices.");
            }

            if (eForm.IsPriorInformation() || eForm.IsExAnte() || (eForm.IsContractModification() && hilmaStatistics == null))
            {
                return null;
            }

            // Contract awards with total value < national threshold do not need to be validated
            if (eForm.IsContractAward() && (eForm.GetTotalValue() ?? 0) < nationalThreshold)
            {
                return null;
            }

            if (hilmaStatistics == null || hilmaStatistics.Count == 0)
            {
                return new ValidationError("hilmaStatistics", "HilmaStatistics is required.");
            }

            var lotIds = eForm.ProcurementProjectLot.Select(x => x.ID.Value);

            foreach (var statistics in hilmaStatistics)
            {
                if (!statistics.Value.Validate(eForm, out var err))
                {
                    return new ValidationError($"hilmaStatistics[{statistics.Key}]", err);
                }

                if (!lotIds.Any(x => statistics.Key == x)) {
                    return new ValidationError($"hilmaStatistics[{statistics.Key}]", $"{statistics.Key} is not a valid LotId");
                }
            }

            return null;
        }

        public static List<ValidationError> ValidateOrganisationIds(EFormContract eForm)
        {
            var errors = new List<ValidationError> { ValidateFinnishOrgIdentifier(eForm.GetPrimaryBuyer()?.CompanyLegalId()) };
            var participatingOrganisationsInFinland = eForm
                .GetParticipatingOrganisations()
                .Where(o => o.Company.PostalAddress.Country.IdentificationCode.Value is "FIN" or "ALA");

            errors.AddRange(participatingOrganisationsInFinland.Select(oid => ValidateFinnishOrgIdentifier(oid.CompanyLegalId())));
            errors = errors.Where(e => e != null).ToList();

            return errors.Any() ? errors : null;
        }

        private static ValidationError ValidateFinnishOrgIdentifier(string value)
        {
            if (value == null)
            {
                return null;
            }

            const string path = "ublExtensions.ublExtension.extensionContent" +
                                ".eformsExtension.organizations.organization.company.partyLegalEntity.companyID";

            return !(value.IsNationalIdentifier() || value.IsPrivateRoadIdentifier())
                ? new ValidationError(path, $"Company Id is not a valid National Identifier or Private Road Identifier")
                : null;
        }

        public static List<ValidationError> ValidateBT36(EFormContract eForm)
        {
            const string path = "procurementProjectLot.procurementProject.plannedPeriod.durationMeasure";
            const int min = 1;
            const int max = 999;

            var values = eForm
                .GetAllLots()
                .ToDictionary(
                    lot => lot.ID.Value,
                    lot => lot
                        .ProcurementProject?
                        .PlannedPeriod?
                        .DurationMeasure?
                        .Value)
                .Where(v => v.Value != null);

            var errors = values.Where(val => val.Value is < min or > max)
                .Select(value => new ValidationError(path, $"Planned duration has to be between {min} and {max} for lot {value.Key}"))
                .ToList();

            return errors.Any() ? errors : null;
        }

        public static List<ValidationError> ValidateBT58(EFormContract eForm)
        {
            const string path = "ProcurementProjectLot.ProcurementProject.ContractExtension.MaximumNumberNumeric";
            const int min = 1;
            const int max = 999;

            var values = eForm
                .GetAllLots()
                .ToDictionary(
                    lot => lot.ID.Value,
                    lot => lot
                        .ProcurementProject?
                        .ContractExtension?
                        .MaximumNumberNumeric?
                        .Value)
                .Where(v => v.Value != null);

            var errors = values.Where(val => val.Value is < min or > max)
                .Select(value => new ValidationError(path, $"Contract extension maximum has to be between {min} and {max} for lot {value.Key}"))
                .ToList();

            return errors.Any() ? errors : null;
        }

        public static List<ValidationError> ValidateBT113(EFormContract eForm)
        {
            const string path = "ProcurementProjectLot.TenderingProcess.FrameworkAgreement.MaximumOperatorQuantity";
            const int min = 1;
            const int max = 999;

            var values = eForm
                .GetAllLots()
                .ToDictionary(
                    lot => lot.ID.Value,
                    lot => lot
                        .TenderingProcess?
                        .FrameworkAgreement?
                        .MaximumOperatorQuantity?
                        .Value)
                .Where(v => v.Value != null);

            var errors = values.Where(val => val.Value is < min or > max)
                .Select(value => new ValidationError(path, $"Maximum operator quantity has to be between {min} and {max} for lot {value.Key}"))
                .ToList();

            return errors.Any() ? errors : null;
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

        public static List<ValidationError> ValidateBT98(EFormContract eForm)
        {
            const string path = "procurementProjectLot.tenderingTerms.tenderValidityPeriod.durationMeasure";
            const int min = 1;
            const int max = 9999;

            var values = eForm
                .GetAllLots()
                .ToDictionary(
                    lot => lot.ID.Value,
                    lot => lot
                        .TenderingTerms?
                        .TenderValidityPeriod?
                        .DurationMeasure?.Value)
                .Where(v => v.Value != null);

            var errors = values.Where(val => val.Value is < min or > max)
                .Select(value => new ValidationError(path, $"Tender validity duration hasto be between {min} and {max} for lot {value.Key}"))
                .ToList();

            return errors.Any() ? errors : null;
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

        public static List<ValidationError> ValidateBT51(EFormContract eForm)
        {
            const string path = "procurementProjectLot.tenderingProcess.economicOperatorShortList.maximumQuantity";
            const int min = 1;
            const int max = 999;

            var values = eForm
                .GetAllLots()
                .ToDictionary(
                    lot => lot.ID.Value,
                    lot => lot.TenderingProcess?.EconomicOperatorShortList?
                        .Where(e => e?.LimitationDescription?
                            .Any(ld => ld.Value.Equals("true", System.StringComparison.OrdinalIgnoreCase)) == true)
                        .Select(e => e.MaximumQuantity?.Value));

            var errors = values
                .Where(val => val.Value?.Any(x => x is null) == true || val.Value?.Any(x => x is < min or > max) == true)
                .Select(value => new ValidationError(path, $"Maximum quantity has to be between {min} and {max} for lot {value.Key}"))
                .ToList();

            return errors.Any() ? errors : null;
        }

        public static List<ValidationError> ValidateBT644(EFormContract eForm)
        {
            const string path = "procurementProjectLot.tenderingTerms.awardingTerms.prize.valueAmount";
            const int min = 1;
            const int max = 999999;

            var values = eForm
                .GetAllLots()
                .ToDictionary(
                    lot => lot.ID.Value,
                    lot => lot
                    .TenderingTerms?
                    .AwardingTerms?
                    .Prize?
                    .Where(e => e
                        .ValueAmount?
                        .Value != null)
                    .Select(e => e.ValueAmount.Value));

            var errors = values
                .Where(val => val.Value?.Any(x => x.Value is < min or > max) == true)
                .Select(value => new ValidationError(path, $"Prize amount has to be between {min} and {max} for lot {value.Key}"))
                .ToList();

            return errors.Any() ? errors : null;
        }

        public static ValidationError ValidateBT150(EFormContract eForm)
        {
            if (!eForm.IsContractAward() && !eForm.IsExAnte())
            {
                return null;
            }

            const string path = "ublExtensions.ublExtension.extensionContent.eformsExtension.noticeResult.settledContract.contractReference.id";

            var contracts = eForm.GetAllContracts();
            var references = contracts
                .Where(c => c.ContractReference != null)
                .SelectMany(c => c.ContractReference);

            var emptyValues = references
                    .Select(r => r?.ID?.Value)
                    .Where(v => string.IsNullOrWhiteSpace(v)) ?? new List<string>();

            return emptyValues?.Any() == true || contracts.Count() != references.Count()
                ? new ValidationError(path, "SettledContract ContractReference ID is required")
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

        public static ValidationError ValidateBT738NotSet(EFormContract eForm)
        {
            const string path = "requestedPublicationDate";

            return !string.IsNullOrEmpty(eForm.RequestedPublicationDate?.Value)
                ? new ValidationError(path, "Requested Publication Date is not supported")
                : null;
        }

        public static ValidationError ValidateHanselESender(EFormContract eForm)
        {
            const string path = "ublExtensions.ublExtension.extensionContent.eformsExtension.organizations.organization.company.partyLegalEntity.companyID";

            var eSenders = eForm.GetESenders();

            return eSenders.Any(e =>
                e?.Company?.PartyLegalEntity?.FirstOrDefault()?.CompanyID?.Value != HanselNationalIdentifier)
                ? new ValidationError(path, "Only Hansel can be set as eSender")
                : null;
        }
    }
}
