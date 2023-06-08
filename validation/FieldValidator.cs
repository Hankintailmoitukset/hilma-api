using System.Text.RegularExpressions;

namespace Hilma.Domain.Validators
{
    public static class FieldValidator
    {
        private const char Dash = '-';

        private static readonly Regex PrivateRoadIdentifierRegex =
            new Regex("/^(Y[0-9]{4}-[0-9]{5})$|([0-9]{3}-(1|2)[0-9]{3}-K[0-9]{1,6})$/", RegexOptions.Compiled);

        public static bool IsNationalIdentifier(this string input)
        {
            if (input == null)
            {
                return false;
            }

            var m = new [] {7, 9, 10, 5, 8, 4, 2};
            var dash = input.IndexOf(Dash);

            if (dash == 6 || dash == 7)
            {
                var parts = input.Split(Dash);
                if (parts.Length == 2 && parts[1].Length == 1)
                {
                    if (int.TryParse(parts[0], out var code) && int.TryParse(parts[1], out var checksum))
                    {
                        var sum = 0;
                        var codePart = parts[0].Length == 7 ? parts[0] : $"0{parts[0]}";

                        for (var i = 0; i < codePart.Length; i++)
                        {
                            sum += m[i] * int.Parse(codePart[i].ToString());
                        }

                        var modulo = sum % 11;

                        if (modulo == 0)
                        {
                            return parts[1] == "0";
                        } else if (modulo == 1)
                        {
                            return false;
                        }
                        else
                        {
                            return parts[1] == $"{11 - modulo}";
                        }
                    }
                }
            }

            return false;
        }

        public static bool IsPrivateRoadIdentifier(this string input)
        {
            if (input == null)
            {
                return false;
            }

            return PrivateRoadIdentifierRegex.IsMatch(input);
        }

        public static bool IsAppropriateNoticeLanguage(this string lang)
        {
            if(lang == "FI" || lang == "SV" || lang == "EN")
            {
                return true;
            }
            return false;
        }
    }
}
