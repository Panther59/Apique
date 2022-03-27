// <copyright file="TestRequestViewModelProfile.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>10-08-2017</date>

namespace RestClientLibrary.MapperProfiles
{
    using RestClientLibrary.ViewModel;

    /// <summary>
    /// Defines the <see cref="TestRequestViewModelProfile" />
    /// </summary>
    public class TestRequestViewModelProfile : BaseProfile
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRequestViewModelProfile"/> class.
        /// </summary>
        public TestRequestViewModelProfile() : base(nameof(TestRequestViewModelProfile))
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// The CreateMaps
        /// </summary>
        protected override void CreateMaps()
        {
            CreateMap<TransactionViewModel, TestRequestViewModel>();
        }

        #endregion
    }
}
