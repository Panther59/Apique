namespace DataLibrary
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Defines the <see cref="TextBoxVisibilityBehavior" />
    /// </summary>
    public static class TextBoxVisibilityBehavior
    {
        /// <summary>
        /// Defines the HtmlProperty
        /// </summary>
        public static readonly DependencyProperty TrackCaretIndexProperty = DependencyProperty.RegisterAttached("TrackCaretIndex", typeof(bool), typeof(TextBoxVisibilityBehavior), new FrameworkPropertyMetadata(OnTrackCaretIndexChanged));

        /// <summary>
        /// Defines the HtmlProperty
        /// </summary>
        public static readonly DependencyProperty SetFocusProperty = DependencyProperty.RegisterAttached("SetFocus", typeof(bool), typeof(TextBoxVisibilityBehavior), new FrameworkPropertyMetadata(OnTrackCaretIndexChanged));

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static bool GetTrackCaretIndex(TextBox d)
        {
            return (bool)d.GetValue(TrackCaretIndexProperty);
        }

        public static void SetTrackCaretIndex(TextBox d, bool value)
        {
            d.SetValue(TrackCaretIndexProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static bool GetSetFocus(TextBox d)
        {
            return (bool)d.GetValue(TrackCaretIndexProperty);
        }

        public static void SetSetFocus(TextBox d, bool value)
        {
            d.SetValue(TrackCaretIndexProperty, value);
        }

        private static void OnTrackCaretIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = d as TextBox;
            if (tb != null)
            {
                tb.IsVisibleChanged -= Tb_IsVisibleChanged;
                tb.IsVisibleChanged += Tb_IsVisibleChanged;
            }
        }

        private static void OnSetFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = d as TextBox;
            if (tb != null)
            {
                tb.IsVisibleChanged -= Tb_IsVisibleChanged;
                tb.IsVisibleChanged += Tb_IsVisibleChanged;
            }
        }

        private static void Tb_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                if (GetSetFocus(tb))
                {
                    tb.Focus();
                }

                if (GetTrackCaretIndex(tb))
                {
                    tb.CaretIndex = tb.Text.Length;
                }
            }
        }
    }
}
