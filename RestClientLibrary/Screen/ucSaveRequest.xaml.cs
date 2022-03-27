﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataLibrary;
using System.Windows.Controls.Primitives;
using RestClientLibrary.View;
using RestClientLibrary.ViewModel;

namespace RestClientLibrary.Screen
{
    /// <summary>
    /// Interaction logic for ucSavedRequest.xaml
    /// </summary>
    public partial class ucSaveRequest : BaseUserControl, ISaveRequestView
    {
        public ucSaveRequest()
        {
            InitializeComponent();
        }
    }
}
