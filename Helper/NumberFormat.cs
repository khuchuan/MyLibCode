using System.Globalization;

namespace Helper
{
    public static class NumberFormat
    {
        public static string ThousandSeparator(float m)
        {
            return string.Format("{0:n0}", m);
        }
        public static string ThousandSeparator(float? m)
        {
            return m != null ? string.Format("{0:n0}", m) : "";
        }
        public static string ThousandSeparator(int m)
        {
            return m.ToString("0,0", new CultureInfo("es-ES"));
        }
        public static string ThousandSeparator(int? m)
        {
            return m != null ? m.Value.ToString("0,0", new CultureInfo("es-ES")) : "";
        }
        public static string ThousandSeparator(decimal m)
        {
            return m.ToString("0,0", new CultureInfo("es-ES"));
        }
        public static string ThousandSeparator(decimal? m)
        {
            return m != null ? m.Value.ToString("0,0", new CultureInfo("es-ES")) : "";
        }
        public static string ThousandSeparator(double m)
        {
            return string.Format("{0:n0}", m);
        }
        public static string ThousandSeparator(double? m)
        {
            return m != null ? string.Format("{0:n0}", m) : "";
        }
    }
}
