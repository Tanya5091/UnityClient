using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class ShowElementScript : MonoBehaviour
	{
		internal static String orig;
		internal static String tr;
		internal static String dt;

		// Start is called before the first frame update
		void Start()
		{
			//Making sure user didn`t delete any of the fields
			GetComponent<Button>().onClick.AddListener(TaskOnClick);
		}

		void Update()
		{
			
		}

		void TaskOnClick()
		{
			orig = this.transform.Find("Orig").GetComponent<Text>().text;
			tr = this.transform.Find("Trans").GetComponent<Text>().text;
			dt = this.transform.Find("DT").GetComponent<Text>().text;
			SceneManager.LoadScene("ViewTrans");
		}
	}
}
