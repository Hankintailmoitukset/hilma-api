using System;
using System.Collections.Generic;

namespace Hilma.Domain.DataContracts.EFormContracts
{
    public static class NoticeTypeCodes
    {
        public const string CORRIGENDUM = "corr";
        public const string EF14 = "14";
        public const string EF15 = "15";

        public static readonly IReadOnlySet<string> PriorInformationNoticeSubTypes = new HashSet<string>(StringComparer.Ordinal) {
            // E1 (national, markkinakartoitus)
            "4",
            "5",
            "6",
            // E2 (national, ennakkoilmoitus)
            "7",
            "8",
            "9",
            // Ex-ante below
            "25",
            "26",
            "27",
            "28",
        };

        public static readonly IReadOnlySet<string> PriorInformationNoticeTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
            "pin-buyer",
            "pin-cfc-social",
            "pin-cfc-standard",
            "pin-only",
            "pin-rtl",
            "pin-tran",
            "qu-sy",
        };

        public static readonly IReadOnlySet<string> ContractNoticeSubTypes = new HashSet<string>(StringComparer.Ordinal) {
            // 10-13 are not used in Finland
            EF14,
            EF15,
            "16",
            "17",
            "18",
            "19",
            // E3 (national)
            "20",
            "21",
            "22",
            "23",
            "24",
        };

        public static readonly IReadOnlySet<string> ContractNoticeTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
            "cn-desg",
            "cn-social",
            "cn-standard",
        };

        public static readonly IReadOnlySet<string> ContractAwardNoticeSubTypes = new HashSet<string>(StringComparer.Ordinal) {
            "29",
            "30",
            "31",
            "32",
            // E4 (national)
            "33",
            "34",
            "35",
            "36",
            "37",
            // Contract modifications below
            "38",
            "39",
            "40",
            // E5 (national) sopimuksen päättäminen
        };

        public static readonly IReadOnlySet<string> ContractAwardNoticeTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
            "can-desg",
            "can-modif",
            "can-social",
            "can-standard",
            "can-tran",
            "veat"
        };

        public static readonly IReadOnlySet<string> ExAnteNoticeSubTypes = new HashSet<string>(StringComparer.Ordinal) {
            "25",
            "26",
            "27",
            "28",
        };

        public static readonly IReadOnlySet<string> ContractModificationNoticeSubTypes = new HashSet<string>(StringComparer.Ordinal) {
            "38",
            "39",
            "40",
        };

        public static readonly IReadOnlySet<string> SocialNoticeSubTypes = new HashSet<string>(StringComparer.Ordinal) {
            "14",
            "20",
            "21",
            "33",
            "34",
            "35",
        };

        public static IReadOnlySet<string> SupportedNoticeSubTypes;

        static NoticeTypeCodes()
        {
            var noticeTypes = new HashSet<string>(
                PriorInformationNoticeSubTypes.Count +
                ContractNoticeSubTypes.Count +
                ContractAwardNoticeSubTypes.Count,
                StringComparer.Ordinal
            );

            foreach (var code in PriorInformationNoticeSubTypes)
            {
                noticeTypes.Add(code);
            }
            foreach (var code in ContractNoticeSubTypes)
            {
                noticeTypes.Add(code);
            }
            foreach (var code in ContractAwardNoticeSubTypes)
            {
                noticeTypes.Add(code);
            }

            SupportedNoticeSubTypes = noticeTypes;
        }
    }
}
