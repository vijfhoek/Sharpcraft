using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sharpcraft
{
	public class User
	{
		public string SessionID { get; private set; }

		private string _name;

		internal User(string name = null, string sessionId = null)
		{
			_name = name;
			SessionID = sessionId;
		}

		public string GetName(bool pretty = false)
		{
			if (pretty)
			{
				char[] a = _name.ToCharArray();
				a[0] = char.ToUpper(a[0]);
				return new string(a);
			}
			return _name;
		}

		public void SetName(string name)
		{
			_name = name;
		}

		public void SetSessionID(string id)
		{
			SessionID = id;
		}
	}
}
