using System;
using System.Collections.Generic;
using System.Linq;
using Hilma.Domain.DataContracts.Notices;
using Hilma.Domain.EForms.Contracts;
using Hilma.Domain.Extensions;
using ProcurementProjectContract = Hilma.Domain.EForms.Contracts.ProcurementProjectContract;

namespace Hilma.Domain.Validators.EForms
{
    /// <summary>
    /// National tailoring validator for eForms.
    /// </summary>
    public class ENoticeValidator
    {
        // Hard coded, should never change
        private const string HanselNationalIdentifier = "0988084-1";
        public static readonly string[] SupportedEFormsSdkVersions = { "eforms-sdk-1.7" };
        private static readonly IReadOnlySet<string> ExactAllowedCodesForCleanVehicles = new HashSet<string>(StringComparer.Ordinal)
            { "60000000" ,"60112000", "60130000", "60140000", "90511000", "60160000",
              "60161000", "64121100", "64121200", "60100000", "90510000", "64121000", "34100000" };
        private static readonly IReadOnlyList<string> WildCardAllowedCodesForCleanVehicles = new List<string> { "3411", "3412", "3413", "3414" };
        private const string PathForBT717 = "procurementProjectLot.tenderingTerms.ublExtensions.ublExtension.extensionContent.eformsExtension.strategicProcurement.applicableLegalBasis";
        private const string PathForBT735 = "procurementProjectLot.tenderingTerms.ublExtensions.ublExtension.extensionContent" +
            ".eformsExtension.strategicProcurement.strategicProcurementInformation.procurementCategoryCode";

        public static List<ValidationError> Validate(EFormContract eForm, Dictionary<string, HilmaStatistics> hilmaStatistics, int nationalThreshold)
        {
            var errors = ValidateHilmaStatistics(eForm, hilmaStatistics, nationalThreshold);
            errors.Add(ValidateEFormsSdkVersion(eForm));
            errors.AddRange(new List<ValidationError> {
                ValidateBT738NotSet(eForm),
                ValidateHanselESender(eForm),
            });

            errors.AddRange(ValidateBT501OrganizationCompany(eForm));
            errors.AddRange(ValidateBT507OrganizationCompany(eForm));
            errors.AddRange(ValidateBT09bProcedure(eForm));
            errors.AddRange(ValidateBT51Lot(eForm));
            errors.AddRange(ValidateNDPrize(eForm));
            errors.AddRange(ValidateBT67aProcedure(eForm));
            errors.AddRange(ValidateBT67bProcedure(eForm));
            errors.AddRange(ValidateBT748Lot(eForm));
            errors.AddRange(ValidateBT75Lot(eForm));
            errors.AddRange(ValidateBT150Contract(eForm));
            errors.AddRange(ValidateBT539Lot(eForm));
            errors.AddRange(ValidateBT161NoticeResult(eForm));
            errors.AddRange(ValidateBT717LotAndBT735Lot(eForm));

            errors.RemoveAll(e => e is null);

            return errors;
        }

        public static ValidationError ValidateEFormsSdkVersion(EFormContract eForm)
        {
            if (eForm.CustomizationID?.Value == null || !IsSdkVersionSupported(eForm.CustomizationID.Value))
            {
                return new ValidationError(
                    "EForms SDK Version",
                    $"Unsupported EForms SDK Version {eForm.CustomizationID?.Value}");
            }

            return null;
        }

        private static bool IsSdkVersionSupported(string sdkVersion)
        {
            return SupportedEFormsSdkVersions.Contains(sdkVersion)
                   || SupportedEFormsSdkVersions.Any(v => sdkVersion.StartsWith($"{v}."));
        }

