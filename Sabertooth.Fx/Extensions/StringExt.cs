using System;

namespace Sabertooth.Fx.Extensions {

    public static class StringExt {

        public static string RemoveLastCharacter(this String instr) {
            return instr.Substring(0, instr.Length - 1);
        }

        public static string RemoveLast(this String instr, int number) {
            return instr.Substring(0, instr.Length - number);
        }

        public static string RemoveFirstCharacter(this String instr) {
            return instr.Substring(1);
        }

        public static string RemoveFirst(this String instr, int number) {
            return instr.Substring(number);
        }
        /// <summary>
        /// Converts a string to a decimal value.
        /// </summary>
        /// <param name="str">The string to be converted.</param>
        /// <returns>If the string can be converted, a decimal is returned, otherwise, returns zero.</returns>
        public static decimal ToDecimal(this string str) {
            if (string.IsNullOrEmpty(str))
                return 0;

            decimal d;

            if (!decimal.TryParse(str, out d))
                return 0;
            else
                return d;
        }

        /// <summary>
        /// Wrap a string with percent sign wild card operators. 
        /// </summary>
        /// <param name="text">The string to be converted.</param>
        /// <returns>The given string wrapped in wild card characters.</returns>
        public static string ToLike(this string text) {
            return string.Concat("%", text, "%");
        }
        /// <summary>
        /// Replaces all single-quote characters with two single quote characters.
        /// </summary>
        /// <param name="text">The original string value.</param>
        /// <returns>The modified string.</returns>
        public static string Sanitize(this string text) {
            return text.Replace("'", "''");
        }
    }
}