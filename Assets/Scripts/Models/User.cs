using System;
namespace Assets.Scripts.Models
{
	[Serializable]
	class User
	{
		private String _password;
		private String _login;

		internal User(String l, String pass)
		{
			Password = pass;
			Login = l;
		}

		public string Password
		{
			get { return _password; }
			private set { _password = value; }
		}

		public string Login
		{
			get { return _login; }
			private set { _login = value; }
		}
	}
}
