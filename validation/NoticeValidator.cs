using System;
using System.Collections.Generic;
using System.Linq;
using Hilma.Domain.DataContracts.Notices;
using Hilma.Domain.EForms.Contracts;
using Hilma.Domain.Extensions;

namespace Hilma.Domain.Validators.EForms
{
    /// <summary>
    /// National tailoring validator for eForms.
    /// </summary>
    public class ENoticeValidator
    {
        // Hard coded, should never change
        private const string HanselNationalIdentifier = "0988084-1";

        public static List<ValidationError> Validate(EFormContract eForm, Dictionary<string, HilmaStatistics> hilmaStatistics, int nationalThreshold)
        {
            var errors = new List<ValidationError> {
                ValidateHilmaStatistics(eForm, hilmaStatistics, nationalThreshold),
                ValidateBT738NotSet(eForm),
                ValidateHanselESender(eForm),
            };

            errors.AddRange(ValidateBT501OrganizationCompany(eForm));
            errors.AddRange(ValidateBT507OrganizationCompany(eForm));
            errors.AddRange(ValidateBT502OrganizationTouchPoint(eForm));
            errors.AddRange(ValidateBT506OrganizationTouchPoint(eForm));
            errors.AddRange(ValidateBT503OrganizationTouchPoint(eForm));
            errors.AddRange(ValidateBT09bProcedure(eForm));
            errors.AddRange(ValidateBT51Lot(eForm));
            errors.AddRange(ValidateNDPrize(eForm));
            errors.AddRange(ValidateBT67aProcedure(eForm));
            errors.AddRange(ValidateBT67bProcedure(eForm));
            errors.AddRange(ValidateBT748Lot(eForm));
            errors.AddRange(ValidateBT75Lot(eForm));
            errors.AddRange(ValidateBT150Contract(eForm));

            errors.RemoveAll(e => e is null);

            return errors;
        }

        /// <summary>
        /// See HilmaStatisics.cs for more details
        /// </summary>
        public static ValidationError ValidateHilmaStatistics(EFormContract eForm, Dictionary<string, HilmaStatistics> hilmaStatistics, int nationalThreshold)
        {
            // Prior info or exante shouldn't have statistics
            if (hilmaStatistics is not null && hilmaStatistics.Count > 0 && (eForm.IsPriorInformation() || eForm.IsExAnte()))
            {
                return new ValidationError("hilmaStatistics",
                    "Hilma statistics is not allowed on prior information and ex-ante notices.");
            }

            if (eForm.IsPriorInformation() || eForm.IsExAnte() || (eForm.IsContractModification() && hilmaStatistics is null))
            {
                return null;
            }

            // Contract awards with total value < national threshold do not need to be validated
            if (eForm.IsContractAward() && (eForm.GetTotalValue() ?? 0) < nationalThreshold)
            {
                return null;
            }

            if (hilmaStatistics is null || hilmaStatistics.Count == 0)
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
        /// BT-502-Organization-TouchPoint
        /// Organisation Contact Point
        /// Contact name is mandatory, when organization has a touch point.
        /// </summary>
        public static List<ValidationError> ValidateBT502OrganizationTouchPoint(EFormContract eForm)
        {
            const string path = "ublExtensions.ublExtension.extensionContent.eformsExtension.organizations.organization.touchPoint.contact.name";

            var errors = eForm.GetAllOrganizations()
                .Where(x => x.TouchPoint?.Any() == true && x.TouchPoint.Any(y => string.IsNullOrEmpty(y.Contact?.Name?.Value)))
                .Select(x => new ValidationError(path, $"organization.touchPoint.contact.name cannot be empty for company {x.Company.PartyName?.FirstOrDefault()?.Name?.Value}"))
                .ToList();

            return errors;
        }

        /// <summary>
        /// BT-506-Organization-TouchPoint
        /// Contact Email Address
        /// Contact email is mandatory, when organization has a touch point.
        /// </summary>
        public static List<ValidationError> ValidateBT506OrganizationTouchPoint(EFormContract eForm)
        {
            const string path = "ublExtensions.ublExtension.extensionContent.eformsExtension.organizations.organization.touchPoint.contact.name";

            var errors = eForm.GetAllOrganizations()
                .Where(x => x.TouchPoint?.Any() == true && x.TouchPoint.Any(y => string.IsNullOrEmpty(y.Contact?.ElectronicMail?.Value)))
                .Select(x => new ValidationError(path, $"organization.touchPoint.contact.electronicMail cannot be empty for company {x.Company.PartyName?.FirstOrDefault()?.Name?.Value}"))
                .ToList();

            return errors;
        }

        /// <summary>
        /// BT-503-Organization-TouchPoint
        /// Contact Telephone Number
        /// Contact telephone number is mandatory, when organization has a touch point.
        /// </summary>
        public static List<ValidationError> ValidateBT503OrganizationTouchPoint(EFormContract eForm)
        {
            const string path = "ublExtensions.ublExtension.extensionContent.eformsExtension.organizations.organization.touchPoint.contact.telephone";

            var errors = eForm.GetAllOrganizations()
                .Where(x => x.TouchPoint?.Any() == true && x.TouchPoint.Any(y => string.IsNullOrEmpty(y.Contact?.Telephone?.Value)))
                .Select(x => new ValidationError(path, $"organization.touchPoint.contact.telephone cannot be empty for company {x.Company.PartyName?.FirstOrDefault()?.Name?.Value}"))
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
    }
}