        /// <summary>
        /// See HilmaStatisics.cs for more details
        /// </summary>
        public static List<ValidationError> ValidateHilmaStatistics(EFormContract eForm, Dictionary<string, HilmaStatistics> hilmaStatistics, int nationalThreshold)
        {
            // Prior info or exante shouldn't have statistics
            if (hilmaStatistics is not null && hilmaStatistics.Count > 0 && (eForm.IsPriorInformation() || eForm.IsExAnte()))
            {
                return new List<ValidationError>() {
                    new ValidationError(
                        "hilmaStatistics",
                        "Hilma statistics is not allowed on prior information and ex-ante notices."
                    )
                };
            }

            if (eForm.IsPriorInformation() || eForm.IsExAnte() || (eForm.IsContractModification() && hilmaStatistics is null))
            {
                return new List<ValidationError>(0);
            }

            // Contract awards with total value < national threshold do not need to be validated
            if (eForm.IsContractAward() && (eForm.GetTotalValue() ?? 0) < nationalThreshold)
            {
                return new List<ValidationError>(0);
            }

            if (hilmaStatistics is null || hilmaStatistics.Count == 0)
            {
                return new List<ValidationError>() {
                    new ValidationError("hilmaStatistics", "HilmaStatistics is required.")
                };
            }

            var lotIds = eForm.ProcurementProjectLot.Select(x => x.ID.Value);
            var errors = new List<ValidationError>();

            foreach (var lotId in lotIds)
            {
                if (!hilmaStatistics.TryGetValue(lotId, out var hilmaStatisticsLot))
                {
                    errors.Add(new ValidationError($"hilmaStatistics[{lotId}]", $"hilmaStatistics.{lotId} is required."));

                    continue;
                }

                if (!hilmaStatisticsLot.Validate(eForm, out var err))
                {
                    errors.Add(new ValidationError($"hilmaStatistics[{lotId}]", err));
                }
            }

            foreach (var statistics in hilmaStatistics)
            {
                if (!lotIds.Any(x => statistics.Key == x)) {
                    errors.Add(new ValidationError($"hilmaStatistics[{statistics.Key}]", $"hilmaStatistics.{statistics.Key} is not a valid LotId."));
                }
            }

            return errors;
        }

        /// <summary>
        /// BT-738-notice
        /// Notice Preferred Publication Date
        /// RequestedPublicationDate is not allowed
        /// </summary>
        public static ValidationError ValidateBT738NotSet(EFormContract eForm)
        {
            const string path = "requestedPublicationDate";

            return !string.IsNullOrEmpty(eForm.RequestedPublicationDate?.Value)
                ? new ValidationError(path, "Requested Publication Date is not supported")
                : null;
        }


        /// <summary>
        /// Service provider parties where service type code == ted-esen must be Hansel
        /// </summary>
        public static ValidationError ValidateHanselESender(EFormContract eForm)
        {
            const string path = "ublExtensions.ublExtension.extensionContent.eformsExtension.organizations.organization.company.partyLegalEntity.companyID";

            var eSenders = eForm.GetESenders();

            return eSenders.Any(e =>
                e?.Company?.PartyLegalEntity?.FirstOrDefault()?.CompanyID?.Value != HanselNationalIdentifier)
                ? new ValidationError(path, "Only Hansel can be set as eSender")
                : null;
        }

        /// <summary>
        /// BT-501-Organization-Company
        /// 
        /// Primary buyer (GroupLeadIndicator = true or only buyer):
        ///     Must have a Finnish y-tunnus / tiekunnan käyttöoikeusyksikkötunnus
        /// Others:
        ///     If country == FIN or ALA -> validated for finnish y-tunnus / tiekunnan käyttöoikeusyksikkötunnus
        ///     Other countries are validated by schematron
        /// </summary>
        public static List<ValidationError> ValidateBT501OrganizationCompany(EFormContract eForm)
        {
            var errors = new List<ValidationError> { ValidateFinnishOrgIdentifier(eForm.GetPrimaryBuyer()?.CompanyLegalId()) };
            var participatingOrganisationsInFinland = eForm
                .GetParticipatingOrganisations()
                .Where(o => o.Company.PostalAddress.Country.IdentificationCode.Value is "FIN" or "ALA");

            errors.AddRange(participatingOrganisationsInFinland.Select(oid => ValidateFinnishOrgIdentifier(oid.CompanyLegalId())));

            return errors.Where(e => e is not null).ToList();
        }

        /// <summary>
        /// Validates for y-tunnus / tiekunnan käyttöoikeusyksikkötunnus
        /// </summary>
        private static ValidationError ValidateFinnishOrgIdentifier(string value)
        {
            if (value is null)
            {
                return null;
            }

            const string path = "ublExtensions.ublExtension.extensionContent" +
                                ".eformsExtension.organizations.organization.company.partyLegalEntity.companyID";

            return !(value.IsNationalIdentifier() || value.IsPrivateRoadIdentifier())
                ? new ValidationError(path, $"Company Id is not a valid National Identifier or Private Road Identifier")
                : null;
        }

