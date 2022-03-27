// <copyright file="ExpandCollapseBehavior.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>23-08-2017</date>

namespace RestClientLibrary.Common
{
    using System;
    using System.Windows;
    using Microsoft.Xaml.Behaviors;
    using AdvanceTextEditor;

    /// <summary>
    /// Defines the <see cref="ExpandCollapseBehavior" />
    /// </summary>
    public class ExpandCollapseBehavior : Behavior<TextEditor>
    {
        #region Fields

        /// <summary>
        /// Defines the CollapseAllFoldingProperty
        /// </summary>
        public static readonly DependencyProperty CollapseAllFoldingProperty =
            DependencyProperty.Register("CollapseAllFolding", typeof(Action), typeof(ExpandCollapseBehavior), new PropertyMetadata(null));

        /// <summary>
        /// Defines the ExpandAllFoldingProperty
        /// </summary>
        public static readonly DependencyProperty ExpandAllFoldingProperty =
            DependencyProperty.Register("ExpandAllFolding", typeof(Action), typeof(ExpandCollapseBehavior), new PropertyMetadata(null));

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the CollapseAllFolding
        /// </summary>
        public Action CollapseAllFolding
        {
            get { return (Action)GetValue(CollapseAllFoldingProperty); }
            set { SetValue(CollapseAllFoldingProperty, value); }
        }

        /// <summary>
        /// Gets or sets the ExpandAllFolding
        /// </summary>
        public Action ExpandAllFolding
        {
            get { return (Action)GetValue(ExpandAllFoldingProperty); }
            set { SetValue(ExpandAllFoldingProperty, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The OnAttached
        /// </summary>
        protected override void OnAttached()
        {
            SetCurrentValue(ExpandAllFoldingProperty, (Action)AssociatedObject.ExpandAllFoldings);
            SetCurrentValue(CollapseAllFoldingProperty, (Action)AssociatedObject.CollapseAllFoldings);
            base.OnAttached();
        }

        #endregion
    }
}
