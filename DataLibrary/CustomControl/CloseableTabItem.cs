// <copyright file="CloseableTabItem.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>14-08-2017</date>

namespace DataLibrary.CustomControl
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;


    /// <summary>
    /// ========================================
    /// .NET Framework 3.0 Custom Control
    /// ========================================
    ///
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CloseableTabItemDemo"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CloseableTabItemDemo;assembly=CloseableTabItemDemo"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file. Note that Intellisense in the
    /// XML editor does not currently work on custom controls and its child elements.
    ///
    ///     <MyNamespace:CloseableTabItem/>
    ///
    /// </summary>
    public class CloseableTabItem : TabItem, IDisposable
    {
        public string GUID { get; set; }

        /// <summary>
        /// The closeClickCommand field
        /// </summary>
        private RelayCommand closeClickCommand;

        /// <summary>
        /// Gets the CloseClickCommand
        /// </summary>
        public RelayCommand CloseClickCommand
        {
            get
            {
                if (this.closeClickCommand == null)
                {
                    this.closeClickCommand = new RelayCommand(command => this.ExecuteCloseClick(), can => this.CanCloseClickExecute());
                }

                return this.closeClickCommand;
            }
        }

        /// <summary>
        /// Executes CloseClick
        /// </summary>
        private void ExecuteCloseClick()
        {
            this.CloseTabCommand?.Execute(this.DataContext);
        }

        /// <summary>
        /// Determines whether CloseClick can be executed or not
        /// </summary>
        private bool CanCloseClickExecute()
        {
            try
            {
                return this.CloseTabCommand != null ? this.CloseTabCommand.CanExecute(this.DataContext) : true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
                return true;
            }
        }


        static CloseableTabItem()
        {
            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CloseableTabItem),
                new FrameworkPropertyMetadata(typeof(CloseableTabItem)));
        }

        public CloseableTabItem()
        {
            GUID = Guid.NewGuid().ToString();
        }

        public static DependencyProperty CloseTabCommandProperty
        = DependencyProperty.Register(
            "CloseTabCommand",
            typeof(ICommand),
            typeof(CloseableTabItem));

        public ICommand CloseTabCommand
        {
            get
            {
                return (ICommand)GetValue(CloseTabCommandProperty);
            }

            set
            {
                SetValue(CloseTabCommandProperty, value);
            }
        }

        public static readonly RoutedEvent CloseTabEvent =
            EventManager.RegisterRoutedEvent("CloseTab", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(CloseableTabItem));

        public event RoutedEventHandler CloseTab
        {
            add { AddHandler(CloseTabEvent, value); }
            remove { RemoveHandler(CloseTabEvent, value); }
        }

        protected override void OnContextMenuOpening(ContextMenuEventArgs e)
        {
            base.OnContextMenuOpening(e);
            this.IsSelected = true;
        }

        ScrollViewer _scrollViewer = null;
        protected override void OnSelected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            if (_scrollViewer == null)
            {
                _scrollViewer = FindParentScrollViewer(this);
            }

            if (_scrollViewer != null)
            {
                GeneralTransform gt = this.TransformToVisual(_scrollViewer);
                Point offset = gt.Transform(new Point(0, 0));
                Double controlLeft = offset.X;

                if (controlLeft < 0 || controlLeft + this.ActualWidth > _scrollViewer.ViewportWidth)
                {
                    double newOffset = controlLeft + _scrollViewer.HorizontalOffset;
                    if (newOffset > (_scrollViewer.ViewportWidth / 2))
                    {
                        newOffset -= (_scrollViewer.ViewportWidth / 2);
                    }
                    else
                    {
                        newOffset = 0;
                    }

                    if (_scrollViewer.HorizontalOffset != newOffset)
                    {
                        _scrollViewer.ScrollToHorizontalOffset(newOffset);
                    }
                }

            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                CloseThisTab();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Button closeButton = base.GetTemplateChild("PART_Close") as Button;
            if (closeButton != null)
                closeButton.Click += new System.Windows.RoutedEventHandler(closeButton_Click);

            if (this.CloseTabCommand != null)
            {
                closeButton.Command = this.CloseClickCommand;
            }
        }

        private void Command_CanExecuteChanged(object sender, EventArgs e)
        {
            this.CloseTabCommand.CanExecute(this.DataContext);
        }

        void _closeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            CloseThisTab();
        }


        void closeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CloseThisTab();
        }
        
        public void CloseThisTab()
        {
            this.RaiseEvent(new RoutedEventArgs(CloseTabEvent, this));

        }
        
        private ScrollViewer FindParentScrollViewer(DependencyObject element)
        {
            if (element == null)
            {
                return null;
            }

            while (element != null)
            {
                ScrollViewer parent = element as ScrollViewer;
                if (parent != null)
                {
                    return parent;
                }
                element = VisualTreeHelper.GetParent(element);
            }

            return null;
        } // FindParentColumn

        ~CloseableTabItem()
        {
        }

        public void Dispose()
        {
            this.DataContext = null;
            this.Content = null;

            Button closeButton = base.GetTemplateChild("PART_Close") as Button;
            if (closeButton != null)
                closeButton.Click -= new System.Windows.RoutedEventHandler(closeButton_Click);

            if (this.CloseTabCommand != null && closeButton.Command != null)
            {
                closeButton.Command.CanExecuteChanged -= this.Command_CanExecuteChanged;
            }
        }
    }
}
