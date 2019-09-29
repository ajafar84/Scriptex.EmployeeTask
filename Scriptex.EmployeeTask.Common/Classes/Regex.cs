namespace Kernel.Classes
{
    public class Regex
    {
        public const string UnSignedIntegerNumber = "^\\d*$";
        public const string PositiveIntegerNumber = "^[1-9]$|^0*[1-9]+[0-9]*$";
        public const string UnSignedDecimalNumber = "^\\d*\\.{0,1}\\d*$";
        public const string PositiveDecimalNumber = "^(\\d*[1-9]\\d*(\\.[0-9]*)?|0*\\.\\d*[1-9]\\d*)$";
        public const string Percentage = "^100(\\.0{0,2}?)?$|^\\d{0,2}(\\.\\d{0,2})?$";
        public const string ArabicName = "^([\\u0621-\\u064A\\u0660-\\u0669\\d]+[-_ ]{0,1})*$";
        public const string ArabicNameWithDiacritics = "^([\\u0600-\\u06ff\\u0750-\\u077f\\ufb50-\\ufc3f\\ufe70-\\ufefc\\d]+[-_ ]{0,1})*$";
        public const string ArabicLetters = "^[\\u0621-\\u064A ]+$";
        public const string ArabicLettersWithDiacritics = "^[\\u0600-\\u06ff\\u0750-\\u077f\\ufb50-\\ufc3f\\ufe70-\\ufefc ]+$";
        public const string EnglishName = "^([a-zA-Z0-9]+[-_ ]{0,1})*$";
        public const string EnglishLetters = "^[a-zA-Z ]+$";
        public const string Mobile = "^(010|011|012)\\d{8}$";
        public const string NationalId = "(?!0)\\d{14}$";
    }
}
