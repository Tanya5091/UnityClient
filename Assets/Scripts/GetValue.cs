using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class GetValue : MonoBehaviour
	{
		public InputField o;
		public Text d;
		public InputField t;
		// Start is called before the first frame update
		void Start()
		{
			o.SetTextWithoutNotify(ShowElementScript.orig);
			t.SetTextWithoutNotify(ShowElementScript.tr);
			d.text = "Date" +ShowElementScript.dt;
		}

		// Update is called once per frame
		void Update()
		{
        
		}
	}
}
