using System.Collections;
using Assets.Scripts.Manager;
using Server;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class FillEntries : MonoBehaviour
	{
		public GameObject myPrefab;
		public static ArrayList queue;
		// Start is called before the first frame update
		void Start()
		{
			queue=new ArrayList();
			foreach (var request in Connect.History)
			{
				GameObject g = (GameObject)Instantiate(myPrefab);
				g.transform.SetParent(this.transform);
				Request req = Utils.FromByteArray<Request>((byte[])request);
				
				g.transform.Find("Orig").GetComponent<Text>().text = req.Text;
				g.transform.Find("Trans").GetComponent<Text>().text = req.TransText;
				g.transform.Find("DT").GetComponent<Text>().text = req.Date.ToShortDateString();

			}
	
		}
		// Update is called once per frame
		void Update()
		{
			////if (queue.Count > 0)
			////{
			////	foreach (String s in queue)
			////	{
			////		GameObject g1 = (GameObject)Instantiate(myPrefab);
			////		g1.transform.SetParent(this.transform);
			////		g1.transform.Find("Orig").GetComponent<Text>().text = "До побачення";
			////		g1.transform.Find("Trans").GetComponent<Text>().text = "Do pobachennya";
			////		g1.transform.Find("DT").GetComponent<Text>().text = "24.11.2019";
			////	}
			////}
			////queue.Clear();
		}
	}
}