        /// <summary>
        /// BT-507-Organization-Company
        /// Organisation Country Subdivision (ie. Nuts code)
        /// Mandatory
        /// </summary>
        public static List<ValidationError> ValidateBT507OrganizationCompany(EFormContract eForm)
        {
            const string path = "ublExtensions.ublExtension.extensionContent.eformsExtension.organizations.organization.company.postalAddress.countrySubentityCode";

            var errors = eForm.GetAllCompanies()
                .Where(x => string.IsNullOrEmpty(x.PostalAddress?.CountrySubentityCode?.Value))
                .Select(x => new ValidationError(path, $"CountrySubentityCode cannot be empty for company {x.PartyName?.FirstOrDefault()?.Name?.Value}"))
                .ToList();

            return errors;
        }

        /// <summary>
        /// BT-09(b)-Procedure
        /// Cross Border Law Description
        /// Description for cross border law is required, when BT-09(a)-Procedure is CrossBorderLaw
        /// </summary>
        public static List<ValidationError> ValidateBT09bProcedure(EFormContract eForm)
        {
            const string path = "tenderingTerms.procurementLegislationDocumentReference.documentDescription";
            var errors = new List<ValidationError>();

            foreach (var legislationDocumentReference in eForm.TenderingTerms?.ProcurementLegislationDocumentReference ?? Enumerable.Empty<DocumentReferenceContract>())
            {
                var isBT09aProcedureSet = string.Equals(legislationDocumentReference.ID?.Value, "CrossBorderLaw", StringComparison.Ordinal);

                if (!isBT09aProcedureSet) continue;

                if (legislationDocumentReference.DocumentDescription is null ||
                    !legislationDocumentReference.DocumentDescription.Any() ||
                    legislationDocumentReference.DocumentDescription.Any(x => string.IsNullOrEmpty(x.Value)))
                {
                    errors.Add(new ValidationError(path, $"BT-09(b)-Procedure cannot be empty when BT-09(a)-Procedure is CrossBorderLaw"));
                }
            }

            return errors;
        }

        /// <summary>
        /// BT-51-Lot
        /// Maximum Candidates Number
        /// Maximum candidates number must be set, when BT-661-Lot is true
        /// </summary>
        public static List<ValidationError> ValidateBT51Lot(EFormContract eForm)
        {
            var path = "procurementProjectLot.tenderingProcess.economicOperatorShortList.maximumQuantity";

            var errors = new List<ValidationError>();

            foreach(var lot in eForm.ProcurementProjectLot)
            {
                var isBT661LotSet = string.Equals(lot.TenderingProcess?.EconomicOperatorShortList?.FirstOrDefault()?.LimitationDescription?.FirstOrDefault()?.Value, "true", StringComparison.Ordinal);

                if (!isBT661LotSet) continue;

                if (lot.TenderingProcess?.EconomicOperatorShortList?.FirstOrDefault().MaximumQuantity?.Value is null)
                {
                    errors.Add(path, $"BT-51-Lot Maximum candidates number must be set, when BT-661-Lot is true for lot {lot.ID.Value}");
                }
            }

            return errors;
        }

        /// <summary>
        /// ND-Prize (BT-644-Lot, BT-44-Lot, BT-45-Lot)
        /// Prize
        /// Prize should only be allowed for design notices (notice subtypes 23, 24, 36 and 37)
        /// </summary>
        public static List<ValidationError> ValidateNDPrize(EFormContract eForm)
        {
            var allowedNoticeSubTypes = new List<string>() { "23", "24", "36", "37" };
            var errors = new List<ValidationError>();

            if (allowedNoticeSubTypes.Contains(eForm.GetNoticeType()))
            {
                return errors;
            }

            var path = "procurementProjectLot.tenderingTerms.awardingTerms.prize";

            foreach (var lot in eForm.ProcurementProjectLot)
            {
                if (lot.TenderingTerms?.AwardingTerms?.Prize?.Any() == true)
                {
                    errors.Add(path, $"Prize is allowed for design notices only (notice subtypes {string.Join(", ", allowedNoticeSubTypes)}).");
                }
            }

            return errors;
        }

