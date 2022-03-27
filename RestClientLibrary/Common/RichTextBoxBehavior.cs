namespace RestClientLibrary.Common
{
    using System;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using DataLibrary;

    /// <summary>
    /// Defines the <see cref = "RichTextBoxBehavior"/>
    /// </summary>
    public class RichTextBoxBehavior
    {
        /// <summary>
        /// Defines the HighlightTextProperty
        /// </summary>
        public static readonly DependencyProperty HighlightTextProperty = DependencyProperty.RegisterAttached("HighlightText", typeof(bool), typeof(RichTextBoxBehavior), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Defines the RichTextProperty
        /// </summary>
        public static readonly DependencyProperty RichTextProperty = DependencyProperty.RegisterAttached("RichText", typeof(string), typeof(RichTextBoxBehavior), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnRichTextChanged));

        /// <summary>
        /// Defines the isUpdating
        /// </summary>
        internal static bool isUpdating = false;

        /// <summary>
        /// The GetHighlightText
        /// </summary>
        /// <param name="dp">The <see cref="DependencyObject"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool GetHighlightText(DependencyObject dp)
        {
            return (bool)dp.GetValue(HighlightTextProperty);
        }

        /// <summary>
        /// The SetHighlightText
        /// </summary>
        /// <param name="dp">The <see cref="DependencyObject"/></param>
        /// <param name="value">The <see cref="bool"/></param>
        public static void SetHighlightText(DependencyObject dp, bool value)
        {
            dp.SetValue(HighlightTextProperty, value);
        }

        /// <summary>
        /// The GetRichText
        /// </summary>
        /// <param name="dp">The <see cref="DependencyObject"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static string GetRichText(DependencyObject dp)
        {
            return (string)dp.GetValue(RichTextProperty);
        }

        /// <summary>
        /// The SetRichText
        /// </summary>
        /// <param name="dp">The <see cref="DependencyObject"/></param>
        /// <param name="value">The <see cref="string"/></param>
        public static void SetRichText(DependencyObject dp, string value)
        {
            dp.SetValue(RichTextProperty, value);
        }

        /// <summary>
        /// The OnRichTextChanged
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/></param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/></param>
        private static void OnRichTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (isUpdating)
            {
                return;
            }

            var txt = d as RichTextBox;
            if (txt == null)
                return;
            txt.TextChanged -= Txt_TextChanged;
            UpdateRtf(txt, e.NewValue != null ? e.NewValue.ToString() : string.Empty);
            txt.TextChanged += Txt_TextChanged;
        }
        
        /// <summary>
        /// The Txt_TextChanged
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="RoutedEventArgs"/></param>
        private static void Txt_TextChanged(object sender, RoutedEventArgs e)
       {
            if (isUpdating)
            {
                return;
            }

            RichTextBox rtf = e.Source as RichTextBox;
            var oldVal = GetRichText(rtf);
            string richText = new TextRange(rtf.Document.ContentStart, rtf.Document.ContentEnd).Text.TrimEnd();
            //UpdateRtf(rtf, richText);
            SetRichText(rtf, richText);
            oldVal = GetRichText(rtf);
        }

        /// <summary>
        /// The UpdateRtf
        /// </summary>
        /// <param name="rtf">The <see cref="RichTextBox"/></param>
        /// <param name="richText">The <see cref="string"/></param>
        private static void UpdateRtf(RichTextBox rtf, string richText)
        {
            if (isUpdating)
            {
                return;
            }

            bool highLight = GetHighlightText(rtf);
            isUpdating = true;
            try
            {
                int index = 0;
                if (rtf.IsReadOnly == false)
                {
                    TextRange range = new TextRange(rtf.Document.ContentStart, rtf.CaretPosition);
                    index = range.Text.Length;
                }

                string regexPattern = @"\{{([^}]*)\}}";
                var result = Regex.Matches(richText, regexPattern);
                TextRange tr = new TextRange(rtf.Document.ContentStart, rtf.Document.ContentEnd);
                tr.Text = "";
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, rtf.Foreground);
                int lastIndex = 0;
                foreach (Match item in result)
                {
                    tr = new TextRange(rtf.Document.ContentEnd, rtf.Document.ContentEnd);
                    tr.Text = richText.Substring(lastIndex, item.Index - lastIndex);
                    tr.ApplyPropertyValue(TextElement.ForegroundProperty, rtf.Foreground);
                    tr = new TextRange(rtf.Document.ContentEnd, rtf.Document.ContentEnd);
                    tr.Text = richText.Substring(item.Index, item.Length);
                    tr.ApplyPropertyValue(TextElement.ForegroundProperty, highLight ? System.Windows.Media.Brushes.Orange : rtf.Foreground);
                    lastIndex = item.Index + item.Length;
                }

                if (lastIndex < richText.Length)
                {
                    tr = new TextRange(rtf.Document.ContentEnd, rtf.Document.ContentEnd);
                    tr.Text = richText.Substring(lastIndex, richText.Length - lastIndex);
                    tr.ApplyPropertyValue(TextElement.ForegroundProperty, rtf.Foreground);
                }

                if (rtf.IsReadOnly == false)
                {
                    TextPointer contentStart = rtf.Document.ContentStart;
                    int res = 0;
                    int j = 0;
                    int cursorIndex = contentStart.GetOffsetToPosition(rtf.Document.ContentEnd);


                    do
                    {
                        if (contentStart.GetPositionAtOffset(1, LogicalDirection.Forward) == null)
                        {
                            break;
                        }
                        else if (contentStart.GetPointerContext(LogicalDirection.Backward) == TextPointerContext.Text)
                        {
                            res += 1;

                            if (res == index)
                            {
                                break;
                            }
                        }

                        contentStart = contentStart.GetPositionAtOffset(1, LogicalDirection.Forward);
                        j += 1;
                    } while (res < index);


                    rtf.CaretPosition = contentStart;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
            finally
            {
                isUpdating = false;
            }
        }
    }
}
