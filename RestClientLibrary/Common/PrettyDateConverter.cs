// <copyright file="PrettyDateConverter.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>10-08-2017</date>

namespace RestClientLibrary.Common
{
    using System;
    using DataLibrary.Common;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Defines the <see cref="PrettyDateConverter" />
    /// </summary>
    public class PrettyDateConverter : IValueConverter
    {
        #region Methods

        #region Public Methods

        /// <summary>
        /// The Convert
        /// </summary>
        /// <param name="value">The <see cref="object"/></param>
        /// <param name="targetType">The <see cref="Type"/></param>
        /// <param name="parameter">The <see cref="object"/></param>
        /// <param name="culture">The <see cref="CultureInfo"/></param>
        /// <returns>The <see cref="object"/></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                var output = ((DateTime)value).GetPrettyString();
                return output;
            }

            return null;
        }

        /// <summary>
        /// The ConvertBack
        /// </summary>
        /// <param name="value">The <see cref="object"/></param>
        /// <param name="targetType">The <see cref="Type"/></param>
        /// <param name="parameter">The <see cref="object"/></param>
        /// <param name="culture">The <see cref="CultureInfo"/></param>
        /// <returns>The <see cref="object"/></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion

        #endregion
    }
}
