namespace RestClientLibrary.Model
{
    using System;
    using AdvanceTextEditor.CodeCompletion;
    using AdvanceTextEditor.Document;
    using AdvanceTextEditor.Editing;

    /// <summary>
    /// Implements AvalonEdit ICompletionData interface to provide the entries in the completion drop down.
    /// </summary>
    public class MyCompletionData : ICompletionData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyCompletionData"/> class.
        /// </summary>
        /// <param name="text">The <see cref="string"/></param>
        public MyCompletionData(string text)
        {
            this.Text = text;
        }

        // Use this property if you want to show a fancy UIElement in the drop down list.
        /// <summary>
        /// Gets the Content
        /// </summary>
        public object Content
        {
            get
            {
                return this.Text;
            }
        }

        /// <summary>
        /// Gets the Description
        /// </summary>
        public object Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the Image
        /// </summary>
        public System.Windows.Media.ImageSource Image
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the Priority
        /// </summary>
        public double Priority
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets or sets the Text
        /// </summary>
        public string Text
        {
            get;
            private set;
        }

        /// <summary>
        /// The Complete
        /// </summary>
        /// <param name="textArea">The <see cref="TextArea"/></param>
        /// <param name="completionSegment">The <see cref="ISegment"/></param>
        /// <param name="insertionRequestEventArgs">The <see cref="EventArgs"/></param>
        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            string closingBraces = null;
            if (textArea.Document.TextLength >= completionSegment.EndOffset + 2)
            {
                closingBraces = textArea.Document.GetText(completionSegment.EndOffset, 2);
            }

            var replaceText = this.Text + (closingBraces == "}}" ? "" : "}}");
            textArea.Document.Replace(completionSegment, replaceText);
        }
    }
}
