// <copyright file="ErrorLog.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>09-08-2017</date>

namespace DataLibrary
{
    using System;
    using System.IO;
    using System.Net;
    using System.Reflection;

    /// <summary>
    /// Defines the <see cref="ApplicationAttributes" />
    /// </summary>
    public class ApplicationAttributes
    {
        #region Fields

        /// <summary>
        /// Defines the 
        /// </summary>
        internal readonly Assembly _Assembly = null;

        /// <summary>
        /// Defines the 
        /// </summary>
        internal readonly AssemblyCompanyAttribute _Company = null;

        /// <summary>
        /// Defines the 
        /// </summary>
        internal readonly AssemblyCopyrightAttribute _Copyright = null;

        /// <summary>
        /// Defines the 
        /// </summary>
        internal readonly AssemblyProductAttribute _Product = null;

        /// <summary>
        /// Defines the 
        /// </summary>
        internal readonly AssemblyTitleAttribute _Title = null;

        /// <summary>
        /// Defines the 
        /// </summary>
        internal Version _Version = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationAttributes"/> class.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/></param>
        public ApplicationAttributes(Assembly assembly = null)
        {
            try
            {
                _Assembly = assembly;
                Title = String.Empty;
                CompanyName = String.Empty;
                Copyright = String.Empty;
                ProductName = String.Empty;
                Version = String.Empty;

                if (_Assembly == null)
                {
                    _Assembly = Assembly.GetEntryAssembly();
                }

                if (_Assembly != null)
                {
                    object[] attributes = _Assembly.GetCustomAttributes(false);

                    foreach (object attribute in attributes)
                    {
                        Type type = attribute.GetType();

                        if (type == typeof(AssemblyTitleAttribute)) _Title = (AssemblyTitleAttribute)attribute;
                        if (type == typeof(AssemblyCompanyAttribute)) _Company = (AssemblyCompanyAttribute)attribute;
                        if (type == typeof(AssemblyCopyrightAttribute)) _Copyright = (AssemblyCopyrightAttribute)attribute;
                        if (type == typeof(AssemblyProductAttribute)) _Product = (AssemblyProductAttribute)attribute;
                    }

                    _Version = _Assembly.GetName().Version;
                }

                if (_Title != null) Title = _Title.Title;
                if (_Company != null) CompanyName = _Company.Company;
                if (_Copyright != null) Copyright = _Copyright.Copyright;
                if (_Product != null) ProductName = _Product.Product;
                if (_Version != null) Version = _Version.ToString();
            }
            catch { }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the CompanyName
        /// </summary>
        public string CompanyName
        {
            get; private set;
        }

        /// <summary>
        /// Gets or sets the Copyright
        /// </summary>
        public string Copyright
        {
            get; private set;
        }

        /// <summary>
        /// Gets or sets the ProductName
        /// </summary>
        public string ProductName
        {
            get; private set;
        }

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title
        {
            get; private set;
        }

        /// <summary>
        /// Gets or sets the Version
        /// </summary>
        public string Version
        {
            get; private set;
        }

        #endregion
    }

    /// <summary>
    /// Logger is used for creating a customized error log files or an error can be registered as
    /// a log entry in the Windows Event Log on the administrator's machine.
    /// </summary>
    public static class ErrorLog
    {
        #region Fields

        /// <summary>
        /// Defines the 
        /// </summary>
        public static ApplicationAttributes applicationDetail;

        /// <summary>
        /// Defines the 
        /// </summary>
        private static string strLogFilePath = string.Empty;

        /// <summary>
        /// Defines the 
        /// </summary>
        private static StreamWriter sw = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="ErrorLog"/> class.
        /// </summary>
        static ErrorLog()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ApplicationDetail
        /// </summary>
        public static ApplicationAttributes ApplicationDetail
        {
            get

            {
                if (applicationDetail == null)
                {
                    applicationDetail = new ApplicationAttributes();
                }
                return applicationDetail;
            }
            set
            {
                applicationDetail = value;
            }
        }

