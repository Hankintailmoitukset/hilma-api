using System.Linq;
using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;
using System.Text;
using Hilma.Domain.EForms.Contracts;
using Hilma.Domain.Extensions;

namespace Hilma.Domain.DataContracts.Notices
{
    /// <summary>
    /// Hilma related statistical information
    /// </summary>
    [Contract]
    public class HilmaStatistics
    {
        #region Green question
        /// <summary>
        /// Tässä hankintamenettelyssä otetaan huomioon energiatehokkuusnäkökohtia
        /// </summary>
        public bool? EnergyEfficiencyConsidered { get; set; }

        /// <summary>
        /// Tässä hankinnassa edistetään vähähiilisyyttä
        /// </summary>
        public bool? LowCarbon { get; set; }

        /// <summary>
        /// Tällä hankinnalla edistetään kiertotaloutta
        /// </summary>
        public bool? CircularEconomy { get; set; }

        /// <summary>
        /// Tässä hankinnassa edistetään luonnon monimuotoisuutta
        /// </summary>
        public bool? Biodiversity { get; set; }

        /// <summary>
        /// Tässä hankinnassa edistetään kestävää ruokajärjestelmää
        /// </summary>
        public bool? SustainableFoodProduction { get; set; }

        /// <summary>
        /// Käytetäänkö hankinnassa Motivan, ympäristömerkkien tai EU GPP kriteerejä
        /// </summary>
        public bool? ListedGreenCriteriaUsed { get; set; }
        #endregion

        #region Social questions
        /// <summary>
        /// Tämä hankinta edistää oikeudenmukaisia työoloja
        /// </summary>
        public bool? JustWorkingConditions { get; set; }

        /// <summary>
        /// Tässä hankinnassa otetaan huomioon työllistämisehto
        /// </summary>
        public bool? EmploymentCondition { get; set; }

        /// <summary>
        /// Kuinka monta työ- ja oppisopimuspaikkaa hankinnalla arvioidaan syntyvän? 
        /// </summary>
        public int? HowManyOpportunitiesIsEstimated { get; set; }

        /// <summary>
        /// Tässä hankinnassa käytetään vastuullisuuden vähimmäisvaatimuksia (Code of conduct)
        /// </summary>
        public bool? CodeOfConduct { get; set; }
        #endregion

        #region Innovation questions
        /// <summary>
        /// Hankinnan valmisteluvaiheessa on kartoitettu tarve/mahdollisuudet uusiin ratkaisuihin tai niiden kehittämiseen
        /// </summary>
        public bool? InnovationConsidered { get; set; }

        /// <summary>
        /// Tavoiteltava ratkaisu tai sen osa on meille ostajana uusi.​
        /// </summary>
        public bool? SolutionNewToBuyer { get; set; }

        /// <summary>
        /// Tavoiteltava ratkaisu tai sen osa on markkinoille tai toimialalle uusi.
        /// </summary>
        public bool? SolutionNewToMarketOrIndustry { get; set; }
        #endregion

        /// <summary>
        /// Tässä hankintamenettelyssä otetaan huomioon innovaationäkökohtia
        /// </summary>
        public bool? SMEParticipationConsidered { get; set; }

        /// <summary>
        /// Tässä hankinnassa on huomioitu palvelunkäyttäjien tai heitä edustavien tahojen osallistuminen hankinnan valmisteluun.
        /// </summary>
        public bool? EndUserInvolved { get; set; }

        #region Validation
        /// <summary>
        /// Checks if any green criteria is selected
        /// </summary>
        /// <returns>If green criteria is selected</returns>
        public bool AnyGreenOptionSelected() => EnergyEfficiencyConsidered == true
                                                || CircularEconomy == true
                                                || LowCarbon == true
                                                || Biodiversity == true
                                                || SustainableFoodProduction == true;
        /// <summary>
        /// Trims the conditional fields
        /// </summary>
        public void Trim(NoticeType type)
        {
            var isSocial = type.IsSocial();

            if (!AnyGreenOptionSelected())
            {
                ListedGreenCriteriaUsed = null;
            }

            if (EmploymentCondition != true)
            {
                HowManyOpportunitiesIsEstimated = null;
            }

            if (!isSocial)
            {
                EndUserInvolved = null;
            }
        }

        public bool Validate(EFormContract eForm, out string errorMessage)
        {
            var errorBuilder = new StringBuilder();
            var isSocial = eForm.IsSocial();

            ValidateGreenCriteria(errorBuilder);
            ValidateSocialCriteria(errorBuilder);
            ValidateInnovation(errorBuilder);


            if (SMEParticipationConsidered == null)
            {
                errorBuilder.AppendLine($"{nameof(SMEParticipationConsidered)} is mandatory");
            }

            if (isSocial && EndUserInvolved == null)
            {
                errorBuilder.AppendLine($"{nameof(EndUserInvolved)} is mandatory, because {eForm.GetNoticeType()} is social notice type");
            }

            if (!isSocial && EndUserInvolved != null)
            {
                errorBuilder.AppendLine($"{nameof(EndUserInvolved)} is forbidden, because {eForm.GetNoticeType()} is not social notice type");
            }

            errorMessage = errorBuilder.ToString().Trim();
            return errorMessage.Length == 0;
        }

