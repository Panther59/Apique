namespace DataLibrary
{
    using System;

    /// <summary>
    /// Defines the <see cref="ByteExtension" />
    /// </summary>
    public static class ByteExtension
    {
        /// <summary>
        /// Defines the OneGb
        /// </summary>
        private const long OneGb = OneMb * 1024;

        /// <summary>
        /// Defines the OneKb
        /// </summary>
        private const long OneKb = 1024;

        /// <summary>
        /// Defines the OneMb
        /// </summary>
        private const long OneMb = OneKb * 1024;

        /// <summary>
        /// Defines the OneTb
        /// </summary>
        private const long OneTb = OneGb * 1024;

        /// <summary>
        /// The ToPrettySize
        /// </summary>
        /// <param name="value">The <see cref="int"/></param>
        /// <param name="decimalPlaces">The <see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string ToPrettySize(this int value, int decimalPlaces = 0)
        {
            return ((long)value).ToPrettySize(decimalPlaces);
        }

        /// <summary>
        /// The ToPrettySize
        /// </summary>
        /// <param name="value">The <see cref="long"/></param>
        /// <param name="decimalPlaces">The <see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string ToPrettySize(this long value, int decimalPlaces = 0)
        {
            var asTb = Math.Round((double)value / OneTb, decimalPlaces);
            var asGb = Math.Round((double)value / OneGb, decimalPlaces);
            var asMb = Math.Round((double)value / OneMb, decimalPlaces);
            var asKb = Math.Round((double)value / OneKb, decimalPlaces);
            string chosenValue = asTb > 1 ? string.Format("{0} Tb", asTb) : asGb > 1 ? string.Format("{0} Gb", asGb) : asMb > 1 ? string.Format("{0} Mb", asMb) : asKb > 1 ? string.Format("{0} Kb", asKb) : string.Format("{0} Byte(s)", Math.Round((double)value, decimalPlaces));
            return chosenValue;
        }
    }
}
