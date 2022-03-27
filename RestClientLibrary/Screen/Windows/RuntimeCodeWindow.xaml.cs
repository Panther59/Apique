namespace RestClientLibrary.Screen
{
    using System.Windows;
    using DataLibrary;

    /// <summary>
    /// Interaction logic for RuntimeCodeWindow.xaml
    /// </summary>
    public partial class RuntimeCodeWindow : BaseWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeCodeWindow"/> class.
        /// </summary>
        public RuntimeCodeWindow()
        {
            InitializeComponent();
        }

        public void LoadData(string code)
        {
            this.txtCode.Text = code;
        }

        /// <summary>
        /// The BtnClose_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="RoutedEventArgs"/></param>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
