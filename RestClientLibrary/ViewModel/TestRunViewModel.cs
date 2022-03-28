// <copyright file="TestRunViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>09-08-2017</date>

namespace RestClientLibrary.ViewModel
{
    using AutoMapper;
    using DataLibrary;
    using Unity;
    using RestClientLibrary.Common;
    using RestClientLibrary.Model;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Xml.Serialization;
	using Newtonsoft.Json;

	/// <summary>
	/// Defines the <see cref="TestRunViewModel" />
	/// </summary>
	public class TestRunViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// The collectionId field
        /// </summary>
        private string collectionId;

        /// <summary>
        /// The collectionName field
        /// </summary>
        private string collectionName;

        /// <summary>
        /// The environment field
        /// </summary>
        private string environment;

        /// <summary>
        /// The environmentId field
        /// </summary>
        private string environmentId;

        /// <summary>
        /// The failedRequests field
        /// </summary>
        private int failedRequests;

        /// <summary>
        /// The interval field
        /// </summary>
        private double interval;

        /// <summary>
        /// The isSelected field
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// Defines the 
        /// </summary>
        private IMapper mapper;

        /// <summary>
        /// The overallResult field
        /// </summary>
        private ResultState overallResult;

        /// <summary>
        /// The passedRequests field
        /// </summary>
        private int passedRequests;

        /// <summary>
        /// The requests field
        /// </summary>
        private ObservableCollection<TestRequestViewModel> requests;

        /// <summary>
        /// The startTime field
        /// </summary>
        private DateTime startTime;

        /// <summary>
        /// The totalRequests field
        /// </summary>
        private int totalRequests;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRunViewModel"/> class.
        /// </summary>
        /// <param name="mapper">The <see cref="IMapper"/></param>
        public TestRunViewModel()
        {
            this.GUID = Guid.NewGuid().ToString();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the CollectionId
        /// </summary>
        public string CollectionId
        {
            get
            {
                return this.collectionId;
            }

            set
            {
                this.collectionId = value;
                this.OnPropertyChanged("CollectionId");
            }
        }

        /// <summary>
        /// Gets or sets the CollectionName
        /// </summary>
        public string CollectionName
        {
            get
            {
                return this.collectionName;
            }

            set
            {
                this.collectionName = value;
                this.OnPropertyChanged("CollectionName");
            }
        }

        /// <summary>
        /// Gets or sets the Environment
        /// </summary>
        public string Environment
        {
            get
            {
                return this.environment;
            }

            set
            {
                this.environment = value;
                this.OnPropertyChanged("Environment");
            }
        }

        /// <summary>
        /// Gets or sets the EnvironmentId
        /// </summary>
        public string EnvironmentId
        {
            get
            {
                return this.environmentId;
            }

            set
            {
                this.environmentId = value;
                this.OnPropertyChanged("EnvironmentId");
            }
        }

        /// <summary>
        /// Gets or sets the FailedRequests
        /// </summary>
        public int FailedRequests
        {
            get
            {
                return this.failedRequests;
            }

            set
            {
                this.failedRequests = value;
                this.OnPropertyChanged("FailedRequests");
            }
        }

        /// <summary>
        /// Gets or sets GUID
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// Gets or sets the Interval
        /// </summary>
        public double Interval
        {
            get
            {
                return this.interval;
            }

            set
            {
                this.interval = value;
                this.OnPropertyChanged("Interval");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsSelected
        /// </summary>
        [JsonIgnore]
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                this.isSelected = value;
                this.OnPropertyChanged("IsSelected");
            }
        }

        /// <summary>
        /// Sets the Mapper
        /// </summary>
        [Dependency]
        [JsonIgnore]
        public IMapper Mapper
        {
            set
            {
                this.mapper = value;
            }
        }

        /// <summary>
        /// Gets or sets the OverallResult
        /// </summary>
        public ResultState OverallResult
        {
            get
            {
                return this.overallResult;
            }

            set
            {
                this.overallResult = value;
                this.OnPropertyChanged("OverallResult");
            }
        }

        /// <summary>
        /// Gets or sets the PassedRequests
        /// </summary>
        public int PassedRequests
        {
            get
            {
                return this.passedRequests;
            }

            set
            {
                this.passedRequests = value;
                this.OnPropertyChanged("PassedRequests");
            }
        }

        /// <summary>
        /// Gets or sets the Requests
        /// </summary>
        public ObservableCollection<TestRequestViewModel> Requests
        {
            get
            {
                return this.requests;
            }

            set
            {
                this.requests = value;
                this.OnPropertyChanged("Requests");
            }
        }

        /// <summary>
        /// Gets or sets the StartTime
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return this.startTime;
            }

            set
            {
                this.startTime = value;
                this.OnPropertyChanged("StartTime");
            }
        }

        /// <summary>
        /// Gets or sets the TotalRequests
        /// </summary>
        public int TotalRequests
        {
            get
            {
                return this.totalRequests;
            }

            set
            {
                this.totalRequests = value;
                this.OnPropertyChanged("TotalRequests");
            }
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The BeginRun
        /// </summary>
        /// <param name="globalData">The <see cref="GlobalVariableModel"/></param>
        /// <param name="category">The <see cref="CategoryViewModel"/></param>
        /// <param name="variables">The <see cref="List{KeyValueModel}"/></param>
        /// <param name="environmentName">The <see cref="string"/></param>
        public void BeginRun(GlobalVariableModel globalData, CategoryViewModel category, string environmentName, string environmentId)
        {
            this.CollectionName = category.Name;
            this.CollectionId = category.Guid;
            this.Environment = environmentName;
            this.EnvironmentId = environmentId;
            this.FailedRequests = 0;
            this.OverallResult = ResultState.InProgress;
            this.PassedRequests = 0;
            this.StartTime = DateTime.Now;
            this.TotalRequests = category.Requests != null ? category.Requests.Count : 0;
            this.BeginSendingRequests(globalData, category.Requests, environment);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The BeginSendingRequests
        /// </summary>
        /// <param name="globalData">The <see cref="GlobalVariableModel"/></param>
        /// <param name="inRequests">The <see cref="ObservableCollection{TransactionViewModel}"/></param>
        /// <param name="variables">The <see cref="List{KeyValueModel}"/></param>
        private async void BeginSendingRequests(GlobalVariableModel globalData, ObservableCollection<TransactionViewModel> inRequests, string environmentName)
        {
            List<TestRequestViewModel> requests = new List<TestRequestViewModel>();
            foreach (var req in inRequests)
            {
                var obj = mapper.Map<TestRequestViewModel>(req);
                await obj.Execute(globalData, environment);

                if (this.Requests == null)
                {
                    this.Requests = new ObservableCollection<TestRequestViewModel>();
                }

                this.Requests.Add(obj);
            }

            this.FailedRequests = this.Requests.Count(x => x.IsValidationSuccessFul == false);
            this.PassedRequests = this.Requests.Count(x => x.IsValidationSuccessFul == true);
            this.OverallResult = this.FailedRequests > 0 ? ResultState.Fail : ResultState.Pass;
            this.Interval = (DateTime.Now - this.StartTime).TotalMilliseconds;
            AppDataHelper.SaveTestRunData(this);
        }

        #endregion

        #endregion
    }
}
