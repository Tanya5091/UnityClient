using System;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Assets.Scripts;
using Assets.Scripts.Manager;
using Assets.Scripts.Models;
using Assets.Scripts.Server;
using Assets.Scripts.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TcpClient = Assets.Scripts.Manager.TcpClient;

namespace Assets
{
	public class LoginScript : MonoBehaviour
	{
		// Start is called before the first frame update
		private User us;
		public InputField log;
		public InputField pass;
		private GameObject g1;
		public Text t;
		public Toggle rememberToggle;
		public string scene;
		private bool loadNext;
		public GameObject loader;
		void Awake()
		{
			GameObject g = this.transform.parent.gameObject;
			g1 = g.transform.Find("LoaderImage").gameObject;
			g1.transform.localScale = new Vector3(0, 0, 0);
			loadNext = false;
			Connect.Instance.GetComponent<TcpClient>().OnMessage += OnMessage;
		}
		// Start is called before the first frame update
		void Start()
		{
		
			try
			{
				var use = SerializationManager.Deserialize<User>(FileFolderHelper.StorageFilePath);
				log.SetTextWithoutNotify(use.Login);
				pass.SetTextWithoutNotify(use.Password);
			}
			
			catch (Exception e)
			{
				log.SetTextWithoutNotify("");
				pass.SetTextWithoutNotify("");
			}
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
			try
			{
				TcpClient tcp = Connect.Instance.GetComponent<TcpClient>();
				g1.transform.localScale = new Vector3(1, 1, 1);
				conn(tcp);
				g1.transform.localScale = new Vector3(0, 0, 0);
			}
			catch (SocketException e)
			{
				t.text = "Couldn`t connect to server";
			}
		}
		void OnMessage(byte[] data)
		{
			var msg = Message.ToMessage(data);
			switch (msg.Type)
			{
				case MessageType.LoginUnsuccessful:
					t.text = "Couldn`t Login";
					break;
				case MessageType.LoginSuccessful:

					us = new User(log.text, pass.text);
					if (!rememberToggle.isOn)
					{
						SerializationManager.Serialize(us, FileFolderHelper.StorageFilePath);
					}
					if (msg.Value.Length != 0)
					{
					
						Connect.History = Utils.FromByteArray<ArrayList>(msg.Value);
			
					}
					else 
					{
						Connect.History = new ArrayList();
					}
					loadNext = true;
					break;
			}
		}

		private async void conn(TcpClient tcp)
		{
			var cts = new CancellationTokenSource();
			await Task.Run(() =>
			{
				cts.CancelAfter(2500);
				try
				{
					tcp.SendingMessage(MessageFactory(MessageType.LoginRequest,
						Encoding.ASCII.GetBytes($"{log.text};{pass.text}")));
				}
				catch (OperationCanceledException)
				{
					t.text += "Couldn`t connect to server";
				}
			});
		}
		private static byte[] MessageFactory(MessageType type, byte[] value)
		{
			return new Message { Type = type, Value = value }.ToBytes();
		}
	}
}
