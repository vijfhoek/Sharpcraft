using System;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Sharpcraft.Forms
{
	public partial class GitHubDialog : Form
	{
		private readonly string _author;
		private string Address { get { return "https://github.com/" + _author + "/Sharpcraft/issues/new"; } }
		private readonly string _type;
		private readonly string _inner;
		private const string SubjectFormat = "[CRASH] [FATAL] {0}";
		private readonly string _subject;
		private const string MessageFormat = "Issue Title: {0}{9}{9}Exception at {1}.{9}{9}{2} ({3}){9}Message: {4}{9}Inner Message: {5}{9}Stack Trace:{9}{6}{9}Inner Stack Trace:{9}{7}{9}{9}System Info:{9}{8}";
		private readonly string _message;
		private readonly string _innerMessage;
		private readonly string _stackTrace;
		private readonly string _innerStackTrace;
		private readonly DateTime _time;
		private readonly string _systemInfo;

		public GitHubDialog(Exception ex, string author, bool systemInfo)
		{
			InitializeComponent();
			_author = author;
			_type = ex.GetType().ToString();
			_subject = string.Format(SubjectFormat, _type);
			if (ex.InnerException != null)
			{
				_inner = ex.InnerException.GetType().ToString();
				_innerMessage = ex.InnerException.Message;
				_innerStackTrace = ex.InnerException.StackTrace;
			}
			else
			{
				_inner = "None";
				_innerMessage = "None";
				_innerStackTrace = "None";
			}
			_message = ex.Message;
			_stackTrace = ex.StackTrace;
			_time = DateTime.Now;
			if (systemInfo)
			{
				var info = new StringBuilder();
				info.Append("Operating System: " + Environment.OSVersion);
				info.Append(Environment.Is64BitOperatingSystem ? " 64-Bit" : " 32-Bit");
				info.Append(Environment.NewLine + "ENV Version: " + Environment.Version);
				bool network = SystemInformation.Network;
				info.Append(Environment.NewLine + "Network is " + (network ? "ENABLED" : "DISABLED"));
				int w = SystemInformation.PrimaryMonitorSize.Width;
				int h = SystemInformation.PrimaryMonitorSize.Height;
				info.Append(Environment.NewLine + "Primary screen size is W:" + w + ", H:" + h);
				_systemInfo = info.ToString();
			}
			else
			{
				_systemInfo = "None";
			}
			IssueBox.Text = string.Format(MessageFormat, _subject, _time.ToString(), _type, _inner, _message, _innerMessage,
			                              _stackTrace, _innerStackTrace, _systemInfo, Environment.NewLine);
		}

		private void GitHubDialogShown(object sender, EventArgs e)
		{
			Process.Start(Address);
		}

		private void IssueBoxEnter(object sender, EventArgs e)
		{
			IssueBox.SelectAll();
		}

		private void ClipboardButtonClick(object sender, EventArgs e)
		{
			Clipboard.SetText(IssueBox.Text);
			StatusLabel.Text = "Successfully copied text to clipboard!";
		}

		private void CloseButtonClick(object sender, EventArgs e)
		{
			Close();
		}

		private void IssueBoxClick(object sender, EventArgs e)
		{
			IssueBox.SelectAll();
		}
	}
}
