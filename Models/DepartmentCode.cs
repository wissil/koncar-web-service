using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KoncarWebService.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DepartmentCode
    {
        UNKNOWN,
        D_21510,
        D_21520,
        D_21540,
        D_21570,
        D_SLFIN,
        D_SLKPO
    }

	public static class DepartmentCodeExtensions
	{
        const string S_21510 = "21510";
        const string S_21520 = "21520";
        const string S_21540 = "21540";
        const string S_21570 = "21570";
        const string S_SLFIN = "SLFIN";
        const string S_SLKPO = "SLKPO";
        const string S_UNKNOWN = "UNKNOWN";

        public static string GetStringValue(this DepartmentCode dep)
		{
			switch (dep)
			{
                case DepartmentCode.D_21510:
                    return S_21510;

				case DepartmentCode.D_21520:
					return S_21520;

				case DepartmentCode.D_21540:
                    return S_21540;

                case DepartmentCode.D_21570:
                    return S_21570;

                case DepartmentCode.D_SLFIN:
                    return S_SLFIN;

                case DepartmentCode.D_SLKPO:
                    return S_SLKPO;

				default:
                    return S_UNKNOWN;
			}
		}

        public static DepartmentCode GetEnumValue(string code) 
        {
            if (code.Equals(S_21510)) 
            {
                return DepartmentCode.D_21510;
            }

			if (code.Equals(S_21520))
			{
                return DepartmentCode.D_21520;
			}

            if (code.Equals(S_21540))
			{
                return DepartmentCode.D_21540;
			}

			if (code.Equals(S_21570))
			{
                return DepartmentCode.D_21570;
			}

            if (code.Equals(S_SLFIN))
			{
                return DepartmentCode.D_SLFIN;
			}

            if (code.Equals(S_SLKPO))
			{
                return DepartmentCode.D_SLKPO;
			}

            return DepartmentCode.UNKNOWN;
        }
	}
}
