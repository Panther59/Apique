// <copyright file="KeyValueModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>05-09-2017</date>

namespace RestClientLibrary.Model
{
    using RestClientLibrary.ViewModel;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="KeyValueModel" />
    /// </summary>
    public class KeyValueModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the GUID
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// Gets or sets the Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the Value
        /// </summary>
        public string Value { get; set; }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The Parse
        /// </summary>
        /// <param name="inputList">The <see cref="System.Collections.ObjectModel.ObservableCollection{KeyValueViewModel}"/></param>
        /// <returns>The <see cref="List{KeyValueModel}"/></returns>
        public static List<KeyValueModel> Parse(System.Collections.ObjectModel.ObservableCollection<KeyValueViewModel> inputList)
        {
            if (inputList == null)
            {
                return null;
            }

            return inputList.Select(x => x.ToModel()).ToList();
        }

        #endregion

        #endregion
    }
}
