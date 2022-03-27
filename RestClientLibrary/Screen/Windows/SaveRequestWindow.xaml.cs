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

namespace RestClientLibrary.Screen
{
    /// <summary>
    /// Interaction logic for SaveRequestWindow.xaml
    /// </summary>
    public partial class SaveRequestWindow : BaseWindow
    {
        public SaveRequestWindow()
        {
            InitializeComponent();
        }

        public void LoadData(RestClientLibrary.ViewModel.SaveRequestViewModel data)
        {
            ucSaveRequest.DataContext = data;
            data.AttachView(ucSaveRequest);
        }
    }
}
