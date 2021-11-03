using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    // Notice types are numbered based on form number so that the first one or two numbers are for form number and the last two number is special type within the form. 
    // i.e Form 01 -> 100, Form 01 reduce time limints -> 101 
    [EnumContract]
    public enum NoticeType
    {
        Undefined = 0,

        /// <summary>
        /// F01_2014
        /// Prior information notice
        /// </summary>
        PriorInformation = 100,

        /// <summary>
        /// F01_2014
        /// Prior information notice for reducing time limits
        /// </summary>
        PriorInformationReduceTimeLimits = 101,


        /// <summary>
        /// F02_2014
        /// Contract notice
        /// </summary>
        Contract = 200,

        /// <summary>
        /// F03_2014
        /// Contract award notice
        /// </summary>
        ContractAward = 300,

        /// <summary>
        /// F03_2014 or F06_2014 depending on organisation. Used by Hilma app, not ETS.
        /// DPS award, when parent project != project
        /// </summary>
        DpsAward = 301,

        /// <summary>
        /// F04_2014
        /// Periodic indicative notice - utilities
        /// </summary>
        PeriodicIndicativeUtilities = 400,

        /// <summary>
        /// F04_2014
        /// Periodic indicative notice - utilities
        /// </summary>
        PeriodicIndicativeUtilitiesReduceTimeLimits = 401,


        /// <summary>
        /// F05_2014
        /// Contract notice - utilities
        /// </summary>
        ContractUtilities = 500,

        /// <summary>
        /// F06_2014
        /// Contract award notice - utilities
        /// </summary>
        ContractAwardUtilities = 600,

        /// <summary>
        /// F07_2014
        /// Qualification system - utilities
        /// </summary>
        QualificationSystemUtilities = 700,

        /// <summary>
        /// F08_2014
        /// Notice on a buyer profile
        /// </summary>
        BuyerProfile = 800,

        /// <summary>
        /// F09_SIMPLIFIED_CONTRACT
        /// R2.0.8.S04
        /// </summary>
        DefenceSimplifiedContract = 900,

        /// <summary>
        /// F10_CONCESSION
        /// R2.0.8.S04
        /// </summary>
        DefenceConcession = 1000,

        /// <summary>
        /// F11_CONTRACT_CONCESSIONNAIRE
        /// R2.0.8.S04
        /// </summary>
        DefenceContractConcessionnaire = 1100,

        /// <summary>
        /// F12_2014
        /// Design contest notice
        /// </summary>
        DesignContest = 1200,

        /// <summary>
        /// F13_2014
        /// Results of design contest
        /// </summary>
        DesignContestResults = 1300,

        /// <summary>
        /// F15_2014
        /// Voluntary ex ante transparency notice
        /// </summary>
        ExAnte = 1500,

        /// <summary>
        /// F16_PRIOR_INFORMATION_DEFENCE
        /// R2.0.8.S04
        /// </summary>
        DefencePriorInformation = 1600,

        /// <summary>
        /// F17_CONTRACT_DEFENCE
        /// R2.0.8.S04
        /// </summary>
        DefenceContract = 1700,

        /// <summary>
        /// F18_CONTRACT_AWARD_DEFENCE
        /// R2.0.8.S04
        /// </summary>
        DefenceContractAward = 1800,

        /// <summary>
        /// F19_CONTRACT_SUB_DEFENCE
        /// R2.0.8.S04
        /// </summary>
        DefenceContractSub = 1900,

        /// <summary>
        /// F20_2014
        /// Modification notice
        /// </summary>
        Modification = 2000,

        /// <summary>
        /// F21_2014
        /// Social and other specific services - public contracts  
        /// </summary>
        SocialContract = 2100,

        /// <summary>
        /// F21_2014
        /// Social and other specific services - prior information notice
        /// </summary>
        SocialPriorInformation = 2101,

        /// <summary>
        /// F21_2014
        /// Social and other specific services - contract award  
        /// </summary>
        SocialContractAward = 2102,

        /// <summary>
        /// F22_2014
        /// Social and other specific services - utilities
        /// </summary>
        SocialUtilities = 2200,

        /// <summary>
        /// F22_2014
        /// Prior information notice for Social and other specific services - utilities
        /// </summary>
        SocialUtilitiesPriorInformation = 2201,

        /// <summary>
        /// F22_2014
        /// Contract award notice for Social and other specific services - utilities
        /// </summary>
        SocialUtilitiesContractAward = 2202,

        /// <summary>
        /// F22_2014
        /// Qualification system notice for Social and other specific services - utilities
        /// </summary>
        SocialUtilitiesQualificationSystem = 2203,

        /// <summary>
        /// F23_2014
        /// Social and other specific services - concession prior information
        /// </summary>
        SocialConcessionPriorInformation = 2300,

        /// <summary>
        /// F23_2014
        /// Social and other specific services - concession award
        /// </summary>
        SocialConcessionAward = 2301,

        /// <summary>
        /// F24_2014
        /// Concession notice
        /// </summary>
        Concession = 2400,

        /// <summary>
        /// F25_2014
        /// Concession award notice
        /// </summary>
        ConcessionAward = 2500,

        /// <summary>
        /// National prior information notice
        /// </summary>
        NationalPriorInformation = 9901,

        /// <summary>
        /// National contract notice
        /// </summary>
        NationalContract = 9902,

        /// <summary>
        /// National contract notice for agriculture
        /// </summary>
        NationalAgricultureContract = 9903,

        /// <summary>
        /// National transparency notice
        /// </summary>
        NationalTransparency = 9904,

        /// <summary>
        /// National direct contract award
        /// </summary>
        NationalDirectAward = 9905,

        /// <summary>
        /// National design contest
        /// </summary>
        NationalDesignContest = 9906,

        /// <summary>
        /// National defence prior information
        /// </summary>
        NationalDefencePriorInformation = 9910,

        /// <summary>
        /// National defence contract
        /// </summary>
        NationalDefenceContract = 9911,

        /// <summary>
        /// National small value procurement (pienhankinta)
        /// </summary>
        NationalSmallValueProcurement = 9912,

        /// <summary>
        /// National small value procurement social and utilities (pienhankinta sosiaali- ja erityispalvelut)
        /// </summary>
        NationalSmallValueProcurementSocial = 9913
    }

}
