using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sharpcraft.Forms
{
	/// <summary>
	/// Dialog for showing exceptions, allows user to send an error report.
	/// </summary>
	public partial class ExceptionDialog : Form
	{
		private readonly Exception _exception;

		private const string ExceptionLabelFormat = "Exception occurred in {0} ({1}), the exception thrown was {2} ({3}). More details below.";

		/// <summary>
		/// Initializes a new instance of ExceptionDialog.
		/// </summary>
		/// <param name="exception">The exception to show.</param>
		public ExceptionDialog(Exception exception)
		{
			InitializeComponent();
			_exception = exception;
			ExceptionLabel.Text = string.Format(ExceptionLabelFormat,
												_exception.Source,
												_exception.TargetSite,
			                                    _exception.GetType(),
												_exception.InnerException == null ? "null" : _exception.InnerException.GetType().ToString());
			ExceptionMessage.Text = _exception.Message;
			ExceptionStackTrace.Text = _exception.StackTrace;
		}

		private void CloseButtonClick(object sender, EventArgs e)
		{
			Close();
		}
	}
}
