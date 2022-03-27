// <copyright file="CertificateViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>05-09-2017</date>

namespace RestClientLibrary.ViewModel
{
    using DataLibrary;
    using RestClientLibrary.Model;
    using System.Security.Cryptography.X509Certificates;
    using System.Xml.Serialization;

    /// <summary>
    /// Defines the <see cref="CertificateViewModel" />
    /// </summary>
    [XmlRoot("Certificate")]
    public class CertificateViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// The friendlyName field
        /// </summary>
        private string friendlyName;

        /// <summary>
        /// The name field
        /// </summary>
        private string name;

        /// <summary>
        /// The subject field
        /// </summary>
        private string subject;

        /// <summary>
        /// The thumbprint field
        /// </summary>
        private string thumbprint;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the FriendlyName
        /// </summary>
        public string FriendlyName
        {
            get
            {
                return this.friendlyName;
            }

            set
            {
                this.friendlyName = value;
                this.OnPropertyChanged("FriendlyName");
            }
        }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets or sets the Subject
        /// </summary>
        public string Subject
        {
            get
            {
                return this.subject;
            }

            set
            {
                this.subject = value;
                this.OnPropertyChanged("Subject");
            }
        }

        /// <summary>
        /// Gets or sets the Thumbprint
        /// </summary>
        public string Thumbprint
        {
            get
            {
                return this.thumbprint;
            }

            set
            {
                this.thumbprint = value;
                this.OnPropertyChanged("Thumbprint");
            }
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The ToModel
        /// </summary>
        /// <returns>The <see cref="CertificateModel"/></returns>
        public CertificateModel ToModel()
        {
            return new CertificateModel
            {
                FriendlyName = this.FriendlyName,
                Name = this.Name,
                Subject = this.Subject,
                Thumbprint = this.Thumbprint
            };
        }

        public static CertificateViewModel Parse(X509Certificate2 certificate, string alias)
        {
            return new CertificateViewModel
            {
                FriendlyName = certificate.FriendlyName,
                Name = alias,
                Subject = certificate.Subject,
                Thumbprint = certificate.Thumbprint,
            };
        }

        public static CertificateViewModel Parse(CertificateModel certificate)
        {
            return new CertificateViewModel
            {
                FriendlyName = certificate.FriendlyName,
                Name = certificate.Name,
                Subject = certificate.Subject,
                Thumbprint = certificate.Thumbprint,
            };
        }

        #endregion

        #endregion
    }
}
