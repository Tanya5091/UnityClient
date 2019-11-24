using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class Connect : MonoBehaviour
	{
		private TcpClient client;

		public static List<string> q;
		//private Text log;
		public Text text;
		private string str = "";

		public static ArrayList History;

		public static Connect Instance;

		void Awake()
		{
			q=new List<string>();
			Instance = this;
			DontDestroyOnLoad(this);
		}

		// Start is called before the first frame update
		void Start()
		{
			Screen.SetResolution(1280, 800, false);
			client = GetComponent<TcpClient>();
			client.OnConnection += OnConnection;
		}

		void Update()
		{
			if(str != "")
			{
				text.text = str;
				str = "";
				SceneManager.LoadScene("WelcomeScene");
			}
		}

		void OnConnection()
		{
			str = "Connected to server!";
			client.OnConnection -= OnConnection;
		}
	}

}
