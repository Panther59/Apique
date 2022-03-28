// <copyright file="ExportDataViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>11-08-2017</date>

namespace RestClientLibrary.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using DataLibrary;
    using DataLibrary.Common;
    using RestClientLibrary.Common;
    using RestClientLibrary.Model;
    using RestClientLibrary.View;

    /// <summary>
    /// Defines the <see cref="ExportDataViewModel" />
    /// </summary>
    public class ExportDataViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// The exportData field
        /// </summary>
        private List<ExportCategoryViewModel> exportData;

        /// <summary>
        /// Defines the 
        /// </summary>
        private ExportDataModel importedData;

        /// <summary>
        /// The isImport field
        /// </summary>
        private bool isImport;

        /// <summary>
        /// Defines the 
        /// </summary>
        private IExportView view;

        #region Commands

        /// <summary>
        /// The closeCommand field
        /// </summary>
        private RelayCommand closeCommand;

        /// <summary>
        /// The exportDataCommand field
        /// </summary>
        private RelayCommand exportDataCommand;

        /// <summary>
        /// The importDataCommand field
        /// </summary>
        private RelayCommand importDataCommand;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportDataViewModel"/> class.
        /// </summary>
        public ExportDataViewModel()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ExportData
        /// </summary>
        public List<ExportCategoryViewModel> ExportData
        {
            get
            {
                return this.exportData;
            }

            set
            {
                this.exportData = value;
                this.OnPropertyChanged("ExportData");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsImport
        /// </summary>
        public bool IsImport
        {
            get
            {
                return this.isImport;
            }

            set
            {
                this.isImport = value;
                this.OnPropertyChanged("IsImport");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsImportSuccessful
        /// </summary>
        public bool IsImportSuccessful
        {
            get; set;
        }

        #region Commands

        /// <summary>
        /// Gets the CloseCommand
        /// </summary>
        public RelayCommand CloseCommand
        {
            get
            {
                if (this.closeCommand == null)
                {
                    this.closeCommand = new RelayCommand(command => this.ExecuteClose());
                }

                return this.closeCommand;
            }
        }

        /// <summary>
        /// Gets the ExportDataCommand
        /// </summary>
        public RelayCommand ExportDataCommand
        {
            get
            {
                if (this.exportDataCommand == null)
                {
                    this.exportDataCommand = new RelayCommand(command => this.ExecuteExportData(), can => this.CanExportDataExecute());
                }

                return this.exportDataCommand;
            }
        }

        /// <summary>
        /// Gets the ImportDataCommand
        /// </summary>
        public RelayCommand ImportDataCommand
        {
            get
            {
                if (this.importDataCommand == null)
                {
                    this.importDataCommand = new RelayCommand(command => this.ExecuteImportData(), can => this.CanImportDataExecute());
                }

                return this.importDataCommand;
            }
        }

        #endregion

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The AttachView
        /// </summary>
        /// <param name="view">The <see cref="IExportView"/></param>
        public void AttachView(IExportView view)
        {
            this.view = view;
        }

        /// <summary>
        /// The ImportData
        /// </summary>
        /// <param name="path">The <see cref="string"/></param>
        public void ImportData(string path)
        {
            this.IsImport = true;

            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader(path);
                string xml = reader.ReadToEnd();
                ExportDataModel importData = JSONHelper.DeserializeFromJson<ExportDataModel>(xml);
                reader.Close();
                reader.Dispose();
                this.importedData = importData;

                var exportData = new List<ExportCategoryViewModel>();
                if (importData.Environments != null && importData.Environments.Count > 0)
                {
                    List<ExportItemViewModel> envItems = new List<ExportItemViewModel>();
                    if (importData.Environments.Any(x => x.Name == "Global"))
                    {
                        envItems.Add(new ExportItemViewModel { Name = "Global", ShouldExport = true });
                    }
                    if (importData.Environments.Any(x => x.Name != "Global"))
                    {
                        foreach (var item in importData.Environments.Where(x => x.Name != "Global"))
                        {
                            envItems.Add(new ExportItemViewModel { Name = item.Name, ShouldExport = true });
                        }
                    }
                    if (envItems.Count > 0)
                    {
                        exportData.Add(new ExportCategoryViewModel() { Name = "Environments", ShouldExport = true, Items = envItems });
                    }
                }

                if (importData.Categories != null && importData.Categories.Count > 0)
                {
                    List<ExportItemViewModel> catItems = importData.Categories.Select(x => new ExportItemViewModel { Name = x.Name, ShouldExport = true }).ToList();
                    exportData.Add(new ExportCategoryViewModel { Name = "Saved Requests", ShouldExport = true, Items = catItems });
                }

                if (importData.TestRuns != null && importData.TestRuns.Count > 0)
                {
                    List<ExportItemViewModel> testRunItem = importData.TestRuns.Select(x => new ExportItemViewModel { Name = string.Format("{0} ({1} - {2})", x.CollectionName, x.StartTime.GetPrettyString(), x.Environment), ShouldExport = true }).ToList();
                    exportData.Add(new ExportCategoryViewModel { Name = "Test Runs", ShouldExport = true, Items = testRunItem });
                }

                if (importData.History != null && importData.History.Count > 0)
                {
                    exportData.Add(new ExportCategoryViewModel { Name = "History", ShouldExport = true, Items = null });
                }

                this.ExportData = exportData;
            }
        }

        /// <summary>
        /// The LoadData
        /// </summary>
        public void LoadData()
        {
            Task.Run(() =>
            {
                var exportData = new List<ExportCategoryViewModel>();
                var environments = AppDataHelper.LoadGlobalData();
                List<ExportItemViewModel> envItems = new List<ExportItemViewModel>();
                if (environments.Variables != null)
                {
                    envItems.Add(new ExportItemViewModel { Name = "Global", ShouldExport = true });
                }
                if (environments.Environments != null)
                {
                    foreach (var item in environments.Environments)
                    {
                        envItems.Add(new ExportItemViewModel { Name = item.Name, ShouldExport = true });
                    }
                }
                if (envItems.Count > 0)
                {
                    exportData.Add(new ExportCategoryViewModel() { Name = "Environments", ShouldExport = true, Items = envItems });
                }

                var savedRequests = AppDataHelper.LoadSavedRequestData();
                if (savedRequests != null && savedRequests.Categories != null && savedRequests.Categories.Count > 0)
                {
                    List<ExportItemViewModel> catItems = savedRequests.Categories.Select(x => new ExportItemViewModel { Name = x.Name, ShouldExport = true }).ToList();
                    exportData.Add(new ExportCategoryViewModel { Name = "Saved Requests", ShouldExport = true, Items = catItems });
                }

                exportData.Add(new ExportCategoryViewModel { Name = "History", ShouldExport = true, Items = null });

                var testRuns = AppDataHelper.LoadTestRunsData();
                if (testRuns != null && testRuns.Count > 0)
                {
                    List<ExportItemViewModel> testRunItem = testRuns.Select(x => new ExportItemViewModel { Name = string.Format("{0} ({1} - {2})", x.CollectionName, x.StartTime.GetPrettyString(), x.Environment), ShouldExport = true }).ToList();
                    exportData.Add(new ExportCategoryViewModel { Name = "Test Runs", ShouldExport = true, Items = testRunItem });
                }

                this.ExportData = exportData;
            });
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Determines whether ExportData can be executed or not
        /// </summary>
        private bool CanExportDataExecute()
        {
            return this.ExportData != null && this.ExportData.Any(x => (x.ShouldExport.HasValue && x.ShouldExport.Value) || x.ShouldExport.HasValue == false);
        }

        /// <summary>
        /// Determines whether ImportData can be executed or not
        /// </summary>
        private bool CanImportDataExecute()
        {
            return this.ExportData != null && this.ExportData.Any(x => (x.ShouldExport.HasValue && x.ShouldExport.Value) || x.ShouldExport.HasValue == false);
        }

        /// <summary>
        /// Executes Close
        /// </summary>
        private void ExecuteClose()
        {
            this.view.CloseParentWindow();
        }

        /// <summary>
        /// Executes ExportData
        /// </summary>
        private void ExecuteExportData()
        {
            string path = this.view.SaveFileDialog();
            if (path != null)
            {
                ExportDataModel exportData = new ExportDataModel();
                var envs = this.ExportData?.FirstOrDefault(x => ((x.ShouldExport.HasValue && x.ShouldExport.Value) || x.ShouldExport.HasValue == false) && x.Name == "Environments")?.Items.Where(x => x.ShouldExport).ToList().Where(x => x.ShouldExport);
                if (envs != null)
                {
                    var isGlobalSelected = envs.Any(x => x.Name == "Global");

                    var environments = AppDataHelper.LoadGlobalData();
                    exportData.Environments = new List<EnvironmentModel>();

                    if (isGlobalSelected)
                    {
                        exportData.Environments.Add(new EnvironmentModel() { Guid = Guid.NewGuid().ToString(), Name = "Global", Variables = environments.Variables });
                    }

                    var environmentsData = environments.Environments?.Where(x => envs.Any(y => y.Name.Equals(x.Name)))?.ToList();
                    exportData.Environments.AddRange(environmentsData);
                }

                var cats = this.ExportData?.FirstOrDefault(x => ((x.ShouldExport.HasValue && x.ShouldExport.Value) || x.ShouldExport.HasValue == false) && x.Name == "Saved Requests")?.Items.Where(x => x.ShouldExport).ToList();
                if (cats != null)
                {
                    var allCats = AppDataHelper.LoadSavedRequestData();
                    var catData = allCats?.Categories?.Where(x => cats.Any(y => y.Name.Equals(x.Name)))?.ToList();

                    if (catData != null)
                    {
                        exportData.Categories = new List<CategoryViewModel>();
                        exportData.Categories.AddRange(catData);
                    }
                }

                var runs = this.ExportData?.FirstOrDefault(x => ((x.ShouldExport.HasValue && x.ShouldExport.Value) || x.ShouldExport.HasValue == false) && x.Name == "Test Runs")?.Items.Where(x => x.ShouldExport).ToList();
                if (runs != null)
                {
                    var allRuns = AppDataHelper.LoadTestRunsData();
                    var runData = allRuns?.Where(x => runs.Any(y => y.Name.Equals(string.Format("{0} ({1} - {2})", x.CollectionName, x.StartTime.GetPrettyString(), x.Environment))))?.ToList();

                    if (runData != null)
                    {
                        exportData.TestRuns = new List<TestRunViewModel>();
                        exportData.TestRuns.AddRange(runData);
                    }
                }

                var historySelected = this.ExportData.Any(x => ((x.ShouldExport.HasValue && x.ShouldExport.Value) || x.ShouldExport.HasValue == false) && x.Name == "History");
                if (historySelected)
                {
                    exportData.History = AppDataHelper.LoadSessionHistoryData();
                }

                string outJson = JSONHelper.SerializeToJson<ExportDataModel>(exportData);
                System.IO.File.WriteAllText(path, outJson);

                this.view.MessageShow("Export Data", "Export was successful");
                this.view.CloseParentWindow();
            }
        }

        /// <summary>
        /// Executes ImportData
        /// </summary>
        private void ExecuteImportData()
        {
            try
            {
                this.ImportSelectedEnvironments();
                this.ImportSelectedSavedRequests();
                this.ImportSelectedTestRuns();
                this.ImportSelectedHistory();

                this.view.MessageShow("Import", "Import is successful");
                this.IsImportSuccessful = true;
                this.view.CloseParentWindow();
            }
            catch (Exception ex)
            {
                this.LogException(ex);
                this.view.MessageShow("Error", ex.Message);
            }
        }

        /// <summary>
        /// The ImportSelectedEnvironments
        /// </summary>
        private void ImportSelectedEnvironments()
        {
            var envs = this.ExportData?.FirstOrDefault(x => ((x.ShouldExport.HasValue && x.ShouldExport.Value) || x.ShouldExport.HasValue == false) && x.Name == "Environments")?.Items.Where(x => x.ShouldExport).ToList();
            var isGlobalSelected = envs != null ? envs.Any(x => x.Name == "Global") : false;
            var environments = AppDataHelper.LoadGlobalData();

            if (isGlobalSelected || (envs != null && envs.Count > 0))
            {
                if (isGlobalSelected)
                {
                    if (environments.Variables == null)
                    {
                        environments.Variables = new List<KeyValueModel>();
                    }
                    var varToAdd = this.importedData.Environments.First(x => x.Name == "Global").Variables;
                    foreach (var varAdd in varToAdd)
                    {
                        if (environments.Variables.Any(x => x.Key == varAdd.Key) == false)
                        {
                            environments.Variables.Add(varAdd);
                        }
                    }
                }

                if (envs != null && envs.Count > 0)
                {
                    if (environments.Environments == null)
                    {
                        environments.Environments = new List<EnvironmentModel>();
                    }

                    var envsToAdd = this.importedData.Environments.Where(x => envs.Where(y => y.Name != "Global").Any(y => y.Name == x.Name));
                    if (envsToAdd != null)
                    {
                        foreach (var item in envsToAdd)
                        {
                            if (environments.Environments.Any(x => x.Name == item.Name))
                            {
                                item.Name += " copy";
                            }

                            environments.Environments.Add(item);
                        }
                    }
                }

                AppDataHelper.SaveGlobalVariablesData(environments);
            }
        }

        /// <summary>
        /// The ImportSelectedHistory
        /// </summary>
        private void ImportSelectedHistory()
        {
            var isHistory = this.ExportData.Any(x => ((x.ShouldExport.HasValue && x.ShouldExport.Value) || x.ShouldExport.HasValue == false) && x.Name == "History");

            if (isHistory)
            {
                var oldHistory = AppDataHelper.LoadSessionHistoryData();
                if (oldHistory == null)
                {
                    oldHistory = new List<SessionHistoryViewModel>();
                }

                oldHistory.AddRange(this.importedData.History);

                AppDataHelper.SaveSessionHistoryData(oldHistory);
            }
        }

        /// <summary>
        /// The ImportSelectedSavedRequests
        /// </summary>
        private void ImportSelectedSavedRequests()
        {
            var savedReqs = this.ExportData?.FirstOrDefault(x => ((x.ShouldExport.HasValue && x.ShouldExport.Value) || x.ShouldExport.HasValue == false) && x.Name == "Saved Requests")?.Items.Where(x => x.ShouldExport).ToList();
            if (savedReqs != null && savedReqs.Count > 0)
            {
                var saved = AppDataHelper.LoadSavedRequestData();
                if (saved.Categories == null)
                {
                    saved.Categories = new System.Collections.ObjectModel.ObservableCollection<CategoryViewModel>();
                }

                var catsToAdd = this.importedData.Categories.Where(x => savedReqs.Any(y => y.Name == x.Name));
                if (catsToAdd != null)
                {
                    foreach (var item in catsToAdd)
                    {
                        if (saved.Categories.Any(x => x.Name == item.Name))
                        {
                            item.Name += " copy";
                        }

                        saved.Categories.Add(item);
                    }
                }

                AppDataHelper.SaveRequestData(saved);
            }
        }

        /// <summary>
        /// The ImportSelectedTestRuns
        /// </summary>
        private void ImportSelectedTestRuns()
        {
            var testRuns = this.ExportData?.FirstOrDefault(x => ((x.ShouldExport.HasValue && x.ShouldExport.Value) || x.ShouldExport.HasValue == false) && x.Name == "Test Runs")?.Items.Where(x => x.ShouldExport).ToList();
            if (testRuns != null && testRuns.Count > 0)
            {
                var originalTestruns = AppDataHelper.LoadTestRunsData();
                if (originalTestruns == null)
                {
                    originalTestruns = new List<TestRunViewModel>();
                }

                var testRunsToAdd = this.importedData.TestRuns.Where(x => testRuns.Any(y => y.Name == string.Format("{0} ({1} - {2})", x.CollectionName, x.StartTime.GetPrettyString(), x.Environment)));
                if (testRunsToAdd != null)
                {
                    foreach (var item in testRunsToAdd)
                    {
                        originalTestruns.Add(item);
                    }
                }

                AppDataHelper.SaveTestRunsData(originalTestruns);
            }
        }

        #endregion

        #endregion
    }
}
