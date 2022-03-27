// <copyright file="ucSavedCategory.xaml.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>08-09-2017</date>

namespace RestClientLibrary.Screen.SavedData
{
    using DataLibrary;
    using RestClientLibrary.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for ucSavedCategory.xaml
    /// </summary>
    public partial class ucSavedCategory : UserControl
    {
        public ucSavedCategory()
        {
            InitializeComponent();
            this.Loaded += this.UcSavedCategory_Loaded;
            this.DataContextChanged += this.UcSavedCategory_DataContextChanged;
        }

        private void UcSavedCategory_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = this.DataContext as CategoryViewModel;
            this.viewModel = viewModel;
        }

        private void UcSavedCategory_Loaded(object sender, RoutedEventArgs e)
        {
            //Style itemContainerStyle = new Style(typeof(ListViewItem));
            //itemContainerStyle.Setters.Add(new Setter(ListViewItem.AllowDropProperty, true));
            //itemContainerStyle.Setters.Add(new EventSetter(ListViewItem.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(ListRequests_PreviewMouseLeftButtonDown)));
            //itemContainerStyle.Setters.Add(new EventSetter(ListViewItem.DropEvent, new DragEventHandler(ListRequests_Drop)));
            //ListRequests.ItemContainerStyle = itemContainerStyle;
        }

        ListViewItem draggingItem = null;
        private DispatcherTimer timer;
        private CategoryViewModel viewModel;

        private void ListRequests_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem)
            {
                draggingItem = sender as ListViewItem;
                this.timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 1, 0), DispatcherPriority.Background, this.CallBack, this.Dispatcher);
                timer.Start();
            }
        }

        private void CallBack(object sender, EventArgs e)
        {
            timer.Stop();
            timer = null;
            DragDrop.DoDragDrop(draggingItem, draggingItem.DataContext, DragDropEffects.All);
            draggingItem.IsSelected = true;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        private void ListRequests_Drop(object sender, DragEventArgs e)
        {
            try
            {
                TransactionViewModel droppedData = e.Data.GetData(typeof(TransactionViewModel)) as TransactionViewModel;
                TransactionViewModel target = ((ListViewItem)(sender)).DataContext as TransactionViewModel;

                this.viewModel.ReorderRequests(droppedData, target);
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        private void ListViewItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            timer?.Stop();
            timer = null;
            draggingItem = null;
        }
    }
}
