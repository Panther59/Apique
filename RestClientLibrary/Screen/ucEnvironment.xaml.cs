// <copyright file="ucEnvironment.xaml.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>06-09-2017</date>

namespace RestClientLibrary.Screen
{
    using DataLibrary;
    using RestClientLibrary.View;
    using RestClientLibrary.ViewModel;

    /// <summary>
    /// Interaction logic for ucEnvironment.xaml
    /// </summary>
    public partial class ucEnvironment : BaseUserControl, IEnvironmentView
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ucEnvironment"/> class.
        /// </summary>
        public ucEnvironment()
        {
            InitializeComponent();
            this.DataContextChanged += this.UcEnvironment_DataContextChanged;
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The AddNewCertificate
        /// </summary>
        /// <returns>The <see cref="CertificateViewModel"/></returns>
        public CertificateViewModel AddNewCertificate()
        {
            AddCertificateWindow window = new AddCertificateWindow();
            window.Resources = (System.Windows.Window.GetWindow(this)).Resources;
            window.ShowDialog(this);

            return window.Certificate;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The UcEnvironment_DataContextChanged
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/></param>
        private void UcEnvironment_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            var viewModel = this.DataContext as EnvironmentViewModel;
            viewModel?.AttachView(this);
        }

        #endregion

        #endregion
    }
}