        /// <summary>
        /// BT-67(a)-Procedure
        /// Exclusion Grounds
        /// Validates that mandatory esclusion grounds are set for competition notices (14, 15, 16, 17, 19, 20, 21, 22, 23, 24), excluding 18 (defence):
        ///     Conviction = crime-org, corruption, fraud, terr-offence, finan-laund, human-traffic
        ///     Taxes and social security = tax-pay, socsec-pay
        ///     Finnish law = nati-ground
        /// </summary>
        public static List<ValidationError> ValidateBT67aProcedure(EFormContract eForm)
        {
            var noticeTypesWithMandatoryExclusionGrounds = new List<string>() { "14", "15", "16", "17", "19", "20", "21", "22", "23", "24" };
            var errors = new List<ValidationError>();

            if (!noticeTypesWithMandatoryExclusionGrounds.Contains(eForm.GetNoticeType()))
            {
                return errors;
            }

            var path = "tenderingTerms.tendererQualificationRequest.specificTendererRequirement.tendererRequirementTypeCode";

            var allExclusionGrounds = eForm.TenderingTerms?.TendererQualificationRequest?
                .SelectMany(x => x?.SpecificTendererRequirement
                .Where(z => z?.TendererRequirementTypeCode?.listName == "exclusion-ground"))
                .Select(f => f.TendererRequirementTypeCode.Value)?.ToList();

            var mandatoryExclusionGrounds = new List<string> { "crime-org", "corruption", "fraud", "terr-offence", "finan-laund", "human-traffic", "tax-pay", "socsec-pay", "nati-ground" };

            foreach (var mandatoryExclusionGround in mandatoryExclusionGrounds ?? Enumerable.Empty<string>())
            {
                if (!allExclusionGrounds.Contains(mandatoryExclusionGround))
                {
                    errors.Add(path, $"Mandatory exclusion ground {mandatoryExclusionGround} is missing!");
                }
            }

            return errors;
        }

        /// <summary>
        /// BT-67(b)-Procedure
        /// Exclusion Grounds
        /// Validates that all selected exclusion grounds have a description
        /// </summary>
        public static List<ValidationError> ValidateBT67bProcedure(EFormContract eForm)
        {
            var errors = new List<ValidationError>();

            if (!eForm.IsContract())
            {
                return errors;
            }

            var path = "tenderingTerms.tendererQualificationRequest.specificTendererRequirement.description";

            var allExclusionGrounds = eForm.TenderingTerms?.TendererQualificationRequest?
                .SelectMany(x => x?.SpecificTendererRequirement
                .Where(z => z?.TendererRequirementTypeCode?.listName == "exclusion-ground"))
                .ToList();

            foreach(var exclusionGround in allExclusionGrounds ?? Enumerable.Empty<TendererRequirementContract>())
            {
                if (exclusionGround.Description is null || !exclusionGround.Description.Any() || exclusionGround.Description.Any(x => string.IsNullOrEmpty(x.Value)) == true)
                {
                    errors.Add(path, $"Exclusion ground description is mandatory for {exclusionGround.TendererRequirementTypeCode.Value}.");
                }
            }

            return errors;
        }

        /// <summary>
        /// BT-748-Lot
        /// Selection Criteria Used
        /// Usage of selection criteria is mandatory for a selection criteria
        /// </summary>
        public static List<ValidationError> ValidateBT748Lot(EFormContract eForm)
        {
            var path = "procurementProjectLot.tenderingTerms.ublExtensions.ublExtension.extensionContent.eformsExtension.selectionCriteria.calculationExpressionCode";

            var errors = new List<ValidationError>();

            foreach(var lot in eForm.ProcurementProjectLot)
            {
                var lotSelectionCriteria = lot.TenderingTerms?.UBLExtensions?.FirstOrDefault()?.ExtensionContent?.EformsExtension?
                    .SelectionCriteria?.Where(x => x?.CriterionTypeCode?.listName == "selection-criterion").ToList();

                foreach (var criteria in lotSelectionCriteria ?? Enumerable.Empty<CriterionContract>())
                {
                    if (criteria.CalculationExpressionCode is null || string.IsNullOrEmpty(criteria.CalculationExpressionCode.Value))
                    {
                        errors.Add(path, $"Usage of selection criteria is mandatory for selection criteria {criteria.CriterionTypeCode.Value} in lot {lot.ID.Value}");
                    }
                }
            }

            return errors;
        }

