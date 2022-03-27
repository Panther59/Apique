// <copyright file="AddCertificateWindow.xaml.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>06-09-2017</date>

namespace RestClientLibrary.Screen
{
	using DataLibrary;
	using RestClientLibrary.ViewModel;
	using System.Security.Cryptography.X509Certificates;

	/// <summary>
	/// Interaction logic for AddCertificateWindow.xaml
	/// </summary>
	public partial class AddCertificateWindow : BaseWindow
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AddCertificateWindow"/> class.
		/// </summary>
		public AddCertificateWindow()
		{
			InitializeComponent();
			this.Closing += this.AddCertificateWindow_Closing;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the Certificate
		/// </summary>
		public CertificateViewModel Certificate { get; private set; }
		public bool IsCertPath
		{
			set
			{
				this.ucAddCertificate.ViewModel.IsFile = value;
			}
		}

		#endregion

		#region Methods

		#region Private Methods

		/// <summary>
		/// The AddCertificateWindow_Closing
		/// </summary>
		/// <param name="sender">The <see cref="object"/></param>
		/// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/></param>
		private void AddCertificateWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Certificate = this.ucAddCertificate.ViewModel.FinalCertificate;
		}

		#endregion

		#endregion
	}
}
