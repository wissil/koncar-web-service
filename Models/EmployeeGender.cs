using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KoncarWebService.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EmployeeGender
    {
        F = 0,
        M = 1,
        UNDEFINED = 2
    }

    public static class EmployeeGenderExtensions
    {
        const string M = "M";
        const string F = "F";
        const string UNDEFINED = "UNDEFINED";

        public static string GetStringValue(this EmployeeGender gender)
        {
            switch (gender)
            {
                case EmployeeGender.F:
                    return F;

                case EmployeeGender.M:
                    return M;

                default:
                    return UNDEFINED;
            }
        }

        public static string GetStringValue(int gender)
        {
            switch (gender)
            {
                case 0:
                    return F;

                case 1:
                    return M;

                default:
                    return UNDEFINED;
            }
        }

        public static int GetIntValue(this EmployeeGender gender)
        {
            switch (gender)
            {
                case EmployeeGender.F:
                    return 0;

                case EmployeeGender.M:
                    return 1;

                default:
                    return 2;
            }
        }

        public static EmployeeGender GetEnumValue(string gender)
        {
            switch (gender)
            {
                case F:
                    return EmployeeGender.F;

                case M:
                    return EmployeeGender.M;

                default:
                    return EmployeeGender.UNDEFINED;
            }
        }

        public static EmployeeGender GetEnumValue(int gender)
        {
            switch (gender)
            {
                case 0:
                    return EmployeeGender.F;

                case 1:
                    return EmployeeGender.M;

                default:
                    return EmployeeGender.UNDEFINED;
            }
        }
    }
}