        /// <summary>
        /// BT-75-Lot
        /// Guarantee Required Description
        /// Guarantee description is required, when guarantee is required (tender-guarantee-required = true)
        /// </summary>
        public static List<ValidationError> ValidateBT75Lot(EFormContract eForm)
        {
            var path = "procurementProjectLot.tenderingTerms.requiredFinancialGuarantee.description";

            var errors = new List<ValidationError>();

            foreach (var lot in eForm.ProcurementProjectLot)
            {
                var lotGuarantees = lot.TenderingTerms?.RequiredFinancialGuarantee?
                    .Where(x => string.Equals(x?.GuaranteeTypeCode?.listName, "tender-guarantee-required", StringComparison.Ordinal)
                        && string.Equals(x?.GuaranteeTypeCode?.Value, "true", StringComparison.Ordinal))
                    .ToList();

                foreach (var lotGuarantee in lotGuarantees ?? Enumerable.Empty<FinancialGuaranteeContract>())
                {
                    if (lotGuarantee.Description is null || !lotGuarantee.Description.Any() || lotGuarantee.Description.Any(x => string.IsNullOrEmpty(x.Value)))
                    {
                        errors.Add(path, $"Guarantee description is required, when guarantee is required. Lot {lot.ID.Value}");
                    }
                }
            }

            return errors;
        }

        /// <summary>
        /// BT-150-Contract
        /// Contract Identifier
        /// Contract reference id is mandatory for each contract
        /// </summary>
        public static List<ValidationError> ValidateBT150Contract(EFormContract eForm)
        {
            var errors = new List<ValidationError>();

            if (!eForm.IsContractAward() && !eForm.IsExAnte())
            {
                return errors;
            }

            const string path = "ublExtensions.ublExtension.extensionContent.eformsExtension.noticeResult.settledContract.contractReference.id";

            var contracts = eForm.GetAllContracts();

            foreach(var contract in contracts ?? Enumerable.Empty<SettledContractContract>())
            {
                if (contract.ContractReference is null || contract.ContractReference?.Any(x => string.IsNullOrEmpty(x.ID.Value)) == true)
                {
                    errors.Add(path, $"ContractReference ID is required for contract {contract.ID?.Value}");
                }
            }

            return errors;
        }

        /// <summary>
        /// BT-539-Lot
        /// Awarding Criterion Type Code
        /// Awarding Criterion Type Code is mandatory for notice subtypes: 7, 8, 9, 14, 15, 16, 17, 18, 19, 20, 21, 23, 24, 25, 26, 27, 28
        /// </summary>
        public static List<ValidationError> ValidateBT539Lot(EFormContract eForm)
        {
            var noticeTypesWithMandatoryawardingCriterionTypeCode = new List<string>()
                { "7", "8", "9", "14", "15", "16", "17", "18", "19", "20", "21", "23", "24", "25", "26", "27", "28" };
            var errors = new List<ValidationError>();

            if (!noticeTypesWithMandatoryawardingCriterionTypeCode.Contains(eForm.GetNoticeType()))
            {
                return errors;
            }

            const string path = "procurementProjectLot.tenderingTerms.awardingTerms.awardingCriterion.subordinateAwardingCriterion.awardingCriterionTypeCode";

            foreach (var lot in eForm.ProcurementProjectLot)
            {
                if (lot.TenderingTerms?.AwardingTerms?.AwardingCriterion?
                    .SelectMany(x => x.SubordinateAwardingCriterion)
                    .Any(x => x.AwardingCriterionTypeCode?.Value is null) == true)
                {
                    errors.Add(path, $"Awarding Criterion Type Code is required for notice subtypes: {string.Join(", ", noticeTypesWithMandatoryawardingCriterionTypeCode)}. Lot {lot.ID.Value}");
                }
            }

            return errors;
        }

