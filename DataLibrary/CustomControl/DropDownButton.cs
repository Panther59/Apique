// <copyright file="DropDownButton.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>30-07-2017</date>

namespace DataLibrary
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;

    /// <summary>
    /// Defines the <see cref = "DropDownButton"/>
    /// </summary>
    public class DropDownButton : ToggleButton
    {
        #region Fields

        /// <summary>
        /// Defines the MenuProperty
        /// </summary>
        public static readonly DependencyProperty MenuProperty = DependencyProperty.Register("Menu", typeof(ContextMenu), typeof(DropDownButton), new UIPropertyMetadata(null, OnMenuChanged));

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref = "DropDownButton"/> class.
        /// </summary>
        public DropDownButton()
        {
            // Bind the ToogleButton.IsChecked property to the drop-down's IsOpen property
            Binding binding = new Binding("Menu.IsOpen");
            binding.Source = this;
            this.SetBinding(IsCheckedProperty, binding);
            DataContextChanged += (sender, args) =>
            {
                if (Menu != null)
                    Menu.DataContext = DataContext;
            }

            ;
        }

        #endregion

        #region Properties

        // *** Properties ***
        /// <summary>
        /// Gets or sets the Menu
        /// </summary>
        public ContextMenu Menu
        {
            get
            {
                return (ContextMenu)GetValue(MenuProperty);
            }

            set
            {
                SetValue(MenuProperty, value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The OnClick
        /// </summary>
        protected override void OnClick()
        {
            if (Menu != null)
            {
                Menu.PlacementTarget = this;
                Menu.Placement = PlacementMode.Bottom;
                Menu.IsOpen = true;
            }
        }

        #region Private Methods

        /// <summary>
        /// The OnMenuChanged
        /// </summary>
        /// <param name = "d">The <see cref = "DependencyObject"/></param>
        /// <param name = "e">The <see cref = "DependencyPropertyChangedEventArgs"/></param>
        private static void OnMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dropDownButton = (DropDownButton)d;
            var contextMenu = (ContextMenu)e.NewValue;
            if (contextMenu != null)
            {
                contextMenu.DataContext = dropDownButton.DataContext;
            }
        }

        #endregion

        #endregion
    }
}