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
using RestClientLibrary.Common;
using RestClientLibrary.Model;
using RestClientLibrary.ViewModel.Validations;

namespace RestClientLibrary.ViewModel
{
    public class TestCaseViewModel : BaseViewModel
    {
        #region Events and Delegates

        #region Event TestCaseRemoved

        public delegate void TestCaseRemovedHandler(TestCaseViewModel tc);
        public event TestCaseRemovedHandler TestCaseRemoved;

        #endregion

        #region Event ResposeReceived

        public delegate void OnResposeReceivedHandler(RestResponse restresponse);
        public event OnResposeReceivedHandler ResposeReceived;
        private void RaiseResposeReceivedEvent(RestResponse restresponse)
        {
            if (ResposeReceived != null)
            {
                ResposeReceived(restresponse);
            }
        }

        public void SubscribeResposeReceivedEvent(OnResposeReceivedHandler handler)
        {
            ClearResposeReceivedEvent();
            ResposeReceived += handler;
        }

        public void ClearResposeReceivedEvent()
        {
            if (ResposeReceived != null)
            {
                foreach (Delegate d in ResposeReceived.GetInvocationList())
                {
                    ResposeReceived -= (OnResposeReceivedHandler)d;
                }
            }
        }

        #endregion

        #region Event OverrideRequest

        public delegate List<KeyValueModel> GetRelatedVariablesHandler();
        public event GetRelatedVariablesHandler GetRelatedVariables;
        private List<KeyValueModel> RaiseGetRelatedVariables()
        {
            if (GetRelatedVariables != null)
            {
                return GetRelatedVariables();
            }
            else
            {
                return null;
            }
        }

        public void SubscribeOverrideRequestEvent(GetRelatedVariablesHandler handler)
        {
            ClearOverrideRequestEvent();
            GetRelatedVariables += handler;
        }

        public void ClearOverrideRequestEvent()
        {
            if (GetRelatedVariables != null)
            {
                foreach (Delegate d in GetRelatedVariables.GetInvocationList())
                {
                    GetRelatedVariables -= (GetRelatedVariablesHandler)d;
                }
            }
        }

        #endregion

        #endregion

        #region Private Declaration

        ITestCaseView _view;

        #endregion

        #region Constructor

        public TestCaseViewModel(ITestCaseView view)
        {
            _view = view;
            LoadData();
        }

        #endregion

