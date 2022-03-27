// <copyright file="HttpStatusCodeConverter.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.Common
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Net;
    using System.Reflection;
    using System.Windows.Data;

    /// <summary>
    /// Defines the <see cref="HttpStatusCodeConverter" />
    /// </summary>
    public class HttpStatusCodeConverter : IValueConverter
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
            if (value is HttpStatusCode)
            {
                HttpStatusCode myEnum = (HttpStatusCode)value;
                string description = GetEnumDescription(myEnum);
                return string.Format("{0} ({1})", (int)myEnum, description);
            }
            else
            {
                return string.Empty;
            }
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
            return string.Empty;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The GetEnumDescription
        /// </summary>
        /// <param name="enumObj">The <see cref="Enum"/></param>
        /// <returns>The <see cref="string"/></returns>
        private string GetEnumDescription(Enum enumObj)
        {
            FieldInfo fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

            object[] attribArray = fieldInfo.GetCustomAttributes(false);

            if (attribArray.Length == 0)
            {
                return enumObj.ToString();
            }
            else
            {
                DescriptionAttribute attrib = attribArray[0] as DescriptionAttribute;
                return attrib != null ? attrib.Description : enumObj.ToString();
            }
        }

        #endregion

        #endregion
    }
}
