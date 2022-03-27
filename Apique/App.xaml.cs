using Apique.Startup;
using DataLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Apique
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            App.Current.DispatcherUnhandledException += this.Current_DispatcherUnhandledException;
            UnityConfig.RegisterTypes();
        }

        /// <summary>
        /// The Current_DispatcherUnhandledException
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="System.Windows.Threading.DispatcherUnhandledExceptionEventArgs"/></param>
        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            ErrorLog.LogException(e.Exception);
        }
    }
}
