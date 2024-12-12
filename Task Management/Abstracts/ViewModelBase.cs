using System;
using System.Diagnostics;
using System.Windows;

namespace Task_Management.Abstracts
{
    /// <summary>
    /// Contains the iNotifyPropertyChanged, Shared commands and properties, and Dialog functions
    /// </summary>
    class ViewModelBase : RaisePropertyChanged
    {
        #region Properties

        #region Version
        /// <summary>
        /// Set the application name
        /// </summary>
        public string ApplicationName { get { return "Task_Management 2024"; } }

        /// <summary>
        /// Set the application version here.
        /// </summary>
        public string Title
        {
            get
            {
                return $"{ApplicationName} v1";
            }
        }

        #endregion

        #endregion

        public ViewModelBase()
        {
        }

        #region Dialog Functions
        /// <summary>
        /// Display an error message
        /// </summary>
        /// <param name="ex">Your exception</param>
        /// <param name="message">Title text. Typically, put here your method name</param>
        public static void ErrorMessage(Exception ex)
        {
            try
            {
                StackTrace stackTrace = new StackTrace(ex);
                System.Reflection.MethodBase method = stackTrace.GetFrame(stackTrace.FrameCount - 1).GetMethod();
                string titleText = method.Name;

                MessageBox.Show(string.Format(ex.Message + "\n\n" + ex.StackTrace + "\n\n{0}", "Please screenshot and send procedures on how this error occured. Thank you."), titleText, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex2)
            {
                ErrorMessage(ex2);
            }
        }

        /// <summary>
        /// Displays an information messagebox
        /// </summary>
        /// <param name="message">Text</param>
        /// <param name="title">Title text</param>
        public static void InformationMessage(string message, string title)
        {
            try
            {
                MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex2)
            {
                ErrorMessage(ex2);
            }
        }

        /// <summary>
        /// Displays a warning messagebox
        /// </summary>
        /// <param name="message">Text</param>
        /// <param name="title">Title text</param>
        public static void WarningMessage(string message, string title)
        {
            try
            {
                MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex2)
            {
                ErrorMessage(ex2);
            }
        }
        /// <summary>
        /// Displays a warning messagebox
        /// </summary>
        /// <param name="message">Text</param>
        /// <param name="title">Title text</param>
        public static void WarningMessage(string message)
        {
            try
            {
                WarningMessage(message, "Warning");
            }
            catch (Exception ex2)
            {
                ErrorMessage(ex2);
            }
        }

        #endregion

    }
}