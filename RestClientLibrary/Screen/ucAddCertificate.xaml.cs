using DataLibrary;
using Microsoft.Win32;
using RestClientLibrary.Common;
using RestClientLibrary.View;
using RestClientLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace RestClientLibrary.Screen
{
    /// <summary>
    /// Interaction logic for ucAddCertificate.xaml
    /// </summary>
    public partial class ucAddCertificate : BaseUserControl, IAddCertificateView
    {

        public ucAddCertificate()
        {
            InitializeComponent();
            this.Loaded += this.UcAddCertificate_Loaded;
            this.ViewModel = new AddCertificateViewModel(this as IAddCertificateView);
        }

        public AddCertificateViewModel ViewModel { get; private set; }

		public string GetFilePath()
		{
            Microsoft.Win32.OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = Constants.ImportCertFilter;
            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                return dialog.FileName;
            }
            else
            {
                return null;
            }
        }

		private void UcAddCertificate_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.ViewModel;
        }

    }
}