        /// <summary>
        /// BT-161-NoticeResult
        /// Total amount
        /// Notice results total amount is required when any lot has a winner.
        /// </summary>
        public static List<ValidationError> ValidateBT161NoticeResult(EFormContract eForm)
        {
            var errors = new List<ValidationError>();

            if (!eForm.IsContractAward())
            {
                return errors;
            }

            const string path = "ublExtensions.ublExtension.extensionContent.eformsExtension.noticeResult.totalAmount";

            if (!(eForm.UBLExtensions?.FirstOrDefault()?.ExtensionContent?.EformsExtension?.NoticeResult?.TotalAmount?.Value > 0))
            {
                var anyLotHasWinners = eForm.UBLExtensions?.FirstOrDefault()?
                    .ExtensionContent?.EformsExtension?.NoticeResult?.LotResult?
                    .Any(x => x?.TenderResultCode?.Value == "selec-w") == true;

                if (anyLotHasWinners)
                {
                    errors.Add(path, $"BT-161-NoticeResult total amount is required when any lot has a winner.");
                }
            }

            return errors;
        }

        /// <summary>
        /// BT-717-Lot Clean Vehicles Directive and
        /// BT-735-Lot CVD Contract Type
        /// Clean Vehicles Directive and CVD Contract Type are allowed only for BT-262 or BT-263 values:
        ///     '60000000,60112000,60130000,60140000,90511000,60160000,60161000,64121100,64121200'
        ///     + ',60100000,90510000,64121000,34100000,3411*,3412*,3413*,3414*'
        /// </summary>
        public static List<ValidationError> ValidateBT717LotAndBT735Lot(EFormContract eForm)
        {
            var errors = new List<ValidationError>();
            // Commodity codes for procedure level
            if (CleanVehiclesAreAllowed(eForm?.ProcurementProject)) {
                return errors;
            }

            foreach (var lot in eForm.ProcurementProjectLot)
            {
                // Commodity codes for lot level
                if (CleanVehiclesAreAllowed(lot.ProcurementProject)) {
                    continue;
                }

                var strategicProcurement = lot.TenderingTerms?.UBLExtensions?.FirstOrDefault()?.ExtensionContent?.EformsExtension?.StrategicProcurement?.FirstOrDefault();
                if (strategicProcurement is null)
                {
                    continue;
                }

                // BT-717
                if (!string.IsNullOrEmpty(strategicProcurement.ApplicableLegalBasis?.Value))
                {
                    errors.Add(PathForBT717, $"Clean Vehicles Directive is allowed only for commodity (Main and Additional) classification codes: " +
                        $"{string.Join(", ", ExactAllowedCodesForCleanVehicles)}, 3411*, 3412*, 3413* and 3414*. Lot {lot.ID.Value}");
                }

                // BT-735
                if (strategicProcurement.StrategicProcurementInformation is not null
                    && strategicProcurement.StrategicProcurementInformation.Any(y => !string.IsNullOrEmpty(y?.ProcurementCategoryCode?.Value)))
                {
                    errors.Add(PathForBT735, $"CVD Contract Type is allowed only for commodity (Main and Additional) classification codes: " +
                        $"{string.Join(", ", ExactAllowedCodesForCleanVehicles)}, 3411*, 3412*, 3413* and 3414*. Lot {lot.ID.Value}");
                }
            }
            return errors;
        }

        private static bool CleanVehiclesAreAllowed(ProcurementProjectContract procurementProject)
        {
            var commodityCodes = new HashSet<string>();
            var mainCommodity = procurementProject?.MainCommodityClassification?.FirstOrDefault()?.ItemClassificationCode?.Value;
            var additionalCommodities = procurementProject?.AdditionalCommodityClassification?
                .Where(x => !string.IsNullOrEmpty(x?.ItemClassificationCode?.Value))
                .Select(y => y.ItemClassificationCode.Value);

            if (!string.IsNullOrEmpty(mainCommodity))
            {
                commodityCodes.Add(mainCommodity);
            }

            if (additionalCommodities is not null)
            {
                foreach (var additionalCommodity in additionalCommodities)
                {
                    commodityCodes.Add(additionalCommodity);
                }

            }

            return commodityCodes.Any(x => ExactAllowedCodesForCleanVehicles.Contains(x) || WildCardAllowedCodesForCleanVehicles.Any(w => x.StartsWith(w, StringComparison.Ordinal)));
        }
    }
}
