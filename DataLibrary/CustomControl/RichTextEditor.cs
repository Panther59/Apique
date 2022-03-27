namespace DataLibrary.CustomControl
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;

    public class RichTextEditor : RichTextBox
    {
        static RichTextEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RichTextEditor), new FrameworkPropertyMetadata(typeof(RichTextBox)));
        }

        public static readonly DependencyProperty RichTextProperty = DependencyProperty.RegisterAttached("RichText", typeof(string), typeof(RichTextEditor), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnRichTextChanged));

        public string RichText
        {
            get { return (string)this.GetValue(RichTextProperty); }
            set { this.SetValue(RichTextProperty, value); }
        }

        private static void OnRichTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var txt = d as RichTextEditor;
            if (txt == null)
                return;
            txt.TextChanged -= Txt_TextChanged;

            SetTextContent(txt, e.NewValue != null ? e.NewValue.ToString() : string.Empty);

            txt.TextChanged += Txt_TextChanged;
        }

        private static void Txt_TextChanged(object sender, RoutedEventArgs e)
        {

            RichTextEditor rtf = e.Source as RichTextEditor;
            string richText = new TextRange(rtf.Document.ContentStart, rtf.Document.ContentEnd).Text.TrimEnd();
            rtf.RichText = richText;
        }

        private static void SetTextContent(RichTextEditor rtf, string richText)
        {
            TextRange range = new TextRange(rtf.Document.ContentStart, rtf.CaretPosition);
            int caretIndex = range.Text.Length;

            TextPointer contentStart = rtf.Document.ContentStart;
            int res = 0;
            int j = 0;
            int cursorIndex = contentStart.GetOffsetToPosition(rtf.Document.ContentEnd);

            range = new TextRange(rtf.Document.ContentStart, rtf.Document.ContentEnd);
            range.Text = richText; /// Set content

            /// set caret index
            do
            {
                if (contentStart.GetPositionAtOffset(1, LogicalDirection.Forward) == null)
                {
                    break;
                }
                else if (contentStart.GetPointerContext(LogicalDirection.Backward) == TextPointerContext.Text)
                {
                    res += 1;

                    if (res == caretIndex)
                    {
                        break;
                    }
                }

                contentStart = contentStart.GetPositionAtOffset(1, LogicalDirection.Forward);
                j += 1;
            } while (res < caretIndex);

            rtf.CaretPosition = contentStart;
        }
    }
}
