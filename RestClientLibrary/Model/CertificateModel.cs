// <copyright file="CertificateModel.cs" company="Accenture">
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
    /// Defines the <see cref="CertificateModel" />
    /// </summary>
    public class CertificateModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets FriendlyName
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets Thumbprint
        /// </summary>
        public string Thumbprint { get; set; }

		public string CertificateFilePath { get; set; }

		public string CertificateFilePassword { get; set; }

		#endregion

		#region Methods

		#region Public Methods

		/// <summary>
		/// The Parse
		/// </summary>
		/// <param name="inputList">The <see cref="System.Collections.ObjectModel.ObservableCollection{CertificateViewModel}"/></param>
		/// <returns>The <see cref="List{CertificateModel}"/></returns>
		public static List<CertificateModel> Parse(System.Collections.ObjectModel.ObservableCollection<CertificateViewModel> inputList)
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
