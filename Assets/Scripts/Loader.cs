using UnityEngine;

namespace Assets.Scripts
{
	public class Loader : MonoBehaviour
	{
		private RectTransform rectComponent;
		private float rotateSpeed = -200f;
		private void Start()
		{
			rectComponent = GetComponent<RectTransform>();
				//	Screen.SetResolution(1280, 800, false);
		
		}
		private void Update()
		{
			rectComponent.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
		}
	}
}
