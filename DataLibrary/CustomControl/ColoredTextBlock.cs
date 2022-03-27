namespace DataLibrary.CustomControl
{
    using System;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:DataLibrary.CustomControl"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:DataLibrary.CustomControl;assembly=DataLibrary.CustomControl"
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
    ///     <MyNamespace: ColoredTextBlock/>
    ///
    /// </summary>
    public class ColoredTextBlock : TextBlock
    {
        /// <summary>
        /// Initializes static members of the <see cref="ColoredTextBlock"/> class.
        /// </summary>
        static ColoredTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColoredTextBlock), new FrameworkPropertyMetadata(typeof(TextBlock)));
        }

        public ColoredTextBlock()
        {
            this.Loaded += this.ColoredTextBlock_Loaded;
        }

        private void ColoredTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.updating)
            {
                return;
            }

            try
            {
                this.updating = true;
                this.Inlines.Clear();

                var text = this.ColoredText != null ? this.ColoredText.ToString() : string.Empty;

                string regexPattern = @"\{{([^}]*)\}}";
                var result = Regex.Matches(text, regexPattern);
                int lastIndex = 0;
                foreach (Match item in result)
                {
                    this.Inlines.Add(new Run(text.Substring(lastIndex, item.Index - lastIndex)));
                    this.Inlines.Add(new Run(text.Substring(item.Index, item.Length)) { Foreground = this.ColoredTextBrush });
                    lastIndex = item.Index + item.Length;
                }

                if (lastIndex < text.Length)
                {
                    this.Inlines.Add(new Run(text.Substring(lastIndex, text.Length - lastIndex)));
                }

                //this.SetValue(TextProperty, text);
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
            finally
            {
                this.updating = false;
            }
        }

        #region ColorTextRegex

        public static readonly DependencyProperty ColorTextRegexProperty =
           DependencyProperty.Register("ColorTextRegex", typeof(string), typeof(ColoredTextBlock));

        public string ColorTextRegex
        {
            get { return (string)GetValue(ColorTextRegexProperty); }
            set { SetValue(ColorTextRegexProperty, value); }
        }

        #endregion

        #region ColoredTextBrush

        public static readonly DependencyProperty ColoredTextBrushProperty =
           DependencyProperty.Register("ColoredTextBrush", typeof(Brush), typeof(ColoredTextBlock));
        private bool updating;

        public Brush ColoredTextBrush
        {
            get { return (Brush)GetValue(ColoredTextBrushProperty); }
            set { SetValue(ColoredTextBrushProperty, value); }
        }

        #endregion

        #region ColoredText

        public static readonly DependencyProperty ColoredTextProperty =
           DependencyProperty.Register("ColoredText", typeof(string), typeof(ColoredTextBlock));

        public string ColoredText
        {
            get { return (string)GetValue(ColoredTextProperty); }
            set { SetValue(ColoredTextProperty, value); }
        }

        #endregion
    }
}