        /// <summary>
        /// Setting LogFile path. If the logfile path is null then it will update error info into LogFile.txt under
        /// application directory.
        /// </summary>
        private static string LogFilePath
        {
            set
            {
                strLogFilePath = value;
            }
            get
            {
                return strLogFilePath;
            }
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// Write error log entry for window event if the bLogType is true. Otherwise, write the log entry to
        /// customized text-based text file
        /// </summary>
        /// <param name="bLogType"></param>
        /// <param name="objException"></param>
        /// <returns>false if the problem persists</returns>
        public static bool LogException(Exception objException)
        {
            try
            {
                if (true != CustomErrorRoutine(objException))
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Create a directory if not exists
        /// </summary>
        /// <param name="strLogPath"></param>
        /// <returns></returns>
        private static bool CheckDirectory(string strLogPath)
        {
            try
            {
                int nFindSlashPos = strLogPath.Trim().LastIndexOf("\\");
                string strDirectoryname = strLogPath.Trim().Substring(0, nFindSlashPos);

                if (false == Directory.Exists(strDirectoryname))
                    Directory.CreateDirectory(strDirectoryname);

                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        /// <summary>
        /// If the LogFile path is empty then, it will write the log entry to LogFile.txt under application directory.
        /// If the LogFile.txt is not availble it will create it
        /// If the Log File path is not empty but the file is not availble it will create it.
        /// </summary>
        /// <param name="objException"></param>
        /// <returns>false if the problem persists</returns>
        private static bool CustomErrorRoutine(Exception objException)
        {
            string strPathName = string.Empty;
            if (strLogFilePath.Equals(string.Empty))
            {
                //Get Default log file path "LogFile.txt"
                strPathName = GetLogFilePath();
            }
            else
            {

                //If the log file path is not empty but the file is not available it will create it
                if (false == File.Exists(strLogFilePath))
                {
                    if (false == CheckDirectory(strLogFilePath))
                        return false;

                    FileStream fs = new FileStream(strLogFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fs.Close();
                }
                strPathName = strLogFilePath;

            }

            bool bReturn = true;
            // write the error log to that text file
            if (true != WriteErrorLog(strPathName, objException))
            {
                bReturn = false;
            }
            return bReturn;
        }

        /// <summary>
        /// The GetApplicationPath
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        private static string GetApplicationPath()
        {
            try
            {
                string strBaseDirectory = AppDomain.CurrentDomain.BaseDirectory.ToString();
                int nFirstSlashPos = strBaseDirectory.LastIndexOf("\\");
                string strTemp = string.Empty;

                if (0 < nFirstSlashPos)
                    strTemp = strBaseDirectory.Substring(0, nFirstSlashPos);

                int nSecondSlashPos = strTemp.LastIndexOf("\\");
                string strTempAppPath = string.Empty;
                if (0 < nSecondSlashPos)
                    strTempAppPath = strTemp.Substring(0, nSecondSlashPos);

                string strAppPath = strTempAppPath.Replace("bin", "");
                return strAppPath;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Check the log file in applcation directory. If it is not available, creae it
        /// </summary>
        /// <returns>Log file path</returns>
        private static string GetLogFilePath()
        {
            try
            {
                // get the base directory
                string baseDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" +
                        ApplicationDetail.ProductName + "\\";

                // search the file below the current directory
                string retFilePath = baseDir + "LogFile.txt";

                // if exists, return the path
                if (File.Exists(retFilePath) == true)
                    return retFilePath;
                //create a text file
                else
                {
                    if (false == CheckDirectory(retFilePath))
                        return string.Empty;

                    FileStream fs = new FileStream(retFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fs.Close();
                }

                return retFilePath;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Write Source,method,date,time,computer,error and stack trace information to the text file
        /// </summary>
        /// <param name="strPathName"></param>
        /// <param name="objException"></param>
        /// <returns>false if the problem persists</returns>
        private static bool WriteErrorLog(string strPathName, Exception objException)
        {
            bool bReturn = false;
            string strException = string.Empty;
            try
            {
                sw = new StreamWriter(strPathName, true);
                sw.WriteLine("Source		: " + objException.Source.ToString().Trim());
                sw.WriteLine("Method		: " + objException.TargetSite.Name.ToString());
                sw.WriteLine("Date		: " + DateTime.Now.ToLongTimeString());
                sw.WriteLine("Time		: " + DateTime.Now.ToShortDateString());
                sw.WriteLine("Computer	: " + Dns.GetHostName().ToString());
                sw.WriteLine("Error		: " + objException.Message.ToString().Trim());
                sw.WriteLine("Stack Trace	: " + objException.StackTrace.ToString().Trim());
                sw.WriteLine("^^-------------------------------------------------------------------^^");
                sw.Flush();
                sw.Close();
                bReturn = true;
            }
            catch (Exception)
            {
                bReturn = false;
            }
            return bReturn;
        }

        #endregion

        #endregion
    }
}

