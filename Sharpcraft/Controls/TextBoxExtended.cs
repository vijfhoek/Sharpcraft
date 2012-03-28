using System.Windows.Forms;

namespace Sharpcraft.Controls
{
	class TextBoxExtended : TextBox
	{
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == (Keys.Control | Keys.Back))
			{
				SendKeys.SendWait("^+{LEFT}{BACKSPACE}");
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