        public ValidationState Validate(NoticeType type, out string errorMessage)
        {
            var errorBuilder = new StringBuilder();
            var isSocial = type.IsSocial();

            ValidateGreenCriteria(errorBuilder);
            ValidateSocialCriteria(errorBuilder);
            ValidateInnovation(errorBuilder);


            if (SMEParticipationConsidered == null)
            {
                errorBuilder.AppendLine($"{nameof(SMEParticipationConsidered)} is mandatory");
            }

            if (isSocial && EndUserInvolved == null)
            {
                errorBuilder.AppendLine($"{nameof(EndUserInvolved)} is mandatory, because {type} is social notice type");
            }

            if (!isSocial && EndUserInvolved != null)
            {
                errorBuilder.AppendLine($"{nameof(EndUserInvolved)} is forbidden, because {type} is not social notice type");
            }

            errorMessage = errorBuilder.ToString().Trim();
            return errorMessage.Length > 0
                ? ValidationState.Invalid
                : ValidationState.Valid;
        }

        private void ValidateInnovation(StringBuilder errorBuilder)
        {
            if (InnovationConsidered == null)
            {
                errorBuilder.AppendLine($"{nameof(InnovationConsidered)} is mandatory");
            }

            if (SolutionNewToBuyer == null)
            {
                errorBuilder.AppendLine($"{nameof(SolutionNewToBuyer)} is mandatory");
            }

            if (SolutionNewToMarketOrIndustry == null)
            {
                errorBuilder.AppendLine($"{nameof(SolutionNewToMarketOrIndustry)} is mandatory");
            }
        }

        private void ValidateGreenCriteria(StringBuilder errorBuilder)
        {
            if (EnergyEfficiencyConsidered == null)
            {
                errorBuilder.AppendLine($"{nameof(EnergyEfficiencyConsidered)} is mandatory");
            }

            if (LowCarbon == null)
            {
                errorBuilder.AppendLine($"{nameof(LowCarbon)} is mandatory");
            }

            if (CircularEconomy == null)
            {
                errorBuilder.AppendLine($"{nameof(CircularEconomy)} is mandatory");
            }

            if (Biodiversity == null)
            {
                errorBuilder.AppendLine($"{nameof(Biodiversity)} is mandatory");
            }

            if (SustainableFoodProduction == null)
            {
                errorBuilder.AppendLine($"{nameof(SustainableFoodProduction)} is mandatory");
            }

            if (AnyGreenOptionSelected() && ListedGreenCriteriaUsed == null)
            {
                errorBuilder.AppendLine($"{nameof(ListedGreenCriteriaUsed)} " +
                                        $"is mandatory because one of the green section criteria " +
                                        $"({nameof(EnergyEfficiencyConsidered)}, {nameof(LowCarbon)}, {nameof(CircularEconomy)}, " +
                                        $"{nameof(Biodiversity)} and {nameof(SustainableFoodProduction)}) is selected.");
            }

            if (!AnyGreenOptionSelected() && ListedGreenCriteriaUsed != null)
            {
                errorBuilder.AppendLine($"{nameof(ListedGreenCriteriaUsed)} " +
                                        $"is forbidden because none of the green section criteria " +
                                        $"({nameof(EnergyEfficiencyConsidered)}, {nameof(LowCarbon)}, " +
                                        $"{nameof(Biodiversity)} and {nameof(SustainableFoodProduction)}) is selected.");
            }
        }

        private void ValidateSocialCriteria(StringBuilder errorBuilder)
        {
            if (JustWorkingConditions == null)
            {
                errorBuilder.AppendLine($"{nameof(JustWorkingConditions)} is mandatory");
            }

            if (EmploymentCondition == null)
            {
                errorBuilder.AppendLine($"{nameof(EmploymentCondition)} is mandatory");
            }

            if (EmploymentCondition == true)
            {
                if (HowManyOpportunitiesIsEstimated == null)
                {
                    errorBuilder.AppendLine($"{nameof(HowManyOpportunitiesIsEstimated)} is mandatory because {nameof(EmploymentCondition)} is selected");
                }

                if (HowManyOpportunitiesIsEstimated < 0)
                {
                    errorBuilder.AppendLine($"{nameof(HowManyOpportunitiesIsEstimated)} cannot be a negative number");
                }
            }

            if (EmploymentCondition != true && HowManyOpportunitiesIsEstimated != null)
            {
                errorBuilder.AppendLine($"{nameof(HowManyOpportunitiesIsEstimated)} is forbidden because {nameof(EmploymentCondition)} is not selected");
            }

            if (CodeOfConduct == null)
            {
                errorBuilder.AppendLine($"{nameof(CodeOfConduct)} is mandatory");
            }
        }
    }
}
