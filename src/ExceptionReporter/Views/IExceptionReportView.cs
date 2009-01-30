
using System;
using System.Windows.Forms;

namespace ExceptionReporting.Views
{
    /// <summary>
    /// The interface (contract) for an ExceptionReportView
    /// </summary>
    internal interface IExceptionReportView
    {
        string ProgressMessage { set; }
        bool EnableEmailButton { set; }
        bool ShowProgressBar { set; }
        bool ShowFullDetail { get; set; }
        int ProgressValue { get;  set; }
        string UserExplanation { get; }
        void ShowErrorDialog(string message, Exception exception);
        void SetEmailCompletedState(bool success);
        void SetEmailCompletedState_WithMessageIfSuccess(bool success, string successMessage);
        void ShowExceptionReport();
        void SetInProgressState();
        void PopulateConfigTab(string filePath);
        void PopulateExceptionTab(Exception exception);
        void PopulateAssembliesTab();
        void PopulateSysInfoTab(TreeNode rootNode);
        void PopulateTabs();
        void SetProgressCompleteState();
    }
}
