namespace RestClientLibrary.Screen
{
    using System;
    using DataLibrary;
    using RestClientLibrary.View;
    using RestClientLibrary.ViewModel;
    using System.Security.Cryptography.X509Certificates;
	using RestClientLibrary.Screen.Windows;

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

        public EnvironmentViewModel AddNewEnvironment(GlobalSetupViewModel globalSetupViewModel, EnvironmentViewModel environment)
        {
            AddEnvironmentWindow window = new AddEnvironmentWindow();
            window.Resources = (System.Windows.Window.GetWindow(this)).Resources;
            window.Environment = environment;
            window.GlobalSetup = globalSetupViewModel;
            window.ShowDialog(this);

            return window.Environment;
        }

		public string AddNewWorkspace()
		{
            AddNameWindow window = new AddNameWindow();
            window.Resources = (System.Windows.Window.GetWindow(this)).Resources;
            window.ShowDialog(this);

            return window.NewName;
        }
	}
}
