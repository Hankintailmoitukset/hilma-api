using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    /// Extension class for notice types
    /// </summary>
    [Configuration]
    public class NoticeTypes
    {
        /// <summary>
        /// The order is important and shown in this order in UI.
        /// </summary>
        public NoticeType[] SupportNoticeTypes { get; } =
        {
            NoticeType.PriorInformation,
            NoticeType.PriorInformationReduceTimeLimits,
            NoticeType.Contract,
            NoticeType.ContractAward,

            NoticeType.PeriodicIndicativeUtilities,
            NoticeType.PeriodicIndicativeUtilitiesReduceTimeLimits,
            NoticeType.ContractUtilities,
            NoticeType.ContractAwardUtilities,

            NoticeType.DesignContest,
            NoticeType.DesignContestResults,

            NoticeType.ExAnte,

            NoticeType.DefencePriorInformation,
            NoticeType.DefenceContract,
            NoticeType.DefenceContractAward,

            NoticeType.Modification,

            NoticeType.SocialPriorInformation,
            NoticeType.SocialContract,
            NoticeType.SocialContractAward,

            NoticeType.SocialUtilitiesPriorInformation,
            NoticeType.SocialUtilitiesQualificationSystem,
            NoticeType.SocialUtilities,
            NoticeType.SocialUtilitiesContractAward,

            NoticeType.SocialConcessionPriorInformation,
            NoticeType.SocialConcessionAward,

            NoticeType.Concession,
            NoticeType.ConcessionAward,

            // Nationals below
            NoticeType.NationalPriorInformation,
            NoticeType.NationalContract, 
            NoticeType.NationalSmallValueProcurement,
            NoticeType.NationalSmallValueProcurementSocial,

            NoticeType.NationalDirectAward,
            NoticeType.NationalDesignContest,

            NoticeType.NationalDefencePriorInformation,
            NoticeType.NationalDefenceContract,

            NoticeType.NationalAgricultureContract,
            NoticeType.NationalTransparency
        };

        public NoticeType[] PublicNotices { get; } = new[] {
            NoticeType.PriorInformation,
            NoticeType.Contract,
            NoticeType.ContractAward,
            NoticeType.Modification,
            NoticeType.SocialPriorInformation
        };

        /// <summary>
        /// National notices
        /// </summary>
        public NoticeType[] NationalNotices { get; } = new[] {
            NoticeType.NationalPriorInformation,
            NoticeType.NationalContract,
            NoticeType.NationalSmallValueProcurement,
            NoticeType.NationalSmallValueProcurementSocial,
            NoticeType.NationalDesignContest,
            NoticeType.NationalDirectAward,
            NoticeType.NationalDefencePriorInformation,
            NoticeType.NationalDefenceContract,
            NoticeType.NationalAgricultureContract,
            NoticeType.NationalTransparency
        };

        /// <summary>
        /// Prior information notice types
        /// </summary>
        public NoticeType[] PriorInformationNotices { get; } = new[] {
            NoticeType.PriorInformation,
            NoticeType.PriorInformationReduceTimeLimits,
            NoticeType.PeriodicIndicativeUtilities,
            NoticeType.PeriodicIndicativeUtilitiesReduceTimeLimits,
            NoticeType.DefencePriorInformation,
            NoticeType.SocialPriorInformation,
            NoticeType.SocialUtilitiesPriorInformation,
            NoticeType.NationalPriorInformation,
            NoticeType.NationalDefencePriorInformation
        };

        /// <summary>
        /// Contract notice types
        /// </summary>
        public NoticeType[] ContractNotices { get; } = new[] {
            NoticeType.Contract,
            NoticeType.ContractUtilities,
            NoticeType.DesignContest,
            NoticeType.DesignContestResults,
            NoticeType.DefenceContract,
            NoticeType.SocialContract,
            NoticeType.SocialUtilities,
            NoticeType.SocialUtilitiesQualificationSystem,
            NoticeType.Concession,
            NoticeType.NationalContract,
            NoticeType.NationalAgricultureContract,
            NoticeType.NationalDesignContest,
            NoticeType.NationalDefenceContract,
            NoticeType.NationalSmallValueProcurement,
            NoticeType.NationalSmallValueProcurementSocial
        };

        /// <summary>
        /// Contract Award notice types
        /// </summary>
        public NoticeType[] ContractAwardNotices { get; } = new[] {
            NoticeType.ContractAward,
            NoticeType.ContractAwardUtilities,
            NoticeType.SocialContractAward,
            NoticeType.DefenceContractAward,
            NoticeType.ConcessionAward,
            NoticeType.SocialUtilitiesContractAward,
            NoticeType.DpsAward,
            NoticeType.DesignContestResults,
            NoticeType.SocialConcessionAward,
            NoticeType.NationalDirectAward
        };

        /// <summary>
        /// Utilities notice types
        /// </summary>
        public NoticeType[] UtilitiesNotices { get; } = new[] {
            NoticeType.PeriodicIndicativeUtilities,
            NoticeType.PeriodicIndicativeUtilitiesReduceTimeLimits,
            NoticeType.ContractAwardUtilities,
            NoticeType.ContractUtilities,
            NoticeType.QualificationSystemUtilities,
            NoticeType.SocialUtilities,
            NoticeType.SocialUtilitiesPriorInformation,
            NoticeType.SocialUtilitiesContractAward,
            NoticeType.SocialUtilitiesQualificationSystem
        };

        /// <summary>
        /// Social notice types
        /// </summary>
        public NoticeType[] SocialNotices { get; } = new[] {
            NoticeType.SocialContract,
            NoticeType.SocialUtilities,
            NoticeType.SocialPriorInformation,
            NoticeType.SocialContractAward,
            NoticeType.SocialConcessionPriorInformation,
            NoticeType.SocialConcessionAward,
            NoticeType.SocialUtilitiesPriorInformation,
            NoticeType.SocialUtilitiesContractAward,
            NoticeType.SocialUtilitiesQualificationSystem,
            NoticeType.NationalSmallValueProcurementSocial
        };

        /// <summary>
        /// Defence notices
        /// </summary>
        public NoticeType[] DefenceNotices { get; } = new[] {
            NoticeType.DefenceConcession,
            NoticeType.DefencePriorInformation,
            NoticeType.DefenceContract,
            NoticeType.DefenceContractAward,
            NoticeType.DefenceContractConcessionnaire,
            NoticeType.DefenceContractSub,
            NoticeType.DefenceSimplifiedContract,
            NoticeType.NationalDefencePriorInformation,
            NoticeType.NationalDefenceContract
        };

        /// <summary>
        /// 2014/24/EU
        /// </summary>
        public NoticeType[] EuPublicCategories { get; } = new[] {
            NoticeType.PriorInformation,
            NoticeType.PriorInformationReduceTimeLimits,
            NoticeType.Contract,
            NoticeType.ContractAward,
            NoticeType.DesignContest,
            NoticeType.DesignContestResults,
            NoticeType.ExAnte,
            NoticeType.Modification,
            NoticeType.SocialPriorInformation,
            NoticeType.SocialContract,
            NoticeType.SocialContractAward
        };

        public NoticeType[] NationalPublicCategories { get; } = new[] {
            NoticeType.NationalPriorInformation,
            NoticeType.NationalContract,
            NoticeType.NationalSmallValueProcurement,
            NoticeType.NationalDesignContest
        };

        /// <summary>
        /// 2009/81/EC
        /// </summary>
        public NoticeType[] EuDefenceCategories { get; } = new[] {
            NoticeType.DefencePriorInformation,
            NoticeType.DefenceContract,
            NoticeType.DefenceContractAward,
            NoticeType.ExAnte
        };

        public NoticeType[] NationalDefenceCategories { get; } = new[] {
            NoticeType.NationalDefencePriorInformation,
            NoticeType.NationalDefenceContract
        };

        /// <summary>
        /// 2014/25/EU
        /// </summary>
        public NoticeType[] EuUtilityCategories { get; } = new[] {
            NoticeType.PeriodicIndicativeUtilities,
            NoticeType.ContractUtilities,
            NoticeType.ContractAwardUtilities,
            NoticeType.QualificationSystemUtilities,
            NoticeType.DesignContest,
            NoticeType.DesignContestResults,
            NoticeType.ExAnte,
            NoticeType.Modification,
            NoticeType.PeriodicIndicativeUtilitiesReduceTimeLimits,
            NoticeType.SocialUtilities,
            NoticeType.SocialUtilitiesPriorInformation,
            NoticeType.SocialUtilitiesContractAward,
            NoticeType.SocialUtilitiesQualificationSystem
        };

        /// <summary>
        /// 2014/23/EU
        /// </summary>
        public NoticeType[] EuLisenceCategories { get; } = new[] {
            NoticeType.Concession,
            NoticeType.ConcessionAward,
            NoticeType.SocialConcessionPriorInformation,
            NoticeType.SocialConcessionAward,
            NoticeType.Modification,
            NoticeType.ExAnte
        };

        public NoticeType[] AgricultureCategories { get; } = new[] {
            NoticeType.NationalAgricultureContract
        };

    }

}
