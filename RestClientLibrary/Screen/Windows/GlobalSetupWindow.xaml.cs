// <copyright file="GlobalSetupWindow.xaml.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>22-08-2017</date>

namespace RestClientLibrary.Screen
{
    using System.Windows;
    using DataLibrary;
    using RestClientLibrary.Model;
    using RestClientLibrary.ViewModel;

    /// <summary>
    /// Interaction logic for GlobalSetupWindow.xaml
    /// </summary>
    public partial class GlobalSetupWindow : BaseWindow
    {
        #region Fields

        /// <summary>
        /// Defines the viewModel
        /// </summary>
        private GlobalSetupViewModel viewModel;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalSetupWindow"/> class.
        /// </summary>
        public GlobalSetupWindow()
        {
            InitializeComponent();
            this.Loaded += GlobalSetupWindow_Loaded;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Environment
        /// </summary>
        public EnvironmentModel Environment { get; internal set; }

        /// <summary>
        /// Gets or sets the GlobalData
        /// </summary>
        public GlobalVariableModel GlobalData { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The OnRenderSizeChanged
        /// </summary>
        /// <param name="sizeInfo">The <see cref="SizeChangedInfo"/></param>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            //If the Height has changed then calc half of the the offset to move the form
            if (sizeInfo.HeightChanged == true)
            {
                this.Top += (sizeInfo.PreviousSize.Height - sizeInfo.NewSize.Height) / 2;
            }

            //If the Width has changed then calc half of the the offset to move the form
            if (sizeInfo.WidthChanged == true)
            {
                this.Left += (sizeInfo.PreviousSize.Width - sizeInfo.NewSize.Width) / 2;
            }
        }

        #region Private Methods

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
        /// The GlobalSetupWindow_Loaded
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="RoutedEventArgs"/></param>
        private void GlobalSetupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.viewModel = new GlobalSetupViewModel(this.ucGlobalSetup);
            viewModel.LoadData(this.GlobalData, this.Environment);

            this.ucGlobalSetup.DataContext = viewModel;
        }

        /// <summary>
        /// The Save_Click
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="RoutedEventArgs"/></param>
        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            var anyError = await this.viewModel.SaveData();
            if (anyError == false)
            {
                this.Environment = viewModel.SelectedEnvironment.ToModel();
                this.Close();
            }
        }

        #endregion

        #endregion
    }
}
