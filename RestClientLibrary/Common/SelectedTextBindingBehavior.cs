namespace RestClientLibrary.Common
{
    using System.Windows;
    using System.Windows.Controls;
    using Microsoft.Xaml.Behaviors;

    /// <summary>
    /// Defines the <see cref="SelectionBindingBehavior" />
    /// </summary>
    public static class SelectionBindingBehavior
    {
        public static readonly DependencyProperty SelectionProperty =
           DependencyProperty.RegisterAttached("Selection", typeof(string), typeof(SelectionBindingBehavior), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedTextChanged));
        
        public static void SetSelection(DependencyObject dp, string value)
        {
            dp.SetValue(SelectionProperty, value);
        }

        public static bool GetSelection(DependencyObject dp)
        {
            return (bool)dp.GetValue(SelectionProperty);
        }

        private static void OnSelectedTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var txt = d as TextBox;
            if (txt == null)
                return;

            txt.SelectionChanged -= Txt_SelectionChanged;

            txt.SelectedText = e.NewValue != null ?e.NewValue.ToString() : string.Empty;
                
            txt.SelectionChanged += Txt_SelectionChanged;
        }

        private static void Txt_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            SetSelection(txt, txt.SelectedText);
        }
    }
}
