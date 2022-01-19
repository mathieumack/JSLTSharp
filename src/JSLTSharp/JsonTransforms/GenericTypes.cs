using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace JSLTSharp.JsonTransforms
{
    public static class GenericTypes
    {
        /// <summary>
        /// Return mulesoft format
        /// </summary>
        /// <param name="value"></param>
        /// <param name="typeWithFormat">type in xsd file ex char10, decimal28.9</param>
        /// <returns>value formatted</returns>
        public static object Cast(string value, string typeWithFormat)
        {
            var r = new Regex(@"(?<type>[a-zA-Z]+)(?<format>(\d*\.?\d))*");
            var m = r.Match(typeWithFormat);
            string type = m.Groups["type"].Value;
            string format = m.Groups["format"].Value;

            switch (type)
            {
                case "date":
                    if (value is null)
                        value = "0000-00-00";
                    return value;
                case "numeric":
                    return FormatNumeric(value, Convert.ToInt32(format));
                case "curr":
                case "decimal":
                case "quantum":
                    return FormatQuantum(value, format);
                case "char":
                case "cuky":
                case "unit":
                case "lang":
                    return value;
                case "time":
                    if (value is null)
                        value = "00:00:00";
                    return value;
            }
            return null;
        }


        private static string FormatQuantum(string value, string format)
        {
            string formating = "{0:0.0##}";
            if (value is null)
                return null;
            if (format != "")
            {
                var splittingFomat = format.Split('.');
                if (splittingFomat.Length > 1 && Convert.ToInt32(splittingFomat[1]) == 0)
                {
                    formating = "{0:0}";
                }
            }

            return String.Format(CultureInfo.InvariantCulture, formating, Convert.ToDouble(value));
        }

        private static string FormatNumeric(string value, int size)
        {
            if (value is null)
                return null;
            var str = new String('0', size);
            return String.Format(CultureInfo.InvariantCulture, "{0," + size + ":" + str + "}", Convert.ToInt32(value));
        }
    }
}
