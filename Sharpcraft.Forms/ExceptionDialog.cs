using System;
using System.Windows.Forms;

namespace Sharpcraft.Forms
{
	/// <summary>
	/// Dialog for showing exceptions, allows user to send an error report.
	/// </summary>
	public partial class ExceptionDialog : Form
	{
		/// <summary>
		/// The exception thrown.
		/// </summary>
		private readonly Exception _exception;

		private readonly string _author;

		/// <summary>
		/// The format passed to string.Format when constructing the exception message.
		/// </summary>
		private const string ExceptionLabelFormat = "Exception occurred in {0} ({1}), the exception thrown was {2} ({3}). More details below.";

		/// <summary>
		/// Initializes a new instance of ExceptionDialog.
		/// </summary>
		/// <param name="exception">The exception to show.</param>
		/// <param name="author">The author who built this version of Sharpcraft.</param>
		public ExceptionDialog(Exception exception, string author = null)
		{
			InitializeComponent();
			_author = author;
			_exception = exception;
			ExceptionLabel.Text = string.Format(ExceptionLabelFormat,
												_exception.Source,
												_exception.TargetSite,
			                                    _exception.GetType(),
												_exception.InnerException == null ? "null" : _exception.InnerException.GetType().ToString());
			ExceptionMessage.Text = _exception.Message;
			ExceptionStackTrace.Text = _exception.StackTrace;
			if (_exception.InnerException != null)
			{
				ExceptionMessage.Text += Environment.NewLine + Environment.NewLine + _exception.InnerException.Message;
				ExceptionStackTrace.Text += Environment.NewLine + Environment.NewLine + _exception.InnerException.StackTrace;
			}
			if (string.IsNullOrEmpty(_author))
			{
				SendButton.Enabled = false;
				ExtraInfoCheckbox.Enabled = false;
			}
			else
			{
				SendButton.Enabled = true;
				ExtraInfoCheckbox.Enabled = true;
			}
		}

		/// <summary>
		/// Handler for the close button, closes the form.
		/// </summary>
		/// <param name="sender">N/A (Not Used) (See MSDN)</param>
		/// <param name="e">N/A (Not Used) (See MSDN)</param>
		private void CloseButtonClick(object sender, EventArgs e)
		{
			Close();
		}

		private void SendButtonClick(object sender, EventArgs e)
		{
			var result = MessageBox.Show("This will open the GitHub issues page for Sharpcraft in your browser and present you with text to copy into it, continue?",
										 "GitHub Issues", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
			if (result != DialogResult.Yes)
				return;
			new GitHubDialog(_exception, _author, ExtraInfoCheckbox.Checked).ShowDialog(this);
		}
	}
}
