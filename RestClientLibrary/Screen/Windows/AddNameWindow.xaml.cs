using DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RestClientLibrary.Screen.Windows
{
	/// <summary>
	/// Interaction logic for AddNameWindow.xaml
	/// </summary>
	public partial class AddNameWindow : BaseWindow
	{
		public AddNameWindow()
		{
			InitializeComponent();
		}

		public string NewName { get; set; }

		private void btnSelect_Click(object sender, RoutedEventArgs e)
		{
			this.NewName = txtName.Text;
			this.Close();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
