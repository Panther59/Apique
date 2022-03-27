// <copyright file="AppDataHelper.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>06-08-2017</date>

namespace RestClientLibrary.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using DataLibrary;
    using RestClientLibrary.Model;
    using RestClientLibrary.ViewModel;

    /// <summary>
    /// Defines the <see cref = "AppDataHelper"/>
    /// </summary>
    public static class AppDataHelper
    {
        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref = "AppDataHelper"/> class.
        /// </summary>
        static AppDataHelper()
        {
            string baseDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            OldAppDataDirectory = baseDir + "\\VSPackage\\RestClient";
            AppDataDirectory = baseDir + "\\Ayvan\\Rest Client\\Data";
            SessionHistoryPath = AppDataDirectory + "\\SessionHistory.rsh";
            SettingFilePath = AppDataDirectory + "\\SessionHistory.rst";
            CertificateFilePath = AppDataDirectory + "\\Certifcate.cert";
            SavedRequestsFilePath = AppDataDirectory + "\\SavedRequests.rsr";
            TestClientFilePath = AppDataDirectory + "\\TestClient.tcr";
            GlobalEnvironmentFilePath = AppDataDirectory + "\\GlobalEnvironment.gev";
            TestRunsFilePath = AppDataDirectory + "\\TestRuns.trr";

            MoveDataToNewDirectory(OldAppDataDirectory, AppDataDirectory);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the AppDataDirectory
        /// </summary>
        public static string AppDataDirectory { get; set; }

        /// <summary>
        /// Gets or sets the CertificateFilePath
        /// </summary>
        public static string CertificateFilePath { get; set; }

        /// <summary>
        /// Gets or sets the GlobalEnvironmentFilePath
        /// </summary>
        public static string GlobalEnvironmentFilePath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsWindowStarting
        /// </summary>
        public static bool IsWindowStarting { get; set; }

        /// <summary>
        /// Gets or sets the AppDataDirectory
        /// </summary>
        public static string OldAppDataDirectory { get; set; }

        /// <summary>
        /// Gets or sets the SavedRequestsFilePath
        /// </summary>
        public static string SavedRequestsFilePath { get; set; }

        /// <summary>
        /// Gets or sets the SessionHistoryPath
        /// </summary>
        public static string SessionHistoryPath { get; set; }

        /// <summary>
        /// Gets or sets the SettingFilePath
        /// </summary>
        public static string SettingFilePath { get; set; }

        /// <summary>
        /// Gets or sets the TestClientFilePath
        /// </summary>
        public static string TestClientFilePath { get; set; }

        /// <summary>
        /// Gets or sets the TestRunsFilePath
        /// </summary>
        public static string TestRunsFilePath { get; set; }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T DeepClone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            string xml = XMLHelper.SerializeToXml<T>(source);
            T obj = XMLHelper.DeserializeFromXml<T>(xml);

            return obj;
        }

        /// <summary>
        /// The GetSessionHistoryData
        /// </summary>
        /// <param name = "guid">The <see cref = "string "/></param>
        /// <returns>The <see cref = "SessionHistoryViewModel"/></returns>
        public static SessionHistoryViewModel GetSessionHistoryData(string guid)
        {
            var sessions = LoadSessionHistoryData();
            if (sessions != null)
            {
                return sessions.FirstOrDefault(x => x.Guid == guid);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// The LoadGlobalData
        /// </summary>
        /// <returns>The <see cref = "GlobalVariableModel"/></returns>
        public static GlobalVariableModel LoadGlobalData()
        {
            GlobalVariableModel setting = ReadSettingFile<GlobalVariableModel>(GlobalEnvironmentFilePath);
            return setting;
        }

        /// <summary>
        /// The LoadSavedRequestData
        /// </summary>
        /// <returns>The <see cref = "SaveRequestDataViewModel"/></returns>
        public static SaveRequestDataViewModel LoadSavedRequestData()
        {
            SaveRequestDataViewModel savedReq = ReadSettingFile<SaveRequestDataViewModel>(SavedRequestsFilePath);
            return savedReq;
        }

        /// <summary>
        /// The LoadSessionHistoryData
        /// </summary>
        /// <returns>The <see cref = "List{SessionHistoryViewModel}"/></returns>
        public static List<SessionHistoryViewModel> LoadSessionHistoryData()
        {
            List<SessionHistoryViewModel> sessions = ReadSettingFile<List<SessionHistoryViewModel>>(SessionHistoryPath); ;
            return sessions;
        }

        /// <summary>
        /// The LoadSettingsData
        /// </summary>
        /// <returns>The <see cref = "SettingsViewModel"/></returns>
        public static SettingsViewModel LoadSettingsData()
        {
            SettingsViewModel setting = ReadSettingFile<SettingsViewModel>(SettingFilePath);
            return setting;
        }

        /// <summary>
        /// The LoadTestRunsData
        /// </summary>
        /// <returns>The <see cref="List{TestExecutionResultViewModel}"/></returns>
        public static List<TestRunViewModel> LoadTestRunsData()
        {
            List<TestRunViewModel> setting = ReadSettingFile<List<TestRunViewModel>>(TestRunsFilePath);
            return setting;
        }

        /// <summary>
        /// The RemoveSession
        /// </summary>
        /// <param name = "session">The <see cref = "SessionHistoryViewModel"/></param>
        public static void RemoveSession(SessionHistoryViewModel session)
        {
            var sessions = LoadSessionHistoryData();
            var sessionToBeRemoved = sessions.FirstOrDefault(x => x.Guid == session.Guid);
            if (sessionToBeRemoved != null)
            {
                sessions.Remove(sessionToBeRemoved);
                SaveSessionHistoryData(sessions);
            }
        }

        /// <summary>
        /// The RemoveTestRun
        /// </summary>
        /// <param name="input">The <see cref="TestRunViewModel"/></param>
        public static void RemoveTestRun(TestRunViewModel input)
        {
            var testRuns = LoadTestRunsData();
            var testRunToBeRemoved = testRuns.FirstOrDefault(x => x.GUID == input.GUID);
            if (testRunToBeRemoved != null)
            {
                testRuns.Remove(testRunToBeRemoved);
                SaveTestRunsData(testRuns);
            }
        }

        /// <summary>
        /// The SaveGlobalVariablesData
        /// </summary>
        /// <param name = "data">The <see cref = "GlobalVariableModel"/></param>
        public static async Task SaveGlobalVariablesData(GlobalVariableModel data)
        {
            try
            {
                await WriteSettingFile<GlobalVariableModel>(GlobalEnvironmentFilePath, data);
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        /// <summary>
        /// The SaveRequestData
        /// </summary>
        /// <param name = "categoryViewModel">The <see cref = "CategoryViewModel"/></param>
        public static async Task SaveRequestData(CategoryViewModel categoryViewModel)
        {
            await Task.Run(() =>
            {
                var savedReqData = LoadSavedRequestData();
                if (savedReqData != null)
                {
                    var cat = savedReqData.Categories.FirstOrDefault(x => x.Guid == categoryViewModel.Guid);
                    if (cat != null)
                    {
                        var catInd = savedReqData.Categories.IndexOf(cat);
                        savedReqData.Categories.RemoveAt(catInd);
                        savedReqData.Categories.Insert(catInd, categoryViewModel);
                        SaveRequestData(savedReqData);
                    }
                }
            });
        }

        /// <summary>
        /// The SaveRequestData
        /// </summary>
        /// <param name = "saveRequestData">The <see cref = "SaveRequestDataViewModel"/></param>
        public static void SaveRequestData(SaveRequestDataViewModel saveRequestData)
        {
            WriteSettingFile<SaveRequestDataViewModel>(SavedRequestsFilePath, saveRequestData);
        }

        /// <summary>
        /// The SaveRequestData
        /// </summary>
        /// <param name = "transactionViewModel">The <see cref = "TransactionViewModel"/></param>
        /// <returns>The <see cref = "Task"/></returns>
        public static async Task SaveRequestData(TransactionViewModel transactionViewModel)
        {
            await Task.Run(() =>
            {
                var savedReqData = LoadSavedRequestData();
                if (savedReqData != null)
                {
                    var cat = savedReqData.Categories.FirstOrDefault(x => x.Requests.Any(r => r.Guid == transactionViewModel.Guid));
                    if (cat != null)
                    {
                        var req = cat.Requests.FirstOrDefault(r => r.Guid == transactionViewModel.Guid);
                        if (req != null)
                        {
                            var reqind = cat.Requests.IndexOf(req);
                            cat.Requests.RemoveAt(reqind);
                            cat.Requests.Insert(reqind, transactionViewModel);
                            var catInd = savedReqData.Categories.IndexOf(cat);
                            savedReqData.Categories.RemoveAt(catInd);
                            savedReqData.Categories.Insert(catInd, cat);
                            SaveRequestData(savedReqData);
                        }
                    }
                }
            });
        }

        /// <summary>
        /// The SaveSessionHistoryData
        /// </summary>
        /// <param name = "sessionHistory">The <see cref = "List{SessionHistoryViewModel}"/></param>
        public static void SaveSessionHistoryData(List<SessionHistoryViewModel> sessionHistory)
        {
            WriteSettingFile<List<SessionHistoryViewModel>>(SessionHistoryPath, sessionHistory);
        }

        /// <summary>
        /// The SaveSettingsData
        /// </summary>
        /// <param name = "data">The <see cref = "SettingsViewModel"/></param>
        public static void SaveSettingsData(SettingsViewModel data)
        {
            WriteSettingFile<SettingsViewModel>(SettingFilePath, data);
        }

        /// <summary>
        /// The SaveTestRunData
        /// </summary>
        /// <param name="testRun">The <see cref="TestRunViewModel"/></param>
        public async static void SaveTestRunData(TestRunViewModel testRun)
        {
            await Task.Run(() =>
            {
                var testRuns = LoadTestRunsData();
                if (testRuns != null)
                {
                    var run = testRuns.FirstOrDefault(x => x.GUID == testRun.GUID);
                    if (run != null)
                    {
                        var runInd = testRuns.IndexOf(run);
                        testRuns.RemoveAt(runInd);
                        testRuns.Insert(runInd, testRun);
                    }
                    else
                    {
                        testRuns.Insert(0, testRun);
                    }

                    SaveTestRunsData(testRuns);
                }
            });
        }

        /// <summary>
        /// The SaveTestRunsData
        /// </summary>
        /// <param name="data">The <see cref="List{TestExecutionResultViewModel}"/></param>
        public static void SaveTestRunsData(List<TestRunViewModel> data)
        {
            WriteSettingFile<List<TestRunViewModel>>(TestRunsFilePath, data);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The MoveDataToNewDirectory
        /// </summary>
        /// <param name="sourcePath">The <see cref="string"/></param>
        /// <param name="destinationPath">The <see cref="string"/></param>
        private static void MoveDataToNewDirectory(string sourcePath, string destinationPath)
        {
            if (Directory.Exists(destinationPath) == false)
            {
                Directory.CreateDirectory(destinationPath);

                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*",
                    SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*",
                    SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(sourcePath, destinationPath), true);
            }
        }

        /// <summary>
        /// The ReadSettingFile
        /// </summary>
        /// <param name="path">The <see cref="string"/></param>
        /// <returns>The <see cref="T"/></returns>
        private static T ReadSettingFile<T>(string path)
            where T : new()
        {
            T output = default(T);
            try
            {
                if (File.Exists(path))
                {
                    StreamReader reader = new StreamReader(path);
                    string xml = reader.ReadToEnd();
                    output = XMLHelper.DeserializeFromXml<T>(xml);
                    reader.Close();
                    reader.Dispose();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }

            if (output == null)
            {
                output = new T();
            }

            return output;
        }

        /// <summary>
        /// The WriteSettingFile
        /// </summary>
        /// <param name="path">The <see cref="string"/></param>
        /// <param name="data">The <see cref="T"/></param>
        private static async Task WriteSettingFile<T>(string path, T data)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (!Directory.Exists(AppDataDirectory))
                    {
                        Directory.CreateDirectory(AppDataDirectory);
                    }

                    string outxml = XMLHelper.SerializeToXml<T>(data);
                    StreamWriter writer = System.IO.File.CreateText(path);
                    writer.Write(outxml);
                    writer.Flush();
                    writer.Dispose();
                }
                catch (Exception ex)
                {
                    ErrorLog.LogException(ex);
                }
            });
        }

        #endregion

        #endregion
    }
}
