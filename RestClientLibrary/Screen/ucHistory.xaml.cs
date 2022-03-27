using DataLibrary;
using RestClientLibrary.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestClientLibrary.Screen
{
    /// <summary>
    /// Interaction logic for ucHistory.xaml
    /// </summary>
    public partial class ucHistory : BaseUserControl, IHistoryView
    {
        public ucHistory()
        {
            InitializeComponent();
        }

        
    }
}
