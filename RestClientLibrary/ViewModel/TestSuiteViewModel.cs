using DataLibrary;
using RestClientLibrary.Common;
using RestClientLibrary.Model;
using RestClientLibrary.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLibrary.ViewModel
{
    public class TestSuiteViewModel : BaseViewModel
    {
        #region Private Daclaration
		
        public delegate void TestSuiteRemovedHandler(TestSuiteViewModel tc);
        public event TestSuiteRemovedHandler TestSuiteRemoved;

        private ITestSuiteView _view;
 
	    #endregion

        #region Constructor

        public TestSuiteViewModel(ITestSuiteView view)
        {
            _view = view;
        }
        
        #endregion
        
        #region Properties

        private string _testSuiteID;
        public string TestSuiteID
        {
            get { return _testSuiteID; }
            set
            {
                _testSuiteID = value;
                OnPropertyChanged("TestSuiteID");
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        private bool _showResult;
        public bool ShowResult
        {
            get { return _showResult; }
            set
            {
                _showResult = value;
                OnPropertyChanged("ShowResult");
            }
        }

        private ResultState _result;
        public ResultState Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }
        
        private ObservableCollection<TestCaseViewModel> _testCasesForSelection;
        public ObservableCollection<TestCaseViewModel> TestCasesForSelection
        {
            get { return _testCasesForSelection; }
            set
            {
                _testCasesForSelection = value;
                OnPropertyChanged("TestCasesForSelection");
            }
        }

        private ObservableCollection<TestCaseViewModel> _testCasesSelected;
        public ObservableCollection<TestCaseViewModel> TestCasesSelected
        {
            get { return _testCasesSelected; }
            set
            {
                _testCasesSelected = value;
                OnPropertyChanged("TestCasesSelected");
            }
        }

        private ObservableCollection<KeyValueViewModel> _variables;
        public ObservableCollection<KeyValueViewModel> Variables
        {
            get { return _variables; }
            set
            {
                _variables = value;
                OnPropertyChanged("Variables");
            }
        }

        private bool _selectTestCasesPopupOpen;
        public bool SelectTestCasesPopupOpen
        {
            get { return _selectTestCasesPopupOpen; }
            set
            {
                _selectTestCasesPopupOpen = value;
                OnPropertyChanged("SelectTestCasesPopupOpen");
            }
        }
        
        #endregion
        
        #region Commands

        private RelayCommand _saveTestSuiteCommand;
        public RelayCommand SaveTestSuiteCommand
        {
            get
            {
                if (_saveTestSuiteCommand == null)
                {
                    _saveTestSuiteCommand = new RelayCommand(command => SaveTestSuite(), can => CanSaveTestSuiteEnabled());
                }
                return _saveTestSuiteCommand;
            }
        }

        private RelayCommand _executeTestsCommand;
        public RelayCommand ExecuteTestsCommand
        {
            get
            {
                if (_executeTestsCommand == null)
                {
                    _executeTestsCommand = new RelayCommand(command => ExecuteTests(), can => CanExecuteTestsEnabled());
                }
                return _executeTestsCommand;
            }
        }

        private RelayCommand _addKeyValuePairCommand;
        public RelayCommand AddKeyValuePairCommand
        {
            get
            {
                if (_addKeyValuePairCommand == null)
                {
                    _addKeyValuePairCommand = new RelayCommand(command => AddKeyValuePair());
                }
                return _addKeyValuePairCommand;
            }
            set { _addKeyValuePairCommand = value; }
        }

        private RelayCommand _addTestCaseCommand;
        public RelayCommand AddTestCaseCommand
        {
            get
            {
                if (_addTestCaseCommand == null)
                {
                    _addTestCaseCommand = new RelayCommand(command => AddTestCase(), can => CanAddTestEnabled());
                }
                return _addTestCaseCommand;
            }
            set { _addTestCaseCommand = value; }
        }

        private RelayCommand _selectTestCasesCommand;
        public RelayCommand SelectTestCasesCommand
        {
            get
            {
                if (_selectTestCasesCommand == null)
                {
                    _selectTestCasesCommand = new RelayCommand(command => SelectTestCases(), can => CanSelectTestCasesEnabled());
                }
                return _selectTestCasesCommand;
            }
        }

        private RelayCommand _testSuiteSelectedCommand;
        public RelayCommand TestSuiteSelectedCommand
        {
            get
            {
                if (_testSuiteSelectedCommand == null)
                {
                    _testSuiteSelectedCommand = new RelayCommand(command => OnTestSuiteSelected());
                }
                return _testSuiteSelectedCommand;
            }
            set { _testSuiteSelectedCommand = value; }
        }

        private RelayCommand _removeTestSuiteCommand;
        public RelayCommand RemoveTestSuiteCommand
        {
            get
            {
                if (_removeTestSuiteCommand == null)
                {
                    _removeTestSuiteCommand = new RelayCommand(command => OnRemoveTestSuite());
                }
                return _removeTestSuiteCommand;
            }
            set { _removeTestSuiteCommand = value; }
        }

        private RelayCommand _resetCommand;
        public RelayCommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                {
                    _resetCommand = new RelayCommand(command => Reset());
                }
                return _resetCommand;
            }
            set { _resetCommand = value; }
        }

        #endregion

        #region Public Function

        public void LoadData(ObservableCollection<TestCaseViewModel> allTestCases)
        {
            if (allTestCases != null)
            {
                TestCasesForSelection = new ObservableCollection<TestCaseViewModel>(allTestCases);

                if (TestCasesSelected != null)
                {
                    foreach (var tc in TestCasesSelected)
                    {
                        tc.TestCaseRemoved += tc_TestCaseRemoved;
                    }
                }
            }
        }

        public static TestSuiteViewModel Parse(Model.TestSuiteModel item, View.IMainView mainView)
        {
            TestSuiteViewModel vm = new TestSuiteViewModel(mainView.TestSuiteView);
            vm.TestSuiteID = item.TestSuiteID;
            vm.Title = item.Title;
            vm.Variables = KeyValueViewModel.Parse(item.Variables);
            vm.TestCasesSelected = TestCaseViewModel.Parse(item.TestCases, mainView.TestCaseView);

            return vm;
        }

        public void ExecuteTests()
        {
            try
            {
                IsBusy = true;
                Result = ResultState.InProgress;
                foreach (var tc in TestCasesSelected)
                {
                    tc.TestCaseResult = ResultState.InProgress;
                    tc.SubscribeResposeReceivedEvent(OnResposeReceived);
                    tc.SubscribeOverrideRequestEvent(TestCase_GetRelatedVariables);
                    tc.ExecuteTest();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        #endregion

        #region Private Functions

        private bool CanSaveTestSuiteEnabled()
        {
            if (string.IsNullOrEmpty(Title) == false &&
                TestCasesSelected != null && TestCasesSelected.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SaveTestSuite()
        {
            try
            {
                if (TestSuiteID.GetString() == string.Empty)
                {
                    TestSuiteID = AppDataHelper.GetNextTestSuiteID();
                }

                AppDataHelper.SaveTestSuite(this);
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        private bool CanExecuteTestsEnabled()
        {
            return CanSaveTestSuiteEnabled();
        }

        private void OnResposeReceived(RestResponse restresponse)
        {
            ShowResult = !TestCasesSelected.Any(x => x.TestCaseResult == ResultState.NotStarted || x.TestCaseResult == ResultState.InProgress);
            if (ShowResult)
            {
                IsBusy = false;
                if (TestCasesSelected.Any(x => x.TestCaseResult == ResultState.Fail))
                {
                    Result = ResultState.Fail;
                }
                else
                {
                    Result = ResultState.Pass;
                }
            }
        }

        private void AddKeyValuePair()
        {
            try
            {
                if (Variables == null)
                {
                    Variables = new ObservableCollection<KeyValueViewModel>();
                }
                Variables.Add(GetEmptyKeyValuePair());
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        private bool CanAddTestEnabled()
        {
            return TestCasesForSelection != null && TestCasesForSelection.Count > 0;
        }

        private KeyValueViewModel GetEmptyKeyValuePair()
        {
            KeyValueViewModel pair = new KeyValueViewModel();
            pair.Remove += pair_Remove;

            return pair;
        }

        void pair_Remove(KeyValueViewModel pair)
        {
            Variables.Remove(pair);

            if (Variables.Count == 0)
            {
                Variables.Add(GetEmptyKeyValuePair());
            }
        }

        private void AddTestCase()
        {
            try
            {
                if (TestCasesForSelection != null)
                {
                    foreach (var tc in TestCasesForSelection)
                    {
                        tc.IsSelected = TestCasesSelected != null &&
                            TestCasesSelected.Any(x => x.TestCaseID == tc.TestCaseID);
                    }
                    SelectTestCasesPopupOpen = true;
                }
                else
                {
                    _view.MessageShow("Test Cases", "There are not test cases defined yet.");
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        private bool CanSelectTestCasesEnabled()
        {
            if (TestCasesForSelection != null && TestCasesForSelection.Any(x => x.IsSelected))
            {
                return true;
            }
            return false;
        }

        private void SelectTestCases()
        {
            try
            {
                SelectTestCasesPopupOpen = false;

                TestCasesSelected = new ObservableCollection<TestCaseViewModel>(TestCasesForSelection.Where(x => x.IsSelected));

                
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        void tc_TestCaseRemoved(TestCaseViewModel tc)
        {
            TestCasesSelected.Remove(tc);
        }

        private void OnTestSuiteSelected()
        {
            try
            {
                MasterEventHandler.RaiseTestSuiteSelectedEvent(this); 
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        private void OnRemoveTestSuite()
        {
            try
            {
                if (TestSuiteRemoved != null)
                {
                    TestSuiteRemoved(this);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        private void Reset()
        {
            try
            {
                this.ShowResult = false;
                this.Result = ResultState.NotStarted;
                this.TestSuiteID = null;
                this.Title = null;
                this.Variables = new ObservableCollection<KeyValueViewModel>();
                this.TestCasesSelected = new ObservableCollection<TestCaseViewModel>();
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        private List<KeyValueModel> TestCase_GetRelatedVariables()
        {
            var variables = this.Variables.Select(x => new KeyValueModel { GUID = x.GUID, Key = x.Key, Value = x.Value }).ToList();

            return variables;
        }

        private string ReplaceVariables(string input)
        {
            if (Variables != null)
            {
                foreach (var variable in Variables)
                {
                    input = input.GetString().Replace(string.Format("{{{{{0}}}}}", variable.Key), variable.Value);
                }
            }
            return input;
        }

        #endregion
    }
}
