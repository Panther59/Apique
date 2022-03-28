namespace RestClientLibrary.Screen
{
    using System.Windows;
    using DataLibrary;
    using RestClientLibrary.ViewModel;

    /// <summary>
    /// Interaction logic for AddEnvironmentWindow.xaml
    /// </summary>
    public partial class AddEnvironmentWindow : BaseWindow
    {
        /// <summary>
        /// Defines the viewModel
        /// </summary>
        private EnvironmentViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref = "AddEnvironmentWindow"/> class.
        /// </summary>
        public AddEnvironmentWindow()
        {
            InitializeComponent();
            this.Loaded += this.AddEnvironmentWindow_Loaded;
        }

        /// <summary>
        /// Gets or sets the Environment
        /// </summary>
        public EnvironmentViewModel Environment
        {
            get;
            set;
        }

        /// <summary>
        /// The AddEnvironmentWindow_Loaded
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/></param>
        private void AddEnvironmentWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.Environment != null)
            {
                this.viewModel = this.Environment;
            }
            else
            {
                this.viewModel = new EnvironmentViewModel();
            }

            viewModel.LoadData();
            this.ucEnvironment.DataContext = viewModel;
        }

        /// <summary>
        /// The Close_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="RoutedEventArgs"/></param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Environment = null;
            this.Close();
        }

        /// <summary>
        /// The Save_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="RoutedEventArgs"/></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var isValid = this.viewModel.IsValid();
            if (isValid)
            {
                this.Environment = viewModel;
                this.Close();
            }
        }
    }
}
