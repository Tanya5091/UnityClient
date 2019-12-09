using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Manager;
using System.Text;
using System.Threading;
using Assets.Scripts.Server;

namespace Assets.Scripts
{
	public class ProcessInput : MonoBehaviour
	{
		public GameObject g;
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
				g.transform.localScale = new Vector3(1, 1, 1);
				transliterateThread = new Thread(InitiateTransliteration);
				threadRunning = true;
				transliterateThread.Start();

			}
			if (transliterateThread != null && !threadRunning)
			{

				g.transform.localScale = new Vector3(0, 0, 0);
				ouf.text = res;
				Connect.Instance.GetComponent<TcpClient>().SendingMessage(MessageFactory(MessageType.SaveRequest, Encoding.Unicode.GetBytes($"{src};{res}")));
				Connect.q.Add(src);
				Connect.q.Add(res);
				Connect.q.Add(System.DateTime.Today.ToShortDateString());
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
			Thread.Sleep(300);
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
