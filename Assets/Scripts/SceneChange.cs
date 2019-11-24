using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
	public string scene;
    // Start is called before the first frame update
    void Start()
    {
		GetComponent<Button>().onClick.AddListener(TaskOnClick);
		
    }

    // Update is called once per frame
    void TaskOnClick()
    {
	    SceneManager.LoadScene(scene);
    }
}
