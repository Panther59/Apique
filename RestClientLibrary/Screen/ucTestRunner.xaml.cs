// <copyright file="ucTestRunner.xaml.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>09-08-2017</date>

namespace RestClientLibrary.Screen
{
    using System.Windows;
    using System.Windows.Controls;
    using Unity;
    using RestClientLibrary.Startup;
    using RestClientLibrary.ViewModel;

    /// <summary>
    /// Interaction logic for ucTestRunner.xaml
    /// </summary>
    public partial class ucTestRunner : UserControl
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ucTestRunner"/> class.
        /// </summary>
        public ucTestRunner()
        {
            InitializeComponent();
            this.Loaded += this.UcTestRunner_Loaded;
        }

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// The UcTestRunner_Loaded
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="RoutedEventArgs"/></param>
        private void UcTestRunner_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = UnityConfig.Container.Resolve<TestRunnerViewModel>();
        }

        #endregion

        #endregion
    }
}
