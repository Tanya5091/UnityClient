using System.Collections;
using System.Text;
using Assets.Scripts;
using Assets.Scripts.Manager;
using Server;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets
{
	public class LoginScript : MonoBehaviour
	{
		// Start is called before the first frame update
		public InputField log;
		public InputField pass;
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
			Connect.Instance.GetComponent<TcpClient>().SendingMessage(MessageFactory(MessageType.LoginRequest, Encoding.ASCII.GetBytes($"{log.text};{pass.text}")));
		}
		void OnMessage(byte[] data)
		{
			var msg = Message.ToMessage(data);
			switch (msg.Type)
			{
				case MessageType.LoginUnsuccessful:
					//text = "Incorrect Password";
					break;
				case MessageType.LoginSuccessful:
					//text = "Login Successfull";
					if(msg.Value.Length != 0)
					{
						Connect.History = Utils.FromByteArray<ArrayList>(msg.Value);
						// foreach (var request in requests)
						// {
						//     //text += $"{Utils.FromByteArray<Request>((byte[])request)}\n";
						// }
					}
					else 
					{
						Connect.History = new ArrayList();
					}
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