        #region Properties

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        private string _testCaseID;
        public string TestCaseID
        {
            get { return _testCaseID; }
            set
            {
                _testCaseID = value;
                OnPropertyChanged("TestCaseID");
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }


        private RestClientViewModel _restClient;
        public RestClientViewModel RestClient
        {
            get { return _restClient; }
            set
            {
                _restClient = value;
                OnPropertyChanged("RestClient");
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

        private ObservableCollection<BaseValidationRuleViewModel> _validations;
        public ObservableCollection<BaseValidationRuleViewModel> Validations
        {
            get { return _validations; }
            set
            {
                _validations = value;
                OnPropertyChanged("Validations");
            }
        }

        private ResultState _testCaseResult;
        public ResultState TestCaseResult
        {
            get { return _testCaseResult; }
            set
            {
                _testCaseResult = value;
                OnPropertyChanged("TestCaseResult");
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

        #endregion

        #region Commands

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

        private RelayCommand _addValidationCommand;
        public RelayCommand AddValidationCommand
        {
            get
            {
                if (_addValidationCommand == null)
                {
                    _addValidationCommand = new RelayCommand(command => AddValidation());
                }
                return _addValidationCommand;
            }
            set { _addValidationCommand = value; }
        }

        private RelayCommand _saveTestCaseCommand;
        public RelayCommand SaveTestCaseCommand
        {
            get
            {
                if (_saveTestCaseCommand == null)
                {
                    _saveTestCaseCommand = new RelayCommand(command => SaveTestCase(), can => CanSaveTestCaseEnabled());
                }
                return _saveTestCaseCommand;
            }
        }

        private RelayCommand _testCaseSelectedCommand;
        public RelayCommand TestCaseSelectedCommand
        {
            get
            {
                if (_testCaseSelectedCommand == null)
                {
                    _testCaseSelectedCommand = new RelayCommand(command => OnTestCaseSelected());
                }
                return _testCaseSelectedCommand;
            }
            set { _testCaseSelectedCommand = value; }
        }

        private RelayCommand _executeTestCommand;
        public RelayCommand ExecuteTestCommand
        {
            get
            {
                if (_executeTestCommand == null)
                {
                    _executeTestCommand = new RelayCommand(command => ExecuteTest(), can => CanExecuteTestEnabled());
                }
                return _executeTestCommand;
            }
        }

        private RelayCommand _removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                if (_removeCommand == null)
                {
                    _removeCommand = new RelayCommand(command => Remove());
                }
                return _removeCommand;
            }
            set { _removeCommand = value; }
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

        #region Public Methods

        public void ResetData()
        {
            ShowResult = false;
            TestCaseResult = ResultState.NotStarted;
            RestClient.IsResponseVisible = false;
        }

        public static TestCaseViewModel Parse(Model.TestCaseModel item, ITestCaseView testCaseView)
        {
            TestCaseViewModel vm = new TestCaseViewModel(testCaseView);
            vm.TestCaseID = item.TestCaseID;
            vm.Title = item.Title;
            vm.RestClient.UrlBase.Url = item.Url;
            vm.RestClient.HeadersBase.SetHeaders(item.Header);
            vm.RestClient.RequestContent = item.Request;
            vm.TestCaseID = item.TestCaseID;
            vm.Validations = BaseValidationRuleViewModel.Parse(item.Validations);
            vm.Variables = KeyValueViewModel.Parse(item.Variables);
            return vm;
        }

        public static ObservableCollection<TestCaseViewModel> Parse(List<TestCaseModel> list, ITestCaseView testCaseView)
        {
            if (list == null)
            {
                return null;
            }
            ObservableCollection<TestCaseViewModel> outList = new ObservableCollection<TestCaseViewModel>();
            foreach (var item in list)
            {
                var output = TestCaseViewModel.Parse(item, testCaseView);
                outList.Add(output);
            }
            return outList;

        }

        public void CreateTestCase(TransactionViewModel item)
        {
            this.RestClient.UrlBase.Url = item.Url;
            this.RestClient.SelectOperation(item.Operation);
            this.RestClient.HeadersBase.SetHeaders(item.Headers);
            this.RestClient.RequestContent = item.RequestContent;
        }

        #endregion

        #region Private Methods

        private void LoadData()
        {
            Variables = new ObservableCollection<KeyValueViewModel>();

            if (_view != null)
            {
                RestClient = new RestClientViewModel(_view.RestClientView);
                RestClient.GetRelatedVariables += RestClient_GetRelatedVariables; ;
            }
        }

        private List<KeyValueModel> RestClient_GetRelatedVariables()
        {
            var variables = this.Variables.Select(x => new KeyValueModel { GUID = x.GUID, Key = x.Key, Value = x.Value }).ToList();

            var otherVariables = this.RaiseGetRelatedVariables();

            variables.AddRange(otherVariables);

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

        private void AddKeyValuePair()
        {
            try
            {
                Variables.Add(GetEmptyKeyValuePair());
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        private void AddValidation()
        {
            try
            {
                if (Validations == null)
                {
                    Validations = new ObservableCollection<BaseValidationRuleViewModel>();
                }
                Validations.Add(GetEmptyValidation());
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        private BaseValidationRuleViewModel GetEmptyValidation()
        {
            BaseValidationRuleViewModel validation = BaseValidationRuleViewModel.GetInstance(ValidationType.Status);
            return validation;
        }

        void validation_RemoveValidation(BaseValidationRuleViewModel validation)
        {
            Validations.Remove(validation);
        }


        private bool CanSaveTestCaseEnabled()
        {
            if (string.IsNullOrEmpty(Title) == false &&
                Validations != null && Validations.Count > 0)
            {
                return true;
            }
            return false;
        }

        private void SaveTestCase()
        {
            try
            {
                if (TestCaseID.GetString() == string.Empty)
                {
                    TestCaseID = AppDataHelper.GetNextTestCaseID();
                }

                AppDataHelper.SaveTestCase(this);
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        private void OnTestCaseSelected()
        {
            try
            {
                MasterEventHandler.RaiseTestCaseSelectedEvent(this);
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        private bool CanExecuteTestEnabled()
        {
            return CanSaveTestCaseEnabled();
        }

        public void ExecuteTest()
        {
            try
            {
                this.TestCaseResult = ResultState.InProgress;
                IsBusy = true;
                foreach (var validation in this.Validations)
                {
                    validation.Result = ResultState.InProgress;
                }
                this.RestClient.SubscribeResposeReceivedEvent(OnResposeReceived);
                this.RestClient.SendButtonClicked();
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        public void OnResposeReceived(RestResponse restresponse)
        {
            this.RestClient.ClearResposeReceivedEvent();
            ShowResult = true;

            foreach (var validation in this.Validations)
            {
                validation.Validate(restresponse);
            }

            if (this.Validations.Any(x => x.Result == ResultState.Fail))
            {
                TestCaseResult = ResultState.Fail;
            }
            else
            {
                TestCaseResult = ResultState.Pass;
            }

            IsBusy = false;
            RaiseResposeReceivedEvent(restresponse);
        }

        private void Remove()
        {
            try
            {
                if (TestCaseRemoved != null)
                {
                    TestCaseRemoved(this);
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
                this.TestCaseID = null;
                this.Title = null;
                this.ShowResult = false;
                this.TestCaseResult = ResultState.NotStarted;
                LoadData();
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        #endregion
    }
}
