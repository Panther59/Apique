namespace DataLibrary
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Defines the <see cref="BrowserBehavior" />
    /// </summary>
    public static class BrowserBehavior
    {
        /// <summary>
        /// Defines the HtmlProperty
        /// </summary>
        public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached("Html", typeof(string), typeof(BrowserBehavior), new FrameworkPropertyMetadata(OnHtmlChanged));

        /// <summary>
        /// The GetHtml
        /// </summary>
        /// <param name="d">The <see cref="WebBrowser"/></param>
        /// <returns>The <see cref="string"/></returns>
        [AttachedPropertyBrowsableForType(typeof(WebBrowser))]
        public static string GetHtml(WebBrowser d)
        {
            return (string)d.GetValue(HtmlProperty);
        }

        /// <summary>
        /// The SetHtml
        /// </summary>
        /// <param name="d">The <see cref="WebBrowser"/></param>
        /// <param name="value">The <see cref="string"/></param>
        public static void SetHtml(WebBrowser d, string value)
        {
            d.SetValue(HtmlProperty, value);
        }

        /// <summary>
        /// The OnHtmlChanged
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/></param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/></param>
        internal static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser wb = d as WebBrowser;
            if (wb != null)
                wb.NavigateToString(e.NewValue as string);
        }
    }
}
