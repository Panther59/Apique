// <copyright file="WindowServiceHandler.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>07-09-2017</date>

namespace RestClientLibrary.Common
{
    /// <summary>
    /// Defines the <see cref="WindowServiceHandler" />
    /// </summary>
    public static class WindowServiceHandler
    {
        #region Delegates

        /// <summary>
        /// The OnConfirmationBoxHandler
        /// </summary>
        /// <param name="title">The <see cref="string"/></param>
        /// <param name="message">The <see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public delegate bool OnConfirmationBoxHandler(string title, string message);

        /// <summary>
        /// The OnMessageBoxHandler
        /// </summary>
        /// <param name="title">The <see cref="string"/></param>
        /// <param name="message">The <see cref="string"/></param>
        public delegate void OnMessageBoxHandler(string title, string message);

        #endregion

        #region Events

        /// <summary>
        /// Defines the ConfirmationBox
        /// </summary>
        public static event OnConfirmationBoxHandler ConfirmationBox;

        /// <summary>
        /// Defines the MessageBox
        /// </summary>
        public static event OnMessageBoxHandler MessageBox;

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The RaiseConfirmationBoxEvent
        /// </summary>
        /// <param name="title">The <see cref="string"/></param>
        /// <param name="message">The <see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool RaiseConfirmationBoxEvent(string title, string message)
        {
            if (ConfirmationBox != null)
            {
                return ConfirmationBox(title, message);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The RaiseMessageBoxEvent
        /// </summary>
        /// <param name="title">The <see cref="string"/></param>
        /// <param name="message">The <see cref="string"/></param>
        public static void RaiseMessageBoxEvent(string title, string message)
        {
            MessageBox?.Invoke(title, message);
        }

        #endregion

        #endregion
    }
}
