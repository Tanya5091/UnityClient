using System.Collections;
using System.Text;
using Assets.Scripts.Manager;
using Assets.Scripts.Server;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class SignUp : MonoBehaviour
	{
		// Start is called before the first frame update
		public InputField log;
		public InputField pass;
		public Toggle rememberToggle;
		public string scene;
		private bool loadNext;
		void Awake()
		{
			loadNext = false;
			Connect.Instance.GetComponent<TcpClient>().OnMessage += OnMessage;
		}
		// Start is called before the first frame update
		void Start()
		{
			//Making sure user didn`t delete any of the fields
			GetComponent<Button>().onClick.AddListener(TaskOnClick);
		}

		void Update()
		{
			if (loadNext)
			{
				
				SceneManager.LoadScene(scene);
			}
		}

		// Update is called once per frame
		void TaskOnClick()
		{
			Connect.History = new ArrayList();
			Connect.Instance.GetComponent<TcpClient>().SendingMessage(MessageFactory(MessageType.SignUpRequest, Encoding.ASCII.GetBytes($"{log.text};{pass.text}")));
			if (!rememberToggle.isOn)
			{
				PlayerPrefs.SetString("login", log.text);
				PlayerPrefs.SetString("password", pass.text);
			}
		}
		void OnMessage(byte[] data)
		{
			var msg = Message.ToMessage(data);
			switch (msg.Type)
			{
				case MessageType.SignUpUnsuccessful:
					//text = "Incorrect Password";
					break;
				case MessageType.SignUpSuccessful:
					loadNext = true;
					break;
			}
		}

		private static byte[] MessageFactory(MessageType type, byte[] value)
		{
			return new Message { Type = type, Value = value }.ToBytes();
		}
	}
}
