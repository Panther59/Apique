namespace RestClientLibrary.Screen
{
    using System;
    using DataLibrary;
    using RestClientLibrary.View;
    using RestClientLibrary.ViewModel;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Interaction logic for ucGlobalSetup.xaml
    /// </summary>
    public partial class ucGlobalSetup : BaseUserControl, IGlobalSetupView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "ucGlobalSetup"/> class.
        /// </summary>
        public ucGlobalSetup()
        {
            InitializeComponent();
        }

        public CertificateViewModel AddNewCertificate()
        {
            AddCertificateWindow window = new AddCertificateWindow();
            window.Resources = (System.Windows.Window.GetWindow(this)).Resources;
            window.ShowDialog(this);

            return window.Certificate;
        }

        public EnvironmentViewModel AddNewEnvironment(EnvironmentViewModel environment)
        {
            AddEnvironmentWindow window = new AddEnvironmentWindow();
            window.Resources = (System.Windows.Window.GetWindow(this)).Resources;
            window.Environment = environment;
            window.ShowDialog(this);

            return window.Environment;
        }
    }
}
