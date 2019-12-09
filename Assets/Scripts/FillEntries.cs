using Assets.Scripts.Manager;
using Assets.Scripts.Server;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class FillEntries : MonoBehaviour
	{
		public GameObject myPrefab;
		
		// Start is called before the first frame update
		async void Start()
		{
			
				for (int i = 0; i < Connect.History.Count; i++)
				{
					var request = Connect.History[i];
					GameObject g = (GameObject) Instantiate(myPrefab);
					g.transform.SetParent(this.transform);
					RequestObject req = Utils.FromByteArray<RequestObject>((byte[]) request);
					g.transform.Find("Orig").GetComponent<Text>().text = req.Text;
					g.transform.Find("Trans").GetComponent<Text>().text = req.TransText;
					g.transform.Find("DT").GetComponent<Text>().text = req.Date.ToShortDateString();

				}
			
			GameObject g1 = (GameObject)Instantiate(myPrefab); 
			for (int i=0; i<Connect.q.Count; i++)
			{

				if (i % 3 == 0)
				{
					if (i > 0) 
					g1 = (GameObject)Instantiate(myPrefab);
					g1.transform.SetParent(this.transform);
					g1.transform.Find("Orig").GetComponent<Text>().text = Connect.q[i];
				}
				else if (i%3==1)
				g1.transform.Find("Trans").GetComponent<Text>().text = Connect.q[i];
				else
				g1.transform.Find("DT").GetComponent<Text>().text = Connect.q[i];

			}

		}
		// Update is called once per frame
		void Update()
		{
		
		}
	}
}
