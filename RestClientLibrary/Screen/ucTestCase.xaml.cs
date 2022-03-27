﻿using DataLibrary;
using RestClientLibrary.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestClientLibrary.Screen
{
    /// <summary>
    /// Interaction logic for ucTestCase.xaml
    /// </summary>
    public partial class ucTestCase : BaseUserControl, ITestCaseView
    {
        public ucTestCase()
        {
            InitializeComponent();
        }

        public IRestClientView RestClientView
        {
            get { return ucRestClient as IRestClientView; }
        }
    }
}
