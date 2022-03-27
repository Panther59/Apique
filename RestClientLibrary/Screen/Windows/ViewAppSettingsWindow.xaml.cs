// <copyright file="ViewAppSettingsWindow.xaml.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>16-08-2017</date>

namespace RestClientLibrary.Screen.Windows
{
    using DataLibrary;
    using RestClientLibrary.ViewModel;

    /// <summary>
    /// Interaction logic for ViewAppSettingsWindow.xaml
    /// </summary>
    public partial class ViewAppSettingsWindow : BaseWindow
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewAppSettingsWindow"/> class.
        /// </summary>
        public ViewAppSettingsWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Sets the ViewModel
        /// </summary>
        public SettingsViewModel ViewModel
        {
            set
            {
                this.ucSettings.DataContext = value;
            }
        }

        #endregion
    }
}
