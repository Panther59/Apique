// <copyright file="ExtTabControl.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>14-08-2017</date>

namespace DataLibrary.CustomControl
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:NotepadControlLibrary.CustomControl"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:NotepadControlLibrary.CustomControl;assembly=NotepadControlLibrary.CustomControl"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:ExtTabControl/>
    ///
    /// </summary>
    public class ExtTabControl : TabControl
    {
        #region Fields
        
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtTabControl"/> class.
        /// </summary>
        public ExtTabControl()
        {
            this.Loaded += new RoutedEventHandler(ExtTabControl_Loaded);
            TabHistory = new List<TabItem>();
            this.SelectionChanged += new SelectionChangedEventHandler(ExtTabControl_SelectionChanged);
        }

        /// <summary>
        /// Initializes static members of the <see cref="ExtTabControl"/> class.
        /// </summary>
        static ExtTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtTabControl), new FrameworkPropertyMetadata(typeof(TabControl)));
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// Gets or sets the TabHistory
        /// </summary>
        public List<TabItem> TabHistory
        {
            get; set;
        }

        #endregion

        #region Methods

        #region Public Methods
                
        /// <summary>
        /// The RemoveItem
        /// </summary>
        /// <param name="closeableTabItem">The <see cref="TabItem"/></param>
        public void RemoveItem(TabItem closeableTabItem)
        {
            Items.Remove(closeableTabItem);
            TabHistory.Remove(closeableTabItem);

            Control uc = closeableTabItem.Content as Control;
            if (uc != null)
            {
                uc = null;
            }
            closeableTabItem.Content = null;
            var vm = closeableTabItem.DataContext;
            if (vm != null)
            {
                vm = null;
            }
            closeableTabItem.DataContext = null;
            closeableTabItem = null;
        }

        #endregion
        
        /// <summary>
        /// The ExtTabControl_Loaded
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="RoutedEventArgs"/></param>
        internal void ExtTabControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// The ExtTabControl_SelectionChanged
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/></param>
        internal void ExtTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedItem != null)
            {
                var tabs = TabHistory.Where(x => x != null && x.Tag == (SelectedItem as TabItem).Tag);
                if (tabs.Any())
                {
                    TabHistory.Remove(tabs.First());
                }

                TabHistory.Insert(0, SelectedItem as TabItem);
            }
        }

        #endregion
    }
}
