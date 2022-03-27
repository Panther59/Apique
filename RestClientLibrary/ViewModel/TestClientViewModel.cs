using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Net;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using DataLibrary;
using RestClientLibrary.View;
using RestClientLibrary.Model;
using RestClientLibrary.Common;

namespace RestClientLibrary.ViewModel
{
    public class TestClientViewModel : BaseViewModel
    {
        #region Private Declaration

        ITestClientView _view;

        #endregion

        #region Constructor

        public TestClientViewModel(ITestClientView view)
        {
            _view = view;
        }

        #endregion

        #region Properties

        private ObservableCollection<TestCaseViewModel> _testCases;
        public ObservableCollection<TestCaseViewModel> TestCases
        {
            get { return _testCases; }
            set
            {
                _testCases = value;
                OnPropertyChanged("TestCases");
            }
        }

        private ObservableCollection<TestSuiteViewModel> _testSuits;
        public ObservableCollection<TestSuiteViewModel> TestSuits
        {
            get { return _testSuits; }
            set
            {
                _testSuits = value;
                OnPropertyChanged("TestSuits");
            }
        }
        
        #endregion

        #region Commands

        private RelayCommand _runAllTestCasesCommand;
        public RelayCommand RunAllTestCasesCommand
        {
            get
            {
                if (_runAllTestCasesCommand == null)
                {
                    _runAllTestCasesCommand = new RelayCommand(command => RunAllTestCases(), can => CanRunAllTestCasesEnabled());
                }
                return _runAllTestCasesCommand;
            }
        }

        private RelayCommand _runAllTestSuitsCommand;
        public RelayCommand RunAllTestSuitsCommand
        {
            get
            {
                if (_runAllTestSuitsCommand == null)
                {
                    _runAllTestSuitsCommand = new RelayCommand(command => RunAllTestSuits(), can => CanRunAllTestSuitsEnabled());
                }
                return _runAllTestSuitsCommand;
            }
        }

        #endregion

        #region Public Methods

        public void LoadData()
        {
            if (TestCases != null)
            {
                foreach (var item in TestCases)
                {
                    item.TestCaseRemoved += testCase_TestCaseRemoved;
                }
            }

            if (TestSuits != null)
            {
                foreach (var item in TestSuits)
                {
                    item.TestSuiteRemoved += item_TestSuiteRemoved;
                }
            }
        }

        #endregion

        #region Private Methods

        void testCase_TestCaseRemoved(TestCaseViewModel tc)
        {
            bool conf = _view.ConfirmationBox("Remove Test Case", string.Format("Are you sure you want to remove {0}-{1}", tc.TestCaseID, tc.Title));

            if (conf)
            {
                AppDataHelper.RemoveTestCase(tc);
            }
        }

        void item_TestSuiteRemoved(TestSuiteViewModel ts)
        {
            bool conf = _view.ConfirmationBox("Remove Test Suite", string.Format("Are you sure you want to remove {0}-{1}", ts.TestSuiteID, ts.Title));

            if (conf)
            {
                AppDataHelper.RemoveTestSuite(ts);
            } 
        }

        void item_TestSuiteSelected(TestSuiteViewModel ts)
        {
            
        }

        private bool CanRunAllTestSuitsEnabled()
        {
            return TestSuits != null && TestSuits.Count > 0;
        }

        private void RunAllTestSuits()
        {
            try
            {
                foreach (var ts in TestSuits)
                {
                    ts.ExecuteTests();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        private bool CanRunAllTestCasesEnabled()
        {
            return TestCases != null && TestCases.Count > 0;
        }

        private void RunAllTestCases()
        {
            try
            {
                foreach (var tc in TestCases)
                {
                    tc.ExecuteTest();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

         
        #endregion

    }
}
