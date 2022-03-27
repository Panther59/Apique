using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataLibrary
{
    /// <summary>
    /// Interaction logic for CircularWait.xaml
    /// </summary>
    public partial class CircularWait : UserControl
    {
        public CircularWait()
        {
            InitializeComponent();
        }

        #region IsBusy

        /// <summary>
        /// IsBusy Dependency Property
        /// </summary>
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(CircularWait),
                new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnIsBusyChanged)));

        /// <summary>
        /// Gets or sets the IsBusy property. This dependency property 
        /// indicates whether the animation should IsBusy.
        /// </summary>
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        /// <summary>
        /// Handles changes to the IsBusy property.
        /// </summary>
        /// <param name="d">CircularWait</param>
        /// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircularWait pBar = (CircularWait)d;
            bool oldIsBusy = (bool)e.OldValue;
            bool newIsBusy = pBar.IsBusy;
            if (newIsBusy)
            {
                pBar.Visibility = Visibility.Visible;
            }
            else
            {
                pBar.Visibility = Visibility.Collapsed;
            }
        }

        #endregion
    }
}
