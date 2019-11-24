using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Manager;
using Server;
using System.Text;
using System.Threading;

namespace Assets.Scripts
{
	public class ProcessInput : MonoBehaviour
	{
		public Button signInButton;
		public InputField inf;
		public InputField ouf;
		private string res;
		private string src;
		private Thread transliterateThread;
		private bool threadRunning;
		void Awake()
		{
			res = src = "";
			threadRunning = false;
			transliterateThread = null;
			Connect.Instance.GetComponent<TcpClient>().OnMessage += OnMessage;
		}
		void Start()
		{
			Button btn = signInButton.GetComponent<Button>();
			btn.onClick.AddListener(TaskOnClick);
		}

		void Update()
		{
			if (inf.readOnly && transliterateThread == null)
			{
				transliterateThread = new Thread(InitiateTransliteration);
				threadRunning = true;
				transliterateThread.Start();
			}
			if (transliterateThread != null && !threadRunning)
			{
				ouf.text = res;
				Connect.Instance.GetComponent<TcpClient>().SendingMessage(MessageFactory(MessageType.SaveRequest, Encoding.ASCII.GetBytes($"{src};{res}")));
				res = "";
				inf.readOnly = false;
				transliterateThread.Join();
				transliterateThread = null;
			}
		}

		private void InitiateTransliteration()
		{
			res = TransliterationModel.Transliterate(src);
			threadRunning = false;
		}

		// Update is called once per frame
		void TaskOnClick()
		{
			src = inf.text;
			inf.readOnly = true;
		}

		void OnMessage(byte[] data)
		{
			var msg = Message.ToMessage(data);
			switch (msg.Type)
			{
				case MessageType.SaveSuccessful:
					//text = "Saved Successfully";
					break;
			}
		}

		private static byte[] MessageFactory(MessageType type, byte[] value)
		{
			return new Message { Type = type, Value = value }.ToBytes();
		}
	}
}
