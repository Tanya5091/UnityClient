using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
	public Button signInButton;

	public String scene;
    // Start is called before the first frame update
    void Start()
    {
	    Button btn = signInButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		
    }

    // Update is called once per frame
    void TaskOnClick()
    {

	    SceneManager.LoadScene(scene);
    }
}
